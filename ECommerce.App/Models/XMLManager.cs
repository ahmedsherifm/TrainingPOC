using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ECommerce.Main.Models
{
    public class XMLManager
    {
        public IEnumerable<T> LoadAll<T>(string filename)
        {
            var path = $@"{GetCurrentDirectory()}\{filename}.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            
            StreamReader reader = new StreamReader(path);

            var lst = (List<T>)serializer.Deserialize(reader);
            reader.Close();

            return lst;
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
