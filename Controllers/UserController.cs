using Microsoft.AspNetCore.Mvc;
using AirBnB_for_Campers___TAKE_HOME_EXAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AirBnB_for_Campers___TAKE_HOME_EXAM.Controllers
{

    public class ResetCodeStorage
    {
        public static Dictionary<string, string> EmailToResetCodeMap { get; } = new Dictionary<string, string>();
    }

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IDataContext _data;
        private Dictionary<string, string> _emailToSecurityCodeMap = new Dictionary<string, string>();
            private Dictionary<string, string> _emailToResetCodeMap; // Add a dictionary to store reset codes



        public UserController(IDataContext data)
        {
            _data = data;
        }

        [HttpGet]

        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_data.getUsers());
        }

        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            // Hash the password before storing
            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.HashedPassword);
            // Store user in the database
            _data.addUser(user);
            return Ok("User has been added!");
        }



        [HttpGet("{id}")]

        public ActionResult<User> Get(int id)
        {
            return Ok(_data.getUserById(id));

        }
        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] UserLoginRequest request)
        {
            var user = _data.getUsers().FirstOrDefault(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword))
            {
                return NotFound("Invalid email or password");
            }
            return Ok(user);
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var userToDelete = _data.getUserById(id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            _data.deleteUser(id);
            return Ok("User has been deleted!");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var existingUser = _data.getUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Update user properties
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.UserName = updatedUser.UserName;
            existingUser.Email = updatedUser.Email;
            existingUser.Roles = updatedUser.Roles;




            // Save changes to data store
            _data.updateUser(existingUser);

            return Ok("User has been updated!");
        }

        [HttpPut("{id}/password")]
        public ActionResult UpdatePassword(int id, [FromBody] string newPassword)
        {
            var existingUser = _data.getUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Hash the new password before updating
            existingUser.HashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            // Save changes to data store
            _data.updateUser(existingUser);

            return Ok("Password has been updated!");
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = _data.getUsers().FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Generate and associate a security code with the user's email
            var securityCode = GenerateSecurityCode();
            AssociateSecurityCodeWithEmail(request.Email, securityCode);
            ResetCodeStorage.EmailToResetCodeMap[request.Email] = securityCode;


            // Send the security code via email
            var emailModel = new Email
            {
                To = request.Email,
                Subject = "Reset Password",
                Body = $"Your security code is: {securityCode}"
            };

            // Make an HTTP POST request to the EmailController to send the email
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://sunwoodsapi.azurewebsites.net/api/Email/send");
                var response = await client.PostAsJsonAsync("send", emailModel);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Failed to send email");
                }
            }
           // _emailToResetCodeMap[request.Email] = securityCode;

            return Ok("Reset password email sent successfully");
        }



        // Backend C# (ASP.NET Core)
        [HttpPost("resetpassword/verify")]
        public IActionResult VerifySecurityCode([FromBody] VerifySecurityCodeRequest request)
        {
            try
            {
                // Log the request parameters
                Console.WriteLine($"Received verification request for email: {request.Email}, code: {request.Code}");

                // Check if the email or code is null or empty
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Code))
                {
                    return BadRequest("Email and code must be provided");
                }

                // Retrieve the email and code from the dictionary
                if (ResetCodeStorage.EmailToResetCodeMap.TryGetValue(request.Email, out string code) && code == request.Code)
                {
                    // Return success message if the code matches
                    return Ok("Code is correct!");
                }
                else
                {
                    return BadRequest("Invalid security code!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error verifying security code: {ex.Message}");
            }
        }


        [HttpPost("resetpassword/update")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            var user = _data.getUsers().FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Update the user's password
            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _data.updateUser(user);

            return Ok("Password updated successfully");
        }



        private string GenerateSecurityCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void AssociateSecurityCodeWithEmail(string email, string securityCode)
        {
            _emailToSecurityCodeMap[email] = securityCode;
        }

     



    }
}
