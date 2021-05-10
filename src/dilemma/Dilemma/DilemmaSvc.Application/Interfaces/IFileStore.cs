namespace DilemmaApp.Services.Dilemma.Application.Interfaces
{
    public interface IFileStore
    {
        public string GetPublicUrlForObject(string key);
    }
}