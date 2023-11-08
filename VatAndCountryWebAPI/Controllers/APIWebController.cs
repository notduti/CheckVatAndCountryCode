using Microsoft.AspNetCore.Mvc;
using APILibDutto5AOttobre23;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIWebDutto5AOttobre23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIWebController : ControllerBase
    {
        private readonly ILogger<APIWebController> _logger;

        public APIWebController(ILogger<APIWebController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "VerificaDutto5AOttobre2023")]
        public async Task<string> CheckEuVatRestDutto5AOttobre23([FromHeader] Header header, [FromBody] Customer customer)
        {
            string fullMethodName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + ": ";
            string ret = "";
            try
            {
                string restResponse = await CheckVat.CheckEuVatDutto5AOttobre23(customer);
                string soapResponse = await GetISOCode.GetISOCodeDutto5AOttobre23(customer);

                ret = await FinalResponse.SerializeFinalResponse(restResponse, soapResponse, header);
            }
            catch(Exception ex) 
            {
                _logger.LogError(fullMethodName + ex.Message);
            }

            return ret;
        }
    }
}