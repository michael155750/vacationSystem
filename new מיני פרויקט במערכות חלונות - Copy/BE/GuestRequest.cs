using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class GuestRequest
   {
        public long GuestRequestKey { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }
        public GuestRequestStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Areas  Area { get; set; }
        public string SubArea { get; set; }
        public Types Type { get; set; }

        public int Adults { get; set; }
        public int Children { get; set; }

        public Choice Pool { get; set; }
        public Choice Jacuzzi { get; set; }
        public Choice Garden { get; set; }
        public Choice ChildrensAttractions { get; set; }

        public override string ToString()
        {
            return "Guest Request Key: " + GuestRequestKey.ToString() + "\n" +
                    "First Name: " + FirstName + "\n" +
                    "LastName: " + LastName + "\n" +
                    "MailAddress: " + MailAddress + "\n" +
                    "Status: " + Status.ToString() + "\n" +
                    "RegistrationDate: " + RegistrationDate.ToString() + "\n" +
                    "EntryDate: " + EntryDate.ToString() + "\n" +
                    "ReleaseDate: " + ReleaseDate.ToString() + "\n" +
                    "Area: " + Area.ToString() + "\n" +
                    "SubArea: " + SubArea + "\n" +
                    "Type of unit: " + Type.ToString() + "\n" +
                    "number of adoults: " + Adults.ToString() + "\n" +
                    "number of children: " + Children.ToString() + "\n" +
                    "want swimming pool? " + Pool.ToString() + "\n" +
                    "want jacuzzi? " + Jacuzzi.ToString() + "\n" +
                    "want a garden? " + Garden.ToString() + "\n" +
                    "want children attractions? " + ChildrensAttractions.ToString() + "\n";
        }
   }
   
}
