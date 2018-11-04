﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Data.Seed
{
    [Serializable]
    public class UserDto
    {
        public string Login { get; set; }
        public string Password { get;set; }
        public string Name { get; set; }
    }
}
