using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;

using BE;
using BL;

namespace PL
{
    enum SelectFunc
    {
        exit,
        AddRequest,
        UpdateRequest,

        AddUnitAndOwner,
        DeleteUnit,
        UpdateUnit,

        AddOrder,
        UpdateOrder,

        GetAllUnits,
        GetAllRequests,
        GetAllOrders,
        GetAllBanks,

        PossibleHstUnt,
        DaysBetween,

        DaysExist,
        NumOfOrdersPerReq,
        NumOfOrdersPerUnit,

        ReqGroupByArea,
        ReqGroupByGuestNum,
        HostsGroupByUnits,
        UnitsGroupByArea,
        OrdersGroupByUnit,
        AddUnitOnly
    }

    class Program
    {
        static void Main(string[] args)
        {
            //BL.Ibl bl = FactoryBL.Instance.getBL();
            BL.Ibl bl = FactoryBL.getBL();

            SelectFunc choice = (SelectFunc)100;

            #region presentation
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("insert 1 to add guest request");
            Console.WriteLine("insert 2 to update guest request");
            Console.WriteLine("insert 3 to add hosting unit if the owner is  exist in the system");
            Console.WriteLine("insert" + SelectFunc.AddUnitOnly + "to add hosting unit if the owner isn't exist in the system");
            Console.WriteLine("insert 4 to delete hosting unit");
            Console.WriteLine("insert 5 to update hosting unit");
            Console.WriteLine("insert 6 to add order");
            Console.WriteLine("insert 7 to update order");
            Console.WriteLine("insert 8 to get all hosting units");
            Console.WriteLine("insert 9 to get all guest requests");
            Console.WriteLine("insert 10 to get all orders");
            Console.WriteLine("insert 11 to get all banks");
            Console.WriteLine("insert 12 to get the possible units for certian date");
            Console.WriteLine("insert 13 to get the days between two dates or" +
                                 "from some date to today");
            Console.WriteLine("insert 14 to get all the orders the time from their creation" +
                                  "or mail was send is large or equal from number of days");

            Console.WriteLine("insert 15 to get number of orders per guest requests");
            Console.WriteLine("insert 16 to get number of orders per hosting unit");

            Console.WriteLine("insert 17 to get all the guest request grouping by area");
            Console.WriteLine("insert 18 to get all the guest request grouping by number of guests");
            Console.WriteLine("insert 19 to get all the hosts grouping by the number of hosting units they own");
            Console.WriteLine("insert 20 to get all the hosting unit grouping by area");
            Console.WriteLine("insert 21 to get all the orders hey grouping by the hosting unit" +
                "of them");
            Console.WriteLine("insert 0 to exit");
            #endregion

            string input;
            input = Console.ReadLine();
            choice = (SelectFunc)Convert.ToInt32(input);
            while (choice != 0)
            {
                switch (choice)
                {
                    case SelectFunc.AddRequest:
                        GuestRequest g = new GuestRequest();

                        Console.WriteLine("insert first name");
                        g.FirstName = Console.ReadLine();
                        Console.WriteLine("insert last name");
                        g.LastName = Console.ReadLine();
                        Console.WriteLine("insert mail adress");
                        g.MailAddress = Console.ReadLine();

                        Console.WriteLine("insert area:");
                        Console.WriteLine("insert 0 for all areas");
                        Console.WriteLine("insert 1 for north");
                        Console.WriteLine("insert 2 for south");
                        Console.WriteLine("insert 3 for center");
                        Console.WriteLine("insert 4 for jerusalem");
                        g.Area = (Areas)Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("insert sub area");
                        g.SubArea = Console.ReadLine();

                        Console.WriteLine("insert number of adults");
                        g.Adults = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("insert number of children");
                        g.Children = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("insert entry date");
                        g.EntryDate = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("insert release date");
                        g.ReleaseDate = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("For all the next questions:");
                        Console.WriteLine("insert 0 if it not necessary");
                        Console.WriteLine("insert 1 for possible");
                        Console.WriteLine("insert 2 if it necessary");

                        Console.WriteLine("Do you want children attractions?");
                        g.ChildrensAttractions = (Choice)Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Do you want a garden in the unit?");
                        g.Garden = (Choice)Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Do you want a jacuzzi?");
                        g.Jacuzzi = (Choice)Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Do you want swimming pool?");
                        g.Pool = (Choice)Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            bl.AddRequest(g);
                        }
                        catch (InvalidEnumArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (DuplicateWaitObjectException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case SelectFunc.UpdateRequest:
                        Console.WriteLine("insert the key number of request you want " +
                            "to update");
                        long key = Convert.ToInt64(Console.ReadLine());
                        Console.WriteLine("insert the number of the new status");
                        Console.WriteLine("insert 1 if deal complete");
                        Console.WriteLine("insert 2 for timeOut");
                        int newStatus = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            bl.UpdateRequest(key, (GuestRequestStatus)newStatus);
                        }
                        catch (InvalidEnumArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case SelectFunc.AddUnitAndOwner:
                        HostingUnit unit = new HostingUnit();

                        Console.WriteLine("insert hosting unit name");
                        unit.HostingUnitName = Console.ReadLine();

                        Console.WriteLine("Insert hosting unit area");
                        unit.Area = (Areas)Convert.ToInt16(Console.ReadLine());

                        Console.WriteLine("Insert hosting unit sub area");
                        unit.SubArea = Console.ReadLine();

                        Host host = new Host();

                        Console.WriteLine("insert owner's first name");
                        host.FirstName = Console.ReadLine();

                        Console.WriteLine("insert owner's last name");
                        host.LastName = Console.ReadLine();

                        Console.WriteLine("insert owner's mail address");
                        host.MailAddress = Console.ReadLine();

                        Console.WriteLine("insert owner's fhone number");
                        host.FhoneNumber = Convert.ToInt64(Console.ReadLine());

                        Console.WriteLine("insert owner's bank account number");
                        host.BankAccountNumber = Convert.ToInt64(Console.ReadLine());

                        Console.WriteLine("Feel the bank branch details according this list");
                        foreach (var item in bl.GetAllBanks())
                        {
                            Console.WriteLine(item);
                            Console.WriteLine();
                        }

                        Console.WriteLine("insert bank number");
                        int bankNumber = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("insert branch number");
                        int branchNumber = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            host.BankBranchDetails = bl.FindBranch(bankNumber, branchNumber);

                            bl.AddUnitAndHost(unit, host);
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (InvalidEnumArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (DuplicateWaitObjectException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case SelectFunc.AddUnitOnly:
                        HostingUnit unit1 = new HostingUnit();

                        Console.WriteLine("insert hosting unit name");
                        unit1.HostingUnitName = Console.ReadLine();

                        Console.WriteLine("Insert hosting unit area");
                        unit1.Area = (Areas)Convert.ToInt16(Console.ReadLine());

                        Console.WriteLine("Insert hosting unit sub area");
                        unit1.SubArea = Console.ReadLine();

                        Console.WriteLine("Insert owner's key");
                        try
                        {
                            unit1.Owner = bl.FindHostByKey(Convert.ToInt64(Console.ReadLine()));
                            bl.AddUnit(unit1);
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (InvalidEnumArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (DuplicateWaitObjectException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case SelectFunc.UpdateUnit:
                        Console.WriteLine("insert the key of the hosting unit you want to update");
                        long hKey = Convert.ToInt64(Console.ReadLine());
                        DateTime date = new DateTime();
                        int days;

                        Console.WriteLine("what do you want to do?");
                        Console.WriteLine("for change the diary insert 1");
                        Console.WriteLine("for change the hosting unit name insert 2");
                        Console.WriteLine("for end insert 0");
                        int switchOn = Convert.ToInt32(Console.ReadLine());
                        while (switchOn != 0)
                        {
                            switch (switchOn)
                            {
                                case 1:
                                    Console.WriteLine("Insert date of beginning and number of " +
                                        "days you want to close");
                                    date = Convert.ToDateTime(Console.ReadLine());
                                    days = Convert.ToInt32(Console.ReadLine());
                                    try
                                    {
                                        bl.UpdateUnitDiary(hKey, date, days);
                                    }
                                    catch (KeyNotFoundException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (ArgumentOutOfRangeException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (ArgumentNullException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;

                                case 2:
                                    Console.WriteLine("Insert the new name");
                                    try
                                    {
                                        bl.UpdateUnitName(hKey, Console.ReadLine());
                                    }
                                    catch (KeyNotFoundException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (ArgumentOutOfRangeException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (ArgumentNullException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    break;

                                default:
                                    Console.WriteLine("ERROR");
                                    break;
                            }
                            switchOn = Convert.ToInt32(Console.ReadLine());
                        }

                        break;

                    case SelectFunc.DeleteUnit:
                        Console.WriteLine("insert the key of the hosting unit you want to delete");
                        try
                        {
                            bl.DeleteUnit(Convert.ToInt64(Console.ReadLine()));
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ArgumentNullException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case SelectFunc.AddOrder:
                        Console.WriteLine("insert the key of the hosting unit you want to add order to" +
                            "and the key of the guest request");
                        try
                        {
                            bl.AddOrder(Convert.ToInt64(Console.ReadLine()), Convert.ToInt64(Console.ReadLine()));
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ArgumentNullException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (DataException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }


                        break;

                    case SelectFunc.UpdateOrder:
                        Console.WriteLine("insert the key of the order you want update and the new status");
                        Console.WriteLine("insert 1 to mail send");
                        Console.WriteLine("insert 2 if the order closed fbecause their was not respnse");
                        Console.WriteLine("insert 3 if the order canceled");
                        Console.WriteLine("insert 4 if dill was made");
                        try
                        {
                            bl.UpdateOrder(Convert.ToInt64(Console.ReadLine())
                                , (OrderStatus)Convert.ToInt16(Console.ReadLine()));
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ArgumentNullException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (DataException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;

                    case SelectFunc.GetAllUnits:
                        foreach (var item in bl.GetAllUnits())
                        {
                            Console.WriteLine(item);
                        }
                        break;

                    case SelectFunc.GetAllRequests:
                        foreach (var item in bl.GetAllRequests())
                        {
                            Console.WriteLine(item);
                        }
                        break;

                    case SelectFunc.GetAllOrders:
                        foreach (var item in bl.GetAllOrders())
                        {
                            Console.WriteLine(item);
                        }
                        break;

                    case SelectFunc.GetAllBanks:
                        foreach (var item in bl.GetAllBanks())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case SelectFunc.PossibleHstUnt:

                        Console.WriteLine("enter a date");
                        DateTime date2;
                        date2 = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("enter number of days");
                        uint days1 = Convert.ToUInt32(Console.ReadLine());

                        try
                        {


                            foreach (var item in bl.PossibleHstUnt(date2, days1))
                            {
                                Console.WriteLine(item);
                            }
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case SelectFunc.DaysBetween:
                        Console.WriteLine("insert 1 to find the range between days and 2 " +
                            "to find the range between date and today");
                        int switch_on = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("insert beginning date");
                        DateTime date1 = Convert.ToDateTime(Console.ReadLine());
                        DateTime date_2;
                        switch (switch_on)
                        {
                            case 1:
                                Console.WriteLine("insert end date");
                                date_2 = Convert.ToDateTime(Console.ReadLine());
                                bl.DaysBetween(date1, date_2);
                                break;
                            case 2:
                                bl.DaysBetween(date1);
                                break;

                        }


                        break;

                    case SelectFunc.DaysExist:
                        Console.WriteLine("insert number of days");
                        uint numOfDays = Convert.ToUInt32(Console.ReadLine());
                        foreach (var item in bl.DaysExist(numOfDays))
                        {
                            Console.WriteLine(item);
                        }

                        break;

                    case SelectFunc.NumOfOrdersPerReq:
                        try
                        {
                            Console.WriteLine("insert key of guest request");
                            GuestRequest req = bl.FindReqByKey(Convert.ToInt64(Console.ReadLine()));
                            Console.WriteLine(bl.NumOfOrdersPerReq(req));
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case SelectFunc.NumOfOrdersPerUnit:
                        Console.WriteLine("insert key of hosting unit");
                        HostingUnit ho = bl.FindUnitByKey(Convert.ToInt64(Console.ReadLine()));
                        Console.WriteLine(bl.NumOfOrdersPerUnit(ho));
                        break;

                    case SelectFunc.HostsGroupByUnits:
                        foreach (var group in bl.HostsGroupByUnits())
                        {
                            foreach (var item in group)
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine();
                        }
                        break;

                    case SelectFunc.ReqGroupByArea:
                        foreach (var group in bl.ReqGroupByArea())
                        {
                            foreach (var item in group)
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine();
                        }
                        break;

                    case SelectFunc.ReqGroupByGuestNum:
                        foreach (var group in bl.ReqGroupByGuestNum())
                        {
                            foreach (var item in group)
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine();
                        }
                        break;

                    case SelectFunc.UnitsGroupByArea:
                        foreach (var group in bl.UnitsGroupByArea())
                        {
                            foreach (var item in group)
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine();
                        }
                        break;

                    case SelectFunc.OrdersGroupByUnit:
                        foreach (var group in bl.OrdersGroupByUnit())
                        {
                            foreach (var item in group)
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine();
                        }
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }

                Console.WriteLine("Please enter your choice: /n");
                input = Console.ReadLine();
                choice = (SelectFunc)Convert.ToInt32(input);
            }
            
        }
    }

}
