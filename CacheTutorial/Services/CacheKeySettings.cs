using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheTutorial.Services
{
    public class CacheKeySettings
    {
        public string Key { get; set; }
        public TimeSpan Expiry { get; set; }
    }
}
