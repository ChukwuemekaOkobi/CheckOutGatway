using BankSimulator.Service;

namespace Test.BankSimulator
{
    public class ProcessOTPTest
    {
        [Fact]
        public void OtpValid()
        {

            var reference = new Guid("91125186-c214-4df4-a4c0-bf8ed8a029d2");
            var otp = "08352";

            var result = BankService.ProcessOtp(reference, otp);

            Assert.True(result.Item1);
        }


        [Fact]
        public void OtpInValid()
        {

            var reference = new Guid("91125186-c214-4df4-a4c0-bf8ed8a029d2");
            var otp = "9860244";

            var result = BankService.ProcessOtp(reference, otp);

            Assert.False(result.Item1);
        }

        [Fact]
        public void OtpInBankReference()
        {

            var reference = new Guid("91195186-c214-4df4-abc1-bf8e08a029d2");
            var otp = "9860244";

            var result = BankService.ProcessOtp(reference, otp);

            Assert.False(result.Item1);
        }


    }
}
