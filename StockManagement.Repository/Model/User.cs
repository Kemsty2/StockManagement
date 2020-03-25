using System;
using System.Collections.Generic;
using System.Text;

namespace StockManagement.Repository.Model
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AccessLevel { get; set; }
    }
}
