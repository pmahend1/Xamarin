using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RegisterAdd.Models.Constants;

namespace RegisterAdd.Services
{
    public class ValidationService
    {
        public static bool IsEmpty(string value) => (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value));


        public static string ValidatePassword(string value)
        {
            if (value.Length < 5)
            {
                return AppConstants.PASSWORD_SHORT;
            }

            if (value.Length > 12)
            {
                return AppConstants.PASSWORD_LONG;
            }

            //Check string contains alpha numeric
            Regex regexContainer = new Regex("^[a-zA-Z0-9]*$");

            if (!regexContainer.IsMatch(value))
            {
                return AppConstants.PASSWORD_ALPHANUMERIC;
            }

            //Check string contains one Alphabet lowercase
            regexContainer = new Regex(".*[a-z]+.*");

            if (!regexContainer.IsMatch(value))
            {
                return AppConstants.PASSWORD_1_ALPHA_LOWER;
            }

            //Check string contains one Alphabet uppercase
            regexContainer = new Regex(".*[A-Z]+.*");

            if (!regexContainer.IsMatch(value))
            {
                return AppConstants.PASSWORD_1_ALPHA_UPPER;
            }

            //Check string contains one Number
            regexContainer = new Regex(".*[0-9]+.*");

            if (!regexContainer.IsMatch(value))
            {
                return AppConstants.PASSWORD_1_NUMBER;
            }

            //check if the password has sequence
            Regex sequenceDuplicates = new Regex(@"(.+)\1");
            MatchCollection matchingSequenceList = sequenceDuplicates.Matches(value);
            if (matchingSequenceList.Count > 0)
            {
                return AppConstants.REPEAT_SEQUENCE;
            }


            return String.Empty;
        }

        public string CheckPasswordRealTime(string value)
        {
            //Check sequence
            Regex sequenceDuplicates = new Regex(@"(.+)\1");
            MatchCollection matchingSequenceList = sequenceDuplicates.Matches(value);
            if (matchingSequenceList.Count > 0)
            {
                return AppConstants.REPEAT_SEQUENCE;
            }

            //Check allowed charecters
            //Check string contains alpha numeric
            Regex regexContainer = new Regex("^[a-zA-Z0-9]*$");

            if (!regexContainer.IsMatch(value))
            {
                return AppConstants.PASSWORD_ALPHANUMERIC;
            }

            //Check length
            if (value.Length < 5)
            {
                return AppConstants.PASSWORD_SHORT;
            }
            else
            {
                //Check string contains one Alphabet lowercase
                regexContainer = new Regex(".*[a-z]+.*");

                if (!regexContainer.IsMatch(value))
                {
                    return AppConstants.PASSWORD_1_ALPHA_LOWER;
                }

                //Check string contains one Alphabet uppercase
                regexContainer = new Regex(".*[A-Z]+.*");

                if (!regexContainer.IsMatch(value))
                {
                    return AppConstants.PASSWORD_1_ALPHA_UPPER;
                }

                //Check string contains one Number
                regexContainer = new Regex(".*[0-9]+.*");

                if (!regexContainer.IsMatch(value))
                {
                    return AppConstants.PASSWORD_1_NUMBER;
                }
            }

            //Check Max length
            if (value.Length > 12)
            {
                return AppConstants.PASSWORD_LONG;
            }

            return String.Empty;
        }
    }
}