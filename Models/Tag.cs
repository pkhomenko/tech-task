using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid PeopleAccountId { get; set; }
        public PeopleAccount PeopleAccount { get; set; }
    }
}
