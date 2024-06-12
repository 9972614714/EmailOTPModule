using System.ComponentModel.DataAnnotations;

namespace EmailOTPModule.Models
{
    public class ValidateOtpRequest
    {
        [Required]
        public string Otp { get; set; }
    }
}
