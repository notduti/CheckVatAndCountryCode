using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILibDutto5AOttobre23
{
    public class CheckVat
    {

        public static readonly HttpClient client = new HttpClient();
        public static async Task<string> CheckEuVatDutto5AOttobre23(Customer customer)
        {
            string fullMethodName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + ": ";
            string ret = "";
            string uri = "https://ec.europa.eu/taxation_customs/vies/rest-api/ms/" + customer.CountryCode + "/vat/" + customer.VatNumber;

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                
                
                ret = await DeserializeJson(await response.Content.ReadAsStringAsync());

            }
            catch (HttpRequestException ex)
            {
                throw new Exception(fullMethodName + ex.Message);
            }

            return ret;
        }

        public static async Task<string> DeserializeJson(string root)
        {
            var deserializedData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(root);

            string name = deserializedData["name"];
            string address = deserializedData["address"];

            return JsonConvert.SerializeObject(new ToJsonHttpResponse(name, address));
        }

    }
}
