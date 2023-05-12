using ChetanSoniAssignmentAgileRecruiTech.DbModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ChetanSoniAssignmentAgileRecruiTech.Repository
{
    public class NudgeRepository
    {
        public Nudge GetNudge(int id)
        {
            try
            {
                using (var context = new DeepTechAssignmentDBContext())
                {
                    var nudge = context.TblNudges.SingleOrDefault(x => x.NudgeId == id);

                    if(nudge == null)
                    {
                        return null;
                    }

                    var nudgedata = new Nudge()
                    {
                        Id = id,
                        EventId = nudge.EventId ?? 0,
                        Title = nudge.Title,
                        Schedule = nudge.Schedule,
                        Description = nudge.Description,
                        InvitationMessage = nudge.InvitationMessage,

                    };

                    if (File.Exists(nudge.Image))
                    {
                        nudgedata.Image = File.ReadAllBytes(nudge.Image);
                    }

                    if (File.Exists(nudge.Icon))
                    {
                        nudgedata.Icon = File.ReadAllBytes(nudge.Icon);
                    }

                    return nudgedata;
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<Nudge> GetLatestNudges(int limit, int page)
        {
            try
            {
                using (var context = new DeepTechAssignmentDBContext())
                {
                    var nudges = context.TblNudges.OrderByDescending(a => a.Schedule).ToList();

                    int i = limit * (page - 1);
                    if (i < nudges.Count)
                    {
                        nudges = nudges.GetRange(i, (nudges.Count - i) > limit ? limit : (nudges.Count - i));
                    }
                    else
                    {
                        nudges = new List<TblNudge>();
                    }

                    var result = new List<Nudge>();

                    foreach (var nudge in nudges)
                    {
                        var nudgedata = new Nudge()
                        {
                            Id = nudge.NudgeId,
                            EventId = nudge.EventId ?? 0,
                            Title = nudge.Title,
                            Schedule = nudge.Schedule,
                            Description = nudge.Description,
                            InvitationMessage = nudge.InvitationMessage,

                        };

                        if (File.Exists(nudge.Image))
                        {
                            nudgedata.Image = File.ReadAllBytes(nudge.Image);
                        }

                        if (File.Exists(nudge.Icon))
                        {
                            nudgedata.Icon = File.ReadAllBytes(nudge.Icon);
                        }

                        result.Add(nudgedata);   
                    }

                    return result;
                }
            }
            catch (Exception)
            {
                return new List<Nudge>();
                throw;
            }
        }

        public bool CreateNudge(TblNudge newnudge)
        {
            try
            {
                using (var context = new DeepTechAssignmentDBContext())
                {
                    context.TblNudges.Add(newnudge);
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

        public bool UpdateNudge(TblNudge newnudge, int id)
        {
            try
            {
                using (var context = new DeepTechAssignmentDBContext())
                {
                    var nudge = context.TblNudges.SingleOrDefault(s => s.NudgeId == id);

                    if (nudge == null)
                    {
                        return false;
                    }

                    nudge.EventId = newnudge.EventId ?? nudge.EventId;
                    nudge.Title = newnudge.Title ?? nudge.Title;
                    nudge.Image = newnudge.Image ?? nudge.Image;
                    nudge.Schedule = newnudge.Schedule ?? nudge.Schedule;
                    nudge.Description = newnudge.Description ?? nudge.Description;
                    nudge.Icon = newnudge.Icon ?? nudge.Icon;
                    nudge.InvitationMessage = newnudge.InvitationMessage ?? nudge.InvitationMessage;

                    if (File.Exists(nudge.Image) && newnudge.Image != null)
                    {
                        File.Delete(nudge.Image);
                    }

                    if (File.Exists(nudge.Icon) && newnudge.Icon != null)
                    {
                        File.Delete(nudge.Icon);
                    }

                    context.TblNudges.Update(nudge);
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

        public bool DeleteNudge(int id)
        {
            try
            {
                using (var context = new DeepTechAssignmentDBContext())
                {
                    var nudge = context.TblNudges.SingleOrDefault(x => x.NudgeId == id);

                    if (nudge == null)
                    {
                        return false;
                    }

                    if (nudge.Icon != null && File.Exists(nudge.Icon))
                    {
                        File.Delete(nudge.Icon);
                    }

                    if (nudge.Image != null && File.Exists(nudge.Image))
                    {
                        File.Delete(nudge.Image);
                    }

                    context.TblNudges.Remove(nudge);

                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
