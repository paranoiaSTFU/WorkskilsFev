using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiOne.DB;

namespace ApiOne.Controllers
{
    public class HotelCommentsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/HotelComments
        public IQueryable<HotelComment> GetHotelComments()
        {
            return db.HotelComments;
        }
        //localhost:50016/api/getHotelCommentZXC?hotelidZXC=4
        [Route("api/getHotelCommentZXC")]
        public IHttpActionResult GetHotelComments(int hotelidZXC)
        {
            var hotelComments = db.HotelComments.ToList().Where(p => p.Hotelid == hotelidZXC).Select(c=>new {c.Hotelid,c.Author,c.CreationDate,c.Text }).ToList();
            return Ok(hotelComments);
        }

        // GET: api/HotelComments/5
        [ResponseType(typeof(HotelComment))]
        public IHttpActionResult GetHotelComment(int id)
        {
            HotelComment hotelComment = db.HotelComments.Find(id);
            if (hotelComment == null)
            {
                return NotFound();
            }

            return Ok(hotelComment);
        }

        // PUT: api/HotelComments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHotelComment(int id, HotelComment hotelComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotelComment.ID)
            {
                return BadRequest();
            }

            db.Entry(hotelComment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelCommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/HotelComments
        [ResponseType(typeof(HotelComment))]
        public IHttpActionResult PostHotelComment(HotelComment hotelComment)
        {
            hotelComment.CreationDate = DateTime.Now;
            if (string.IsNullOrWhiteSpace(hotelComment.Author) || hotelComment.Author.Length > 100)
                ModelState.AddModelError("Author", "Author is ..... ну ты понял");
            if(string.IsNullOrWhiteSpace(hotelComment.Text))
                ModelState.AddModelError("Text", "Text is ..... ну ты понял");
            if (!(db.Hotels.ToList().FirstOrDefault(p=>p.ID==hotelComment.Hotelid) is Hotel))
                ModelState.AddModelError("Hotelid", "Hotelid is ..... ну ты понял");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HotelComments.Add(hotelComment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hotelComment.ID }, hotelComment);
        }

        // DELETE: api/HotelComments/5
        [ResponseType(typeof(HotelComment))]
        public IHttpActionResult DeleteHotelComment(int id)
        {
            HotelComment hotelComment = db.HotelComments.Find(id);
            if (hotelComment == null)
            {
                return NotFound();
            }

            db.HotelComments.Remove(hotelComment);
            db.SaveChanges();

            return Ok(hotelComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HotelCommentExists(int id)
        {
            return db.HotelComments.Count(e => e.ID == id) > 0;
        }
    }
}