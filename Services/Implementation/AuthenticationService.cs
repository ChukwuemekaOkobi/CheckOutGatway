using Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    /// <summary>
    /// Simple Authentication Service used to Authenticate Merchant Api Key 
    /// </summary>
    public class AuthenticationService : IAuthentication
    {

        ApplicationDbContext _context;

        public AuthenticationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Authenticate(string apiKey)
        {
            var Merchant = _context.Merchants.FirstOrDefault(x=>x.ApiKey == apiKey);

            return Merchant != null;
        }

    }
}
