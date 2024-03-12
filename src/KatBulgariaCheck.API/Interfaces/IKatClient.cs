using FluentResults;
using KatBulgariaCheck.Models.Kat;
using KatBulgariaCheck.Models.Kat.Enums;

namespace KatBulgariaCheck.API.Interfaces
{
    public interface IKatClient
    {
        Task<Result<KatResponse>> GetPersonalObligations(ObligatedIndividualSearchType obligatedIndividualSearchType);

        Task<Result<KatResponse>> GetCompanyObligations();
    }
}