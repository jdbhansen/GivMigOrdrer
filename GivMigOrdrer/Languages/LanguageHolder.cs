using System.Collections.Generic;

namespace GivMigOrdrer.Languages
{
    public class LanguageHolder
    {
        private static readonly Dictionary<string, ILanguage> _langDict = new Dictionary<string, ILanguage>();
        private ILanguage _selectedLanguage;

        public LanguageHolder(string languageId = "en")
        {
            //init languages
            ILanguage danish = new DanishLanguage();
            ILanguage english = new EnglishLanguage();
            ILanguage spanish = new SpanishLanguage();
            //add languages
            _langDict.Add(english.Id, english);
            _langDict.Add(danish.Id, danish);
            _langDict.Add(spanish.Id, spanish);
            SelectLanguage(languageId);
        }

        public string GetTextWithName(string name)
        {
            return _selectedLanguage.GetTextWithName(name);
        }

        public void SelectLanguage(string languageId)
        {
            _selectedLanguage = _langDict.ContainsKey(languageId) ? _langDict[languageId] : _langDict[$"en"];
        }


    }
}
