using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HappyTourManager
{
    class LoginViewModel : Bindable
    {
        public string Username { get; set; }

        public SecureString Password { get; set; }

        



    }
}
