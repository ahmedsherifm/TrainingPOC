namespace ECommerce.DAL.XMLManager
{
    public interface IXMLManager
    {
        T Load<T>(string filename);

        void Save<T>(string filename, T data);
    }
}
