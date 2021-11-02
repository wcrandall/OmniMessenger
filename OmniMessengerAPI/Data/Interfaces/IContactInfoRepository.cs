using System.Collections.Generic;
using OmniMessengerAPI.Dto;

namespace OmniMessengerAPI.Data.Interfaces
{
    public interface IContactInfoRepository
    {
        void InsertContactInfo(ContactInfo contactInfo);
        List<ContactInfo> GetContactInfos();
    }
}