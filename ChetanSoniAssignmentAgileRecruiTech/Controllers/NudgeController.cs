using ChetanSoniAssignmentAgileRecruiTech.DbModels;
using ChetanSoniAssignmentAgileRecruiTech.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace ChetanSoniAssignmentAgileRecruiTech.Controllers
{
    [Route("api/v3/nudges")]
    [ApiController]
    public class NudgeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(int id, string type, int limit, int page)
        {
            var repo = new NudgeRepository();

            if(type == null)
            {
                var data = repo.GetNudge(id);

                if(data == null)
                {
                    return NotFound();
                }

                return Ok(data);
            }

            var nudges  = repo.GetLatestNudges(limit, page);

            return Ok(nudges);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] NudgeData nudgeData)
        {
            string imagepath = await UploadImage(nudgeData.Image);
            string iconpath = await UploadImage(nudgeData.Icon);

            TblNudge newnudge = new TblNudge()
            {
                EventId = nudgeData.EventId,
                Title = nudgeData.Title,
                Image = imagepath,
                Schedule = nudgeData.Schedule,
                Description = nudgeData.Description,
                Icon= iconpath,
                InvitationMessage= nudgeData.InvitationMessage
            };

            var repo = new NudgeRepository();

            var success = repo.CreateNudge(newnudge);

            if(success)
            {
                return Ok();
            }

            return BadRequest();
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] NudgeData nudgeData, int id) {

            string? imagepath = null;
            string? iconpath = null;

            if(nudgeData.Image != null) imagepath = await UploadImage(nudgeData.Image);
            if(nudgeData.Icon != null) iconpath = await UploadImage(nudgeData.Icon);

            TblNudge newnudge = new TblNudge()
            {
                EventId = nudgeData.EventId,
                Title = nudgeData.Title,
                Image = imagepath,
                Schedule = nudgeData.Schedule,
                Description = nudgeData.Description,
                Icon = iconpath,
                InvitationMessage = nudgeData.InvitationMessage
            };

            var repo = new NudgeRepository();

            var success = repo.UpdateNudge(newnudge, id);

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
            var repo = new NudgeRepository();

            var success = repo.DeleteNudge(id);

            if (success)
            {
                return Ok();
            }

            return NotFound();
        }

        private async Task<string?> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"Upload\Image" , $"{special}_{file.FileName}");

            using (FileStream fs = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return Path.Combine(@"Upload\Image", $"{special}_{file.FileName}");
        }
    }
}
