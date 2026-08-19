using FluentValidation;
using PhoneNumbers;

namespace StudentWebPortal.Validators.Common
{
    public static class PhoneValidationExtensions
    {
        private static readonly PhoneNumberUtil PhoneUtil = PhoneNumberUtil.GetInstance();

        public static IRuleBuilderOptions<T, string?> ValidPhoneNumber<T>(
            this IRuleBuilder<T, string?> ruleBuilder,
            string defaultRegion = "IN")
        {
            return ruleBuilder
                .Must(phone => BeAValidPhoneNumber(phone, defaultRegion))
                .WithMessage("Invalid phone number for the specified region.");
        }

        private static bool BeAValidPhoneNumber(string? phone, string defaultRegion)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return true; // let .When()/NotEmpty() handle required-ness separately

            try
            {
                var parsed = PhoneUtil.Parse(phone, defaultRegion);
                return PhoneUtil.IsValidNumber(parsed);
            }
            catch
            {
                return false;
            }
        }
    }
}