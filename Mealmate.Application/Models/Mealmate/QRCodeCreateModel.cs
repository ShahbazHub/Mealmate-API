using Mealmate.Application.Models.Base;

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mealmate.Application.Models
{
    public class QRCodeCreateModel
    {
        [Required]
        public int TableId { get; set; }
    }
}