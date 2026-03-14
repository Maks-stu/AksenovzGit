using System.Numerics;
using System.Xml.Linq;
using Application.Services.Interfaces;
using ClassLibrary.Classes;

namespace Application.Services.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly ApplicationContext _context;

        public PartnerService()
        {
            _context = new ApplicationContext();
        }
        public Partner CreatePartner(Partner partner)
        {
            var entity = partner;
            entity.Id = _context.Partners.Any() ? _context.Partners.Max(l => l.Id) + 1 : 1;

            _context.Partners.Add(entity);
            _context.SaveChanges();
            Console.WriteLine("Create Partner " + entity.Id);
            return entity;
        }
        public Partner? GetPartnerById(int id)
        {
            var entity = _context.Partners.Find(id);
            if (entity == null) return null;
            Console.WriteLine("Get Partner by ID " + entity.Id);
            return entity;
        }
        public List<Partner> GetPartnersAll()
        {
            Console.WriteLine("Get All Partner ");
            return _context.Partners.ToList();
        }
        public Partner UpdatePartner(int id, Partner partner)
        {
            var entity = _context.Partners.Find(id) ?? throw new Exception("Partner not found");
            entity.Type = partner.Type;
            entity.Name = partner.Name;
            entity.LegalAddress = partner.LegalAddress;
            entity.Inn = partner.Inn;
            entity.DirectorName = partner.DirectorName;
            entity.Phone = partner.Phone;
            entity.Email = partner.Email;
            entity.Logo = partner.Logo;
            entity.Rating = partner.Rating;

            _context.SaveChanges();
            Console.WriteLine("Update Partner " + entity.Id);
            return entity;
        }
        public bool DeletePartner(int id)
        {
            var entity = _context.Partners.Find(id);
            if (entity == null) return false;
            _context.Partners.Remove(entity);
            _context.SaveChanges();
            Console.WriteLine("Delete Partner " + entity.Id);
            return true;
        }
    }
}