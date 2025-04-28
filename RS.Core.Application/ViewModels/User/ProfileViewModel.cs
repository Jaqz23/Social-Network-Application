using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RS.Core.Application.ViewModels.User
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es requerido.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar un teléfono.")]
        [RegularExpression(@"^(809|829|849)\d{7}$", ErrorMessage = "El número de teléfono debe tener el formato de República Dominicana")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public string? ProfilePicture { get; set; }
        public IFormFile? File { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string? ConfirmPassword { get; set; }
    }

}
