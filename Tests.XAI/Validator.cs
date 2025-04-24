using Apps.XAI.Connections;
using Blackbird.Applications.Sdk.Common.Authentication;
using Tests.XAI.Base;

namespace Tests.XAI
{
    [TestClass]
    public class Validator : TestBase
    {
        [TestMethod]
        public async Task ValidatesCorrectConnection()
        {
            var validator = new ConnectionValidator();

            var result = await validator.ValidateConnection(Creds, CancellationToken.None);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public async Task DoesNotValidateIncorrectConnection()
        {
            var newCreds = Creds.Select(x => new AuthenticationCredentialsProvider(AuthenticationCredentialsRequestLocation.None, x.KeyName + "_incorrect", x.Value + "_incorrect"));
            var validator = new ConnectionValidator();
             var result = await validator.ValidateConnection(newCreds, CancellationToken.None);
            Assert.IsFalse(result.IsValid);
        }
    }
}