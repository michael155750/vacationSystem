using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DAL
{
    public interface Idal
    {
       
        /// <summary>
        /// Insert into the data source new guest request
        /// </summary>
        /// <param name="guestRequest"></param>
        void AddRequest(GuestRequest guestRequest);
        /// <summary>
        /// update the status of guest request
        /// </summary>
        /// <param name="guestRequest"></param>
        void UpdateRequest(GuestRequest guestRequest);
        /// <summary>
        /// Add new hosting unit in the data source 
        /// </summary>
        /// <param name="hostingUnit"></param>
        void AddUnit(HostingUnit hostingUnit);
        /// <summary>
        /// Add new hosting unit when the host is also new
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <param name="host"></param>
        void AddUnitAndHost(HostingUnit hostingUnit, Host host);
        /// <summary>
        /// Delete a hosting unit from the data source
        /// </summary>
        /// <param name="hostingUnit"></param>
        void DeleteUnit(HostingUnit hostingUnit);
        /// <summary>
        /// update the diary and the name of a hosting unit 
        /// </summary>
        /// <param name="hostingUnit"></param>
        void UpdateUnit(HostingUnit hostingUnit);
        /// <summary>
        /// update the diary and the name of a hosting unit 
        /// </summary>
        /// <param name="hostingUnit"></param>
        void UpdateUnit(HostingUnit hostingUnit, long key);
        /// <summary>
        /// Add a new order into the data source
        /// </summary>
        /// <param name="order"></param>
        void AddOrder(Order order);
        /// <summary>
        /// Update the status of an order in the data source
        /// </summary>
        /// <param name="order"></param>
        void UpdateOrder(Order order);

        /// <summary>
        /// return a collection of all the hosting units in the data source
        /// </summary>
        /// <returns></returns>
        IEnumerable<HostingUnit> GetAllUnits();
        /// <summary>
        /// Return a collection of all the guest requests in the data source
        /// </summary>
        /// <returns></returns>
        IEnumerable<GuestRequest> GetAllRequests();
        /// <summary>
        /// Return a collection of all the orders in the data source
        /// </summary>
        /// <returns></returns>
        IEnumerable<Order> GetAllOrders();
        /// <summary>
        /// return a collection of all the bank branches in the Israel
        /// </summary>
        /// <returns></returns>
        IEnumerable<BankBranch> GetAllBanks();

    }
}
