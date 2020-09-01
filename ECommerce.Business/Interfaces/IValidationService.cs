namespace ECommerce.Business.Interfaces
{
    public interface IValidationService
    {
        bool IsRegexValid(string value, string expression);
    }
}
