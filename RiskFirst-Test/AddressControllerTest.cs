using System;
using Xunit;
using RiskFirst.Models;
using RiskFirst.Controllers;
using System.Linq;
using System.Collections.Generic;

namespace RiskFirst_Test
{
    public class AddressControllerTest
    {
     
        AddressController _controller;
        private readonly AddressContext _context;

        public AddressControllerTest(AddressContext context)
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

            this._controller = new AddressController(this._context);
        }

        [Fact]
        public void GetAllAddressesCase()
        {
            var okResult = _controller.GetAddresses();
            var items = Assert.IsAssignableFrom<IEnumerable<Address>>(okResult).Count();
            Assert.Equal(3, items);
        }

        [Fact]
        public void GetAddressByLocationCase()
        {
            var okResult = _controller.GetAddressesByLocation("Brooklyn");
            Assert.IsType<Address>(okResult);
        }

        [Fact]
        public void GetAddressByLocation_InvalidLocationCase()
        {
            var okResult = _controller.GetAddressesByLocation("The Bronx");
            var items = Assert.IsAssignableFrom<IEnumerable<Address>>(okResult).Count();
            Assert.Equal(0, items);
        }
    }
}
