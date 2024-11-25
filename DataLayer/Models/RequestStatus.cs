using bumbo.Models;
using System.ComponentModel.DataAnnotations;

public class RequestStatus
{
    [Key, StringLength(100)]
    public string RequestStatusName { get; set; }
    public ICollection<BranchRequestsEmployee> BranchRequestsEmployee { get; set; }
}
