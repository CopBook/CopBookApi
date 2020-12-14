using CopBookApi.Interfaces.Api.Cop;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopBookApi.Models.Api
{
    public class Cop : ICop
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BadgeId { get; set; }
    }
}
