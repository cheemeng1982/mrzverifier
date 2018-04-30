using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassportValidator;
using Newtonsoft.Json;
using System.Web;

namespace UnitTestMRZVerifier
{
    /// <summary>
    /// Unit test class to test the logic of PassportValidator DLL
    /// </summary>
    [TestClass]
    public class MRZVerifierTester
    {
        PassportManager pm = new PassportManager();

        [TestMethod]
        [DataRow(@"{'pn':'958781724','n':'GBR','dob':'380124','g':'F','ped':'160101','mrz':'9587817249GBR3801242F1601013<<<<<<<<<<<<<<08'}")]
        [DataRow(@"{'pn':'742564695','n':'GBR','dob':'491222','g':'F','ped':'160101','mrz':'7425646954GBR4912228F1601013<<<<<<<<<<<<<<06'}")]
        [DataRow(@"{'pn':'125599844','n':'MYS','dob':'821113','g':'M','ped':'220101','mrz':'1255998441MYS8211136M2201018<<<<<<<<<<<<<<02'}")]
        [DataRow(@"{'pn':'785623541','n':'SGP','dob':'851201','g':'M','ped':'250131','mrz':'7856235417SGP8512017M2501316<<<<<<<<AA<<<<04'}")]

        public void TestPassportPositiveData(string jsonNotation)
        {
            var validationResult = InvokeValidation(jsonNotation);

            Assert.IsTrue(validationResult.PassportNumberCheckDigit, string.Format("PassportNumberCheckDigit : {0}", validationResult.PassportNumberCheckDigit));
            Assert.IsTrue(validationResult.DateOfBirthCheckDigit, string.Format("DateOfBirthCheckDigit : {0}", validationResult.DateOfBirthCheckDigit));
            Assert.IsTrue(validationResult.PassportExpiryDateCheckDigit, string.Format("PassportExpiryDateCheckDigit : {0}", validationResult.PassportExpiryDateCheckDigit));
            Assert.IsTrue(validationResult.PersonalNumberCheckDigit, string.Format("PersonalNumberCheckDigit : {0}", validationResult.PersonalNumberCheckDigit));
            Assert.IsTrue(validationResult.FinalCheckDigit, string.Format("FinalCheckDigit : {0}", validationResult.FinalCheckDigit));

            Assert.IsTrue(validationResult.GenderCrossCheck, string.Format("GenderCrossCheck : {0}", validationResult.GenderCrossCheck));
            Assert.IsTrue(validationResult.DateOfBirthCrossCheck, string.Format("DateOfBirthCrossCheck : {0}", validationResult.DateOfBirthCrossCheck));
            Assert.IsTrue(validationResult.ExpirationDateCrossCheck, string.Format("ExpirationDateCrossCheck : {0}", validationResult.ExpirationDateCrossCheck));
            Assert.IsTrue(validationResult.NationalityCrossCheck, string.Format("NationalityCrossCheck : {0}", validationResult.NationalityCrossCheck));
            Assert.IsTrue(validationResult.PassportNumberCrossCheck, string.Format("PassportNumberCrossCheck : {0}", validationResult.PassportNumberCrossCheck));
        }
        
        [TestMethod]
        [DataRow(@"{'pn':'742564695','n':'GBR','dob':'491222','g':'F','ped':'160101','mrz':'7425646951GBR4912228F1601013<<<<<<<<<<<<<<06'}")]
        [DataRow(@"{'pn':'125599844','n':'MYS','dob':'821113','g':'M','ped':'220101','mrz':'1255998449MYS8211136M2201018<<<<<<<<<<<<<<02'}")]

        public void TestMRZInvalidPassportNumberCheckDigit(string jsonNotation)
        {
            var validationResult = InvokeValidation(jsonNotation);

            Assert.IsFalse(validationResult.PassportNumberCheckDigit, string.Format("PassportNumberCheckDigit : {0}", validationResult.PassportNumberCheckDigit));
            Assert.IsTrue(validationResult.DateOfBirthCheckDigit, string.Format("DateOfBirthCheckDigit : {0}", validationResult.DateOfBirthCheckDigit));
            Assert.IsTrue(validationResult.PassportExpiryDateCheckDigit, string.Format("PassportExpiryDateCheckDigit : {0}", validationResult.PassportExpiryDateCheckDigit));
            Assert.IsTrue(validationResult.PersonalNumberCheckDigit, string.Format("PersonalNumberCheckDigit : {0}", validationResult.PersonalNumberCheckDigit));
            Assert.IsFalse(validationResult.FinalCheckDigit, string.Format("FinalCheckDigit : {0}", validationResult.FinalCheckDigit));

            Assert.IsTrue(validationResult.GenderCrossCheck, string.Format("GenderCrossCheck : {0}", validationResult.GenderCrossCheck));
            Assert.IsTrue(validationResult.DateOfBirthCrossCheck, string.Format("DateOfBirthCrossCheck : {0}", validationResult.DateOfBirthCrossCheck));
            Assert.IsTrue(validationResult.ExpirationDateCrossCheck, string.Format("ExpirationDateCrossCheck : {0}", validationResult.ExpirationDateCrossCheck));
            Assert.IsTrue(validationResult.NationalityCrossCheck, string.Format("NationalityCrossCheck : {0}", validationResult.NationalityCrossCheck));
            Assert.IsTrue(validationResult.PassportNumberCrossCheck, string.Format("PassportNumberCrossCheck : {0}", validationResult.PassportNumberCrossCheck));
        }

        [TestMethod]
        [DataRow(@"{'pn':'888141344','n':'MYS','dob':'990613','g':'M','ped':'281201','mrz':'8881413445MYS9906135M2812014<<<<<<<<<<<<<<00'}")]
        public void TestMRZInvalidDOBCheckDigit(string jsonNotation)
        {
            var validationResult = InvokeValidation(jsonNotation);

            Assert.IsTrue(validationResult.PassportNumberCheckDigit, string.Format("PassportNumberCheckDigit : {0}", validationResult.PassportNumberCheckDigit));
            Assert.IsFalse(validationResult.DateOfBirthCheckDigit, string.Format("DateOfBirthCheckDigit : {0}", validationResult.DateOfBirthCheckDigit));
            Assert.IsTrue(validationResult.PassportExpiryDateCheckDigit, string.Format("PassportExpiryDateCheckDigit : {0}", validationResult.PassportExpiryDateCheckDigit));
            Assert.IsTrue(validationResult.PersonalNumberCheckDigit, string.Format("PersonalNumberCheckDigit : {0}", validationResult.PersonalNumberCheckDigit));
            Assert.IsFalse(validationResult.FinalCheckDigit, string.Format("FinalCheckDigit : {0}", validationResult.FinalCheckDigit));

            Assert.IsTrue(validationResult.GenderCrossCheck, string.Format("GenderCrossCheck : {0}", validationResult.GenderCrossCheck));
            Assert.IsTrue(validationResult.DateOfBirthCrossCheck, string.Format("DateOfBirthCrossCheck : {0}", validationResult.DateOfBirthCrossCheck));
            Assert.IsTrue(validationResult.ExpirationDateCrossCheck, string.Format("ExpirationDateCrossCheck : {0}", validationResult.ExpirationDateCrossCheck));
            Assert.IsTrue(validationResult.NationalityCrossCheck, string.Format("NationalityCrossCheck : {0}", validationResult.NationalityCrossCheck));
            Assert.IsTrue(validationResult.PassportNumberCrossCheck, string.Format("PassportNumberCrossCheck : {0}", validationResult.PassportNumberCrossCheck));
        }

        [TestMethod]
        [DataRow(@"{'pn':'888141344','n':'MYS','dob':'990613','g':'M','ped':'281201','mrz':'8881413445MYS9906138M2812010<<<<<<<<<<<<<<00'}")]
        public void TestMRZInvalidPassportExpiryDateCheckDigit(string jsonNotation)
        {
            var validationResult = InvokeValidation(jsonNotation);

            Assert.IsTrue(validationResult.PassportNumberCheckDigit, string.Format("PassportNumberCheckDigit : {0}", validationResult.PassportNumberCheckDigit));
            Assert.IsTrue(validationResult.DateOfBirthCheckDigit, string.Format("DateOfBirthCheckDigit : {0}", validationResult.DateOfBirthCheckDigit));
            Assert.IsFalse(validationResult.PassportExpiryDateCheckDigit, string.Format("PassportExpiryDateCheckDigit : {0}", validationResult.PassportExpiryDateCheckDigit));
            Assert.IsTrue(validationResult.PersonalNumberCheckDigit, string.Format("PersonalNumberCheckDigit : {0}", validationResult.PersonalNumberCheckDigit));
            Assert.IsFalse(validationResult.FinalCheckDigit, string.Format("FinalCheckDigit : {0}", validationResult.FinalCheckDigit));

            Assert.IsTrue(validationResult.GenderCrossCheck, string.Format("GenderCrossCheck : {0}", validationResult.GenderCrossCheck));
            Assert.IsTrue(validationResult.DateOfBirthCrossCheck, string.Format("DateOfBirthCrossCheck : {0}", validationResult.DateOfBirthCrossCheck));
            Assert.IsTrue(validationResult.ExpirationDateCrossCheck, string.Format("ExpirationDateCrossCheck : {0}", validationResult.ExpirationDateCrossCheck));
            Assert.IsTrue(validationResult.NationalityCrossCheck, string.Format("NationalityCrossCheck : {0}", validationResult.NationalityCrossCheck));
            Assert.IsTrue(validationResult.PassportNumberCrossCheck, string.Format("PassportNumberCrossCheck : {0}", validationResult.PassportNumberCrossCheck));
        }

        [TestMethod]
        [DataRow(@"{'pn':'888141344','n':'MYS','dob':'990613','g':'M','ped':'281201','mrz':'8881413445MYS9906138M2812014<<<<<<<<<<<<<<80'}")]
        public void TestMRZInvalidPassportPersonalNumberCheckDigit(string jsonNotation)
        {
            var validationResult = InvokeValidation(jsonNotation);

            Assert.IsTrue(validationResult.PassportNumberCheckDigit, string.Format("PassportNumberCheckDigit : {0}", validationResult.PassportNumberCheckDigit));
            Assert.IsTrue(validationResult.DateOfBirthCheckDigit, string.Format("DateOfBirthCheckDigit : {0}", validationResult.DateOfBirthCheckDigit));
            Assert.IsTrue(validationResult.PassportExpiryDateCheckDigit, string.Format("PassportExpiryDateCheckDigit : {0}", validationResult.PassportExpiryDateCheckDigit));
            Assert.IsFalse(validationResult.PersonalNumberCheckDigit, string.Format("PersonalNumberCheckDigit : {0}", validationResult.PersonalNumberCheckDigit));
            Assert.IsFalse(validationResult.FinalCheckDigit, string.Format("FinalCheckDigit : {0}", validationResult.FinalCheckDigit));

            Assert.IsTrue(validationResult.GenderCrossCheck, string.Format("GenderCrossCheck : {0}", validationResult.GenderCrossCheck));
            Assert.IsTrue(validationResult.DateOfBirthCrossCheck, string.Format("DateOfBirthCrossCheck : {0}", validationResult.DateOfBirthCrossCheck));
            Assert.IsTrue(validationResult.ExpirationDateCrossCheck, string.Format("ExpirationDateCrossCheck : {0}", validationResult.ExpirationDateCrossCheck));
            Assert.IsTrue(validationResult.NationalityCrossCheck, string.Format("NationalityCrossCheck : {0}", validationResult.NationalityCrossCheck));
            Assert.IsTrue(validationResult.PassportNumberCrossCheck, string.Format("PassportNumberCrossCheck : {0}", validationResult.PassportNumberCrossCheck));
        }

        [TestMethod]
        [DataRow(@"{'pn':'888141344','n':'MYS','dob':'990613','g':'M','ped':'281201','mrz':'8881413445MYS9906138M2812014<<<<<<<<<<<<<<09'}")]
        public void TestMRZInvalidFinalCheckDigit(string jsonNotation)
        {
            var validationResult = InvokeValidation(jsonNotation);

            Assert.IsTrue(validationResult.PassportNumberCheckDigit, string.Format("PassportNumberCheckDigit : {0}", validationResult.PassportNumberCheckDigit));
            Assert.IsTrue(validationResult.DateOfBirthCheckDigit, string.Format("DateOfBirthCheckDigit : {0}", validationResult.DateOfBirthCheckDigit));
            Assert.IsTrue(validationResult.PassportExpiryDateCheckDigit, string.Format("PassportExpiryDateCheckDigit : {0}", validationResult.PassportExpiryDateCheckDigit));
            Assert.IsTrue(validationResult.PersonalNumberCheckDigit, string.Format("PersonalNumberCheckDigit : {0}", validationResult.PersonalNumberCheckDigit));
            Assert.IsFalse(validationResult.FinalCheckDigit, string.Format("FinalCheckDigit : {0}", validationResult.FinalCheckDigit));

            Assert.IsTrue(validationResult.GenderCrossCheck, string.Format("GenderCrossCheck : {0}", validationResult.GenderCrossCheck));
            Assert.IsTrue(validationResult.DateOfBirthCrossCheck, string.Format("DateOfBirthCrossCheck : {0}", validationResult.DateOfBirthCrossCheck));
            Assert.IsTrue(validationResult.ExpirationDateCrossCheck, string.Format("ExpirationDateCrossCheck : {0}", validationResult.ExpirationDateCrossCheck));
            Assert.IsTrue(validationResult.NationalityCrossCheck, string.Format("NationalityCrossCheck : {0}", validationResult.NationalityCrossCheck));
            Assert.IsTrue(validationResult.PassportNumberCrossCheck, string.Format("PassportNumberCrossCheck : {0}", validationResult.PassportNumberCrossCheck));
        }

        [TestMethod]
        [DataRow(@"{'pn':'125599822','n':'MYS','dob':'821113','g':'M','ped':'220101','mrz':'1255998441MYS8211136M2201018<<<<<<<<<<<<<<02'}")]
        [DataRow(@"{'pn':'785603541','n':'SGP','dob':'851201','g':'M','ped':'250131','mrz':'7856235417SGP8512017M2501316<<<<<<<<AA<<<<04'}")]

        public void TestMRZInvalidPassportNumberCrossCheck(string jsonNotation)
        {
            var validationResult = InvokeValidation(jsonNotation);

            Assert.IsTrue(validationResult.PassportNumberCheckDigit, string.Format("PassportNumberCheckDigit : {0}", validationResult.PassportNumberCheckDigit));
            Assert.IsTrue(validationResult.DateOfBirthCheckDigit, string.Format("DateOfBirthCheckDigit : {0}", validationResult.DateOfBirthCheckDigit));
            Assert.IsTrue(validationResult.PassportExpiryDateCheckDigit, string.Format("PassportExpiryDateCheckDigit : {0}", validationResult.PassportExpiryDateCheckDigit));
            Assert.IsTrue(validationResult.PersonalNumberCheckDigit, string.Format("PersonalNumberCheckDigit : {0}", validationResult.PersonalNumberCheckDigit));
            Assert.IsTrue(validationResult.FinalCheckDigit, string.Format("FinalCheckDigit : {0}", validationResult.FinalCheckDigit));

            Assert.IsTrue(validationResult.GenderCrossCheck, string.Format("GenderCrossCheck : {0}", validationResult.GenderCrossCheck));
            Assert.IsTrue(validationResult.DateOfBirthCrossCheck, string.Format("DateOfBirthCrossCheck : {0}", validationResult.DateOfBirthCrossCheck));
            Assert.IsTrue(validationResult.ExpirationDateCrossCheck, string.Format("ExpirationDateCrossCheck : {0}", validationResult.ExpirationDateCrossCheck));
            Assert.IsTrue(validationResult.NationalityCrossCheck, string.Format("NationalityCrossCheck : {0}", validationResult.NationalityCrossCheck));
            Assert.IsFalse(validationResult.PassportNumberCrossCheck, string.Format("PassportNumberCrossCheck : {0}", validationResult.PassportNumberCrossCheck));
        }

        [TestMethod]
        [DataRow(@"{'pn':'888141344','n':'MYS','dob':'990613','g':'<','ped':'281201','mrz':'8881413445MYS9906138M2812014<<<<<<<<<<<<<<00'}")]
        public void TestMRZInvalidGenderCrossCheck(string jsonNotation)
        {
            var validationResult = InvokeValidation(jsonNotation);

            Assert.IsTrue(validationResult.PassportNumberCheckDigit, string.Format("PassportNumberCheckDigit : {0}", validationResult.PassportNumberCheckDigit));
            Assert.IsTrue(validationResult.DateOfBirthCheckDigit, string.Format("DateOfBirthCheckDigit : {0}", validationResult.DateOfBirthCheckDigit));
            Assert.IsTrue(validationResult.PassportExpiryDateCheckDigit, string.Format("PassportExpiryDateCheckDigit : {0}", validationResult.PassportExpiryDateCheckDigit));
            Assert.IsTrue(validationResult.PersonalNumberCheckDigit, string.Format("PersonalNumberCheckDigit : {0}", validationResult.PersonalNumberCheckDigit));
            Assert.IsTrue(validationResult.FinalCheckDigit, string.Format("FinalCheckDigit : {0}", validationResult.FinalCheckDigit));

            Assert.IsFalse(validationResult.GenderCrossCheck, string.Format("GenderCrossCheck : {0}", validationResult.GenderCrossCheck));
            Assert.IsTrue(validationResult.DateOfBirthCrossCheck, string.Format("DateOfBirthCrossCheck : {0}", validationResult.DateOfBirthCrossCheck));
            Assert.IsTrue(validationResult.ExpirationDateCrossCheck, string.Format("ExpirationDateCrossCheck : {0}", validationResult.ExpirationDateCrossCheck));
            Assert.IsTrue(validationResult.NationalityCrossCheck, string.Format("NationalityCrossCheck : {0}", validationResult.NationalityCrossCheck));
            Assert.IsTrue(validationResult.PassportNumberCrossCheck, string.Format("PassportNumberCrossCheck : {0}", validationResult.PassportNumberCrossCheck));
        }

        [TestMethod]
        [DataRow(@"{'pn':'888141344','n':'MYS','dob':'990613','g':'M','ped':'281222','mrz':'8881413445MYS9906138M2812014<<<<<<<<<<<<<<00'}")]
        public void TestMRZInvalidPassportExpirationDateCrossCheck(string jsonNotation)
        {
            var validationResult = InvokeValidation(jsonNotation);

            Assert.IsTrue(validationResult.PassportNumberCheckDigit, string.Format("PassportNumberCheckDigit : {0}", validationResult.PassportNumberCheckDigit));
            Assert.IsTrue(validationResult.DateOfBirthCheckDigit, string.Format("DateOfBirthCheckDigit : {0}", validationResult.DateOfBirthCheckDigit));
            Assert.IsTrue(validationResult.PassportExpiryDateCheckDigit, string.Format("PassportExpiryDateCheckDigit : {0}", validationResult.PassportExpiryDateCheckDigit));
            Assert.IsTrue(validationResult.PersonalNumberCheckDigit, string.Format("PersonalNumberCheckDigit : {0}", validationResult.PersonalNumberCheckDigit));
            Assert.IsTrue(validationResult.FinalCheckDigit, string.Format("FinalCheckDigit : {0}", validationResult.FinalCheckDigit));

            Assert.IsTrue(validationResult.GenderCrossCheck, string.Format("GenderCrossCheck : {0}", validationResult.GenderCrossCheck));
            Assert.IsTrue(validationResult.DateOfBirthCrossCheck, string.Format("DateOfBirthCrossCheck : {0}", validationResult.DateOfBirthCrossCheck));
            Assert.IsFalse(validationResult.ExpirationDateCrossCheck, string.Format("ExpirationDateCrossCheck : {0}", validationResult.ExpirationDateCrossCheck));
            Assert.IsTrue(validationResult.NationalityCrossCheck, string.Format("NationalityCrossCheck : {0}", validationResult.NationalityCrossCheck));
            Assert.IsTrue(validationResult.PassportNumberCrossCheck, string.Format("PassportNumberCrossCheck : {0}", validationResult.PassportNumberCrossCheck));
        }

        [TestMethod]
        [DataRow(@"{'pn':'888141344','n':'GBR','dob':'990613','g':'M','ped':'281201','mrz':'8881413445MYS9906138M2812014<<<<<<<<<<<<<<00'}")]
        public void TestMRZInvalidNationalityCrossCheck(string jsonNotation)
        {
            var validationResult = InvokeValidation(jsonNotation);

            Assert.IsTrue(validationResult.PassportNumberCheckDigit, string.Format("PassportNumberCheckDigit : {0}", validationResult.PassportNumberCheckDigit));
            Assert.IsTrue(validationResult.DateOfBirthCheckDigit, string.Format("DateOfBirthCheckDigit : {0}", validationResult.DateOfBirthCheckDigit));
            Assert.IsTrue(validationResult.PassportExpiryDateCheckDigit, string.Format("PassportExpiryDateCheckDigit : {0}", validationResult.PassportExpiryDateCheckDigit));
            Assert.IsTrue(validationResult.PersonalNumberCheckDigit, string.Format("PersonalNumberCheckDigit : {0}", validationResult.PersonalNumberCheckDigit));
            Assert.IsTrue(validationResult.FinalCheckDigit, string.Format("FinalCheckDigit : {0}", validationResult.FinalCheckDigit));

            Assert.IsTrue(validationResult.GenderCrossCheck, string.Format("GenderCrossCheck : {0}", validationResult.GenderCrossCheck));
            Assert.IsTrue(validationResult.DateOfBirthCrossCheck, string.Format("DateOfBirthCrossCheck : {0}", validationResult.DateOfBirthCrossCheck));
            Assert.IsTrue(validationResult.ExpirationDateCrossCheck, string.Format("ExpirationDateCrossCheck : {0}", validationResult.ExpirationDateCrossCheck));
            Assert.IsFalse(validationResult.NationalityCrossCheck, string.Format("NationalityCrossCheck : {0}", validationResult.NationalityCrossCheck));
            Assert.IsTrue(validationResult.PassportNumberCrossCheck, string.Format("PassportNumberCrossCheck : {0}", validationResult.PassportNumberCrossCheck));
        }

        [TestMethod]
        [DataRow(@"{'pn':'888141344','n':'MYS','dob':'821113','g':'M','ped':'281201','mrz':'8881413445MYS9906138M2812014<<<<<<<<<<<<<<00'}")]
        public void TestMRZInvalidDateOfBirthCrossCheck(string jsonNotation)
        {
            var validationResult = InvokeValidation(jsonNotation);

            Assert.IsTrue(validationResult.PassportNumberCheckDigit, string.Format("PassportNumberCheckDigit : {0}", validationResult.PassportNumberCheckDigit));
            Assert.IsTrue(validationResult.DateOfBirthCheckDigit, string.Format("DateOfBirthCheckDigit : {0}", validationResult.DateOfBirthCheckDigit));
            Assert.IsTrue(validationResult.PassportExpiryDateCheckDigit, string.Format("PassportExpiryDateCheckDigit : {0}", validationResult.PassportExpiryDateCheckDigit));
            Assert.IsTrue(validationResult.PersonalNumberCheckDigit, string.Format("PersonalNumberCheckDigit : {0}", validationResult.PersonalNumberCheckDigit));
            Assert.IsTrue(validationResult.FinalCheckDigit, string.Format("FinalCheckDigit : {0}", validationResult.FinalCheckDigit));

            Assert.IsTrue(validationResult.GenderCrossCheck, string.Format("GenderCrossCheck : {0}", validationResult.GenderCrossCheck));
            Assert.IsFalse(validationResult.DateOfBirthCrossCheck, string.Format("DateOfBirthCrossCheck : {0}", validationResult.DateOfBirthCrossCheck));
            Assert.IsTrue(validationResult.ExpirationDateCrossCheck, string.Format("ExpirationDateCrossCheck : {0}", validationResult.ExpirationDateCrossCheck));
            Assert.IsTrue(validationResult.NationalityCrossCheck, string.Format("NationalityCrossCheck : {0}", validationResult.NationalityCrossCheck));
            Assert.IsTrue(validationResult.PassportNumberCrossCheck, string.Format("PassportNumberCrossCheck : {0}", validationResult.PassportNumberCrossCheck));
        }


        private PassportValidationResult InvokeValidation(string jn)
        {
            var pi = JsonConvert.DeserializeObject<PassportInput>(HttpUtility.UrlDecode(jn));
            var validationResult = pm.ValidatePassportData(pi);

            return validationResult;
        }
    }
}
