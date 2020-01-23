using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using DS;

namespace ConsoleApp1
{
    class XMLApp
    {
        static void Main(string[] args)
        {
            XmlSerializer x = new XmlSerializer(DataSource.ordersList.GetType());
            FileStream fs = new FileStream(@"XML_OrderFile.xml", FileMode.Create);
            x.Serialize(fs, DataSource.ordersList);

        }
    }
}
