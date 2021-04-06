namespace DilemmaSvc.Application.Common
{
    public interface IFileStore
    {
        public string GetUrlForObjectKey(string key);
    }
}