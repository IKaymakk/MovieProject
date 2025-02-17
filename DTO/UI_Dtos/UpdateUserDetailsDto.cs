using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UI_Dtos;

public class UpdateUserDetailsDto
{
    public int id { get; set; }
    public string userName { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string mailAddress { get; set; }
}
