﻿using Microsoft.EntityFrameworkCore;
using Portail_OptiVille.Data.FormModels;
using Portail_OptiVille.Data.Models;

namespace Portail_OptiVille.Data.Services
{
    public class CoordonneeService
    {
        private readonly A2024420517riGr1Eq6Context _context;

        public CoordonneeService(A2024420517riGr1Eq6Context context)
        {
            _context = context;
        }

        public async Task SaveCoordonneeData(CoordonneeFormModel coordonneeFormModelDto)
        {
            var lastFournisseurId = await _context.Fournisseurs.MaxAsync(f => (int?)f.IdFournisseur);
            var lastCoordonneId = await _context.Coordonnees.MaxAsync(f => (int?)f.IdCoordonnee);
            var coordonnee = new Coordonnee
            {
                NoCivique = coordonneeFormModelDto.NoEntreprise,
                Rue = coordonneeFormModelDto.RueEntreprise,
                Bureau = coordonneeFormModelDto.BureauEntreprise,
                Ville = coordonneeFormModelDto.VilleEntreprise,
                Province = coordonneeFormModelDto.ProvinceEntreprise,
                CodePostal = coordonneeFormModelDto.CodePostalEntreprise,
                CodeRegionAdministrative = coordonneeFormModelDto.RegionAdmEntreprise?.Substring(coordonneeFormModelDto.RegionAdmEntreprise.IndexOf('(')
                                                                                     + 1, coordonneeFormModelDto.RegionAdmEntreprise.IndexOf(')')
                                                                                     - coordonneeFormModelDto.RegionAdmEntreprise.IndexOf('(') - 1),
                RegionAdministrative = coordonneeFormModelDto.RegionAdmEntreprise?.Substring(coordonneeFormModelDto.RegionAdmEntreprise.IndexOf(' ') + 1),
                SiteInternet = coordonneeFormModelDto.SiteWebEntreprise,
                Fournisseur = lastFournisseurId
            };

            var telephones = new List<Telephone>();
            foreach (var telephoneFromList in coordonneeFormModelDto.PhoneList)
            {
                var telephone = new Telephone
                {
                    Type = telephoneFromList.TypeTelEntreprise,
                    NumTelephone = telephoneFromList.NoTelEntreprise,
                    Poste = telephoneFromList.PosteTelEntreprise,
                    Contact = null,
                    Coordonnee = lastCoordonneId
                };

                telephones.Add(telephone);
            }

            try
            {
                await _context.Telephones.AddRangeAsync(telephones);
                _context.Coordonnees.Add(coordonnee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de la sauvegarde des coordonnées", ex);
            }
        }
    }
}