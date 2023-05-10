

namespace ChetanSoniAssignmentAgileRecruiTech
{
    public class Event
    {
        public int? Id { get; set; }
        public string? Type { get; set; } = "event";
        public int? Uid { get; set; } = 18;
        public string? Name { get; set; }
        public string? Tagline { get; set; }
        public DateTime? Schedule { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public int? Moderator { get; set; }
        public int? Category { get; set; }
        public int? Sub_category { get; set; }
        public int? Rigor_rank { get; set; }
        public List<int?> Attendees { get; set; }
    }
}
