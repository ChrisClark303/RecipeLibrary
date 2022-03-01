namespace RecipeLibrary.Core
{
    public interface IMongoSettings
    {
        string Password { get; set; }
        string Uri { get; set; }
        string UserName { get; set; }
    }
}