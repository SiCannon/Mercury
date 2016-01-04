using System;
using System.IO;
using System.Xml.Serialization;

namespace MusicBrainz.Helpers
{
    public static class Xml
    {
        public static void Serialize(object o, string filename)
        {
            var ser = new XmlSerializer(o.GetType());
            var writer = new StreamWriter(filename);
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            ser.Serialize(writer, o, ns);
            writer.Close();
        }

        public static T DeserializePlainFromFile<T>(string filename)
        {
            var ser = new XmlSerializer(typeof(T));
            var reader = new StreamReader(filename);
            var o = ser.Deserialize(reader);
            reader.Close();
            return (T)o;
        }

        public static T DeserializeMbz<T>(string xmlText) where T: class
        {
            if (!string.IsNullOrEmpty(xmlText))
            {
                var ser = new XmlSerializer(typeof(T), "http://musicbrainz.org/ns/mmd-2.0#");
                using (var reader = new StringReader(xmlText))
                {
                    var o = ser.Deserialize(reader);
                    return (T)o;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
