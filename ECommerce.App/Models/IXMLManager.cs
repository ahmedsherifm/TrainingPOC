using System.Collections.Generic;

namespace ECommerce.Main.Models
{
    public interface IXMLManager
    {
        T Load<T>(string filename);

        void Save<T>(string filename, T data);
    }
}
