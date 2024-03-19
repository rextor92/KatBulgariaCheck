using FluentResults;
using KatBulgariaCheck.Models.Kat;

namespace KatBulgariaCheck.API.Interfaces
{
    public interface IKatClient
    {
        Task<Result<KatResponse>> PersonalCheckByEgnAndIdCardAsync(string egn, string idCard);

        Task<Result<KatResponse>> PersonalCheckByEgnAndDriversLicenseAsync(string egn, string driversLicense);

        Task<Result<KatResponse>> PersonalCheckByEgnAndVehicleRegistrationAsync(string egn, string vehicleRegistration);

        Task<Result<KatResponse>> GetCompanyObligationsAsync();
    }
}