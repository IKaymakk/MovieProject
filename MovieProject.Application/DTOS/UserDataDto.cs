﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.DTOS
{
    public class UserDataDto
    {
        public int AppUserId { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public bool IsExist { get; set; }
    }
}
