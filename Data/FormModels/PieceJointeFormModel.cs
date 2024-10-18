using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Portail_OptiVille.Data.FormModels
{
    public class PieceJointeFormModel
    {
        public IBrowserFile? Fichier { get; set; }

        public List<IBrowserFile> ListFichiers = new List<IBrowserFile>();
    }
}
