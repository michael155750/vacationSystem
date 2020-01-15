using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using DAL;
using BE;

namespace BL
{
    public class BL_imp : Ibl
    {
        //DAL.Idal dal = FactoryDal.Instance.getDal();
        DAL.Idal dal = FactoryDal.getDal();

        #region Guest request

        public void AddRequest(GuestRequest guestRequest)
        {

            if (guestRequest.Area < 0 || guestRequest.Area > (Areas)4)
            {
                throw new InvalidEnumArgumentException("Please insert correct area");
            }
            if (guestRequest.ChildrensAttractions < 0 || guestRequest.ChildrensAttractions > (Choice)2 ||
                guestRequest.Garden < 0 || guestRequest.Garden > (Choice)2 ||
                guestRequest.Jacuzzi < 0 || guestRequest.Jacuzzi > (Choice)2 ||
                guestRequest.Pool < 0 || guestRequest.Pool > (Choice)2)
            {
                throw new InvalidEnumArgumentException("Please insert correct choice");
            }
            if (guestRequest.Type < 0 || guestRequest.Type > (Types)2)
                throw new InvalidEnumArgumentException("Please insert correct Type");
            if (guestRequest.EntryDate == guestRequest.ReleaseDate)
                throw new ArgumentException("Guest request have to be for at least 2 days");
            try
            {
                dal.AddRequest(guestRequest);
            }
            catch (DuplicateWaitObjectException exc)
            {
                throw exc;
            }
        }

        public void UpdateRequest(long guestRequestKey, GuestRequestStatus newStatus)
        {
            GuestRequest certianRequest = FindReqByKey(guestRequestKey);
            if (newStatus < 0 || newStatus > (GuestRequestStatus)2)
                throw new InvalidEnumArgumentException("Please insert correct status");
            certianRequest.Status = newStatus;
            try
            {
                dal.UpdateRequest(certianRequest);
            }
            catch (KeyNotFoundException exc)
            {
                throw exc;
            }
        }

        public IEnumerable<GuestRequest> GetAllRequests()
        {
            return dal.GetAllRequests();
        }

        public List<GuestRequest> RequestUnderCond(del func)
        {
            List<GuestRequest> lst = (List<GuestRequest>)dal.GetAllRequests();
            foreach (var item in lst)
            {
                if (!func(item))
                {
                    lst.Remove(item);

                }
            }
            return lst;
        }

        public int NumOfOrdersPerReq(GuestRequest req)
        {
            //List<Order> lst = (List<Order>)dal.GetAllOrders();
            //int numOfOrders = 0;
            //foreach (var item in lst)
            //{
            //    if (item.GuestRequestKey == req.GuestRequestKey)
            //        numOfOrders++;
            //}
            //return numOfOrders;
            int numOfOrders = 0;
            IEnumerable<Order> lst = from item in dal.GetAllOrders()
                                     where item.GuestRequestKey == req.GuestRequestKey
                                     select item;
            foreach (var item in lst)
            {
                numOfOrders++;
            }
            return numOfOrders;
        }

        #endregion

        #region Hosting unit

        public void AddUnit(HostingUnit hostingUnit)
        {
            hostingUnit.Diary = new bool[12, 31];
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    hostingUnit.Diary[i, j] = false;
                }
            }

            if (hostingUnit.Area < 0 || hostingUnit.Area > (Areas)4)
            {
                throw new InvalidEnumArgumentException("Please insert correct area");
            }
            try
            {
                dal.AddUnit(hostingUnit);
            }
            catch (DuplicateWaitObjectException exc)
            {
                throw exc;
            }
        }

        public void AddUnitAndHost(HostingUnit hostingUnit, Host host)
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    hostingUnit.Diary[i, j] = false;
                }
            }
            hostingUnit.Owner.MyCommition = 0;

            if (hostingUnit.Owner.FhoneNumber < 20000000 || hostingUnit.Owner.FhoneNumber > 9999999999)
                throw new ArgumentOutOfRangeException("Please insert correct fhone number");

            if (hostingUnit.Owner.CollectionClearance < 0 || hostingUnit.Owner.CollectionClearance > (YesNo)1)
                throw new InvalidEnumArgumentException("Please insert correct choice");

            try
            {
                dal.AddUnitAndHost(hostingUnit, host);
            }
            catch (DuplicateWaitObjectException exc)
            {
                throw exc;
            }

        }

        public void DeleteUnit(long hostingUnitKey)
        {
            HostingUnit certianUnit = FindUnitByKey(hostingUnitKey);
            var allOrders = GetAllOrders();
            foreach (var item in GetAllOrders())
            {
                if ((item.Status == OrderStatus.MailSend ||
                    item.Status == OrderStatus.NotYetHandle)
                    && item.HostingUnitKey == hostingUnitKey)
                    throw new ArgumentException("can't delete, their is still open order");
            }
            try
            {
                dal.DeleteUnit(certianUnit);
            }
            catch (Exception exc) //catch 2 tipes of exceptions.
            {
                if (exc is KeyNotFoundException)
                    throw exc;
                if (exc is ArgumentNullException)
                    throw new ArgumentNullException("The key exists, /n Unable to find content by key");
            }
        }

        public void UpdateUnit(HostingUnit hostingUnit , long key)
        {
            try
            {
                dal.UpdateUnit(hostingUnit);
            }
            catch (Exception exc)
            {
                if (exc is KeyNotFoundException)
                    throw exc;
                if (exc is ArgumentNullException)
                    throw new ArgumentNullException("The key exists, /n Unable to find content by key");
                if (exc is ArgumentException)
                    throw new ArgumentException("The key exists, /n Unable to remone oldest content by key");
                if (exc is ArgumentOutOfRangeException)
                    throw new ArgumentOutOfRangeException("The key exists, / n the index is out of range");
            }
        }

        public void UpdateUnitName(long key, string name)
        {
            HostingUnit unit = FindUnitByKey(key);
            unit.HostingUnitName = name;
            try
            {
                dal.UpdateUnit(unit);
            }
            catch (Exception exc)
            {
                if (exc is KeyNotFoundException)
                    throw exc;
                if (exc is ArgumentNullException)
                    throw new ArgumentNullException("The key exists, /n Unable to find content by key");
                if (exc is ArgumentException)
                    throw new ArgumentException("The key exists, /n Unable to remone oldest content by key");
                if (exc is ArgumentOutOfRangeException)
                    throw new ArgumentOutOfRangeException("The key exists, / n the index is out of range");
            }
        }

        public void UpdateUnitDiary(long key, DateTime date, int days)
        {
            HostingUnit unit = FindUnitByKey(key);

            DateTime tmpDate = date;
            for (int i = 1; i <= days; i++)
            {
                if (unit.Diary[tmpDate.Month, tmpDate.Day])
                    throw new ArgumentException("The days is allready invited");
                tmpDate = tmpDate.AddDays(i);
            }
            for (int i = 1; i <= days; i++)
            {
                unit.Diary[date.Month, date.Day] = true;
                date = date.AddDays(i);
            }
            try
            {
                dal.UpdateUnit(unit);
            }
            catch (Exception exc)
            {
                if (exc is KeyNotFoundException)
                    throw exc;
                if (exc is ArgumentNullException)
                    throw new ArgumentNullException("The key exists, /n Unable to find content by key");
                if (exc is ArgumentException)
                    throw new ArgumentException("The key exists, /n Unable to remone oldest content by key");
                if (exc is ArgumentOutOfRangeException)
                    throw new ArgumentOutOfRangeException("The key exists, / n the index is out of range");
            }
        }

        public IEnumerable<HostingUnit> GetAllUnits()
        {
            return dal.GetAllUnits();
        }

        public List<HostingUnit> PossibleHstUnt(DateTime date, uint days)
        {
            List<HostingUnit> lst = (List<HostingUnit>)dal.GetAllUnits();

            foreach (var item in lst)
            {
                DateTime tempDate = date;
                for (int i = 0; i < days; i++)
                {
                    if (item.Diary[date.Month - 1, date.Day - 1])
                    {
                        lst.Remove(item);
                        break;
                    }
                    else
                    {
                        tempDate = tempDate.AddDays(1);
                        if (tempDate.Year != date.Year)
                            throw new IndexOutOfRangeException("The system is only for 2020!");
                    }
                }
            }
            return lst;
        }

        public int NumOfOrdersPerUnit(HostingUnit unit)
        {
            //List<Order> lst = (List<Order>)dal.GetAllOrders();
            //int numOfOrders = 0;
            //foreach (var item in lst)
            //{
            //    if (item.HostingUnitKey == unit.HostingUnitKey)
            //        numOfOrders++;
            //}
            //return numOfOrders;
            int numOfOrders = 0;
            IEnumerable<Order> lst = from item in dal.GetAllOrders()
                                     where item.HostingUnitKey == unit.HostingUnitKey
                                     select item;
            foreach (var item in lst)
            {
                numOfOrders++;
            }

            return numOfOrders;

        }

        #endregion

        #region Order

        public void AddOrder(long guestReqKey, long UnitKey)
        {
            if (!dal.GetAllUnits().Any(x => x.HostingUnitKey == UnitKey))
                throw new KeyNotFoundException("The hosting unit key doesn't exist");
            if (!dal.GetAllRequests().Any(x => x.GuestRequestKey == guestReqKey))
                throw new KeyNotFoundException("The guest request key doesn't exist");
            Order order = new Order
            {
                GuestRequestKey = guestReqKey,
                HostingUnitKey = UnitKey,
                CreateDate = DateTime.Now,
                OrderDate = DateTime.MinValue,
                Status = OrderStatus.NotYetHandle
            };

            GuestRequest certianRequest = FindReqByKey(guestReqKey);
            if (certianRequest.Status != GuestRequestStatus.Active)
                throw new DataException("the request is not active");
            DateTime entryDate = certianRequest.EntryDate;
            DateTime releaseDate = certianRequest.ReleaseDate;

            bool[,] diary = null;
            try
            {
                foreach (var item in dal.GetAllUnits())
                {
                    if (item.HostingUnitKey == order.HostingUnitKey)
                    {
                        diary = item.Diary;
                    }
                }
            }
            catch (Exception exc)
            {
                if (exc is ArgumentNullException)
                    throw new ArgumentNullException("Unable to copy content to array or list");
                if (exc is ArgumentException)
                    throw new ArgumentException("Unable to copy content to array or list");
            }

            for (DateTime i = entryDate; i < releaseDate; i.AddDays(1))
            {
                if (diary[i.Month - 1, i.Day - 1] == true)
                    throw new DataException("the dates immpossible");
            }
            try
            {
                dal.AddOrder(order);
            }
            catch (DuplicateWaitObjectException exc)
            {
                throw exc;
            }
        }

        public void UpdateOrder(long orderKey, OrderStatus newStatus)
        {
            var unitCollection = dal.GetAllUnits();//gets all hosting units

            HostingUnit currentUnit = new HostingUnit();
            foreach (var item in unitCollection)//find the current hosting unit 
            {
                if (item.HostingUnitKey == FindOrderByKey(orderKey).HostingUnitKey)
                    currentUnit = item;
            }
            if (newStatus == OrderStatus.MailSend && currentUnit.Owner.CollectionClearance == YesNo.No)
                throw new DataException("Their is not collection clearence");

            Order currentOrder = FindOrderByKey(orderKey);//find the current order to update
            if (currentOrder.Status == OrderStatus.DillMade)
                throw new DataException("cannot change the status when dill made");

            DateTime entryDate = new DateTime();
            DateTime releaseDate = new DateTime();
            GuestRequest currentRequest = FindReqByKey(currentOrder.GuestRequestKey);
            entryDate = currentRequest.EntryDate;
            releaseDate = currentRequest.ReleaseDate;

            if (newStatus == OrderStatus.MailSend)
            {
                currentOrder.OrderDate = DateTime.Now;
                //suppose to changed in the finnal step
                //Console.WriteLine("mail send");
            }

            if (newStatus == OrderStatus.DillMade)
            {
                //Calculate the commition for the website
                currentUnit.Owner.MyCommition +=
                    DaysBetween(entryDate, releaseDate) * BE.Configuration.Commition;

                //change the dates in the diary of the unit
                for (DateTime i = entryDate; i < releaseDate; i = i.AddDays(1))
                {
                    currentUnit.Diary[i.Month, i.Day] = true;
                }

                //Change the status of all the requests of the client
                currentRequest.Status = GuestRequestStatus.DealComplete;
                List<GuestRequest> allclientReq =
                    RequestUnderCond(x => x.FirstName == currentRequest.FirstName &&
                                        x.LastName == currentRequest.LastName);
                foreach (var item in allclientReq)
                {
                    item.Status = GuestRequestStatus.DealComplete;
                }
            }

            dal.UpdateOrder(currentOrder);

        }

        public IEnumerable<Order> GetAllOrders()
        {
            return dal.GetAllOrders();
        }

        #endregion

        #region Bank

        public IEnumerable<BankBranch> GetAllBanks()
        {
            return dal.GetAllBanks();
        }

        public BankBranch FindBranch(int bankNum, int branchNum)
        {
            if (!dal.GetAllBanks().Any(
                x => x.BankNumber == bankNum))
                throw new KeyNotFoundException("A bank with this number didn't found");
            if (!dal.GetAllBanks().Any(
               x => x.BranchNumber == branchNum && x.BankNumber == bankNum))
                throw new KeyNotFoundException("A branch with this number didn't found in this bank");
            return dal.GetAllBanks().First(
                x => x.BankNumber == bankNum && x.BranchNumber == branchNum);
            //foreach (var item in dal.GetAllBanks())
            //{ ==
            //    if (item.BranchNumber == branchNum && item.BankNumber == bankNum)
            //        bankBranch = item;

            //}


        }

        #endregion

        #region Grouping

        public List<IGrouping<Areas, GuestRequest>> ReqGroupByArea()
        {
            //var result = from item in dal.GetAllRequests()
            //             group item by item.Area into grou
            //             select new { area = grou.Key, req = grou };
            //List<GuestRequest> lst = new List<GuestRequest>();
            //foreach (var grou in result)
            //    foreach (var item in grou.req)
            //    {
            //        lst.Add(item);
            //    }
            //return lst;
            var result = from item in dal.GetAllRequests()
                         group item by item.Area into grou                         
                         select grou;
            return (List<IGrouping<Areas, GuestRequest>>)result;

        }

        public List<IGrouping<int, GuestRequest>> ReqGroupByGuestNum()
        {
            var result = from item in dal.GetAllRequests()
                         let GuestNum = item.Children + item.Adults
                         group item by GuestNum into grou
                         select grou;


            return (List<IGrouping<int, GuestRequest>>)result;

        }

        public List<IGrouping<int, Host>> HostsGroupByUnits()
        {
            var result = from item in dal.GetAllUnits()
                         group item by item.Owner.HostKey into grou
                         orderby grou.Count()
                         select grou;
            return (List<IGrouping<int, Host>>)result;


        }

        public List<IGrouping<Areas, HostingUnit>> UnitsGroupByArea()
        {
            var result = from item in dal.GetAllUnits()
                         group item by item.Area into grou
                         select grou;
            return (List<IGrouping<Areas, HostingUnit>>)result;
        }

        public List<IGrouping<HostingUnit, Order>> OrdersGroupByUnit()
        {
            var result = from item in dal.GetAllOrders()
                         group item by item.HostingUnitKey into grou
                         select grou;
            return (List<IGrouping<HostingUnit, Order>>)result;
        }

        #endregion

        #region Days

        public int DaysBetween(DateTime date1, DateTime date2)
        {
            return (int)(date2 - date1).TotalDays;
        }

        public int DaysBetween(DateTime date1)
        {
            return (int)(DateTime.Today - date1).TotalDays;

        }

        public List<Order> DaysExist(uint days)
        {
            List<Order> lst = (List<Order>)dal.GetAllOrders();
            foreach (var item in lst)
            {
                if ((DateTime.Today - item.OrderDate).TotalDays < days &&
                (DateTime.Today - item.CreateDate).TotalDays < days)
                {
                    lst.Remove(item);
                }
            }
            return lst;
        }

        #endregion
               
        #region Find by key

        public GuestRequest FindReqByKey(long key)
        {
            //GuestRequest certianRequest = new GuestRequest();
            //foreach (var item in dal.GetAllRequests())
            //{
            //    if (item.GuestRequestKey == key)
            //        certianRequest = item;

            //}
            //return certianRequest;
            if (!dal.GetAllRequests().Any(x => x.GuestRequestKey == key))
                throw new KeyNotFoundException("The guest request key doesn't exist");
            return dal.GetAllRequests().First(x => x.GuestRequestKey == key);
        }

        public HostingUnit FindUnitByKey(long key)
        {
            //HostingUnit certianUnit = new HostingUnit();
            //foreach (var item in dal.GetAllUnits())
            //{
            //    if (item.HostingUnitKey == key)
            //        certianUnit = item;

            //}
            //return certianUnit;
            if (!dal.GetAllUnits().Any(x => x.HostingUnitKey == key))
                throw new KeyNotFoundException("The hosting unit key doesn't exist");
            return dal.GetAllUnits().First(x => x.HostingUnitKey == key);
        }

        public Order FindOrderByKey(long key)
        {
            //Order certianOrder = new Order();
            //foreach (var item in dal.GetAllOrders())
            //{
            //    if (item.OrderKey == key)
            //        certianOrder = item;

            //}
            //return certianOrder;
            if (!dal.GetAllOrders().Any(x => x.OrderKey == key))
                throw new KeyNotFoundException("The order key doesn't exist");

            return dal.GetAllOrders().First(x => x.OrderKey == key);
        }

        public Host FindHostByKey(long key)
        {
            if (!dal.GetAllUnits().Any(x => x.Owner.HostKey == key))
                throw new KeyNotFoundException("The host key doesn't exist");
            return dal.GetAllUnits().First(x => x.Owner.HostKey == key).Owner;
        }

        #endregion

    }
}
