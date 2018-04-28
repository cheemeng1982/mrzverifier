using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PassportValidator
{
    public class PassportValidationResult
    {
        [JsonProperty("pncd")]
        public bool PassportNumberCheckDigit { get; set; }
        [JsonProperty("dobcd")]
        public bool DateOfBirthCheckDigit { get; set; }
        [JsonProperty("pedcd")]
        public bool PassportExpiryDateCheckDigit { get; set; }
        [JsonProperty("pnumcd")]
        public bool PersonalNumberCheckDigit { get; set; }
        [JsonProperty("fcd")]
        public bool FinalCheckDigit { get; set; }
        [JsonProperty("gcd")]
        public bool GenderCrossCheck { get; set; }
        [JsonProperty("dobcc")]
        public bool DateOfBirthCrossCheck { get; set; }
        [JsonProperty("edcc")]
        public bool ExpirationDateCrossCheck { get; set; }
        [JsonProperty("ncc")]
        public bool NationalityCrossCheck { get; set; }
        [JsonProperty("pncc")]
        public bool PassportNumberCrossCheck { get; set; }
    }
}