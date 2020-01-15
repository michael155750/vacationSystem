using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using BE;


namespace DAL
{
    public class Dal_imp : Idal
    {
        
        #region Guest request

        public void AddRequest(GuestRequest guestRequest)
        {
            guestRequest.GuestRequestKey = Configuration.globalGuestRequestKey++;

            foreach (var item in DataSource.guestRequestsList)
            {
                if (guestRequest.GuestRequestKey == item.GuestRequestKey)
                    throw new DuplicateWaitObjectException("Guest Request Key exist in the system");
            }

            guestRequest.Status = GuestRequestStatus.Active;
            DataSource.guestRequestsList.Add(guestRequest);
        }

        public void UpdateRequest(GuestRequest guestRequest)
        {
            //Checks whether the key request exists in the system
            bool flag = false;
            foreach (var item in DataSource.guestRequestsList)
            {
                if (guestRequest.GuestRequestKey == item.GuestRequestKey)
                    flag = true;
            }
            if (!flag)
                throw new KeyNotFoundException("The item key does not exist");

            DataSource.guestRequestsList
                [DataSource.guestRequestsList.FindIndex(x => x.GuestRequestKey == guestRequest.GuestRequestKey)] //Return the index of the value
                .Status = guestRequest.Status;
            //var a = from item in DataSource.guestRequestsList
            //        where item.GuestRequestKey == guestRequest.GuestRequestKey
            //        select item;

        }

        public IEnumerable<GuestRequest> GetAllRequests()
        {
            var temp = from item in DataSource.guestRequestsList
                       let item1 = item
                       select item1;

            List<GuestRequest> temp2 = temp.ToList();
            GuestRequest[] target = new GuestRequest[temp2.Count];

            temp2.CopyTo(target);

            return target.ToList();

            //GuestRequest[] target = new GuestRequest[DataSource.guestRequestsList.Count];
            //DataSource.guestRequestsList.CopyTo(target);
            //return target;
        }

        #endregion

        #region Hosting Unit

        public void AddUnit(HostingUnit hostingUnit)
        {
            //DataSource.hostingUnitsList.Any(x => hostingUnit.Owner.HostKey == x.Owner.HostKey)
            //hostingUnit.Owner.HostKey = Configuration.globalHostKey++;  

            hostingUnit.HostingUnitKey = Configuration.globalHostingUnitKey++;

            foreach (var item in DataSource.hostingUnitsList)
            {
                if (hostingUnit.HostingUnitKey == item.HostingUnitKey)
                    throw new DuplicateWaitObjectException("Histing unut Key exist in the system");
            }

            DataSource.hostingUnitsList.Add(hostingUnit);
        }

        public void AddUnitAndHost(HostingUnit hostingUnit, Host host)
        {
            hostingUnit.HostingUnitKey = Configuration.globalHostingUnitKey++;
            //Checks whether the hosting unit key exists in the system
            foreach (var item in DataSource.hostingUnitsList)
            {
                if (hostingUnit.HostingUnitKey == item.HostingUnitKey)
                    throw new DuplicateWaitObjectException("The hosting Unit key already exist");
            }

            host.HostKey = Configuration.globalHostKey++;
            //Checks whether the host key exists in the system
            foreach (var item in DataSource.hostingUnitsList)
            {
                if (hostingUnit.Owner.HostKey == item.Owner.HostKey)
                    throw new DuplicateWaitObjectException("The host key already exist");
            }

            DataSource.hostingUnitsList.Add(hostingUnit);
        }

        public void DeleteUnit(HostingUnit hostingUnit)
        {
            //Checks whether the key hosting unit exists in the system
            bool flag = false;
            foreach (var item in DataSource.hostingUnitsList)
            {
                if (hostingUnit.HostingUnitKey == item.HostingUnitKey)
                    flag = true;
            }
            if (!flag)
                throw new KeyNotFoundException("The item key does not exist");

            int key = DataSource.hostingUnitsList.FindIndex(x => x.HostingUnitKey == hostingUnit.HostingUnitKey);

            DataSource.hostingUnitsList.RemoveRange(key, 0);
        }

        public void UpdateUnit(HostingUnit hostingUnit)
        {
            //Checks whether the key hosting unit exists in the system
            bool flag = false;
            foreach (var item in DataSource.hostingUnitsList)
            {
                if (hostingUnit.HostingUnitKey == item.HostingUnitKey)
                    flag = true;
            }
            if (!flag)
                throw new KeyNotFoundException("The item key does not exist");

            int key = DataSource.hostingUnitsList.FindIndex(x => x.HostingUnitKey == hostingUnit.HostingUnitKey);

            DataSource.hostingUnitsList.RemoveRange(key, 0);
            DataSource.hostingUnitsList.Insert(key, hostingUnit);

            //HostingUnit h = new HostingUnit();
            //h = DataSource.hostingUnitsList.Find(x => x.HostingUnitKey == hostingUnit.HostingUnitKey);
        }

        public void UpdateUnit(HostingUnit hostingUnit , long key)
        {
            //Checks whether the key hosting unit exists in the system
            bool flag = false;
            foreach (var item in DataSource.hostingUnitsList)
            {
                if (hostingUnit.HostingUnitKey == item.HostingUnitKey)
                    flag = true;
            }
            if (!flag)
                throw new KeyNotFoundException("The item key does not exist");

            int index = DataSource.hostingUnitsList.FindIndex(x => x.HostingUnitKey == key);

            DataSource.hostingUnitsList.RemoveRange(index, 0);
            DataSource.hostingUnitsList.Insert(index, hostingUnit);

            //HostingUnit h = new HostingUnit();
            //h = DataSource.hostingUnitsList.Find(x => x.HostingUnitKey == hostingUnit.HostingUnitKey);
        }

        public IEnumerable<HostingUnit> GetAllUnits()
        {
            var temp = from item in DataSource.hostingUnitsList
                       select item;

            List<HostingUnit> temp2 = temp.ToList();
            HostingUnit[] target = new HostingUnit[temp2.Count];

            temp2.CopyTo(target);

            return target.ToList();
        }

        #endregion

        #region Order

        public void AddOrder(Order order)
        {
            order.OrderKey = Configuration.globalOrderKey++;

            foreach (var item in DataSource.ordersList)
            {
                if (item.OrderKey == order.OrderKey)
                    throw new DuplicateWaitObjectException("The hosting Unit key already exist");
            }

            DataSource.ordersList.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            //Checks whether the order exists in the system
            bool flag = false;
            foreach (var item in DataSource.ordersList)
            {
                if (order.OrderKey == item.OrderKey)
                    flag = true;
            }
            if (!flag)
                throw new KeyNotFoundException("The item key does not exist");

            DataSource.ordersList
                [DataSource.ordersList.FindIndex(x => x.OrderKey == order.OrderKey)]
                //Return the index of the value
                .Status = order.Status;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var temp = from item in DataSource.ordersList
                       select item ;

            List<Order> temp2 = temp.ToList();

            Order[] target = new Order[temp2.Count];

            temp2.CopyTo(target);

            return target.ToList();
        }

        #endregion

        #region Bank

        public IEnumerable<BankBranch> GetAllBanks()
        {
            List<BankBranch> banks = new List<BankBranch>();
            BankBranch temp = new BankBranch()
            {
                BankName = "Discount",
                BankNumber = 11,
                BranchAddress = "Zabotinsky 11",
                BranchCity = "Jerusalem",
                BranchNumber = 203
            };
            banks.Add(temp);

            temp.BankName = "Hapoalim";
            temp.BankNumber = 12;
            temp.BranchAddress = "Zabotinsky 15";
            temp.BranchCity = "Jerusalem";
            temp.BranchNumber = 391;
            banks.Add(temp);

            temp.BankName = "Hapoalim";
            temp.BankNumber = 12;
            temp.BranchAddress = "Aba Hushi 34";
            temp.BranchCity = "Haifa";
            temp.BranchNumber = 456;
            banks.Add(temp);

            temp.BankName = "Igud";
            temp.BankNumber = 13;
            temp.BranchAddress = "Ben Yehuda 88";
            temp.BranchCity = "Tel Aviv";
            temp.BranchNumber = 482;
            banks.Add(temp);

            temp.BankName = "Yahav";
            temp.BankNumber = 04;
            temp.BranchAddress = "Berdichevsky 92";
            temp.BranchCity = "Ramat Gan";
            temp.BranchNumber = 012;
            banks.Add(temp);


            return banks;
        }

        #endregion
    }
}
