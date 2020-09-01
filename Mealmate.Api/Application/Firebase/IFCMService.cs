using FirebaseAdmin.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Api.Application.Firebase
{
    public interface IFCMService
    {
        Task<BatchResponse> SendMulticastAsync(List<string> registrationTokens, Notification notification);
        Task<string> SendToTokenAsync(string registrationToken, Notification notification);
    }
}