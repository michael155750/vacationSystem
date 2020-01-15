using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Order
    {
        public long HostingUnitKey { get; set; }
        public long GuestRequestKey { get; set; }
        public long OrderKey { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }
        public override string ToString()
        {
            return "Hosting unit key: " + HostingUnitKey.ToString() + "/n" +
                    "Guest request key: " + GuestRequestKey.ToString() + "/n" +
                    "Order key: " + OrderKey.ToString() + "/n" +
                    "Status: " + Status.ToString() + "/n" +
                    "Orderd date: " + OrderDate.ToString() + "/n" +
                    "Create date" + CreateDate.ToString() + "/n";
        }
    }
}
