using Microsoft.EntityFrameworkCore;
using Portail_OptiVille.Data.FormModels;
using Portail_OptiVille.Data.Models;

namespace Portail_OptiVille.Data.Services
{
    public class FichierService
    {
        private readonly A2024420517riGr1Eq6Context _context;

        public FichierService(A2024420517riGr1Eq6Context context)
        {
            _context = context;
        }

        public async Task SaveFichierData(PieceJointeFormModel pieceJointeFormModelDto)
        {
            var lastFournisseurId = await _context.Fournisseurs.MaxAsync(f => (int?)f.IdFournisseur);
            var fichiers = new List<Fichier>();
            foreach (var fichierFromList in pieceJointeFormModelDto.ListFichiers)
            {
                var fileExtension = Path.GetExtension(fichierFromList.Name).ToLower();
                var fichier = new Fichier
                {
                    Nom = fichierFromList.Name,
                    Type = fileExtension,
                    Taille = (int)fichierFromList.Size,
                    DateCreation = DateTime.Now,
                    Fournisseur = lastFournisseurId
                };

                fichiers.Add(fichier);
            }
            
            try
            {
                await _context.Fichiers.AddRangeAsync(fichiers);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de la sauvegarde des fichiers", ex);
            }
        }
    }
}
