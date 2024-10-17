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

            // CREATE USER-SPECIFIC FOLDER
            var sanitizedFolderName = string.Join("_", identificationFormModelDto.NomEntreprise.Split(Path.GetInvalidFileNameChars()))
                                        .Replace(" ", "_")
                                        .ToLower();
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", lastFournisseurId.ToString() + sanitizedFolderName);
            Directory.CreateDirectory(folderPath); // Create the folder if it doesn't exist

            foreach (var fichierFromList in pieceJointeFormModelDto.ListFichiers)
            {
                try
                {
                    var filePath = Path.Combine(folderPath, fichierFromList.Name).ToLower();
                    using (var fileStream = fichierFromList.OpenReadStream(maxAllowedSize: 75 * 1024 * 1024)) // 75 MB limit
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            await fileStream.CopyToAsync(stream);
                        }
                    }

                    // After file is saved, add file information to the database model
                    var fileExtension = Path.GetExtension(fichierFromList.Name).ToLower();
                    var fichier = new Fichier
                    {
                        Nom = fichierFromList.Name,
                        Type = fileExtension,
                        Taille = (int)fichierFromList.Size,  // File size in bytes
                        DateCreation = DateTime.Now,
                        Path = Path.Combine("files", lastFournisseurId.ToString() + identificationFormModelDto.NomEntreprise, fichierFromList.Name).ToLower(),
                        Fournisseur = lastFournisseurId
                    };

                    fichiers.Add(fichier); // Add file data to the list for DB insert

                }
                catch (Exception ex)
                {
                    // LOG the error (you can use a logging framework or simply log to console for now)
                    Console.WriteLine($"Error saving file {fichierFromList.Name}: {ex.Message}");
                    // Continue to the next file without breaking the loop
                    continue;
                }
            }

            // SAVE FILE DATA TO THE DATABASE AFTER ALL FILES ARE HANDLED
            try
            {
                if (fichiers.Count > 0)
                {
                    await _context.Fichiers.AddRangeAsync(fichiers);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // LOG or HANDLE DATABASE ERROR
                throw new Exception("Une erreur est survenue lors de la sauvegarde des fichiers dans la base de données", ex);
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
