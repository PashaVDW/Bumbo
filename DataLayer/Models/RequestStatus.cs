using bumbo.Models;
using System.ComponentModel.DataAnnotations;

public class RequestStatus
{
    [Key, StringLength(100)]
    public string RequestStatusName { get; set; }

    [Required]
    public int RequestToBranchId { get; set; }

    [Required]
    public int BranchId { get; set; }
}
