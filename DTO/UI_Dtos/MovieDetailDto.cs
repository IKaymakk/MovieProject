using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UI_Dtos
{
    public class MovieDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Trailer { get; set; }
        public string HashTag { get; set; }
        public decimal Score { get; set; }
        public decimal ImbdScore { get; set; }
        public List<string> Genres { get; set; } 
        public int RunTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReleaseDate { get; set; }

      
    }

}
