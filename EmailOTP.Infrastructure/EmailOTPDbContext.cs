
using EmailOTP.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailOTP.Infrastructure
{
    public class EmailOTPDbContext : DbContext
    {
        public EmailOTPDbContext(DbContextOptions<EmailOTPDbContext> options) : base(options)
        {

            

        }

       public DbSet<UserOTPLog> UserOTPLogs { get; set; } 

    }
}
