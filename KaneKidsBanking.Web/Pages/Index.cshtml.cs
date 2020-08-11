using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KaneKidsBanking.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }
        public IConfiguration Configuration { get; }

        public class SiteUser
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPost()
        {
            
            var users = Configuration.GetSection("SiteUsers").Get<SiteUser[]>();

            if (users.Where(c => c.UserName.ToUpper() == UserName.ToUpper() && c.Password == Password).Count() == 1)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserName)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToPage("/Account/Index");           
            }
            
            Message = "Invalid attempt";
            return Page();
        }
    }
}
