namespace DilemmaSvc.Application.Common
{
    public interface IFileStore
    {
        public string GetUrlForObject(string key);
    }
}