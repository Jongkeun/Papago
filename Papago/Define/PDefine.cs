using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papago
{
    /// <summary>
    /// Define Area
    /// </summary>
    public class PDefine
    {
        public static string APP_NAME = "Papago";
        public static string CONFIGURATION = @"./Config";

        public static string BASE_URL = "https://openapi.naver.com";
        public static string DETECT_API = "/v1/papago/detectLangs";
        public static string TRANSLATE_NMT_API = "/v1/papago/n2mt";

        public static string X_HEADER_ID= "X-Naver-Client-Id";
        public static string X_HEADER_SCRETE = "X-Naver-Client-Secret";

        public const int OPEN = 31197;
        public const int WM_HOTKEY = 0x0312;
    }
}
