using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PassportValidator;
using System.Text;

namespace PassportValidator
{
    public class PassportManager
    {
        const int MRZ_CHAR_COUNT = 44;
        private int[] WEIGHT = { 7, 3, 1 };

        public PassportValidationResult ValidatePassportData(PassportInput pi)
        {
            try
            {
                var scannedData = RetrievePassportDataFromMRZ(pi.MRZ);

                PassportValidationResult pvr = new PassportValidationResult();

                int checkDigit = -1;

                // Check digit process
                checkDigit = GetCheckDigit(TransformToNumericRepresentation(scannedData.PassportNumber));
                pvr.PassportNumberCheckDigit = (checkDigit.ToString() == scannedData.FirstBlockCheckDigit);

                checkDigit = GetCheckDigit(TransformToNumericRepresentation(scannedData.DateOfBirth));
                pvr.DateOfBirthCheckDigit = (checkDigit.ToString() == scannedData.SecondBlockCheckDigit);

                checkDigit = GetCheckDigit(TransformToNumericRepresentation(scannedData.PassportExpiryDate));
                pvr.PassportExpiryDateCheckDigit = (checkDigit.ToString() == scannedData.ThirdBlockCheckDigit);

                checkDigit = GetCheckDigit(TransformToNumericRepresentation(scannedData.PersonalNumber));
                pvr.PersonalNumberCheckDigit = (checkDigit.ToString() == scannedData.ForthBlockCheckDigit);

                checkDigit = GetCheckDigit(TransformToNumericRepresentation(scannedData.PassportNumber + scannedData.FirstBlockCheckDigit + scannedData.DateOfBirth + scannedData.SecondBlockCheckDigit + scannedData.PassportExpiryDate + scannedData.ThirdBlockCheckDigit + scannedData.PersonalNumber + scannedData.ForthBlockCheckDigit));
                pvr.FinalCheckDigit = (checkDigit.ToString() == scannedData.FinalCheckDigit);

                // Cross check data process
                pvr.GenderCrossCheck = scannedData.Sex == pi.Gender;
                pvr.DateOfBirthCrossCheck = scannedData.DateOfBirth == pi.DateOfBirth;
                pvr.ExpirationDateCrossCheck = scannedData.PassportExpiryDate == pi.PassportExpiryDate;
                pvr.NationalityCrossCheck = scannedData.Nationality == pi.Nationality;
                pvr.PassportNumberCrossCheck = scannedData.PassportNumber == pi.PassportNum;

                return pvr;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private PassportData RetrievePassportDataFromMRZ(string mrz)
        {
            if(string.IsNullOrEmpty(mrz))
            {
                throw new PassportDataException("The given MRZ value is empty");
            }
            else if(mrz.Length != MRZ_CHAR_COUNT)
            {
                throw new PassportDataException(string.Format("The input MRZ character count is not correct : {0}", mrz.Length));
            }

            StringBuilder errors = new StringBuilder();

            PassportData pd = new PassportData();
            pd.PassportNumber = mrz.Substring(0, 9);
            pd.FirstBlockCheckDigit = mrz.Substring(9, 1);
            pd.Nationality = mrz.Substring(10, 3);
            pd.DateOfBirth = mrz.Substring(13, 6);
            pd.SecondBlockCheckDigit = mrz.Substring(19, 1);
            pd.Sex = mrz.Substring(20, 1);
            pd.PassportExpiryDate = mrz.Substring(21, 6);
            pd.ThirdBlockCheckDigit = mrz.Substring(27, 1);
            pd.PersonalNumber = mrz.Substring(28, 14);
            pd.ForthBlockCheckDigit = mrz.Substring(42, 1);
            pd.FinalCheckDigit = mrz.Substring(43, 1);

            // Validate fields
            if(!IsValidDateFormat(pd.DateOfBirth))
            {
                errors.AppendLine(string.Format("Invalid date of birth given in MRZ : \"{0}\"\n", pd.DateOfBirth));
            }
            if (!IsValidDateFormat(pd.PassportExpiryDate))
            {
                errors.AppendLine(string.Format("Invalid expiration date given in MRZ : \"{0}\"\n", pd.PassportExpiryDate));
            }
            if (!IsValidGender(pd.Sex))
            {
                errors.AppendLine(string.Format("Invalid gender given in MRZ : \"{0}\"\n", pd.Sex));
            }

            if(!string.IsNullOrEmpty(errors.ToString()))
            {
                throw new PassportDataException(errors.ToString());
            }

            return pd;
        }

        private bool IsValidDateFormat(string dateInput)
        {
            DateTime Result = new DateTime();
            string dateFormats = "yyMMdd";

            return DateTime.TryParseExact(dateInput, dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out Result);
        }

        private bool IsValidGender (string genderInput)
        {
            string[] genders = new string[] { "M", "F", "<" };
            return genders.Any(g => g == genderInput);
        }

        private int GetCheckDigit(List<int> numbers)
        {
            int sum = 0;
            int indexWeight = 0;
            for (int i = 0; i < numbers.Count(); i++)
            {
                sum += numbers[i] * (WEIGHT[indexWeight % 3]);
                indexWeight++;
            }

            return sum % 10;
        }

        private List<int> TransformToNumericRepresentation(string inputBlock)
        {
            List<int> output = new List<int>();
            foreach (char z in inputBlock)
            {
                if (!Helper.DictInputCode.ContainsKey(z.ToString()))
                    throw new PassportDataException(string.Format("Invalid character in input to transform : \"{0}\"", z));

                output.Add(Helper.DictInputCode[z.ToString()]);
            }
            return output;
        }
    }
}