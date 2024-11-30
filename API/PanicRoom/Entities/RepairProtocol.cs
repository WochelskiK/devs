namespace PanicRoom.Entities
{
    public class RepairProtocol
    {
        public int Id { get; set; } // Primary Key
        public int IssueID { get; set; } // Foreign Key to Issue
        public Issue Issue { get; set; } // Navigation property
    }
}
