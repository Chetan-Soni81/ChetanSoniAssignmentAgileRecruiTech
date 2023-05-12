using ChetanSoniAssignmentAgileRecruiTech.DbModels;
using ChetanSoniAssignmentAgileRecruiTech.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ChetanSoniAssignmentAgileRecruiTech.Controllers
{
    [Route("api/v3/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(int id, string type, int limit, int page)
        {
            var repo = new EventRepository();

            if (string.IsNullOrEmpty(type))
            {
                var data = repo.GetEvent(id);

                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data);
            } else
            {
                var data = repo.GetLatestEvents(limit,page);

                if(data==null)
                {
                    return NotFound();
                }

                return Ok(data);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] EventData eventdata)
        {
            string path = await UploadFile(eventdata.Image);

            var newevent = new TblEvent
            {
                Type = eventdata.Type,
                UserId = eventdata.Uid,
                Name = eventdata.Name,
                Tagline = eventdata.Tagline,
                Schedule = eventdata.Schedule,
                Description = eventdata.Description,
                Image = path,
                Moderator = eventdata.Moderator,
                Category = eventdata.Category,
                SubCategory = eventdata.Sub_category,
                RigorRank = eventdata.Rigor_rank
            };

            List<TblAttendee> attendees = new List<TblAttendee>();

            if (eventdata.Attendees != null)
            {
                foreach (int i in eventdata.Attendees)
                {
                    attendees.Add(new TblAttendee { UserId = i });
                }
            }

            var repo = new EventRepository();
            var success = repo.CreateEvent(newevent, attendees);

            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] EventData eventdata, int id)
        {
            string? path = null;

            if (eventdata.Image != null) path = await UploadFile(eventdata.Image);

            var newevent = new TblEvent
            {
                EventId = id,
                Name = eventdata.Name,
                Tagline = eventdata.Tagline,
                Schedule = eventdata.Schedule,
                Description = eventdata.Description,
                Image = path,
                Moderator = eventdata.Moderator,
                Category = eventdata.Category,
                SubCategory = eventdata.Sub_category,
                RigorRank = eventdata.Rigor_rank
            };

            var repo = new EventRepository();
            var success = repo.UpdateEvent(newevent, id);

            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var repo = new EventRepository();

            var success = repo.DeleteEvent(id);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        private async Task<string> UploadFile(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"Upload\Image", special + "_" + file.FileName);

            using (FileStream fs = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            var filename = special + "_" + file.FileName;

            return Path.Combine(@"Upload\Image", filename);
        }
    }
}
