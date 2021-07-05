namespace GivMigOrdrer.Languages
{
    public interface ILanguage
    {
        string Id { get; }
        string GetTextWithName(string name);
    }
}
