using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserCrudAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Hobbies { get; set; }
        public string ImageUpload { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
