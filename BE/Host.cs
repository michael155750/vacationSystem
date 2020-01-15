using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public long HostKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long FhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankBranch BankBranchDetails { get; set; }
        public long BankAccountNumber { get; set; }
        public float MyCommition { get; set; }
        public YesNo CollectionClearance { get; set; }
        public override string ToString()
        {
            return "Host key: " + HostKey.ToString() + "\n" +
                    "First Name: " + FirstName + "\n" +
                    "Last Name: " + LastName + "\n" +
                    "Fhone Number: " + FhoneNumber.ToString() + "\n" +
                    "Mail Address: " + MailAddress + "\n" +
                    "Bank Branch Details: " + BankBranchDetails.ToString() + "\n" +
                    "Bank Account Number: " + BankAccountNumber.ToString() + "\n" +
                    "my commition: "+ MyCommition.ToString() + "\n" +
                    "Collection Clearance? " + CollectionClearance.ToString() + "\n";
        }

    }
}
