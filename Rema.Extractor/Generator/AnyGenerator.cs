using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Rema.Extractor.Generator
{
    class AnyGenerator
    {
        public static void Generate<TEntity>(string path, string objectDescription, List<TEntity> entities)
        {
            XmlSerializer writer = new XmlSerializer(entities.GetType());
            StreamWriter file = new StreamWriter(Path.Combine(path, objectDescription) + ".xml");
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            writer.Serialize(file, entities, ns);
            file.Close();

            Console.WriteLine("Wrote {0} {1}", entities.Count, objectDescription);

        }
    }
}
