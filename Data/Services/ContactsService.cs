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
            var lastCoordonneId = await _context.Coordonnees.MaxAsync(f => (int?)f.IdCoordonnee);
            var contacts = new List<Contact>();
            var telephones = new List<Telephone>();
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

                var telephone = new Telephone
                {
                    Type = contactFromList.TypeTelephone,
                    NumTelephone = contactFromList.Telephone,
                    Poste = contactFromList.Poste,
                    Contact = null,
                    Coordonnee = lastCoordonneId
                };

                telephones.Add(telephone);
                contacts.Add(contact);
            }

            try
            {
                await _context.Telephones.AddRangeAsync(telephones);
                await _context.Contacts.AddRangeAsync(contacts);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de la sauvegarde des contacts", ex);
            }
        }
    }
}