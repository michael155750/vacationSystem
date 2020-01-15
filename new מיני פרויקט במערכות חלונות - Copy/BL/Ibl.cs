using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public delegate bool del(GuestRequest rq);

    public interface Ibl
    {       
        /// <summary>
        /// Add new guest request
        /// </summary>
        /// <param name="guestRequest">new guest request with all the data</param>
        void AddRequest(GuestRequest guestRequest);
        /// <summary>
        /// Change the status of the request
        /// </summary>
        /// <param name="guestRequestKey"></param>
        /// <param name="newStatus"></param>
        void UpdateRequest(long guestRequestKey, GuestRequestStatus newStatus);

        /// <summary>
        /// Add new hosting unit when the host is also new
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <param name="host"></param>
        void AddUnitAndHost(HostingUnit hostingUnit, Host host);

        /// <summary>
        /// Add new hosting unit
        /// </summary>
        /// <param name="hostingUnit">new hosting unit with all the data</param>

        void AddUnit(HostingUnit hostingUnit);
        /// <summary>
        /// Delete hosting unit by key
        /// </summary>
        /// <param name="hostingUnitKey"></param>
        void DeleteUnit(long hostingUnitKey);
        /// <summary>
        /// Change the name of the hosting unit
        /// </summary>
        /// <param name="key">the key of the hosting unit</param>
        /// <param name="name">the new name</param>
        void UpdateUnitName(long key, string name);
        /// <summary>
        /// chnage unbooked days to booked days 
        /// </summary>
        /// <param name="key">the hosting unit key</param>
        /// <param name="date">date of beginning</param>
        /// <param name="days">number of days</param>
        void UpdateUnitDiary(long key, DateTime date, int days);
        /// <summary>
        /// update the diary and the name of a hosting unit 
        /// </summary>
        /// <param name="hostingUnit"></param>
        void UpdateUnit(HostingUnit hostingUnit, long key);
        /// <summary>
        /// Add new order 
        /// </summary>
        /// <param name="guestReqKey">the key of the guest request the order connect to</param>
        /// <param name="UnitKey">The unit offered</param>
        void AddOrder(long guestReqKey, long UnitKey);
        /// <summary>
        /// Change the status of the order
        /// </summary>
        /// <param name="orderKey"></param>
        /// <param name="newStatus"></param>
        void UpdateOrder(long orderKey, OrderStatus newStatus);

        /// <summary>
        /// Returns data struct of all hosting unit
        /// in the data base
        /// </summary>
        /// <returns></returns>
        IEnumerable<HostingUnit> GetAllUnits();
        /// <summary>
        /// Returns data struct of all guest requests
        /// in the data base
        /// </summary>
        /// <returns></returns>
        IEnumerable<GuestRequest> GetAllRequests();
        /// <summary>
        /// Returns data struct of all orders
        /// in the data base
        /// </summary>
        /// <returns></returns>
        IEnumerable<Order> GetAllOrders();
        /// <summary>
        /// Returns data struct of all bank branch
        /// exist in Israel
        /// </summary>
        /// <returns></returns>
        IEnumerable<BankBranch> GetAllBanks();

        /// <summary>
        /// Get a date and number of vication days and return all 
        /// the possible hosting units
        /// </summary>
        /// <param name="date">Date the vication begins</param>
        /// <param name="days">Number of days for the vication</param>
        /// <returns></returns>
        List<HostingUnit> PossibleHstUnt(DateTime date, uint days);
        /// <summary>
        /// return the number of days between two dates
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        int DaysBetween(DateTime date1, DateTime date2);
        /// <summary>
        /// Return the number of days from date to today
        /// </summary>
        /// <param name="date1"></param>
        /// <returns></returns>
        int DaysBetween(DateTime date1);
        /// <summary>
        /// return all the orders the time from their creating
        /// or mail was send is large or equal days
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        List<Order> DaysExist(uint days);
        /// <summary>
        /// Returns a list of Guest requests that follow a condition
        /// </summary>
        /// <param name="func">dalagate fanction gets GuestRequest and 
        /// returns bool</param>
        /// <returns></returns>
        List<GuestRequest> RequestUnderCond(del func);
        /// <summary>
        /// Returns the number or orders for certian guest request
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        int NumOfOrdersPerReq(GuestRequest req);
        /// <summary>
        /// Returns the number or orders for certian hosting unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        int NumOfOrdersPerUnit(HostingUnit unit);

        /// <summary>
        /// Find the guest request own the key
        /// </summary>
        /// <param name="key">guest request key</param>
        /// <returns></returns>
        GuestRequest FindReqByKey(long key);
        /// <summary>
        /// Find the hostint unit own the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        HostingUnit FindUnitByKey(long key);
        /// <summary>
        /// Find the order own the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>order key</returns>
        Order FindOrderByKey(long key);
        /// <summary>
        /// Find the host from the data source by key
        /// </summary>
        /// <param name="key">key of the host</param>
        /// <returns></returns>
        Host FindHostByKey(long key);

        /// <summary>
        /// Returns all the guest request in the data source
        /// grouping by areas
        /// </summary>
        /// <returns></returns>
        List<IGrouping<Areas, GuestRequest>> ReqGroupByArea();
        /// <summary>
        /// Returns all the guest request in the data source
        /// grouping by number of guests
        /// </summary>
        /// <returns></returns>
        List<IGrouping<int, GuestRequest>> ReqGroupByGuestNum();
        /// <summary>
        /// return a list of hosts grouping by the number of units
        /// they own
        /// </summary>
        /// <returns></returns>
        List<IGrouping<int, Host>> HostsGroupByUnits();
        /// <summary>
        /// Return a list of hosting units grouping by their areas
        /// </summary>
        /// <returns></returns>
        List<IGrouping<Areas, HostingUnit>> UnitsGroupByArea();
        /// <summary>
        /// Return a list of orders grouping by hosting unit 
        /// </summary>
        /// <returns></returns>
        List<IGrouping<HostingUnit, Order>> OrdersGroupByUnit();
        /// <summary>
        /// find the branch from the pool
        /// </summary>
        /// <param name="bankNum">bank number</param>
        /// <param name="branchNum">branch number<branch number/param>
        /// <returns>new BankBranch</returns>
        BankBranch FindBranch(int bankNum, int branchNum);
    }
}
