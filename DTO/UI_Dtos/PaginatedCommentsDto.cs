using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UI_Dtos;

public class PaginatedCommentsDto
{
    public List<CommentListDto> Comments { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}


public class CommentListDto
{
    public int appUserId { get; set; }
    public int movieId { get; set; }
    public string commentTitle { get; set; }
    public string commentText { get; set; }
    public string userName { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string appUserImage { get; set; }
    public DateTime createdDate { get; set; }

}


