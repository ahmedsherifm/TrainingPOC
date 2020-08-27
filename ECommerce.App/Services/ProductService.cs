using ECommerce.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Main.Services
{
    public class ProductService : IProductService
    {
        private IList<Product> _products = new List<Product>
        {
            new Product
            {
                Id = 1,
                    Name = "LG Smart TV 21\"",
                    Description = "This LG LED TV offers unmatched entertainment. With the resolution of 1920 x 1080 pixels, this 43inch TV renders clear and detailed images so that you don’t miss out even on minutest of details. The top-notch dynamic color processing technology brings images to life. Thanks to the Virtual surround sound, this LG TV recreates the exceptional sound, thereby making your favorite sporting events all the more fun. With HDMI and USB port, you can connect entertainment devices to your TV. The eye-catching frames add a touch of modernity to this LG FHD TV. With built-in HD receiver, you are set to enjoy HD content immediately without any additional external support. The slim construction of this LG TV makes it a must-have for your living room.",
                    Price=  300,
                    Quantity= 6,
                    ImagePosterUrl = "https://www.lg.com/pa_en/images/tvs/MD05606915/gallery/medium01.jpg",
                    CreatedDate = new DateTime(2019,7,19,21,2,50,4),
                    Images = new List<string>
                    {
                        "https://www.lg.com/pa_en/images/tvs/MD05606915/gallery/medium01.jpg",
                        "https://thegoodguys.sirv.com/products/50052624/50052624_562897.PNG",
                        "https://www.lg.com/pa_en/images/tvs/MD05606915/gallery/medium01.jpg",
                        "https://thegoodguys.sirv.com/products/50052624/50052624_562897.PNG",
                        "https://www.lg.com/pa_en/images/tvs/MD05606915/gallery/medium01.jpg",
                    }
            },
            new Product
            {
                Id = 2,
                Name = "LG Smart TV 43\"",
                Description = "This LG LED TV offers unmatched entertainment. With the resolution of 1920 x 1080 pixels, this 43inch TV renders clear and detailed images so that you don’t miss out even on minutest of details. The top-notch dynamic color processing technology brings images to life. Thanks to the Virtual surround sound, this LG TV recreates the exceptional sound, thereby making your favorite sporting events all the more fun. With HDMI and USB port, you can connect entertainment devices to your TV. The eye-catching frames add a touch of modernity to this LG FHD TV. With built-in HD receiver, you are set to enjoy HD content immediately without any additional external support. The slim construction of this LG TV makes it a must-have for your living room.",
                Price=  1000,
                Quantity= 2,
                ImagePosterUrl = "https://www.lg.com/pa_en/images/tvs/MD05606915/gallery/medium01.jpg",
                CreatedDate = new DateTime(2019,7,19,21,2,50,4),
                Images = new List<string>
                {
                    "https://www.lg.com/pa_en/images/tvs/MD05606915/gallery/medium01.jpg",
                    "https://thegoodguys.sirv.com/products/50052624/50052624_562897.PNG",
                    "https://www.lg.com/pa_en/images/tvs/MD05606915/gallery/medium01.jpg",
                    "https://thegoodguys.sirv.com/products/50052624/50052624_562897.PNG",
                    "https://www.lg.com/pa_en/images/tvs/MD05606915/gallery/medium01.jpg",
                }
            }
        };

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public IList<Product> GetProductsOrderedByPrice()
        {
            return _products.OrderBy(p => p.Price).ToList();
        }

        public IList<Product> GetProductsOrderedByPriceAndFiltered(int minPrice, int maxPrice)
        {
            return GetProductsOrderedByPrice()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToList();
        }
    }
}
