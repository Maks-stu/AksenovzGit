using ClassLibrary.Classes;

namespace Application.Services.Interfaces
{
    public interface IPartnerService
    {
        Partner CreatePartner(Partner partner);
        List<Partner> GetPartnersAll();
        Partner GetPartnerById(int id);
        Partner UpdatePartner(int id, Partner partner);
        bool DeletePartner(int id);
    }
}