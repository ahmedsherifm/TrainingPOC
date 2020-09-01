using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ECommerce.DAL.Models
{

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Products
    {

        private List<Product> _productList;

        /// <remarks/>
        [XmlElementAttribute("Product")]
        public List<Product> ProductList
        {
            get
            {
                return _productList;
            }
            set
            {
                _productList = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class Product
    {

        private int _id;

        private string _name;

        private string _description;

        private decimal _price;

        private int _quantity;

        private string _imagePosterUrl;

        private List<string> _images;

        /// <remarks/>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        /// <remarks/>
        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }

        /// <remarks/>
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }

        /// <remarks/>
        public string ImagePosterUrl
        {
            get
            {
                return _imagePosterUrl;
            }
            set
            {
                _imagePosterUrl = value;
            }
        }

        /// <remarks/>
        [XmlArrayItemAttribute("Image", IsNullable = false)]
        public List<string> Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
            }
        }
    }
}