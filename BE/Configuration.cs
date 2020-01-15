using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    static public class Configuration
    {
        
        static public long globalGuestRequestKey { get; set; } = 10000003;
        static public long globalHostingUnitKey { get; set; } = 10000003;
        static public long globalOrderKey { get; set; } = 10000000;
        static public long globalHostKey { get; set; } = 10000003;
        static public float Commition { get; set; } = 10;
    }
}
