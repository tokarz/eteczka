using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Eteczka.BE.DTO;

namespace Eteczka.BE.Utils
{
    public class JrwaParser
    {

        public OrganizacjaDTO WczytajStrukture(string sciezkaDoXml)
        {
            OrganizacjaDTO result = new OrganizacjaDTO();
            result.Name = "TopFarms";
            XmlDocument projectDoc = new XmlDocument();
            
            projectDoc.Load(sciezkaDoXml);
            XmlNodeList nodeList = projectDoc.SelectNodes("//Organizacja");
            foreach (XmlNode node in nodeList)
            {
                foreach(XmlNode subNode in node.ChildNodes)
                {
                    
                }
            }

            return result;
        }

    }
}
