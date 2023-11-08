using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILibDutto5AOttobre23
{
    public class FinalResponse
    {
        public static async Task<string> SerializeFinalResponse(string restResponse, string soapResponse, Header header)
        {
            string fullMethodName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + ": ";
            string ret = "";

            try
            {
                JObject restParsed = JObject.Parse(restResponse);
                JObject soapParsed = JObject.Parse(soapResponse);

                JObject result = new JObject();
                result.Merge(restParsed);
                result.Merge(soapParsed);
                result.Add("devName", header.DevName);
                result.Add("team", header.Team);

                ret = result.ToString();
            }
            catch (JsonException ex)
            {
                throw new JsonException(fullMethodName + ex.Message);
            }

            return ret;
        }
    }
}
