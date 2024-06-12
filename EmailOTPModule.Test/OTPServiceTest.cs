using EmailOTP.Domain.Enums;
using EmailOTP.Domain.Models;
using EmailOTP.Domain.Repositories;
using EmailOTP.Domain.Service;
using EmailOTP.Service;
using Moq;

namespace EmailOTPModule.Test
{
    public class OTPServiceTest
    {
        private readonly Mock<IEmailOTPRepository> _emailOTPRepoMock;
        private readonly OTPService _otpService;
        private readonly Mock<IEmailService> _emailServiceMock;



        public OTPServiceTest()
        {
            _emailOTPRepoMock = new Mock<IEmailOTPRepository>();
            _emailServiceMock = new Mock<IEmailService>();

            _otpService = new OTPService(_emailServiceMock.Object,_emailOTPRepoMock.Object);
        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GenerateOTPAsync_ShouldReturnEmailOk_WhenEmailIsValid()
        {
            // Arrange
            var email = "arun.atsev@dso.org.sg";


            _emailServiceMock.Setup(repo => repo.SendEmailAsync(email, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);


            // Act
            var result = await _otpService.GenerateOTPAsync(email);


            // Assert
            Assert.That(result,Is.EqualTo(EmailStatus.STATUS_EMAIL_OK));
            _emailOTPRepoMock.Verify(repo => repo.SaveUserOTPAsync(It.IsAny<UserOTPLog>()), Times.Once);

        }

        [Test]
        public async Task GenerateOTPAsync_ShouldReturnEmailInValid_WhenEmailIsInValid()
        {
            // Arrange
            var email = "arun.atsev@x.org.sg";


            _emailServiceMock.Setup(repo => repo.SendEmailAsync(email, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);


            // Act
            var result = await _otpService.GenerateOTPAsync(email);


            // Assert
            Assert.That(result, Is.EqualTo(EmailStatus.STATUS_EMAIL_INVALID));
            _emailOTPRepoMock.Verify(repo => repo.SaveUserOTPAsync(It.IsAny<UserOTPLog>()), Times.Never);

        }

        [Test]
        public async Task GenerateOTPAsync_ShouldReturnEmailFail_WhenEmailIsNotExist()
        {
            // Arrange
            var email = "usernotexist@dso.org.sg";


            _emailServiceMock.Setup(repo => repo.SendEmailAsync(email, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);


            // Act
            var result = await _otpService.GenerateOTPAsync(email);


            // Assert
            Assert.That(result, Is.EqualTo(EmailStatus.STATUS_EMAIL_FAIL));
            _emailOTPRepoMock.Verify(repo => repo.SaveUserOTPAsync(It.IsAny<UserOTPLog>()), Times.Never);

        }

        [Test]
        public async Task CheckOtpAsync_ShouldReturnOtpOk_WhenOtpIsValid()
        {
            // Arrange
            var email = "arun.atsev@dso.org.sg";
            var validOtp = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("123456"));

            var otpEntry = new UserOTPLog
            {
                Email = email,
                OTP = "123456",
                ExpirationTime = DateTime.UtcNow.AddMinutes(1),
                Attempts = 0
            };

            _emailOTPRepoMock.Setup(repo => repo.GetOTPInfoByEmailAsync(email)).ReturnsAsync(otpEntry);
            _emailOTPRepoMock.Setup(repo => repo.UpdateUserOTPAsync(It.IsAny<UserOTPLog>())).Returns(Task.CompletedTask);

            // Act
            var result = await _otpService.CheckOtpAsync(email, validOtp);

            // Assert
            Assert.That(result,Is.EqualTo(OTPStatus.STATUS_OTP_OK));
            _emailOTPRepoMock.Verify(repo => repo.UpdateUserOTPAsync(It.IsAny<UserOTPLog>()), Times.AtLeastOnce);
        }

        [Test]
        public async Task CheckOtpAsync_ShouldReturnOtpFail_WhenOtpIsInValid()
        {
            // Arrange
            var email = "arun.atsev@dso.org.sg";
            var validOtp = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("123456"));

            var otpEntry = new UserOTPLog
            {
                Email = email,
                OTP = "123457",
                ExpirationTime = DateTime.UtcNow.AddMinutes(1),
                Attempts = 0
            };

            _emailOTPRepoMock.Setup(repo => repo.GetOTPInfoByEmailAsync(email)).ReturnsAsync(otpEntry);
            _emailOTPRepoMock.Setup(repo => repo.UpdateUserOTPAsync(It.IsAny<UserOTPLog>())).Returns(Task.CompletedTask);

            // Act
            var result = await _otpService.CheckOtpAsync(email, validOtp);

            // Assert
            Assert.That(result, Is.EqualTo(OTPStatus.STATUS_OTP_FAIL));
            _emailOTPRepoMock.Verify(repo => repo.UpdateUserOTPAsync(It.IsAny<UserOTPLog>()), Times.AtLeastOnce);
        }

        [Test]
        public async Task CheckOtpAsync_ShouldReturnOtpTimeOut_WhenOtpIsExpired()
        {
            // Arrange
            var email = "arun.atsev@dso.org.sg";
            var validOtp = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("123456"));

            var otpEntry = new UserOTPLog
            {
                Email = email,
                OTP = "123456",
                ExpirationTime = DateTime.UtcNow.AddMinutes(-1),
                Attempts = 0
            };

            _emailOTPRepoMock.Setup(repo => repo.GetOTPInfoByEmailAsync(email)).ReturnsAsync(otpEntry);
            _emailOTPRepoMock.Setup(repo => repo.UpdateUserOTPAsync(It.IsAny<UserOTPLog>())).Returns(Task.CompletedTask);

            // Act
            var result = await _otpService.CheckOtpAsync(email, validOtp);

            // Assert
            Assert.That(result, Is.EqualTo(OTPStatus.STATUS_OTP_TIMEOUT));
        }

    }
}