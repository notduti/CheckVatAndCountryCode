using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILibDutto5AOttobre23
{
    public class Header
    {
        [FromHeader]
        public string? DevName { get; set; }
        [FromHeader]
        public string? Team { get; set; }

    }
}
