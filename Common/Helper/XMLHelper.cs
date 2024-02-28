
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Common.Helper
{
    public static class XMLHelper
    {
        public static string GetXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Encoding = new UnicodeEncoding(false, false)
            };
            StringBuilder xml = new StringBuilder();
            using (XmlWriter xw = XmlWriter.Create(xml, settings))
            {
                serializer.Serialize(xw, obj);
            }
            var newXML = xml.ToString();
            return newXML;

        }
    }
}
