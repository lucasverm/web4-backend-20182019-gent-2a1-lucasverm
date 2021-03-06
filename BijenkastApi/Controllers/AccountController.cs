﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using BijenkastApi.Models;
using BijenkastApi.DTOs;
using System.Diagnostics;
using System.Net.Http;

namespace RecipeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IImkerRepository _imkerRepository;
        private readonly IConfiguration _config;

        public AccountController(
          SignInManager<IdentityUser> signInManager,
          UserManager<IdentityUser> userManager,
          IImkerRepository imkerRepository,
          IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _imkerRepository = imkerRepository;
            _config = config;
        }

        private async Task<FacebookImkerDTO> VerifyFacebookAccessToken(string accessToken)
        {
            FacebookImkerDTO fbUser = null;
            var path = "https://graph.facebook.com/me?fields=email,first_name,last_name&access_token=" + accessToken;
            var client = new HttpClient();
            var uri = new Uri(path);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                fbUser = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookImkerDTO>(content);
                return fbUser;
            }
            return null;

           
        }

        [HttpPost("loginfacebook")]
        [AllowAnonymous]
        public async Task<string> GetTokenFromFacebook(FacebookTokenDTO facebookTokenDTO)
        {

            var facebookUser = await VerifyFacebookAccessToken(facebookTokenDTO.token);
            var systemUser = await _userManager.FindByNameAsync(facebookUser.email);
            if (systemUser != null)
            {
                string uitvoerToken = GetToken(systemUser);
                return uitvoerToken;
            }
            else
            {
                IdentityUser user = new IdentityUser { UserName = facebookUser.email, Email = facebookUser.email };
                Imker imker = new Imker { email = facebookUser.email, voornaam = facebookUser.voornaam, achternaam = facebookUser.achternaam, facebookimker = true };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    _imkerRepository.Add(imker);
                    _imkerRepository.SaveChanges();
                    string uitvoerToken = GetToken(user);
                    return uitvoerToken;
                }

            }
            return null;
           
        }

        [AllowAnonymous]
        [HttpGet("checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return user == null;
        }

        [HttpGet("checkusernamevoorwijzigen")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserNameForChange(string email)
        {
            var uitvoer = false;
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
            {
                uitvoer = true;
            }
            else
            {
                var aangemeldeGebruiker = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == aangemeldeGebruiker)
                {
                    uitvoer = true;
                }
            }

            return uitvoer;
        }

        [HttpGet("geefimker")]
        public ActionResult<Imker> geefImker()
        {
            Imker imker = _imkerRepository.GetBy(User.Identity.Name);
            if (imker == null)
            {
                return Unauthorized();
            }
            return imker;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model">the login details</param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<String>> CreateToken(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    string token = GetToken(user);
                    return Created("", token); //returns only the token
                }
            }
            return BadRequest("De inlog gegevens zijn niet correct!");
        }

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="model">the user details</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<String>> Register(RegisterDTO model)
        {
            IdentityUser user = new IdentityUser { UserName = model.Email, Email = model.Email };
            Imker imker = new Imker { email = model.Email, voornaam = model.FirstName, achternaam = model.LastName, facebookimker = false };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _imkerRepository.Add(imker);
                _imkerRepository.SaveChanges();
                string token = GetToken(user);
                return Created("", token);
            }
            return BadRequest();
        }

        [HttpPost("wijzigwachtwoord")]
        public async Task<ActionResult<Boolean>> changePassword(WachtwoordDTO wachtwoorden)
        { 
            if(wachtwoorden.wachtwoord != wachtwoorden.wachtwoordbevestiging)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, wachtwoorden.wachtwoord);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        private String GetToken(IdentityUser user)
        {
            // Create the token
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.Email),
              new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              null, null,
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPut]
        public async Task<ActionResult<Imker>> PutImkerAsync(ImkerDTO imkerDTO)
        {
            Imker imker = _imkerRepository.GetBy(User.Identity.Name);
            if (imker == null) { return Unauthorized(); };
            imker.achternaam = imkerDTO.achternaam;
            imker.voornaam = imkerDTO.voornaam;
            var user = await _userManager.FindByEmailAsync(imker.email);
            user.UserName = imkerDTO.email;
            user.Email = imkerDTO.email;
            await _userManager.UpdateAsync(user);
            imker.email = imkerDTO.email;
            _imkerRepository.Update(imker);
            _imkerRepository.SaveChanges();
            return imker;
        }
    }
}