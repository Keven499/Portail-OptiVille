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
            // var sanitizedFolderName = string.Join("_", identificationFormModelDto.NomEntreprise.Split(Path.GetInvalidFileNameChars()))
            //                             .Replace(" ", "_")
            //                             .ToLower();

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", lastFournisseurId.ToString());
            Directory.CreateDirectory(folderPath);

            foreach (var fichierFromList in pieceJointeFormModelDto.ListFichiers)
            {
                try
                {
                    if (fichierFromList == null)
                    {
                        Console.WriteLine("File is null, skipping.");
                        continue;
                    }
                    var filePath = Path.Combine(folderPath, fichierFromList.Name).ToLower();
                    if (File.Exists(filePath))
                    {
                        continue;
                    }
                    using (var fileStream = fichierFromList.OpenReadStream(maxAllowedSize: 75 * 1024 * 1024)) // 75 MB limit
                    {
                        if (fileStream == null)
                        {
                            Console.WriteLine($"File stream is null for {fichierFromList.Name}. Skipping.");
                            continue;
                        }
                        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            await fileStream.CopyToAsync(stream);
                        }
                    }
                    var fileExtension = Path.GetExtension(fichierFromList.Name).ToLower();
                    Console.WriteLine(fichierFromList.Name);
                    var fichier = new Fichier
                    {
                        // NE PAS METTRE L'EXTENSION DANS LE NOM
                        Nom = fichierFromList.Name,
                        Type = fileExtension,
                        Taille = (int)fichierFromList.Size, // File size in bytes
                        DateCreation = DateTime.Now,
                        Path = Path.Combine("files", lastFournisseurId.ToString(), fichierFromList.Name).ToLower(),
                        Fournisseur = lastFournisseurId
                    };
                    fichiers.Add(fichier); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving file {fichierFromList?.Name}: {ex.Message}");
                    continue; 
                }
            }

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
                throw new Exception("Une erreur est survenue lors de la sauvegarde des fichiers dans la base de données", ex);
            }
        }

        public async Task UpdateFichierData(PieceJointeFormModel pieceJointeFormModelDto, int fournisseurID)
        {
            var fichiers = new List<Fichier>();

            // DEFINE FOLDER PATH WITH THE USER ID
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fournisseurID.ToString());

            // CHECK IF DIRECTORY DOES NOT EXIST
            /* if (!Directory.Exists(folderPath))
            {
                //Delete the existing directory and its contents
                //Directory.Delete(folderPath, true); 
                Directory.CreateDirectory(folderPath);
            } */


            // Create a new directory
            //Directory.CreateDirectory(folderPath);
            foreach (var fichierFromList in pieceJointeFormModelDto.ListFichiers)
            {
                try
                {
                    if (fichierFromList == null)
                    {
                        Console.WriteLine("File is null, skipping.");
                    }

                    var filePath = Path.Combine(folderPath, fichierFromList.Name).ToLower();
                    if (File.Exists(filePath))
                    {
                        Console.WriteLine($"File already exists: {filePath}. Skipping.");
                    }

                        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            await fichierFromList.OpenReadStream(maxAllowedSize: 75 * 1024 * 1024).CopyToAsync(stream);
                        }

                    var fileExtension = Path.GetExtension(fichierFromList.Name).ToLower();
                    Console.WriteLine(fichierFromList.Name);

                    var fichier = new Fichier
                    {
                        // NE PAS METTRE L'EXTENSION DANS LE NOM
                        Nom = fichierFromList.Name,
                        Type = fichierFromList.ContentType,
                        Taille = (int)fichierFromList.Size, // File size in bytes
                        DateCreation = DateTime.Now,
                        Path = Path.Combine("files", fournisseurID.ToString(), fichierFromList.Name, fileExtension).ToLower(),
                        Fournisseur = fournisseurID
                    };
                    //fichiers.Add(fichier);
                    _context.Fichiers.Add(fichier);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving file {fichierFromList?.Name}: {ex.Message}");
                }
            }

            try
            {
                /* if (fichiers.Count > 0)
                {
                    await _context.Fichiers.AddRangeAsync(fichiers);
                    await _context.SaveChangesAsync();
                } */
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de la sauvegarde des fichiers dans la base de données", ex);
            }
        }

        public async Task DeleteAllFichiersData(List<Fichier> listFichiers)
        {
            foreach (var fichier in listFichiers)
            {
                try
                {
                    // Construct the full file path on the server based on the stored path in the database
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fichier.Path.Replace("\\", Path.DirectorySeparatorChar.ToString()));

                    // Check if the file exists on the server
                    if (File.Exists(fullPath))
                    {
                        // Delete the file from the server
                        File.Delete(fullPath);
                    }
                    else
                    {
                        Console.WriteLine($"File not found: {fullPath}, skipping deletion from server.");
                    }

                    // Remove the file entry from the database
                    _context.Fichiers.Remove(fichier);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file {fichier.Nom}: {ex.Message}");
                    continue;
                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving changes to the database during bulk file deletion.", ex);
            }
        }

        public async Task DeleteOneFichierData(Fichier fichier)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fichier.Path.Replace("\\", Path.DirectorySeparatorChar.ToString()));
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                else
                {
                    Console.WriteLine($"File not found: {fullPath}, skipping deletion from server.");
                }
                _context.Fichiers.Remove(fichier);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the file {fichier.Nom} from the server or database.", ex);
            }
        }
    }
}
