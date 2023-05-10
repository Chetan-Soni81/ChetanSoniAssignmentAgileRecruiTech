

using ChetanSoniAssignmentAgileRecruiTech.DbModels;
using System.Diagnostics.Tracing;
using System.Security.Cryptography.X509Certificates;

namespace ChetanSoniAssignmentAgileRecruiTech.Repository
{
    public class EventRepository
    {
        public bool CreateEvent(TblEvent eventdata, List<TblAttendee> attendees)
        {
            try
            {
                using (var context = new DeepTechAssignmentDBContext())
                {
                    context.TblEvents.Add(eventdata);
                    context.SaveChanges();

                    int id = eventdata.EventId;

                    foreach (TblAttendee a in attendees)
                    {
                        a.EventId = id;
                    }

                    context.TblAttendees.AddRange(attendees);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public Event GetEvent(int id)
        {
            try
            {
                using (var context = new DeepTechAssignmentDBContext())
                {
                    var eventdata = context.TblEvents.FirstOrDefault(e => e.EventId == id);

                    if (eventdata == null)
                    {
                        return null;
                    }

                    var data = new Event()
                    {
                        Id = eventdata.EventId,
                        Type = eventdata.Type,
                        Uid = eventdata.UserId,
                        Name = eventdata.Name,
                        Tagline = eventdata.Tagline,
                        Description = eventdata.Description,
                        Schedule = eventdata.Schedule,
                        Moderator = eventdata.Moderator,
                        Category = eventdata.Category,
                        Sub_category = eventdata.SubCategory,
                        Rigor_rank = eventdata.RigorRank
                    };

                    if (System.IO.File.Exists(eventdata.Image))
                    {
                        data.Image = System.IO.File.ReadAllBytes(eventdata.Image);
                    }

                    data.Attendees = eventdata.TblAttendees.Select(a => a.UserId).ToList();

                    return data;
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }
}
