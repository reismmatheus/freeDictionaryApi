using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreeDictionary.Service.FreeDictionaryApi.Model
{
    public class FreeDictionaryApiResult
    {
        [JsonPropertyName("word")]
        public string Word;

        [JsonPropertyName("phonetics")]
        public List<PhoneticResult> Phonetics;

        [JsonPropertyName("meanings")]
        public List<MeaningResult> Meanings;

        [JsonPropertyName("license")]
        public LicenseResult License;

        [JsonPropertyName("sourceUrls")]
        public List<string> SourceUrls;

        [JsonPropertyName("title")]
        public string Title;

        [JsonPropertyName("message")]
        public string Message;

        [JsonPropertyName("resolution")]
        public string Resolution;
    }

    public class DefinitionResult
    {
        [JsonPropertyName("definition")]
        public string Definition;

        [JsonPropertyName("synonyms")]
        public List<object> Synonyms;

        [JsonPropertyName("antonyms")]
        public List<object> Antonyms;

        [JsonPropertyName("example")]
        public string Example;
    }

    public class LicenseResult
    {
        [JsonPropertyName("name")]
        public string Name;

        [JsonPropertyName("url")]
        public string Url;
    }

    public class MeaningResult
    {
        [JsonPropertyName("partOfSpeech")]
        public string PartOfSpeech;

        [JsonPropertyName("definitions")]
        public List<DefinitionResult> Definitions;

        [JsonPropertyName("synonyms")]
        public List<string> Synonyms;

        [JsonPropertyName("antonyms")]
        public List<string> Antonyms;
    }

    public class PhoneticResult
    {
        [JsonPropertyName("audio")]
        public string Audio;

        [JsonPropertyName("sourceUrl")]
        public string SourceUrl;

        [JsonPropertyName("license")]
        public License License;

        [JsonPropertyName("text")]
        public string Text;
    }
}
