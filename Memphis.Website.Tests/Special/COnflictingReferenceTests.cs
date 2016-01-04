using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memphis.Website.Tests.Special
{
    [TestClass]
    public class ConflictingReferenceTests
    {
        [TestMethod]
        public void FindConflictingReferences()
        {
            var assemblies = GetAllAssemblies(@"C:\Users\Simon\Documents\Visual Studio 2013\Projects\Various\Mercury\Mercury.Console\bin\Debug");

            var references = GetReferencesFromAllAssemblies(assemblies);

            var groupsOfConflicts = FindReferencesWithTheSameShortNameButDiffererntFullNames(references);

            foreach (var group in groupsOfConflicts)
            {
                Debug.WriteLine($"Possible conflicts for {group.Key}:");
                foreach (var reference in group)
                {
                    Debug.WriteLine("{0} references {1}",
                                          reference.Assembly.Name.PadRight(25),
                                          reference.ReferencedAssembly.FullName);
                }
            }
        }

        private IEnumerable<IGrouping<string, Reference>> FindReferencesWithTheSameShortNameButDiffererntFullNames(List<Reference> references)
        {
            return from reference in references
                   group reference by reference.ReferencedAssembly.Name
                       into referenceGroup
                   where referenceGroup.ToList().Select(reference => reference.ReferencedAssembly.FullName).Distinct().Count() > 1
                   select referenceGroup;
        }

        private List<Reference> GetReferencesFromAllAssemblies(List<Assembly> assemblies)
        {
            var references = new List<Reference>();
            foreach (var assembly in assemblies)
            {
                foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
                {
                    references.Add(new Reference
                    {
                        Assembly = assembly.GetName(),
                        ReferencedAssembly = referencedAssembly
                    });
                }
            }
            return references;
        }

        private List<Assembly> GetAllAssemblies(string path)
        {
            var files = new List<FileInfo>();
            var directoryToSearch = new DirectoryInfo(path);
            files.AddRange(directoryToSearch.GetFiles("*.dll", SearchOption.AllDirectories));
            files.AddRange(directoryToSearch.GetFiles("*.exe", SearchOption.AllDirectories));

            var result = new List<Assembly>();
            foreach (var file in files)
            {
                try
                {
                    result.Add(Assembly.LoadFile(file.FullName));
                }
                catch
                {
                    Debug.WriteLine($"Failed to load assembly {file.FullName}");
                }
            }
            return result;
        }

        private class Reference
        {
            public AssemblyName Assembly { get; set; }
            public AssemblyName ReferencedAssembly { get; set; }
        }
    }
}
