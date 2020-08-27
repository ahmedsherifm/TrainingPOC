using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ECommerce.Main.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImagePosterUrl { get; set; }
        [XmlIgnore]
        public ICollection<string> Images { get; set; }
    }
}
