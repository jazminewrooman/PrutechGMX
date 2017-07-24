 using System;
using System.Collections.Generic;
using System.Text;

namespace GMX
{
    public class LoginModel
    {
    }

    public class LoginUsers
    {
        public string CryptPwd { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public System.DateTime ExpDate { get; set; }
        public int ExtId { get; set; }
        public int ParentId { get; set; }
        public string[] Roles { get; set; }
        public string Source { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

    }
}
