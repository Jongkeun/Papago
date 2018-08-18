using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papago
{
    public class DetectRsp
    {
        public string langCode { get; set; }
    }

    public class TranslateNMTRsp
    {
        public TranslateNMTMessage message { get; set; }
    }

    public class TranslateNMTMessage
    {
        public TranslateNMTResult result { get; set; }
    }
    public class TranslateNMTResult
    {
        public string srcLangType { get; set; }
        public string tarLangType { get; set; }
        public string translatedText { get; set; }
    }
}
