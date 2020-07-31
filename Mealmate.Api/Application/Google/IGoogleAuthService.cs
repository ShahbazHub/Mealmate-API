using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Api.Services
{
    public interface IGoogleAuthService
    {
        Task<GoogleTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
        Task<GoogleUserInfoResult> GetUserInfoAsync(string accessToken);
    }
}
