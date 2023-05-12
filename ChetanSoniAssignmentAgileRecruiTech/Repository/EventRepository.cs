

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

        public bool UpdateEvent(TblEvent eventdata, int id)
        {
            try
            {
                using (var context = new DeepTechAssignmentDBContext())
                {
                    var oldevent = context.TblEvents.SingleOrDefault(x => x.EventId == id);

                    oldevent.Name = eventdata.Name ?? oldevent.Name;
                    oldevent.Tagline = eventdata.Tagline ?? oldevent.Tagline;
                    oldevent.Image = eventdata.Image ?? oldevent.Image;
                    oldevent.Schedule = eventdata.Schedule ?? oldevent.Schedule;
                    oldevent.Description = eventdata.Description ?? oldevent.Description;
                    oldevent.Moderator = eventdata.Moderator ?? oldevent.Moderator;
                    oldevent.Category = eventdata.Category ?? oldevent.Category;
                    oldevent.SubCategory = eventdata.SubCategory ?? oldevent.SubCategory;
                    oldevent.RigorRank = eventdata.RigorRank ?? oldevent.RigorRank;

                    if (File.Exists(oldevent.Image) && eventdata.Image != null)
                    {
                        File.Delete(oldevent.Image);
                    }

                    context.TblEvents.Update(oldevent);
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

        public bool DeleteEvent(int id)
        {
            try
            {
                using (var context = new DeepTechAssignmentDBContext())
                {
                    var evt = context.TblEvents.SingleOrDefault(x => x.EventId == id);

                    if (evt.Image != null && File.Exists(evt.Image))
                    {
                        File.Delete(evt.Image);
                    }

                    context.TblEvents.Remove(evt);
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

                    if (File.Exists(eventdata.Image))
                    {
                        data.Image = File.ReadAllBytes(eventdata.Image);
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

        public List<Event> GetLatestEvents(int limit, int page)
        {
            try
            {
                using (var context = new DeepTechAssignmentDBContext())
                {
                    var events = context.TblEvents.OrderByDescending(a => a.Schedule).ToList();

                    int i = limit * (page - 1);
                    if (i < events.Count)
                    {
                        events = events.GetRange(i, (events.Count - i) > limit ? limit : (events.Count - i));
                    } else
                    {
                        events = new List<TblEvent>();
                    }




                    var result = new List<Event>();

                    foreach (var e in events)
                    {
                        var data = new Event()
                        {
                            Id = e.EventId,
                            Type = e.Type,
                            Uid = e.UserId,
                            Name = e.Name,
                            Tagline = e.Tagline,
                            Description = e.Description,
                            Schedule = e.Schedule,
                            Moderator = e.Moderator,
                            Category = e.Category,
                            Sub_category = e.SubCategory,
                            Rigor_rank = e.RigorRank
                        };

                        if (File.Exists(e.Image))
                        {
                            data.Image = File.ReadAllBytes(e.Image);
                        }

                        data.Attendees = e.TblAttendees.Select(a => a.UserId).ToList();

                        result.Add(data);
                    }

                    return result;
                }
            }
            catch (Exception)
            {
                return new List<Event>();
                throw;
            }
        }
    }
}
