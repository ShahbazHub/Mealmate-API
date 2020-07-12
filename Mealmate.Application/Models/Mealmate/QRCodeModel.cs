using Mealmate.Application.Models.Base;

using System;
using System.Text.Json.Serialization;

namespace Mealmate.Application.Models
{
    public class QRCodeModel : BaseModel
    {
        [JsonIgnore]
        public byte[] Code { get; set; }
        public DateTimeOffset Created { get; set; }

        public int TableId { get; set; }
        //public TableModel Table { get; set; }
    }
}