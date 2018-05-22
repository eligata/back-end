using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.API.Data.Services;
using App.API.Models;
using App.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        #region Fields and Properties

        public AccountService AccountService { get; }

        #endregion

        public AccountController(AccountService accountService)
        {
            AccountService = accountService;
        }


        // POST api/account/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(Message.INVLID_DATA);

            var user = await AccountService.GetUserByEmail(model.Email);

            if (user == null)
                return BadRequest(Message.USER_DOESNT_EXIST);

            var signInResult = await AccountService.Login(user, model.Password);

            if (signInResult.Succeeded)
            {
                string jwToken = AccountService.GetUserToken(user);
                return Ok(jwToken);
            }
            else
                return BadRequest(Message.LOGIN_FAIL);
        }

        // POST api/account/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(Message.INVLID_DATA);

            var registrationResult = await AccountService.Register(model);

            return Ok(new
            {
                succeded = registrationResult.Succeeded,
                message = registrationResult.Succeeded ? Message.REGISTRATION_SUCCESS : Message.REGISTRATION_FAIL
            });
        }
    }
}