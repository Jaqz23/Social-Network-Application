using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RS.Core.Application.ViewModels.Post
{
    public class SavePostViewModel
    {
        public int Id { get; set; }
        public string? User_Id { get; set; }


        [Required(ErrorMessage = "Debe ingresar contenido")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Required(ErrorMessage = "Debes elegir una opción")]
        public string PublicationType { get; set; }
        public DateTime PostDate { get; set; } = DateTime.UtcNow;

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public string? Archive { get; set; }

    }
}
