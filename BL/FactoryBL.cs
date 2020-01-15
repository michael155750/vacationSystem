using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL 
{
   public sealed class FactoryBL : Ibl
    {
        //private static readonly FactoryBL instance = new FactoryBL();
        //public static FactoryBL Instance { get { return instance; } }

        public static Ibl getBL()
        {
            //return Instance;
            return new BL_imp();
        }

        public void AddOrder(long guestReqKey, long UnitKey)
        {
            throw new NotImplementedException();
        }

        public void AddRequest(GuestRequest guestRequest)
        {
            throw new NotImplementedException();
        }

        public void AddUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void AddUnitAndHost(HostingUnit hostingUnit, Host host)
        {
            throw new NotImplementedException();
        }

        public List<HostingUnit> PossibleHstUnt(DateTime date, uint days)
        {
            throw new NotImplementedException();
        }

        public int DaysBetween(DateTime date1, DateTime date2)
        {
            throw new NotImplementedException();
        }

        public int DaysBetween(DateTime date1)
        {
            throw new NotImplementedException();
        }

        public List<Order> DaysExist(uint days)
        {
            throw new NotImplementedException();
        }

        public void DeleteUnit(long hostingUnitKey)
        {
            throw new NotImplementedException();
        }

        public BankBranch FindBranch(int bankNum, int branchNum)
        {
            throw new NotImplementedException();
        }

        public Order FindOrderByKey(long key)
        {
            throw new NotImplementedException();
        }

        public GuestRequest FindReqByKey(long key)
        {
            throw new NotImplementedException();
        }

        public HostingUnit FindUnitByKey(long key)
        {
            throw new NotImplementedException();
        }

        public Host FindHostByKey(long key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BankBranch> GetAllBanks()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GuestRequest> GetAllRequests()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HostingUnit> GetAllUnits()
        {
            throw new NotImplementedException();
        }

       

        public List<IGrouping<int, Host>> HostsGroupByUnits()
        {
            throw new NotImplementedException();
        }

        public int NumOfOrdersPerReq(GuestRequest req)
        {
            throw new NotImplementedException();
        }

        public int NumOfOrdersPerUnit(HostingUnit unit)
        {
            throw new NotImplementedException();
        }

        public List<IGrouping<HostingUnit, Order>> OrdersGroupByUnit()
        {
            throw new NotImplementedException();
        }

        public List<HostingUnit> PossibleHstUnt(DateTime date, int days)
        {
            throw new NotImplementedException();
        }

        public List<IGrouping<Areas, GuestRequest>> ReqGroupByArea()
        {
            throw new NotImplementedException();
        }

        public List<IGrouping<int, GuestRequest>> ReqGroupByGuestNum()
        {
            throw new NotImplementedException();
        }

        public List<GuestRequest> RequestUnderCond(del func)
        {
            throw new NotImplementedException();
        }

        public List<IGrouping<Areas, HostingUnit>> UnitsGroupByArea()
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(long orderKey, OrderStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public void UpdateRequest(long guestRequestKey, GuestRequestStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public void UpdateUnitDiary(long key, DateTime date, int days)
        {
            throw new NotImplementedException();
        }

        public void UpdateUnitName(long key, string name)
        {
            throw new NotImplementedException();
        }
        public void UpdateUnit(HostingUnit hostingUnit, long key)
        {
            throw new NotImplementedException();
        }
    }
}
