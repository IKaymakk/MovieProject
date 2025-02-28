using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UI_Dtos;

public class CreateAppUserDto
{
    public string username { get; set; }     // Değiştirildi
    public string firstname { get; set; }    // Değiştirildi
    public string lastname { get; set; }     // Değiştirildi
    public string email { get; set; }        // Değiştirildi
    public string image { get; set; }
    public string password { get; set; }
    public int appRoleId { get; set; } = 1;
}
