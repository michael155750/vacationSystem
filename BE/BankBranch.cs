using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Bankdetails//represent bank account
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        public long BankAccountNumber { get; set; }
        public float MyCommition { get; set; }
        public YesNo CollectionClearance { get; set; }
        public override string ToString()
        {
            return "Bank Account Number: " + BankAccountNumber.ToString() + "\n" +
                    "my commition: " + MyCommition.ToString() + "\n" +
                    "Collection Clearance? " + CollectionClearance.ToString() + "\n" +
                    "Bank name: " + BankName + "/n" +
                    "Bank Number: " + BankNumber.ToString() + "/n" +
                    "Branch number: " + BranchNumber.ToString() + "/n" +
                    "Branch address: " + BranchAddress + "/n" +
                    "Branch city: " + BranchCity + "/n";
        }
    }
}