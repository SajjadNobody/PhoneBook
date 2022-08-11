using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.AppSettingProperty
{
    public class AppSetting
    {
        public JwtSetting JwtSetting { get; set; }
    }
    public class JwtSetting
    {
        public string Issued { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; } /*"MyPowerFullSecurityKeyEver2"*/
    }
}
