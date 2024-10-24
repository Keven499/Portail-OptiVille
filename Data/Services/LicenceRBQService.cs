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
            var licenceRBQdata = await _context.Licencerbqs.FindAsync(licenceRBQFormModelDto.NumeroLicence);
            if(licenceRBQdata == null)
            {
                if (licenceRBQFormModelDto.NumeroLicence != null)
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
            else
            {
                var currentCategories = await _context.Categorierbqs.Where(c => licenceRBQdata.IdCategorieRbqs.Select(id => id.IdLicenceRbqs).Contains(c.IdLicenceRbqs)).ToListAsync();
                licenceRBQdata.Fournisseur = licenceRBQdata.Fournisseur;
                licenceRBQdata.IdLicenceRbq = licenceRBQFormModelDto.NumeroLicence;
                licenceRBQdata.Statut = licenceRBQFormModelDto.StatutLicence;
                licenceRBQdata.Type = licenceRBQFormModelDto.TypeLicence;

                foreach (var codeSousCategorie in licenceRBQFormModelDto.CodeSousCategorie)
                {
                    var categorieRBq = await _context.Categorierbqs
                        .FirstOrDefaultAsync(p => p.CodeSousCategorie == codeSousCategorie);

                    if (categorieRBq != null && !licenceRBQdata.IdCategorieRbqs.Contains(categorieRBq))
                    {
                        licenceRBQdata.IdCategorieRbqs.Add(categorieRBq);
                    }
                }
            
                foreach (var existingCategory in currentCategories.ToList())
                {
                    if (!licenceRBQFormModelDto.CodeSousCategorie.Contains(existingCategory.CodeSousCategorie))
                    {
                        var categoryToRemove = licenceRBQdata.IdCategorieRbqs
                            .FirstOrDefault(c => c.IdLicenceRbqs == existingCategory.IdLicenceRbqs);
                        if (categoryToRemove != null)
                        {
                            licenceRBQdata.IdCategorieRbqs.Remove(categoryToRemove);
                        }
                    }
                }

            _context.Licencerbqs.Update(licenceRBQdata);
            await _context.SaveChangesAsync();
            }
        }
    }
}
