using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace APILibDutto5AOttobre23
{
    public class GetISOCode
    {
        public static readonly HttpClient client = new HttpClient();
        public static async Task<string> GetISOCodeDutto5AOttobre23(Customer customer)
        {
            string fullMethodName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + ": ";
            string ret = "";

            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n  <soap:Body>\r\n    <ListOfCountryNamesByName xmlns=\"http://www.oorsprong.org/websamples.countryinfo\">\r\n    </ListOfCountryNamesByName>\r\n  </soap:Body>\r\n</soap:Envelope>";

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso");

                StringContent content = new StringContent(xml, null, "text/xml");

                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                ret = await DeserializeXml(await response.Content.ReadAsStringAsync(), customer.CountryDescription);

            }
            catch (Exception ex)
            {
                throw new Exception(fullMethodName + ex.Message);
            }

            return ret;
        }

        public static async Task<string> DeserializeXml(string root, string countryDescription)
        {
            string fullMethodName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + ": ";
            string ret = "";

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(root);

                string childName = "";
                string childText = "";
                string childXml = "";

                while (xmlDoc.DocumentElement.Name == "soap:Envelope" || xmlDoc.DocumentElement.Name == "soap:Body" || xmlDoc.DocumentElement.Name == "m:ListOfCountryNamesByNameResponse" || xmlDoc.DocumentElement.Name == "m:ListOfCountryNamesByNameResult")
                {
                    string tempXmlString = xmlDoc.DocumentElement.InnerXml;

                    if (xmlDoc.DocumentElement.Name == "m:ListOfCountryNamesByNameResult")
                    {
                        foreach (XmlNode child in xmlDoc.DocumentElement.ChildNodes)
                        {
                            childName = child.Name;
                            childText = child.InnerText;
                            childXml = child.InnerXml;

                            if ((child.Name == "m:tCountryCodeAndName") && (child.ChildNodes.Item(1).InnerText == countryDescription))
                            {
                                ret = child.ChildNodes.Item(0).InnerText;
                                break;
                            }
                        }
                    }
                    else
                    {
                        xmlDoc.LoadXml(tempXmlString);
                    }

                    if (ret != string.Empty) break;
                }
            }
            catch (Exception ex)
            {

            }

            return "{\n  \"isoCode\": \"" + ret + "\" \n}";
        }
    }
}
