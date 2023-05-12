namespace ChetanSoniAssignmentAgileRecruiTech
{
    public class NudgeData
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string? Title { get; set; }
        public IFormFile? Image { get; set; }
        public DateTime? Schedule { get; set; }
        public string? Description { get; set; }
        public IFormFile? Icon { get; set; }
        public string? InvitationMessage { get; set; }
    }
}
