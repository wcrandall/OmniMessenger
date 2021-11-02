using System.Collections.Generic;
using OmniMessengerAPI.Data.Interfaces;
using OmniMessengerAPI.Dto;

namespace OmniMessengerAPI.Data
{
    public class ContactInfoRepository : IContactInfoRepository
    {
        public ContactInfoRepository()
        {
            this.CreateContactInfoTable();
        }

        private void CreateContactInfoTable()
        {
            string command = @"
                CREATE TABLE IF NOT EXISTS contacts
                (
                    contact_id INTEGER PRIMARY KEY,
                    first_name TEXT,
                    last_name TEXT,
                    email_address TEXT,
                    phone_number TEXT
                )";
            Connection.ExecuteNonQuery(command);
        }

        public void InsertContactInfo(ContactInfo contactInfo)
        {
            string command = @"
                INSERT INTO contacts(first_name, last_name, email_address, phone_number)
                VALUES ($first_name, $last_name, $email_address, $phone_number)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                ["$first_name"] = contactInfo.FirstName,
                ["$last_name"] = contactInfo.LastName,
                ["$email_address"] = contactInfo.Email,
                ["$phone_number"] = contactInfo.PhoneNumber
            };

            Connection.ExecuteNonQuery(command, parameters);
        }

        public List<ContactInfo> GetContactInfos()
        {
            string command = @"
                SELECT contact_id,
                       first_name,
                       last_name,
                       email_address,
                       phone_number
                FROM contacts";

            Dictionary<int, object[]> objects = Connection.ExecuteReader(command);

            List<ContactInfo> contactInfos = new List<ContactInfo>();

            foreach (KeyValuePair<int, object[]> entry in objects)
            {
                contactInfos.Add(new ContactInfo
                {
                    FirstName = entry.Value[1].ToString(),
                    LastName = entry.Value[2].ToString(),
                    Email = entry.Value[3].ToString(),
                    PhoneNumber = entry.Value[4].ToString()
                });
            }

            return contactInfos;
        }
    }
}