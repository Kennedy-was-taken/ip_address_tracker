using ip_address_tracker.Interfaces;
using ip_address_tracker.Services;
using System.Diagnostics;

namespace TestAPI.Services
{
    [TestCaseOrderer(
    ordererTypeName: "TestAPI.PriorityOrderer",
    ordererAssemblyName: "TestAPI")]
    public class ValidateTest
    {
        private readonly ValidateService validate;

        public ValidateTest()
        {
            validate = new ValidateService();
        }

        [Fact]
        [TestPriority(1)]
        public void PassedIPAddressTrue()
        {
            bool containsIP = validate.isIPAddress("8.8.8.8");

            if (containsIP)
            {
                Debug.WriteLine("Is an IP Address");
                Assert.True(containsIP);
            }

            else
            {
                Debug.WriteLine("Isn't an IP Address");
                Assert.Fail();
            }

        }

        [Fact]
        [TestPriority(2)]
        public void PassedIPAddressFalse()
        {
            bool containsIP = validate.isIPAddress("8.dfg.8.ght");

            if (containsIP)
            {
                Debug.WriteLine("Is an IP Address");
                Assert.Fail();
            }

            else
            {
                Debug.WriteLine("Isn't an IP Address");
                Assert.False(containsIP);
            }

        }

        [Fact]
        [TestPriority(3)]
        public void PassedDomainTrue()
        {
            bool containsDomainName = validate.isIPAddress("google.com");

            if (!containsDomainName)
            {
                Debug.WriteLine("Is a Domain Name");
                Assert.True(true);
            }

            else
            {
                Debug.WriteLine("Isn't a Domain Name");
                Assert.Fail();
            }

        }

        [Fact]
        [TestPriority(4)]
        public void PassedDomaninfalse()
        {
            bool containsDomainName = validate.isIPAddress("8.8.8.8");

            if (containsDomainName)
            {
                Debug.WriteLine("Isn't a Domain Name");
                Assert.False(!containsDomainName);
            }

            else
            {
                Debug.WriteLine("Is a Domain Name");
                Assert.Fail();
            }

        }
    }
}
