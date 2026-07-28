using CustomerAuthService.BLL;
using CustomerAuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CustomerAuthService.API.Controllers.LoginController;

namespace CustomerAuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly JwtService _jwtService;

        public CustomersController(ICustomerService service, JwtService jwtService)
        {
            _service = service;
            _jwtService = jwtService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
       => Ok(await _service.GetCustomers());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var customer = await _service.GetCustomer(id);
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(Customer customer)
        {
            await _service.CreateCustomer(customer);
            return Ok(customer);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            await _service.UpdateCustomer(customer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteCustomer(id);
            return Ok();
        }

       
        [HttpPut("{customerId}/role")]
        public async Task<IActionResult> UpdateRole( int customerId, [FromBody] int roleId)
        {
            var result = await _service.UpdateRoleAsync(customerId, roleId);

            if (!result)
                return NotFound();

            return Ok(new
            {
                Message = "Role updated successfully."
            });
        }


        [HttpPut("test")]
        public IActionResult Test()
        {
            return Ok("PUT working");
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(Models.RefreshRequest request)
        {
            {
                Customer refuser = _service.GetUserByRefreshToken(request);

                if (refuser == null)
                    return Unauthorized("Invalid Refresh Token");

                //if (user.RefreshTokenExpiry < DateTime.UtcNow)
                //    return Unauthorized("Refresh Token Expired");

                var newAccessToken = _jwtService.GenerateToken(refuser);

                return Ok(new
                {
                    AccessToken = newAccessToken
                });
            }
        }


        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count =  _service.GetCustomers().Result.ToList().Count();
            return Ok(count);
        }
    }

}