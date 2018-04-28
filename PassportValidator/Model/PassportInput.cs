using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PassportValidator
{
    public class PassportInput
    {
        [JsonProperty("pn")]
        public string PassportNum { get; set; }
        [JsonProperty("n")]
        public string Nationality { get; set; }
        [JsonProperty("dob")]
        public string DateOfBirth { get; set; }
        [JsonProperty("g")]
        public string Gender { get; set; }
        [JsonProperty("ped")]
        public string PassportExpiryDate { get; set; }
        [JsonProperty("mrz")]
        public string MRZ { get; set; }
    }
}