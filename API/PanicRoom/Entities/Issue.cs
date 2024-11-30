namespace PanicRoom.Entities
{
    public class Issue
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get;set ; } = string.Empty;
        public string Title {  get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AdditionalInformations { get; set; } = string.Empty;
        public int CategoryID { get; set; }
        public IssueStatus IssueStatusEnum { get; set; }
        public IssuePriority IssuePriorityEnum { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get;set ; }
        public int? UserAssignedID { get; set; }
        public User? UserAssigned { get; set; }
        public int? ReportedById { get; set; }
        public User? ReportedBy { get; set; }
        public int? ResolvedById { get; set; }
        public User? ResolvedBy { get; set; }


    }
    public enum IssueStatus
    {
        Open,
        InProgress,
        Resolved,
        Closed
    }

    public enum IssuePriority
    {
        Low,
        High,
        Medium
    }
}
