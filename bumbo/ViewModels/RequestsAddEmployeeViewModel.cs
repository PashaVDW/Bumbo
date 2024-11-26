namespace bumbo.ViewModels
{
    public class RequestsAddEmployeeViewModel
    {
        public List<Branch> AllBranches { get; set; }
        public string SearchTerm { get; set; }
        public int RequestId { get; set; }
        public string PreviousPage { get; set; }
        public RequestsUpdateViewModel RequestsVM { get; set; }
    }
}
