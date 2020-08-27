using System.Collections.Generic;

namespace ECommerce.Main.Models
{
    public interface IXMLManager
    {
        IEnumerable<T> LoadAll<T>(string filename);
    }
}
