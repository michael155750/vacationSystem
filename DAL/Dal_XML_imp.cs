using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Net.Mail;using System.Net;
using System.Xml;
using System.Threading;

namespace DAL
{
    class Dal_XML_imp:Idal
    {
        
        private static Dal_XML_imp instance = null;
        private static readonly object padlock = new object();


        public static Dal_XML_imp Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new Dal_XML_imp();
                    return instance;
                }
            }
        }

       
        
        private XElement OrderRoot;
        private XElement ConfigRoot;
        

        private const string RequestRootPath = @"..\..\..\XML_files\XML_Req.xml";
        private const string UnitRootPath = @"..\..\..\XML_files\XML_Unit.xml";
        private const string OrderRootPath = @"..\..\..\XML_files\XML_Order.xml";
        private const string ConfigRootPath = @"..\..\..\XML_files\XML_Config.xml";
        private const string BanksRootPath = @"..\..\..\XML_files\XML_Banks.xml";
        private const string BanksWebRootPath = @"..\..\..\XML_files\XML_BanksFromWeb.xml";

        private Dal_XML_imp()
        {
            Thread t1 = new Thread(downloadBanks);
            t1.Start();

            if (!File.Exists(RequestRootPath))
            {
               
                List<GuestRequest> lst = new List<GuestRequest>();
                SaveListToXML(lst, RequestRootPath);
            }
            if (!File.Exists(UnitRootPath))
            {
                List<HostingUnit> lst = new List<HostingUnit>();
                SaveListToXML(lst, UnitRootPath);
            }
            if (!File.Exists(OrderRootPath))
            {
                OrderRoot = new XElement("Orders");
                OrderRoot.Save(OrderRootPath);
            }
           else
            {
                try
                {
                    OrderRoot = XElement.Load(OrderRootPath);

                }
                catch
                {
                    throw new DirectoryNotFoundException("Problem with loading the file");
                }

                try
                {
                    ConfigRoot = XElement.Load(ConfigRootPath);

                }
                catch
                {
                    throw new DirectoryNotFoundException("Problem with loading the file");
                }
            }

        }

        

        public static void SaveListToXML<T>(List<T> list, string path)
        {
            try
            {
                XmlSerializer x = new XmlSerializer(list.GetType());
                FileStream fs = new FileStream(path, FileMode.Create);
                x.Serialize(fs, list);
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new XmlException("problem with xml files", ex);
            }
        }

        public static List<T> LoadListFromXML<T>(string path)
        {
            try
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream fs = new FileStream(path, FileMode.Open);
                list = (List<T>)x.Deserialize(fs);
                fs.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new XmlException("problem with xml files", ex);
            }
            
        }

        private void downloadBanks()
        {
            WebClient wc = new WebClient();
            try
            {
                string BanksServerPath =
                    @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";

                wc.DownloadFile(BanksServerPath, BanksWebRootPath);
            }
            catch (Exception)
            {
                string BanksServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(BanksServerPath, BanksWebRootPath);
            }
            finally
            {
                wc.Dispose();
            }

            XElement bankWebRoot = new XElement("ATMs");
            try
            {
                bankWebRoot = XElement.Load(BanksWebRootPath);

            }
            catch
            {
                throw new DirectoryNotFoundException("Problem with loading the file");
            }

            XElement bankRoot = new XElement("banks");
            //try
            //{
            //    bankRoot = new XElement("banks");

            //}
            //catch
            //{
            //    throw new DirectoryNotFoundException("Problem with loading the file");
            //}
            
            var ATMs = from ATM in bankWebRoot.Elements()
                       select new
                       {
                           BankName = new XElement("BankName", ATM.Element("שם_בנק").Value),
                           BankNumber = new XElement("BankNumber", ATM.Element("קוד_בנק").Value),
                           BranchAddress = new XElement("BranchAddress", ATM.Element("כתובת_ה-ATM").Value),
                           BranchCity = new XElement("BranchCity", ATM.Element("ישוב").Value),
                           BranchNumber = new XElement("BranchNumber", ATM.Element("קוד_סניף").Value),
                       };
            ATMs.Distinct();
            foreach (var item in ATMs)
            {
                bankRoot.Add(new XElement("bank", item.BankName, item.BankNumber, item.BranchNumber,
                    item.BranchAddress,item.BranchCity));
            }

            bankRoot.Save(BanksRootPath);
        }

            #region Guest request

            public void AddRequest(GuestRequest guestRequest)
        {
            guestRequest.GuestRequestKey = long.Parse( ConfigRoot.Element("globalGuestRequestKey").Value);
            guestRequest.GuestRequestKey++;
            ConfigRoot.Element("globalGuestRequestKey").Value = guestRequest.GuestRequestKey.ToString();
            ConfigRoot.Save(ConfigRootPath);

            List<GuestRequest> guestRequestsList = LoadListFromXML<GuestRequest>(RequestRootPath);
            foreach (var item in guestRequestsList)
            {
                if (guestRequest.GuestRequestKey == item.GuestRequestKey)
                    throw new DuplicateWaitObjectException("Guest Request Key exist in the system");
            }

            guestRequest.Status = GuestRequestStatus.Active;
            guestRequestsList.Add(guestRequest);
            SaveListToXML<GuestRequest>(guestRequestsList, RequestRootPath);
        }

        public void UpdateRequest(GuestRequest guestRequest)
        {
            //Checks whether the key request exists in the system
            bool flag = false;
            List<GuestRequest> guestRequestsList = LoadListFromXML<GuestRequest>(RequestRootPath);
            foreach (var item in guestRequestsList)
            {
                if (guestRequest.GuestRequestKey == item.GuestRequestKey)
                    flag = true;
            }
            if (!flag)
                throw new KeyNotFoundException("The item key does not exist");

            guestRequestsList
                [guestRequestsList.FindIndex(x => x.GuestRequestKey == guestRequest.GuestRequestKey)] //Return the index of the value
                .Status = guestRequest.Status;
            SaveListToXML<GuestRequest>(guestRequestsList, RequestRootPath);

        }

        public IEnumerable<GuestRequest> GetAllRequests()
        {
           

            return LoadListFromXML<GuestRequest>(RequestRootPath); 

            
        }

        #endregion

        #region Hosting Unit

        public void AddUnit(HostingUnit hostingUnit)
        {


           

            hostingUnit.HostingUnitKey = long.Parse(ConfigRoot.Element("globalHostingUnitKey").Value);
            hostingUnit.HostingUnitKey++;
            ConfigRoot.Element("globalHostingUnitKey").Value = hostingUnit.HostingUnitKey.ToString();
            ConfigRoot.Save(ConfigRootPath);

            var hostingUnitsList = LoadListFromXML<HostingUnit>(UnitRootPath);
            foreach (var item in hostingUnitsList)
            {
                if (hostingUnit.HostingUnitKey == item.HostingUnitKey)
                    throw new DuplicateWaitObjectException("Hosting unit Key exist in the system");
            }

            hostingUnitsList.Add(hostingUnit);
            SaveListToXML<HostingUnit>(hostingUnitsList, UnitRootPath);
        }

        public void AddUnitAndHost(HostingUnit hostingUnit, Host host)
        {

            hostingUnit.HostingUnitKey = long.Parse(ConfigRoot.Element("globalHostingUnitKey").Value);
            hostingUnit.HostingUnitKey++;
            ConfigRoot.Element("globalHostingUnitKey").Value = hostingUnit.HostingUnitKey.ToString();
            ConfigRoot.Save(ConfigRootPath);

            //Checks whether the hosting unit key exists in the system
            var hostingUnitsList = LoadListFromXML<HostingUnit>(UnitRootPath);
            foreach (var item in hostingUnitsList)
            {
                if (hostingUnit.HostingUnitKey == item.HostingUnitKey)
                    throw new DuplicateWaitObjectException("Hosting unit Key exist in the system");
            }

            host.HostKey = long.Parse(ConfigRoot.Element("globalHostKey").Value);
            host.HostKey++;
            ConfigRoot.Element("globalHostKey").Value = host.HostKey.ToString();
            ConfigRoot.Save(ConfigRootPath);

            var hostList = from item in hostingUnitsList
                           select new { item.Owner.HostKey };

            foreach (var item in hostList)
            {
                if (host.HostKey == item.HostKey)
                    throw new DuplicateWaitObjectException("Host Key exist in the system");
            }

            hostingUnitsList.Add(hostingUnit);
            SaveListToXML<HostingUnit>(hostingUnitsList, UnitRootPath);



        }

        public void DeleteUnit(HostingUnit hostingUnit)
        {
            //Checks whether the key hosting unit exists in the system
            bool flag = false;
            var hostingUnitsList = LoadListFromXML<HostingUnit>(UnitRootPath);
            foreach (var item in hostingUnitsList)
            {
                if (hostingUnit.HostingUnitKey == item.HostingUnitKey)
                    flag = true;
            }
            if (!flag)
                throw new KeyNotFoundException("The item key does not exist");

            int key = hostingUnitsList.FindIndex(x => x.HostingUnitKey == hostingUnit.HostingUnitKey);

            hostingUnitsList.RemoveRange(key, 0);
            SaveListToXML<HostingUnit>(hostingUnitsList, UnitRootPath);
        }

        public void UpdateUnit(HostingUnit hostingUnit)
        {
            //Checks whether the key hosting unit exists in the system
            bool flag = false;
            var hostingUnitsList = LoadListFromXML<HostingUnit>(UnitRootPath);
            foreach (var item in hostingUnitsList)
            {
                if (hostingUnit.HostingUnitKey == item.HostingUnitKey)
                    flag = true;
            }
            if (!flag)
                throw new KeyNotFoundException("The item key does not exist");

            int key = hostingUnitsList.FindIndex
                (x => x.HostingUnitKey == hostingUnit.HostingUnitKey);

            hostingUnitsList.RemoveRange(key, 0);
            hostingUnitsList.Insert(key, hostingUnit);
            SaveListToXML<HostingUnit>(hostingUnitsList, UnitRootPath);

           
        }

        public void UpdateUnit(HostingUnit hostingUnit, long key)
        {
            var hostingUnitsList = LoadListFromXML<HostingUnit>(UnitRootPath);
            //Checks whether the key hosting unit exists in the system

            bool flag = false;
            foreach (var item in hostingUnitsList)
            {
                if (hostingUnit.HostingUnitKey == item.HostingUnitKey)
                    flag = true;
            }
            if (!flag)
                throw new KeyNotFoundException("The item key does not exist");

            int index = hostingUnitsList.FindIndex(x => x.HostingUnitKey == key);

            hostingUnitsList.RemoveRange(index, 0);
            hostingUnitsList.Insert(index, hostingUnit);
            SaveListToXML<HostingUnit>(hostingUnitsList, UnitRootPath);
            
        }

        public IEnumerable<HostingUnit> GetAllUnits()
        {
           

            return LoadListFromXML<HostingUnit>(UnitRootPath);
        }

        #endregion

        #region Order

        public void AddOrder(Order order)
        {
            order.OrderKey = long.Parse(ConfigRoot.Element("globalOrderKey").Value);
            order.OrderKey++;
            ConfigRoot.Element("globalOrderKey").Value = order.OrderKey.ToString();
            ConfigRoot.Save(ConfigRootPath);

            var ordersList = (from item in OrderRoot.Elements()
                             where item.Element("OrderKey").Value == order.OrderKey.ToString()
                             select item).FirstOrDefault();

            
                if (ordersList != null)
                    throw new DuplicateWaitObjectException("The order key already exist");

            XElement CreateDate = new XElement("CreateDate", order.CreateDate);
            XElement GuestRequestKey = new XElement("GuestRequestKey", order.GuestRequestKey);
            XElement HostingUnitKey = new XElement("HostingUnitKey", order.HostingUnitKey);
            XElement OrderDate = new XElement("OrderDate", order.OrderDate);
            XElement OrderKey = new XElement("OrderKey", order.OrderKey);
            XElement Status = new XElement("Status", order.Status);

            OrderRoot.Add(new XElement("order", CreateDate, GuestRequestKey, HostingUnitKey,
                OrderDate, OrderKey, Status));
            OrderRoot.Save(OrderRootPath);
        }

        public void UpdateOrder(Order order)
        {
            //Checks whether the order exists in the system
            XElement oldOrder = (from item in OrderRoot.Elements()
                            where item.Element("OrderKey").Value == order.OrderKey.ToString()
                            select item).FirstOrDefault();
            if (oldOrder == null)
                throw new KeyNotFoundException("The item key does not exist");

            oldOrder.Remove();

            XElement CreateDate = new XElement("CreateDate", order.CreateDate);
            XElement GuestRequestKey = new XElement("GuestRequestKey", order.GuestRequestKey);
            XElement HostingUnitKey = new XElement("HostingUnitKey", order.HostingUnitKey);
            XElement OrderDate = new XElement("OrderDate", order.OrderDate);
            XElement OrderKey = new XElement("OrderKey", order.OrderKey);
            XElement Status = new XElement("Status", order.Status);

            OrderRoot.Add(new XElement("order", CreateDate, GuestRequestKey, HostingUnitKey,
                OrderDate, OrderKey, Status));
            OrderRoot.Save(OrderRootPath);
            
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var temp = from item in OrderRoot.Elements()
                       
                       select new Order
                       {
                           CreateDate = DateTime.Parse(item.Element("CreateDate").Value),
                           GuestRequestKey =long.Parse( item.Element("GuestRequestKey").Value),
                           HostingUnitKey = long.Parse(item.Element("HostingUnitKey").Value),
                           OrderDate = DateTime.Parse(item.Element("OrderDate").Value),
                           OrderKey = long.Parse(item.Element("OrderKey").Value),
                           Status = (OrderStatus)int.Parse(item.Element("Status").Value)
                       };

            

            return temp;
        }

        #endregion

        #region Bank

        public IEnumerable<BankBranch> GetAllBanks()
        {
            var banks = LoadListFromXML<BankBranch>(BanksRootPath);
            
            //BankBranch temp = new BankBranch()
            //{
            //    BankName = "Discount",
            //    BankNumber = 11,
            //    BranchAddress = "Zabotinsky 11",
            //    BranchCity = "Jerusalem",
            //    BranchNumber = 203
            //};
            //banks.Add(temp);

            //temp.BankName = "Hapoalim";
            //temp.BankNumber = 12;
            //temp.BranchAddress = "Zabotinsky 15";
            //temp.BranchCity = "Jerusalem";
            //temp.BranchNumber = 391;
            //banks.Add(temp);

            //temp.BankName = "Hapoalim";
            //temp.BankNumber = 12;
            //temp.BranchAddress = "Aba Hushi 34";
            //temp.BranchCity = "Haifa";
            //temp.BranchNumber = 456;
            //banks.Add(temp);

            //temp.BankName = "Igud";
            //temp.BankNumber = 13;
            //temp.BranchAddress = "Ben Yehuda 88";
            //temp.BranchCity = "Tel Aviv";
            //temp.BranchNumber = 482;
            //banks.Add(temp);

            //temp.BankName = "Yahav";
            //temp.BankNumber = 04;
            //temp.BranchAddress = "Berdichevsky 92";
            //temp.BranchCity = "Ramat Gan";
            //temp.BranchNumber = 012;
            //banks.Add(temp);


            return banks;
        }

        #endregion
    }


}
/*<GuestRequest>
    <GuestRequestKey>10000000</GuestRequestKey>
    <FirstName>Meir</FirstName>
    <LastName>Ochaun</LastName>
    <MailAddress>Meir@gmail.com</MailAddress>
    <Status>Active</Status>
    <RegistrationDate>2019-06-10T00:00:00</RegistrationDate>
    <EntryDate>2019-07-10T00:00:00</EntryDate>
    <ReleaseDate>2019-07-15T00:00:00</ReleaseDate>
    <Area>Center</Area>
    <SubArea>Jerusalem</SubArea>
    <Type>Zimmer</Type>
    <Adults>2</Adults>
    <Children>1</Children>
    <Pool>Unnecessary</Pool>
    <Jacuzzi>Necessary</Jacuzzi>
    <Garden>Possible</Garden>
    <ChildrensAttractions>Necessary</ChildrensAttractions>
  </GuestRequest>
  <GuestRequest>
    <GuestRequestKey>10000001</GuestRequestKey>
    <FirstName>Ori</FirstName>
    <LastName>Len</LastName>
    <MailAddress>ori@gmail.com</MailAddress>
    <Status>Active</Status>
    <RegistrationDate>2019-01-10T00:00:00</RegistrationDate>
    <EntryDate>2019-03-10T00:00:00</EntryDate>
    <ReleaseDate>2019-03-15T00:00:00</ReleaseDate>
    <Area>North</Area>
    <SubArea>Haifa</SubArea>
    <Type>Zimmer</Type>
    <Adults>2</Adults>
    <Children>3</Children>
    <Pool>Unnecessary</Pool>
    <Jacuzzi>Necessary</Jacuzzi>
    <Garden>Possible</Garden>
    <ChildrensAttractions>Necessary</ChildrensAttractions>
  </GuestRequest>
  <GuestRequest>
    <GuestRequestKey>10000002</GuestRequestKey>
    <FirstName>Shalom</FirstName>
    <LastName>Yadid</LastName>
    <MailAddress>Shalom@gmail.com</MailAddress>
    <Status>Active</Status>
    <RegistrationDate>2019-05-10T00:00:00</RegistrationDate>
    <EntryDate>2019-06-10T00:00:00</EntryDate>
    <ReleaseDate>2019-06-15T00:00:00</ReleaseDate>
    <Area>Jerusalem</Area>
    <SubArea>Jerusalem</SubArea>
    <Type>Camping</Type>
    <Adults>4</Adults>
    <Children>5</Children>
    <Pool>Unnecessary</Pool>
    <Jacuzzi>Necessary</Jacuzzi>
    <Garden>Necessary</Garden>
    <ChildrensAttractions>Possible</ChildrensAttractions>
  </GuestRequest> 
  */
