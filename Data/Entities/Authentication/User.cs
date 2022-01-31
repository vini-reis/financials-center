using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Data.Entities.Authentication
{
    [CollectionName("Users")]
    public class User : MongoIdentityUser<Guid>
    {
        public User() { }

        public string LastName { get; set; }
        public string Tag { get; set; }
        public bool Active { get; set; }

    }
}
