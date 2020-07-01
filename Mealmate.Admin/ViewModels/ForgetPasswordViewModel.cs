using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Admin.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}