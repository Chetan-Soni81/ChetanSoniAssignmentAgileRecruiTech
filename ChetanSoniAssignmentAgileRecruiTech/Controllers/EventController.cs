using ChetanSoniAssignmentAgileRecruiTech.DbModels;
using ChetanSoniAssignmentAgileRecruiTech.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ChetanSoniAssignmentAgileRecruiTech.Controllers
{
    [Route("api/v3/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(int id)
        {
            var repo = new EventRepository();

            var data = repo.GetEvent(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
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
        public async Task<IActionResult> Put([FromForm] EventData eventdata)
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

            foreach (int i in eventdata.Attendees)
            {
                attendees.Add(new TblAttendee { UserId = i });
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
        [HttpDelete]
        public IActionResult Delete(int id)
        {
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
