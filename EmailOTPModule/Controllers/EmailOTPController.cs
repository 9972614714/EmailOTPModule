using EmailOTP.Domain.Service;
using EmailOTPModule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace EmailOTPModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailOTPController : ControllerBase
    {
        private readonly IOTPService _otpService;

        public EmailOTPController(IOTPService otpService)
        {
            _otpService = otpService; 
        }

        [HttpPost("generate-otp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GenerateOTP([FromBody] string email)
        {

            try
            {
                var status = await _otpService.GenerateOTPAsync(email);

                return Ok(status.GetDisplayName());
            }
            catch (Exception)
            {
                throw;
            }
            

        }


        [HttpPost("check-otp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CheckOtp([FromQuery] string email, [FromBody] ValidateOtpRequest otpRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Input is not valid");
                }
                using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(otpRequest.Otp)))
                {
                    var status = await _otpService.CheckOtpAsync(email, stream);
                    return Ok(status.GetDisplayName());
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
