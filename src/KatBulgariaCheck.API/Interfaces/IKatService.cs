using FluentResults;
using KatBulgariaCheck.Models.Kat;

namespace KatBulgariaCheck.API.Interfaces
{
    public interface IKatService
    {
        Task<Result<KatResponse>> GetPersonalObligationsAsync();

        Task<Result<KatResponse>> GetCompanyObligationsAsync();
    }
}