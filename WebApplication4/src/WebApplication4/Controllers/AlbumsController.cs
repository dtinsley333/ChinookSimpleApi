using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using WebApplication4.Models;
using System.Collections;
using Microsoft.AspNet.Cors;

namespace WebApplication4.Controllers
{
    [Produces("application/json")]
    public class AlbumsController : Controller
    {
        private ApplicationDbContext _context;

        public AlbumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("api/Albums")]
        [HttpGet]
        [EnableCors("AllowAll")]
        public JsonResult AlbumSales()
        {
         
            IEnumerable sumOfTrackSalesPerAlbum = (from invl in _context.InvoiceLine
                                           join trk in _context.Track
                                           on invl.TrackId equals trk.TrackId
                                           join al in _context.Album
                                           on trk.AlbumId equals al.AlbumId
                                           join inv in _context.Invoice
                                           on invl.InvoiceId equals inv.InvoiceId
                                           join ar in _context.Artist
                                           on al.ArtistId equals ar.ArtistId
                                           into g
                                                         select new
                                                         {
                                                             AlbumName = al.Title,
                                                             TrackSoldAmount = g.Sum(i=>invl.UnitPrice)
                                                         }
                                                            ).OrderByDescending(a=>a.TrackSoldAmount).Take(6).ToList();


            return Json(sumOfTrackSalesPerAlbum);


        }
       



        // GET: api/Albums
        [HttpGet]
        public IEnumerable<Album> GetAlbum()
        {
            return _context.Album;
        }

        // GET: api/Albums/5
        [HttpGet("{id}", Name = "GetAlbum")]
        public IActionResult GetAlbum([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Album album = _context.Album.Single(m => m.AlbumId == id);

            if (album == null)
            {
                return HttpNotFound();
            }

            return Ok(album);
        }

        // PUT: api/Albums/5
        [HttpPut("{id}")]
        public IActionResult PutAlbum(int id, [FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != album.AlbumId)
            {
                return HttpBadRequest();
            }

            _context.Entry(album).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
                {
                    return HttpNotFound();
                }
                else
                {
                    throw;
                }
            }

            return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        }

        // POST: api/Albums
        [HttpPost]
        public IActionResult PostAlbum([FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.Album.Add(album);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AlbumExists(album.AlbumId))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetAlbum", new { id = album.AlbumId }, album);
        }

        // DELETE: api/Albums/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAlbum(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Album album = _context.Album.Single(m => m.AlbumId == id);
            if (album == null)
            {
                return HttpNotFound();
            }

            _context.Album.Remove(album);
            _context.SaveChanges();

            return Ok(album);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlbumExists(int id)
        {
            return _context.Album.Count(e => e.AlbumId == id) > 0;
        }
    }
}