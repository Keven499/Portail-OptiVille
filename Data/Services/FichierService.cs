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

        public async Task SaveFichierData(PieceJointeFormModel pieceJointeFormModelDto, IdenticationFormModel identificationFormModelDto)
        {
            var lastFournisseurId = await _context.Fournisseurs.MaxAsync(f => (int?)f.IdFournisseur);
            var fichiers = new List<Fichier>();
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", lastFournisseurId.ToString() + identificationFormModelDto.NomEntreprise);
            Directory.CreateDirectory(folderPath);

            foreach (var fichierFromList in pieceJointeFormModelDto.ListFichiers)
            {
                var filePath = Path.Combine(folderPath, fichierFromList.Name).ToLower();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    using (var fileStream = fichierFromList.OpenReadStream())
                    {
                        await fileStream.CopyToAsync(stream);
                    }
                }

                var fileExtension = Path.GetExtension(fichierFromList.Name).ToLower();
                var fichier = new Fichier
                {
                    Nom = fichierFromList.Name,
                    Type = fileExtension,
                    Taille = (int)fichierFromList.Size, // IN BYTES
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


        public async Task DeleteAllFichiersData(List<Fichier> Listfichiers)
        {
            foreach (var fichier in Listfichiers)
            {
                _context.Fichiers.Remove(fichier);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOneFichierData(Fichier fichier)
        {
            _context.Fichiers.Remove(fichier);
            await _context.SaveChangesAsync();
        }
    }
}
