using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace RiskFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressContext _context;

        public AddressController(AddressContext context)
        {
            this._context = context;

            if (this._context.AddressItems.Count() == 0)
            {
                this._context.AddressItems.AddRange(
                    new Address { ID = 1, FirstName = "John", LastName = "Smith", StreetAddress = "94th Street", City = "Brooklyn", Country = "USA" },
                    new Address { ID = 2, FirstName = "Jane", LastName = "Smith", StreetAddress = "97th Street", City = "Brooklyn", Country = "USA" },
                    new Address { ID = 3, FirstName = "Joey", LastName = "Gambino", StreetAddress = "93rd Street", City = "Queens", Country = "USA" }
                    );
                this._context.SaveChanges();
            }
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return await this._context.AddressItems.ToListAsync();

        }

        // GET: api/addresses/{location}
        [HttpGet("{location}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddressesByLocation(string location)
        {
            var addressList = await this._context.AddressItems.Where(loc => loc.City.ToLower() == location.ToLower()).ToListAsync();

            if (addressList.Count() == 0)
            {
                return NotFound();
            }

            return addressList;
        }


    }
}
