using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UtilityLib.SOAP
{
    public class SOAPHelper
    {
        public void CreateSoapRequestFile(string filePath, params string[] values)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                XmlTextWriter writer = new XmlTextWriter(fs, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("soap", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");

                writer.WriteStartElement("soap", "Body", null);
                writer.WriteStartElement("ShipmentStatus");
                writer.WriteAttributeString("xmlns", "http://www.xxxonline.com/");

                writer.WriteStartElement("Vendor");
                writer.WriteElementString("VendorNumber", values[0]);
                writer.WriteElementString("Password", values[1]);
                writer.WriteEndElement();

                writer.WriteStartElement("Shipment");
                writer.WriteElementString("ReferenceNumber", values[2]);
                writer.WriteElementString("TrackingNumber", values[3]);
                writer.WriteStartElement("Statuses");
                writer.WriteStartElement("Status");
                writer.WriteElementString("Code", values[4]);
                writer.WriteElementString("Description", values[5]);
                writer.WriteElementString("Signature", values[6]);
                writer.WriteElementString("Time1", values[7]);
                writer.WriteElementString("Time2", values[8]);
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
                fs.Close();
            }
        }
    }
}
