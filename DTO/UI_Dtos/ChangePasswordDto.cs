using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UI_Dtos;

public class ChangePasswordDto
{
    public int id { get; set; }
    public string oldPassword { get; set; }
    public string newPassword { get; set; }
    public string confirmNewPassword { get; set; }
}
