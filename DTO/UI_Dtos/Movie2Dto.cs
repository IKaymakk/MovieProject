using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UI_Dtos
{
    public class Movie2Dto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string image2 { get; set; }
        public string description { get; set; }
        public string description2 { get; set; }
        public string director { get; set; }
        public string writer { get; set; }
        public string trailer { get; set; }
        public string hashTag { get; set; }
        public decimal score { get; set; }
        public int year { get; set; }
        public int runTime { get; set; }
        public decimal imbdScore { get; set; }
        public DateTime releaseDate { get; set; }
        public DateTime createdDate { get; set; }
    }
}
