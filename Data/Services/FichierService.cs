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

        public async Task SaveFichierData(PieceJointeFormModel pieceJointeDto, FournisseurFormModel fournisseurDto)
        {
            var piecesJointes = new Fichier
            {
                // IL FAUT REVOIR LA STRUCTURE DU DTO DE PIECESJOINTES, POTENTIELLEMENT EN CALANT LES ATTRIBUTS TAILLE, TYPE, ETC.
                Nom = pieceJointeDto.Fichier.Name,
                Type = pieceJointeDto.Fichier.ContentType,
                Taille = (int)Math.Round((double)pieceJointeDto.Fichier.Size),
                DateCreation = DateTime.Now
            };

            _context.Fichiers.Add(piecesJointes);
            await _context.SaveChangesAsync();
        }
    }
}
