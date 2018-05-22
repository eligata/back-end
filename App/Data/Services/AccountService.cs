using App.API.Helpers;
using App.API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Data.Services
{
    public class AccountService
    {
        #region Fields and Properties

        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }
        public TokenManager TokenManager { get; }

        #endregion

        #region CTOR

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, TokenManager tokenManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.TokenManager = tokenManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Find user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<IdentityUser> GetUserByEmail(string email)
        {
            return await UserManager.FindByNameAsync(email);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<SignInResult> Login(IdentityUser user, string password)
        {
            return await SignInManager.PasswordSignInAsync(user, password, false, false);
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var appUser = new IdentityUser { UserName = model.Email, Email = model.Email };
            return await UserManager.CreateAsync(appUser, model.Password);
        }

        /// <summary>
        /// Generate user token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetUserToken(IdentityUser user)
        {
            return TokenManager.GetUserToken(user);
        }

        #endregion
    }
}
