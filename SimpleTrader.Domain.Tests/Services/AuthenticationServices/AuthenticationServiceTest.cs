using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.Domain.Exceptions;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTest
    {
        private AuthenticationService _authenticationService;
        private Mock<IPasswordHasher> _mockPasswordHasher;
        private Mock<IAccountService> _mockAccountService;

        [SetUp]
        public void Setup()
        {
            _mockPasswordHasher = new Mock<IPasswordHasher>(); 
            _mockAccountService = new Mock<IAccountService>();
            _authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);
        }


        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsAccountForCorrectUsername()
        {
            //Arrange
            string expectedUsername = "testUser";
            string password = "password";

            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });

            _mockPasswordHasher.Setup(s =>s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);

            _authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);

            //Act
            Account acc = await _authenticationService.Login(expectedUsername, password);

            //Assert
            string actualUsername = acc.AccountHolder.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithInorrectPasswordForExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            //Arrange
            string expectedUsername = "testUser";
            string password = "password";

            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });

            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            AuthenticationService authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);

            //Act
            InvalidPasswordException exception =Assert.ThrowsAsync<InvalidPasswordException>(() => _authenticationService.Login(expectedUsername, password));

            //Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            //Arrange
            string expectedUsername = "testUser";
            string password = "password";

            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            AuthenticationService authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);

            //Act
            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(() => _authenticationService.Login(expectedUsername, password));

            //Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Register_WithPasswordsNotMatching_ReturnsPasswordsDoNotMatch()
        {
            string password = "password";
            string confirmPassword = "cofrimPassword";

            RegistrationResult expectedResult = RegistrationResult.PasswordsDoNotMatch;

            RegistrationResult actualResult =await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Register_WithAlreadyExistingEmail_ReturnsEmailAlreadyExists()
        {
            string email = "test@gmail.com";
            _mockAccountService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new Account());

            RegistrationResult expectedResult = RegistrationResult.EmailAlreadyExists;

            RegistrationResult actualResult = await _authenticationService.Register(email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Register_WithAlreadyExistingUsername_ReturnsEmailAlreadyExists()
        {
            string username = "testUsername";
            _mockAccountService.Setup(s => s.GetByUsername(username)).ReturnsAsync(new Account());

            RegistrationResult expectedResult = RegistrationResult.UsernameAlreadyExists;

            RegistrationResult actualResult = await _authenticationService.Register(It.IsAny<string>(), username, It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Register_WithNonExistingUsernameAndMatchingPasswords_ReturnsSuccess()
        {
            RegistrationResult expectedResult = RegistrationResult.Success;

            RegistrationResult actualResult = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
