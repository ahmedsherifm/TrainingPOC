using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ECommerce.Main.Models
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
