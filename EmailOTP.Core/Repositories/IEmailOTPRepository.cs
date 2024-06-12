using EmailOTP.Domain.Models;

namespace EmailOTP.Domain.Repositories
{
    public interface IEmailOTPRepository
    {
        /// <summary>
        /// Retrieve OTP info for the given email.
        /// </summary>
        /// <param name="email">Holds the email address.</param>
        /// <returns>Returns OTP info for the given email.</returns>
        Task<UserOTPLog> GetOTPInfoByEmailAsync(string email);

        /// <summary>
        /// Remove all the OTP logs for the provided email id.
        /// </summary>
        /// <param name="email"> Holds the email address.</param>
        /// <returns></returns>
        Task RemoveUserOTPsAsync(string email);

        /// <summary>
        /// Save OTP details.
        /// </summary>
        /// <param name="userOTPLog">OTP info for the given user.</param>
        /// <returns></returns>
        Task SaveUserOTPAsync(UserOTPLog userOTPLog);

        Task UpdateUserOTPAsync(UserOTPLog userOTPLog);
    }

}
