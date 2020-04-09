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
            var target = new AuthenticationService();

            var actual = target.IsValid("joey", "91000000");
            Assert.True(actual);
        }
    }
}