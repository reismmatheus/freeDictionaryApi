using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.Model
{
    public class AppSettingsConfiguration
    {
        public string SecretKey { get; set; }
        public string FreeDictionaryApiUrl { get; set; }
        public string FileWordsUrl { get; set; }
    }
}
