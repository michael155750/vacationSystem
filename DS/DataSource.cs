using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public static class DataSource
    {
        static public List<HostingUnit> hostingUnitsList = new List<HostingUnit>
        {
            new HostingUnit()
            {
                HostingUnitKey = 10000000,
                HostingUnitName = "zimmer galil",
                Diary = new bool[12,31],
                Area = Areas.North,
                SubArea = "Haifa",
                Pool = true,
                Jacuzzi = true,
                Garden = true,
                ChildrensAttractions = false,

                Owner = new Host()
                {
                    HostKey = 10000000,
                    FirstName = "Michael",
                    LastName = "Berg",
                    FhoneNumber = 0500000000,
                    MailAddress = "Michael@gmail.comm",
                    BankAccountNumber = 111111,
                    CollectionClearance = YesNo.Yes,
                    BankBranchDetails = new BankBranch()
                    {
                       BankName = "Hapoalim",
                       BankNumber = 12,
                       BranchAddress = "Aba Hushi 34",
                       BranchCity = "Haifa",
                       BranchNumber = 456
                    }
                }
            },
            new HostingUnit()
            {
                HostingUnitKey = 10000001,
                HostingUnitName = "Villa Ba-kfar",
                Diary = new bool[12,31],
                Area = Areas.Jerusalem,
                SubArea = "Jerusalem",
                Pool = true,
                Jacuzzi = true,
                Garden = true,
                ChildrensAttractions = false,

                Owner = new Host()
                {
                    HostKey = 10000001,
                    FirstName = "Tzur",
                    LastName = "Levi",
                    FhoneNumber = 0511111111,
                    MailAddress = "Tzur@gmail.comm",
                    BankAccountNumber = 222222,
                    CollectionClearance = YesNo.Yes,
                    BankBranchDetails = new BankBranch()
                    {
                        BankName = "Discount",
                        BankNumber = 11,
                        BranchAddress = "Zabotinsky 11",
                        BranchCity = "Jerusalem",
                        BranchNumber = 203
                    }
                }
            },
            new HostingUnit()
            {
                HostingUnitKey = 10000002,
                HostingUnitName = "Family villa",
                Diary = new bool[12,31],
                 Area = Areas.Jerusalem,
                SubArea = "Jerusalem",
                Pool = true,
                Jacuzzi = true,
                Garden = true,
                ChildrensAttractions = false,

                Owner = new Host()
                {
                    HostKey = 10000002,
                    FirstName = "David",
                    LastName = "MOshe",
                    FhoneNumber = 0533333333,
                    MailAddress = "David@gmail.comm",
                    BankAccountNumber = 333333,
                    CollectionClearance = YesNo.No,
                    BankBranchDetails = new BankBranch()
                    {
                        BankName = "Hapoalim",
                        BankNumber = 12,
                        BranchAddress = "Zabotinsky 15",
                        BranchCity = "Jerusalem",
                        BranchNumber = 391
                    }
                }
            },
        };
        static public List<GuestRequest> guestRequestsList = new List<GuestRequest>
        {
           new GuestRequest()
           {
                GuestRequestKey = 10000000,
                FirstName = "Meir",
                LastName = "Ochaun",
                MailAddress = "Meir@gmail.com",
                Status = GuestRequestStatus.Active,
                RegistrationDate = new DateTime(2019, 06, 10),
                EntryDate = new DateTime(2019, 07,10),
                ReleaseDate = new DateTime(2019, 07, 15),
                Area = Areas.Center,
                SubArea  = "Jerusalem",
                Type  = Types.Zimmer,
                Adults = 2,
                Children  = 1,
                Pool  = Choice.Unnecessary,
                Jacuzzi = Choice.Necessary,
                Garden = Choice.Possible,
                ChildrensAttractions = Choice.Necessary
           },
           new GuestRequest()
           {
                GuestRequestKey = 10000001,
                FirstName = "Ori",
                LastName = "Len",
                MailAddress = "ori@gmail.com",
                Status = GuestRequestStatus.Active,
                RegistrationDate = new DateTime(2019, 01, 10),
                EntryDate = new DateTime(2019, 03,10),
                ReleaseDate = new DateTime(2019, 03, 15),
                Area = Areas.North,
                SubArea  = "Haifa",
                Type  = Types.Zimmer,
                Adults = 2,
                Children  = 3,
                Pool  = Choice.Unnecessary,
                Jacuzzi = Choice.Necessary,
                Garden = Choice.Possible,
                ChildrensAttractions = Choice.Necessary
           },
           new GuestRequest()
           {
                GuestRequestKey = 10000002,
                FirstName = "Shalom",
                LastName = "Yadid",
                MailAddress = "Shalom@gmail.com",
                Status = GuestRequestStatus.Active,
                RegistrationDate = new DateTime(2019, 05, 10),
                EntryDate = new DateTime(2019, 06,10),
                ReleaseDate = new DateTime(2019, 06, 15),
                Area = Areas.Jerusalem,
                SubArea  = "Jerusalem",
                Type  = Types.Camping,
                Adults = 4,
                Children  = 5,
                Pool  = Choice.Unnecessary,
                Jacuzzi = Choice.Necessary,
                Garden = Choice.Necessary,
                ChildrensAttractions = Choice.Possible
           }
        };
        static public List<Order> ordersList = new List<Order>
        {

        };
    };

};


