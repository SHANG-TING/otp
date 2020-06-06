using System;
using NUnit.Framework;
using NSubstitute;
using OtpLib;

namespace OtpTests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private IProfile _profile;
        private IToken _token;
        private ILogger _logger;
        private AuthenticationService _target;

        [SetUp]
        public void SetUp()
        {
            _profile = Substitute.For<IProfile>();
            _token = Substitute.For<IToken>();
            _logger = Substitute.For<ILogger>();
            _target = new AuthenticationService(_profile, _token, _logger);
        }

        [Test]
        public void IsValidTest()
        {
            GivenPorfile("joey", "91");
            GivenToken("000000");
            ShouldBeValid("joey", "91000000");
        }

        [Test]
        public void IsInValidTest()
        {
            GivenPorfile("joey", "91");
            GivenToken("000000");
            ShouldBeInValid("joey", "Wrong Password!");
        }

        [Test]
        public void should_not_log_when_valid()
        {
            GivenPorfile("joey", "91");
            GivenToken("000000");
            WhenValid();
            ShouldNotLog();
        }

        [Test]
        public void should_log_when_invalid()
        {
            GivenPorfile("joey", "91");
            GivenToken("000000");
            WhenInValid();
            ShouldLogWith();
        }

        private void ShouldLogWith()
        {
            _logger.Received(1).Save(
                Arg.Is<string>(m => m.Contains("joey") && m.Contains("faild"))
            );
        }

        private void ShouldNotLog()
        {
            _logger.DidNotReceiveWithAnyArgs().Save(Arg.Any<string>());
        }

        private void WhenValid()
        {
            _target.IsValid("joey", "91000000");
        }

        private void WhenInValid()
        {
            _target.IsValid("joey", "Wrong Password!");
        }

        private void ShouldBeInValid(string account, string password)
        {
            var actual = _target.IsValid(account, password);
            Assert.IsFalse(actual);
        }


        private void ShouldBeValid(string account, string password)
        {
            var actual = _target.IsValid(account, password);
            Assert.IsTrue(actual);
        }

        private void GivenToken(string token)
        {
            _token.GetRandom("").ReturnsForAnyArgs(token);
        }

        private void GivenPorfile(string account, string password)
        {
            _profile.GetPassword(account).Returns(password);
        }
    }
}