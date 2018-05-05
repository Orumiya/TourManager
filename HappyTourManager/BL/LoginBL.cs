// <copyright file="LoginBL.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using DATA;
    using DATA.Interfaces;

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
        /// decides whether the user with the correct password exists
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">string password</param>
        /// <returns>true, if user can be logged in
        /// false, if not</returns>
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

        /// <summary>
        /// decides whether the user with the password can be registered,
        /// if yes, the user is saved
        /// </summary>
        /// /// <param name="username">username</param>
        /// <param name="password">string password</param>
        /// <returns>returns true, if the user is saved,
        /// throws exceptions, if the user can't be created</returns>
        public bool RegisterUser(string username, string password)
        {
            bool alreadyExist = this.IsTheUserExists(username);
            try
            {
                if (!alreadyExist)
                {
                    if (this.UsernamePasswordConstraints(username) && this.UsernamePasswordConstraints(password))
                    {
                        string encriptedPassword = this.PasswordEncoder(password);
                        this.userRepository.Create(new User { Username = username, Password = encriptedPassword });
                        return true;
                    }
                }
                else
                {
                    throw new InvalidOperationException("user already exists");
                }
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(e.Message);
            }

            return false;
        }

        /// <summary>
        /// Encrypts the password with SHA256
        /// </summary>
        /// <param name="rawPassword">raw password</param>
        /// <returns>encrypted password</returns>
        private string PasswordEncoder(string rawPassword)
        {
            byte[] raw = Encoding.Unicode.GetBytes(rawPassword);
            byte[] hashResult = HashAlgorithm.Create("SHA256").ComputeHash(raw);
            string password = Convert.ToBase64String(hashResult);
            return password;
        }

        /// <summary>
        /// checks if the username/password contain atleast 5 chars and less than 15 chars,
        /// must contain only numbers and letters, and cannot contain special characters
        /// </summary>
        /// <param name="userpass">desired username or password</param>
        /// <returns>returns true, if the username/password is valid</returns>
        private bool UsernamePasswordConstraints(string userpass)
        {
            if (userpass.Length >= 5 && userpass.Length < 15)
            {
                if (userpass.Any(c => char.IsLetterOrDigit(c)) && !userpass.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    return true;
                }
                else
                {
                    throw new InvalidOperationException("username/password must contain only numbers and letters, and cannot contain special characters");
                }
            }
            else
            {
                throw new InvalidOperationException("username/password must be atleast 5 characters and less than 15 characters");
            }
        }

        /// <summary>
        /// returns true, if the username has been found
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>returns false, if the username not exists</returns>
        private bool IsTheUserExists(string username)
        {
            var query = this.userRepository.GetAll();

            try
            {
                this.whichUser = query.Single(e => e.Username.Equals(username));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
