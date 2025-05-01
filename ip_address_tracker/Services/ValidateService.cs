

using ip_address_tracker.Interfaces;

namespace ip_address_tracker.Services
{
    public class ValidateService : IValidateInterface
    {

        public bool isIPAddress(string ipAddress)
        {
            string[] parts = ipAddress.Split('.');

            if (parts.Length != 4)
            {
                return false;
            }

            // Checks if the octat is numerical and is in range
            foreach (string part in parts)
            {
                if (!int.TryParse(part, out int num))
                {
                    return false;
                }

                if (num < 0 || num > 255)
                {
                    return false;
                }
            }

           
            return true;
        }


    }
}
