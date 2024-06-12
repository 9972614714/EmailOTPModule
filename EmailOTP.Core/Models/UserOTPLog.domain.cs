
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailOTP.Domain.Models
{
    public class UserOTPLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string OTP { get; set; }

        [Required]
        public DateTime ExpirationTime { get; set; }

        public int Attempts { get; set; }

    }
}
