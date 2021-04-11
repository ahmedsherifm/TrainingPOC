using System;
using System.IO;
using System.Xml.Serialization;

namespace ECommerce.DAL.XMLManager
{
    public class XMLManager : IXMLManager
    {
        public T Load<T>(string filename)
        {
            var path = $@"{GetCurrentDirectory()}\{filename}.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            StreamReader reader = new StreamReader(path);

            var data = (T)serializer.Deserialize(reader);
            reader.Close();

            return data;
        }

        public void Save<T>(string filename, T data)
        {
            var path = $@"{GetCurrentDirectory()}\{filename}.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            TextWriter writer = new StreamWriter(path);

            serializer.Serialize(writer, data);
            writer.Close();
        }

        private string GetCurrentDirectory()
        {
            string path;

            path = AppDomain.CurrentDomain.BaseDirectory;
            if (path.IndexOf(@"\bin") > 0)
            {
                path = path.Substring(0, path.LastIndexOf(@"\bin"));
            }

            return path;
        }
    }
}
