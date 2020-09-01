using Mealmate.Core.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mealmate.Core.Entities
{
    public class FCMRegistrationToken: Entity
    {
        public string RegistrationToken { get; set; }
        public string ClientId { get; set; }
        public DateTimeOffset CreationDate { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
