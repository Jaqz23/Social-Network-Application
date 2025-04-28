using System.ComponentModel.DataAnnotations;

namespace RS.Core.Application.ViewModels.User
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Debe ingresar su correo electrónico.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
        public string Email { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
