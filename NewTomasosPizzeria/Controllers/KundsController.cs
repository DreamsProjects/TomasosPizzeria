using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewTomasosPizzeria.Models;

namespace NewTomasosPizzeria.Controllers
{
    public class KundsController : Controller
    {
        private readonly NyTomasosContext _context;

        public KundsController(NyTomasosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var session = HttpContext.Session.GetString("KundId");

            id = Convert.ToInt32(session);

            var edit = _context.Kund.Where(x => x.KundId == id);

            return View(await edit.ToListAsync());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Create(Kund kund)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool Exist = _context.Kund.Any(x => x.AnvandarNamn.Equals(kund.AnvandarNamn));

                    if (!Exist)
                    {
                        Exist = _context.Kund.Any(x => x.Email.Equals(kund.Email));

                        if (!Exist)
                        {
                            _context.Add(kund);
                            _context.SaveChanges();
                            return RedirectToAction(nameof(Login));
                        }

                        else
                        {
                            ModelState.AddModelError(string.Empty, "Emailadressen är redan upptagen.");
                        }
                    }

                    else
                    {
                        ModelState.AddModelError(string.Empty, "Användarnamnet är redan upptaget");
                    }
                }
            }

            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Gick inte att spara, försök igen om en stund! Om problemet fortfarande är kvar, " +
                    "var vänlig kontakta din system administratör!");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Kund kund) //async för att ha RedirectToAction
        {
            var account = _context.Kund.Where(x => x.AnvandarNamn == kund.AnvandarNamn && x.Losenord == kund.Losenord).FirstOrDefault();

            if (account != null)
            {
                HttpContext.Session.SetString("KundId", account.KundId.ToString());

                var value = (HttpContext.Session.GetString("Varukorg"));

                if (value != null)
                {
                    return RedirectToAction("CheckOut", "MatrattProdukts");
                }

                return RedirectToAction("Welcome", "MatrattProdukts", kund.KundId);
            }
            else
            {
                ModelState.AddModelError("", "Användarnamnet eller lösenordet är fel!");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Welcome", "MatrattProdukts");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kund = await _context.Kund.SingleOrDefaultAsync(m => m.KundId == id);

            if (kund == null)
            {
                return NotFound();
            }
            return View(kund);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Namn,Gatuadress,Postnr,Postort,Email,Telefon,AnvandarNamn,Losenord")] Kund kund)
        {
            var kundID = HttpContext.Session.GetString("KundId");
            kund.KundId = Convert.ToInt32(kundID);

            if (id != kund.KundId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var update = _context.Kund.Where(x => x.KundId == kund.KundId);

                    _context.Update(kund);

                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!KundExists(kund.KundId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Welcome", "MatrattProdukts");
            }
            return View(kund);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kund = await _context.Kund.SingleOrDefaultAsync(m => m.KundId == id);

            if (kund == null)
            {
                return NotFound();
            }

            return View(kund);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kundId = HttpContext.Session.GetString("KundId");
            id = Convert.ToInt32(kundId);

            var kund = await _context.Kund.SingleOrDefaultAsync(m => m.KundId == id);
            _context.Kund.Remove(kund);
            await _context.SaveChangesAsync();

            string deleted = "0";

            HttpContext.Session.SetString("KundId", deleted);

            return RedirectToAction("Welcome", "MatrattProdukts");
        }

        private bool KundExists(int id)
        {
            return _context.Kund.Any(e => e.KundId == id);
        }
    }
}
