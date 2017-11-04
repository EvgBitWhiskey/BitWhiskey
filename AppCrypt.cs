using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitWhiskey
{
    class AppCrypt
    {
        public static string EncryptData(string d)
        {            
            var data = Encoding.UTF8.GetBytes(d);
            var encrypted_data =  ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted_data);
        }

        public static string DecryptData(string encrypted_d)
        {
            var encrypted_data = Convert.FromBase64String(encrypted_d);
            var data = ProtectedData.Unprotect(encrypted_data, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(data);
        }

    }

}
