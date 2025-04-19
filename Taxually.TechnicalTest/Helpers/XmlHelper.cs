using System.Xml.Serialization;

namespace Taxually.TechnicalTest.Helpers
{
    public class XmlHelper
    {
        public string SerializeToXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
    }
}
