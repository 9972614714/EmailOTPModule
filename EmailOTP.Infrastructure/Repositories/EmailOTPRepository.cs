
using EmailOTP.Domain.Models;
using EmailOTP.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmailOTP.Infrastructure.Repositories
{
    public class EmailOTPRepository : IEmailOTPRepository
    {
        private readonly EmailOTPDbContext _emailOTPContext;

        #region Public_Methods
        public EmailOTPRepository(EmailOTPDbContext emailOTPDbContext)
        {
            _emailOTPContext = emailOTPDbContext;

        }

        public async Task SaveUserOTPAsync(UserOTPLog userOTPLog)
        {
            try
            {
                await RemoveUserOTPsAsync(userOTPLog.Email);

                _emailOTPContext.Add(userOTPLog);
                await _emailOTPContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task UpdateUserOTPAsync(UserOTPLog userOTPLog)
        {
            try
            {
                _emailOTPContext.Update(userOTPLog);
                await _emailOTPContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserOTPLog> GetOTPInfoByEmailAsync(string email)
        {
            try
            {
                return await _emailOTPContext.UserOTPLogs.FirstOrDefaultAsync(e => e.Email.Trim() == email.Trim());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task RemoveUserOTPsAsync(string email)
        {
            try
            {
                var userOTPs = _emailOTPContext.UserOTPLogs.Where(e => e.Email.Trim() == email.Trim());
                if (userOTPs.Any())
                {
                    _emailOTPContext.RemoveRange(userOTPs);
                }
                await _emailOTPContext.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        #endregion

    }
}
