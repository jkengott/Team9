using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team9.Models;

namespace Team9.Controllers
{
    public class ArtistsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        //for search...
        //var query = from p in db.purchases
        //          join pi in db.purchaseitems on p.PurchaseID equals pi.purchase.purchaseID
        //          where blah blah blah
        //          select pi.PurchaseItemSong.SongID

        // GET: Artists
        public ActionResult Index()
        {
            return View(db.Artists.ToList());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // find artist id
            Artist artist = db.Artists.Find(id);
            // viewbag for average artist rating
            ViewBag.AverageArtistRating = getAverageRating(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // GET: Artists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistID,ArtistName")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        // GET: Artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllGenres = GetAllGenres(@artist);
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtistID,ArtistName")] Artist artist, Int32[] SelectedGenres)
        {
            if (ModelState.IsValid)
            {
                //find associated Artist
                Artist artistToChange = db.Artists.Find(@artist.ArtistID);

                //change Genres
                //remove any existing genres
                artistToChange.ArtistGenre.Clear();

                //if there are events to add then add them
                if (SelectedGenres != null)
                {
                    foreach (int GenreID in SelectedGenres)
                    {
                        Genre genreToAdd = db.Genres.Find(GenreID);
                        artistToChange.ArtistGenre.Add(genreToAdd);
                    }
                }
                artistToChange.ArtistName = @artist.ArtistName;                

                db.Entry(artistToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //repopulate the viewbag
            ViewBag.AllGenres = GetAllGenres(@artist);
            return View(artist);
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = db.Artists.Find(id);
            db.Artists.Remove(artist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // gets the average rating for an artist
        public Decimal getAverageRating(int? id)
        {
            Decimal count = 0;
            Decimal total = 0;
            Decimal average;

            Artist artist = db.Artists.Find(id);
            foreach (Rating r in artist.ArtistRatings)
            {
                count += 1;
                total += r.RatingValue;
            }
            if (count == 0)
            {
                average = 0;
            }
            else
            {
                average = total / count;
            }

            return average;
        }
        //gets all genres for drop dow list when editing
        public MultiSelectList GetAllGenres(Artist @artist)
        {
            //populate list of Genres
            var query = from g in db.Genres
                        orderby g.GenreName
                        select g;

            //convert to list and execute query
            List<Genre> allGenres = query.ToList();

            //create list of selected Genres
            List<Int32> SelectedGenres = new List<Int32>();

            //Loop through list of Genres and add GenreID
            foreach (Genre g in @artist.ArtistGenre)
            {
                SelectedGenres.Add(g.GenreID);
            }
            //convert to multiselect
            MultiSelectList allGenresList = new MultiSelectList(allGenres, "GenreID", "GenreName", SelectedGenres);

            return allGenresList;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
