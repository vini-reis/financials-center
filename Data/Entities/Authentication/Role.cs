using System;
using System.Collections.Generic;
using System.Text;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Data.Entities.Authentication
{
    [CollectionName("Roles")]
    public class Role : MongoIdentityRole<Guid>
    {

    }
}
