using Microsoft.EntityFrameworkCore;
using Portail_OptiVille.Data.FormModels;
using Portail_OptiVille.Data.Models;

namespace Portail_OptiVille.Data.Services
{
    public class LicenceRBQService
    {
        private readonly A2024420517riGr1Eq6Context _context;

        public LicenceRBQService(A2024420517riGr1Eq6Context context)
        {
            _context = context;
        }

        public async Task SaveLicenceRBQData(LicenceRBQFormModel licenceRBQFormModelDto)
        {
            var lastFournisseurId = await _context.Fournisseurs.MaxAsync(f => (int?)f.IdFournisseur);
            var licenceRBQ = new Licencerbq
            {
                IdLicenceRbq = licenceRBQFormModelDto.NumeroLicence?.Replace("-", string.Empty),
                Type = licenceRBQFormModelDto.TypeLicence,
                Statut = licenceRBQFormModelDto.StatutLicence,
                Fournisseur = lastFournisseurId
            };

            try
            {
                _context.Licencerbqs.Add(licenceRBQ);
                await _context.SaveChangesAsync();

                foreach (var codeSousCategorie in licenceRBQFormModelDto.CodeSousCategorie)
                {
                    var categorieRBq = await _context.Categorierbqs
                        .FirstOrDefaultAsync(p => p.CodeSousCategorie == codeSousCategorie);

                    if (categorieRBq != null && !licenceRBQ.IdCategorieRbqs.Contains(categorieRBq))
                    {
                        licenceRBQ.IdCategorieRbqs.Add(categorieRBq);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de la sauvegarde de la licence RBQ", ex);
            }
        }
    }
}
