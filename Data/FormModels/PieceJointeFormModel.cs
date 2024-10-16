using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Portail_OptiVille.Data.FormModels
{
    public class PieceJointeFormModel
    {
        [FileExtensionsAttribute(Extensions = "doc,docx,pdf,xls,xlsx,jpg,jpeg,png", ErrorMessage = "Format de fichier non valide.")]

        public IBrowserFile? Fichier { get; set; }

        public List<IBrowserFile> ListFichiers = new List<IBrowserFile>();
    }
}
