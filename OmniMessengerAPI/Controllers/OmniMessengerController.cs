using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OmniMessengerAPI.Dto;

namespace OmniMessengerAPI.Controllers
{
    [Route("message")]
    [ApiController]
    public class OmniMessengerController:ControllerBase
    {
        private readonly List<ContactInfo> contactInfos = new List<ContactInfo>{
            new ContactInfo{
                FirstName = "nofirstname",
                LastName = "nolastname",
                PhoneNumber = "",
                Email = ""
            }
        };

        [HttpGet]
        public ActionResult<List<ContactInfo>> GetContactInfos(){
            return Ok(contactInfos);
        }
    }
}