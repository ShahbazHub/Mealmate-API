using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mealmate.Core.Entities
{
    public class FCMRegistrationToken
    {

        public string Id { get; set; }

        public string ClientId { get; set; }
        public string RegistrationToken { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
