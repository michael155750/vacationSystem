using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public sealed class FactoryDal : Idal
    {
        private string DalType = "xml";

        public Idal GetDal()
        {
            if (DalType == "xml") return Dal_XML_imp.Instance;

            else return Dal_imp.Instance;
        }

        public void AddOrder(Order order)
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

        public void DeleteUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bankdetails> GetAllBanks()
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

        

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void UpdateRequest(GuestRequest guestRequest)
        {
            throw new NotImplementedException();
        }

        public void UpdateUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void AddUnitAndHost(HostingUnit hostingUnit, Host host)
        {
            throw new NotImplementedException();
        }

        public void UpdateUnit(HostingUnit hostingUnit, long key)
        {
            throw new NotImplementedException();
        }
    }
}
