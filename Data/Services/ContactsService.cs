using Microsoft.EntityFrameworkCore;
using Portail_OptiVille.Data.FormModels;
using Portail_OptiVille.Data.Models;

namespace Portail_OptiVille.Data.Services
{
    public class ContactsService
    {
        private readonly A2024420517riGr1Eq6Context _context;

        public ContactsService(A2024420517riGr1Eq6Context context)
        {
            _context = context;
        }

        public async Task SaveContactsData(ContactHosterFormModel contactHosterFormModelDto)
        {
            var lastFournisseurId = await _context.Fournisseurs.MaxAsync(f => (int?)f.IdFournisseur);
            foreach (var contactFromList in contactHosterFormModelDto.ContactList)
            {
                var contact = new Contact
                {
                    Prenom = contactFromList.Prenom,
                    Nom = contactFromList.Nom,
                    Fonction = contactFromList.Fonction,
                    AdresseCourriel = contactFromList.AdresseCourriel,
                    Fournisseur = lastFournisseurId 
                };

                try
                {
                    _context.Contacts.Add(contact);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Une erreur est survenue lors de la sauvegarde du contact", ex);
                }

                var lastContactId = await _context.Contacts.MaxAsync(f => (int?)f.IdContact);
                var telephone = new Telephone
                {
                    Type = contactFromList.TypeTelephone,
                    NumTelephone = contactFromList.Telephone,
                    Poste = contactFromList.Poste,
                    Contact = lastContactId,
                    Coordonnee = null
                };

                try
                {
                    _context.Telephones.Add(telephone);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Une erreur est survenue lors de la sauvegarde du téléphone", ex);
                }
            }
        }
    }
}