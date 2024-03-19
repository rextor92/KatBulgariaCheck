using FluentValidation;
using KatBulgariaCheck.API.Helpers.Settings;

namespace KatBulgariaCheck.API.Helpers.Validators
{
    public sealed class IndividualObligationsSearchSettingsValidator : AbstractValidator<IndividualObligationsSearchSettings>
    {
        public IndividualObligationsSearchSettingsValidator()
        {
            RuleFor(x => x.PersonalIdentityNumber)
                .NotEmpty()
                .WithMessage("PersonalIdentityNumber is required.")
                .Matches(@"^\d{10}$")
                .WithMessage("PersonalIdentityNumber should be 10 digits.");

            RuleFor(x => x.PersonalIdCardNumber)
                .Matches(@"^\d{9}$")
                .When(x => !string.IsNullOrWhiteSpace(x.PersonalIdCardNumber))
                .WithMessage("PersonalIdCardNumber should be 9 digits.");

            RuleFor(x => x.DrivingLicenseNumber)
                .Matches(@"^\d{9}$")
                .When(x => !string.IsNullOrWhiteSpace(x.PersonalIdCardNumber))
                .WithMessage("DrivingLicenseNumber should be 9 digits.");

            RuleFor(x => x.VehicleRegistrationNumber)
                .Matches(@"^[A-Za-z0-9]{8}$")
                .When(x => !string.IsNullOrWhiteSpace(x.VehicleRegistrationNumber))
                .WithMessage("VehicleRegistrationNumber should be 8 symbols and only contain English letters and numbers.");

            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.PersonalIdCardNumber)
                    || !string.IsNullOrWhiteSpace(x.DrivingLicenseNumber)
                    || !string.IsNullOrWhiteSpace(x.VehicleRegistrationNumber))
                .WithMessage("At least one of PersonalIdCardNumber, DrivingLicenseNumber, or VehicleRegistrationNumber should be present.");
        }
    }
}