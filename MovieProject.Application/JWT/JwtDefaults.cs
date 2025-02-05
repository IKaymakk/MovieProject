using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.JWT
{
    public class JwtDefaults
    {
        public const string? Issuer = "https://localhost:44358/";
        public const string? Audience = "https://localhost:64625/";
        public const string? Key = "jwtsecuritytoken315231jwtsecuritytoken";
        public const int ExpireTime = 1;
    }
}
