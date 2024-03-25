using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGWorld_Listings_App
{
    internal class Firm
    {
        private String
            _name,
            _phoneNumber,
            _emailAddress,
            _address;

        public Firm(string name, string phoneNumber, string emailAddress, string address)
        {
            _name = name;
            _phoneNumber = phoneNumber;
            _emailAddress = emailAddress;
            _address = address;
        }

        public string Name { get {return _name; } }
        public string PhoneNumber { get { return _phoneNumber; } }
        public string EmailAddress { get { return _emailAddress; } }
        public string Address { get { return _address; } }
    }
}
