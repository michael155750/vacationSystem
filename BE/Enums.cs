using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum GuestRequestStatus
    {
        Active,
        DealComplete,
        TimeOut
    }

    public enum OrderStatus
    {
        NotYetHandle,
        MailSend,
        ClosedNotResponse,
        Canceled,
        DillMade
    }

    public enum Areas
    {
        All,
        North,
        South,
        Center,
        Jerusalem
    }

    public enum Types
    {
        Zimmer,
        Hotel,
        Camping        
    }

    public enum Choice
    {
        Necessary,
        Possible,
        Unnecessary 
    }

    public  enum YesNo
    {
        Yes,
        No
    }   
}
