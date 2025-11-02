using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Settings
{
    public class JwtSettings
    {
        //Відправник токена
        public string Issuer { get; set; } = string.Empty;
        //Одержувач токена
        public string Audience { get; set; } = string.Empty;
        //Ключ для шифрування
        public string SecretKey { get; set; } = string.Empty;
        //Час життя токена
        public int ExpiresInHours { get; set; }

    }
}
