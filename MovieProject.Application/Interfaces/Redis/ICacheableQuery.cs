using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Interfaces.Redis;

public interface ICacheableQuery
{
    string CacheKey { get;}
    TimeSpan? CacheTime { get; }
}