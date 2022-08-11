using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.AppSettingProperty
{
    // this class is for giving value from appsettings.json
    public class AppSetting
    {
        public JwtSetting JwtSetting { get; set; }
    }
    public class JwtSetting
    {
        public string Issued { get; set; } = null!; // ( = null!) it's not necessary
        public string Audience { get; set; } = null!;
        public string Key { get; set; } = null!; // it can't be null 
    }
}
