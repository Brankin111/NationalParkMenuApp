using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalParksMenuApp
{
    //JSON object properties.
    public class NationalPark
    {
        //Park's four letter abbreviation
        //Serves as object identification
        [JsonProperty("LocationNumber")]
        public string LocationNumber { get; set; }

        //The park's name
        [JsonProperty("LocationName")]
        public string LocationName { get; set; }

        //The park's street address
        [JsonProperty("Address")]
        public string Address { get; set; }

        //The park's city
        [JsonProperty("City")]
        public string City { get; set; }

        //The park's state
        [JsonProperty("State")]
        public string State { get; set; }

        //The park's zip code
        [JsonProperty("ZipCode")]
        public string ZipCode { get; set; }

        //The park's phone number
        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        //The park's fax number
        [JsonProperty("FaxNumber")]
        public string FaxNumber { get; set; }

        //The park's latitude
        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        //The park's longitude
        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        //The park's location on map
        [JsonProperty("Location")]
        public Location Location { get; set; }

        public override string ToString()
        {
            //returns a string specific to this particular instance of the object class.
            return $"{this.LocationNumber}: {this.LocationName}, {this.State}";
        }
    }
}