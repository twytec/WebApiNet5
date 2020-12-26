using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNet5.ViewModels
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public DateTime TokenExpire { get; set; }
    }
}
