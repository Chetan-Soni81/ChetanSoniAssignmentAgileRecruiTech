namespace ChetanSoniAssignmentAgileRecruiTech
{
    public class Nudge
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string? Title { get; set; }
        public byte[]? Image { get; set; }
        public DateTime? Schedule { get; set; }
        public string? Description { get; set; }
        public byte[]? Icon { get; set; }
        public string? InvitationMessage { get; set;}
    }
}
