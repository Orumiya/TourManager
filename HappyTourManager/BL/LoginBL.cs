using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DATA;
using DATA.Interfaces;
using DATA.Repositoriees;

namespace BL
{
    public class LoginBL
    {
        private readonly IRepository<User> userRepository;
        private User whichUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginBL"/> class.
        /// </summary>
        /// <param name="userRepository">input param</param>
        public LoginBL(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

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

        public bool Login(string username, string password)
        {
            if (this.IsTheUserExists(username))
            {
                if (this.PasswordEncoder(password).Equals(this.whichUser.Password))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsTheUserExists(string username)
        {
            var query = this.userRepository.GetAll();
            bool exist = false;

            try
            {
                this.whichUser = query.Single(e => e.Username.Equals(username));
            }
            catch (Exception e)
            {
                exist = false;
                return exist;
            }

            return exist = true;
        }
    }
}
