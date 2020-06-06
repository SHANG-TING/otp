using System;
using NUnit.Framework;
using OtpLib;

namespace OtpTests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        [Test]
        public void IsValidTest()
        {
            var target = new AuthenticationService(new StubProfile(), new StubToken());

            var actual = target.IsValid("joey", "91000000");
            Assert.True(actual);
        }
    }

    public class StubProfile : IProfile
    {
        public string GetPassword(string account)
        {
            return account == "joey" ? "91" : string.Empty;
        }
    }

    public class StubToken : IToken
    {
        public string GetRandom(string account)
        {
            return "000000";
        }
    }
}