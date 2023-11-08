using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILibDutto5AOttobre23
{
    public class ToJsonHttpResponse
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public ToJsonHttpResponse(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
