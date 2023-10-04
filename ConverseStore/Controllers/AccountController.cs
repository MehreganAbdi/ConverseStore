using Context.Data;
using Context.Models;
using ConverseStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace ConverseStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly StoreDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager,
                                    SignInManager<AppUser> signInManager,
                                    StoreDbContext applicationDBContext)
        {
            _context = applicationDBContext;
            _signInManager = signInManager;
            _userManager = userManager;

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user == null)
            {
                TempData["Error"] = "This User Doesn't Exist , Try To Register First";
                return RedirectToAction("Register", "Account");
            }
            var passCheck = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false , false);
            if (passCheck.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            TempData["Error"] = "Wrong Password ";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var reloadSafety = new RegisterVM();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            var existanse = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if(existanse != null)
            {
                TempData["Error"] = "This Email Alredy Exists";
            }
            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.UserName.Replace(" ", ""),
                Address = new Address()
                {
                    City = registerVM.City,
                    Street = registerVM.Street == null ? "empty" : registerVM.Street,
                    AddressDetails = registerVM.AddressDetails == null ? "empty" : registerVM.AddressDetails,
                    PostalCode = registerVM.PostalCode == null ? "empty" : registerVM.PostalCode
                }
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                SendEmail(newUser.Email, "Registration", "Registration Completed! \n Ckeck Out Our New Sneakers On Our Web . \n");
                return RedirectToAction("Index", "Home");
            }
            return View(registerVM);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("mehreganabdiwebmail@gmail.com");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "kxbo ipin pkyn vgfo";
                    var sub = subject;
                    var body = message + $"\n{DateTime.Now}\nThanks For Registration , Hope You Enjoy Our WebSite ,\n Wish You Luck :)";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }
    }
}
