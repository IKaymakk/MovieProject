using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UI_Dtos;

public class CreateCommentDto
{
    public string commentTitle { get; set; }
    public string commentText { get; set; }
    public int movieId { get; set; }
    public int appUserId { get; set; }

}
