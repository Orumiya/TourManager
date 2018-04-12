using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DATA.Repositoriees;

namespace BL
{
    public class LoginBL
    {
        /// <summary>
        /// Encrypts the password
        /// </summary>
        /// <param name="rawPassword">raw password</param>
        /// <returns>encrypted password</returns>
        public string PasswordEncoder(string rawPassword)
        {
            byte[] raw = Encoding.Unicode.GetBytes(rawPassword);
            byte[] hashResult = HashAlgorithm.Create("SHA256").ComputeHash(raw);
            string password = Convert.ToBase64String(hashResult);
            return password;
        }

        public void Login(string username, string password)
        {

        }

        private bool IsTheUserExists(string username)
        {
            UserRepository userrepo = new UserRepository();
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
