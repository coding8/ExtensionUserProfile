using ExtensionUserProfile.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace ExtensionUserProfile.Controllers
{
    public class UserProfileController : Controller
    {
        private ZUserContext db=new ZUserContext();
        // GET: UserProfile
        public ActionResult Index()
        {
            var user=db.ZUserProfile;
            return View(user.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.ZUserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Item,ZUserId")] ZUserProfile zUserProfile)
        {
            if (ModelState.IsValid)
            {
                db.ZUserProfile.Add(zUserProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ZUserId = new SelectList(db.Users, "Id", "Email", zUserProfile.ZUserId);
            return View(zUserProfile);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZUserProfile zUserProfile = db.ZUserProfile.Find(id);
            if (zUserProfile == null)
            {
                return HttpNotFound();
            }
            return View(zUserProfile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZUserProfile zUserProfile = db.ZUserProfile.Find(id);
            db.ZUserProfile.Remove(zUserProfile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZUserProfile zUserProfile = db.ZUserProfile.Find(id);
            if (zUserProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.ZUserId = new SelectList(db.Users, "Id", "Email", zUserProfile.ZUserId);
            return View(zUserProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Item,ZUserId")] ZUserProfile zUserProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zUserProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ZUserId = new SelectList(db.Users, "Id", "Email", zUserProfile.ZUserId);
            return View(zUserProfile);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZUserProfile zUserProfile = db.ZUserProfile.Find(id);
            if (zUserProfile == null)
            {
                return HttpNotFound();
            }
            return View(zUserProfile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}