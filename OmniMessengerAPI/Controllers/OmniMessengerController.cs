using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OmniMessengerAPI.Data.Interfaces;
using OmniMessengerAPI.Dto;

namespace OmniMessengerAPI.Controllers
{
    [Route("message")]
    [ApiController]
    public class OmniMessengerController:ControllerBase
    {
        private IContactInfoRepository contactInfoRepository;
        public OmniMessengerController()
        {
            contactInfoRepository.InsertContactInfo(new ContactInfo{
                FirstName = "noname",
                LastName = "nolastname",
                Email = "none@none.com",
                PhoneNumber = "38838383883"
            });    
        }

        [HttpGet]
        public ActionResult<List<ContactInfo>> GetContactInfos(){
            
            return Ok();
        }

    }
}