﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team9.Models;
using Microsoft.AspNet.Identity;

namespace Team9.Controllers
{

    public class AlbumsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public bool hasPurchased(int id)
        {
            String CurrentUserId = User.Identity.GetUserId();
            var query = from p in db.Purchases
                        join pi in db.PurchaseItems on p.PurchaseID equals pi.Purchase.PurchaseID
                        where p.isPurchased == false && p.PurchaseUser.Id == CurrentUserId
                        select pi.PurchaseItemSong.SongID;

            List<Int32> SongIDs = query.ToList();
            if (SongIDs.Contains(id))
            {
                return true;
            }
            return false;
        }

        public Decimal getAverageRating(int? id)
        {
            Decimal count = 0;
            Decimal total = 0;
            Decimal average;

            Album Album = db.Albums.Find(id);
            foreach (Rating r in Album.AlbumRatings)
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

        // GET: Albums
        public ActionResult Index()
        {
            return View(db.Albums.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            ViewBag.AverageAlbumRating = getAverageRating(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //GET: Add to cart
        // TODO: set up role requirements
        public ActionResult addAlbumToCart(int id)
        {
            String CurrentUserId = User.Identity.GetUserId();
            var query = from p in db.Purchases
                        where p.isPurchased == false && p.PurchaseUser.Id == CurrentUserId
                        select p;

            Purchase NewPurchase = new Purchase();
            Album album = db.Albums.Find(id);
            List<Purchase> PurchaseList = new List<Purchase>();
            PurchaseList = query.ToList();
            if (PurchaseList.Count() == 1)
            {
                NewPurchase = PurchaseList[0];

                //TODOXX: IF for discounted price
                //newItem.PurchaseItemPrice = song.SongPrice;
                foreach (Song s in album.Songs)
                {
                    if (hasPurchased(s.SongID))
                    {
                        continue;
                        //TODO:Error message to not add song?
                        // use a next to add all other songs that have not been added?
                    }
                    else
                    {
                        PurchaseItem newItem = new PurchaseItem();
                        //Check if there is a discount price
                        if (s.DiscountPrice.Equals(null))
                        {
                            newItem.PurchaseItemPrice = s.SongPrice;
                        }
                        else
                        {
                            newItem.PurchaseItemPrice = s.DiscountPrice;
                        }
                        newItem.PurchaseItemSong = s;
                        newItem.Purchase = NewPurchase;
                        db.PurchaseItems.Add(newItem);
                        db.SaveChanges();

                    }
                }
            }
            else
            {
                NewPurchase.PurchaseUser = db.Users.Find(CurrentUserId);
                NewPurchase.isPurchased = false;
                db.Purchases.Add(NewPurchase);
                db.SaveChanges();
                PurchaseList = query.ToList();
                NewPurchase = PurchaseList[0];

                //TODOXX: IF for discounted price

                foreach (Song s in album.Songs)
                {
                    if (hasPurchased(s.SongID))
                    {
                        //TODO:Error message to not add song?
                        // use a next to add all other songs that have not been added?
                    }
                    else
                    {
                        PurchaseItem newItem = new PurchaseItem();
                        //Check if discount price is null
                        if (s.DiscountPrice.Equals(null))
                        {
                            newItem.PurchaseItemPrice = s.SongPrice;
                        }
                        else
                        {
                            newItem.PurchaseItemPrice = s.DiscountPrice;
                        }
                        newItem.PurchaseItemSong = s;
                        newItem.Purchase = NewPurchase;
                        db.PurchaseItems.Add(newItem);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index", "Purchases");
        }






        // GET: Albums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumID,AlbumName,AlbumPrice")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumID,AlbumName,AlbumPrice")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
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
