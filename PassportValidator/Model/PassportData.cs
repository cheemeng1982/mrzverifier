using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PassportValidator
{
    public class PassportData
    {
        public string PassportNumber { get; set; }
        public string FirstBlockCheckDigit { get; set; }
        public string Nationality { get; set; }
        public string DateOfBirth { get; set; }
        public string SecondBlockCheckDigit { get; set; }
        public string Sex { get; set; }
        public string PassportExpiryDate { get; set; }
        public string ThirdBlockCheckDigit { get; set; }
        public string PersonalNumber { get; set; }
        public string ForthBlockCheckDigit { get; set; }
        public string FinalCheckDigit { get; set; }
    }
}