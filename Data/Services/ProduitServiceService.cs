using Microsoft.EntityFrameworkCore;
using Portail_OptiVille.Data.FormModels;
using Portail_OptiVille.Data.Models;

namespace Portail_OptiVille.Data.Services
{
    public class ProduitServiceService
    {
        private readonly A2024420517riGr1Eq6Context _context;

        public ProduitServiceService(A2024420517riGr1Eq6Context context)
        {
            _context = context;
        }

        public async Task SaveProduitServiceData(ProduitServiceFormModel produitServiceFormModelDto)
        {   
            var lastFournisseurId = await _context.Fournisseurs.MaxAsync(f => (int?)f.IdFournisseur);
            var fournisseur = await _context.Fournisseurs
                .Include(f => f.IdProduitServices)
                .FirstOrDefaultAsync(f => f.IdFournisseur == lastFournisseurId);

            if (fournisseur == null)
            {
                throw new Exception("Fournisseur not found.");
            }

            try
            {
                fournisseur.DetailSpecification = produitServiceFormModelDto.Message;
                var selectedProduitIds = produitServiceFormModelDto.SousProduitSelected.Keys;
                var produitServices = await _context.Produitservices
                    .Where(p => selectedProduitIds.Contains(p.CodeUnspsc))
                    .ToListAsync();
                foreach (var produitService in produitServices)
                {
                    fournisseur.IdProduitServices.Add(produitService);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de la sauvegarde des produits et services", ex);
            }
        }
    }
}
