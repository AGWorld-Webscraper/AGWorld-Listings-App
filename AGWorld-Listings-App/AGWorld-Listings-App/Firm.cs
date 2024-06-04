using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AGWorld_Listings_App
{
    internal class Firm
    {
        [JsonInclude]
        public String _name { get; init; }
        [JsonInclude]
        public String _phoneNumber { get; init; }
        [JsonInclude]
        public String _faxNumber { get; init; }
        [JsonInclude]
        public String _address { get; init; }



        [JsonConstructor]
        public Firm(string _name, string _phoneNumber, string _faxNumber, string _address)
        {
            this._name = _name;
            this._phoneNumber = _phoneNumber;
            this._faxNumber = _faxNumber;
            this._address = _address;
        }

        public Firm(Firm other)
        {
            _name = other._name;
            _phoneNumber = other._phoneNumber;
            _faxNumber = other._faxNumber;
            _address = other._address;
        }

        public String toString()
        {
            return
                _name + "\n -" +
                _phoneNumber + "\n -" +
                _faxNumber + "\n -" +
                _address;
        }

        public String getName() { return _name; }
    }
}
