using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papago
{
    class PLangType
    {
        public Dictionary<string, string> LangMap = new Dictionary<string, string>();

        public PLangType()
        {
            LangMap.Add("ko", "Korean");
            LangMap.Add("ja", "Japanese");
            LangMap.Add("zh-cn", "Simplified Chinese");
            LangMap.Add("zh-tw", "Traditional Chinese");
            LangMap.Add("hi", "Hindi");
            LangMap.Add("en", "English");
            LangMap.Add("es", "Spanish");
            LangMap.Add("fr", "French");
            LangMap.Add("de", "German");
            LangMap.Add("pt", "Portgal");
            LangMap.Add("vi", "Vietnamese");
            LangMap.Add("id", "Indonesian");
            LangMap.Add("th", "Thai");
            LangMap.Add("re", "Russian");
            LangMap.Add("it", "Italian");
            LangMap.Add("unk", "Unknown");
        }
    }
}
