using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class OdrzanaNastavaController : Controller
    {
        private MojContext _context;

        public OdrzanaNastavaController(MojContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var listaOdjeljenje = _context.Odjeljenje.Select(o => new OdrzanaNastavaIndexVM
            {
                Id = o.Id,
                Nastavnik = o.Razrednik.Ime + ' ' + o.Razrednik.Prezime,
                Skola = o.Skola.Naziv,
                NastavnikId = o.RazrednikID
            }).ToList();
            return View(listaOdjeljenje);
        }
        public IActionResult Odaberi(int id)
        {
            OdrzanaNastavaOdaberiVM model = new OdrzanaNastavaOdaberiVM
            {
                NastavnikId = id,
                Rows = _context.MaturskiIspit.Include(p => p.PredajePredmet).ThenInclude(o => o.Odjeljenje).Where(n => n.PredajePredmet.NastavnikID == id).Select(m => new OdrzanaNastavaOdaberiVM.Row
                {
                    Id = m.Id,
                    Skola = m.Skola.Naziv,
                    Predmet = m.PredajePredmet.Predmet.Naziv,
                    Datum = m.Datum,
                    NisuPristupiliUcenici = _context.MaturskiIspitStavke.Where(stavke=>stavke.MaturskiIspitId == m.Id)
                                            .Include(mtd=>mtd.DodjeljenPredmet)
                                            .ThenInclude(dp=>dp.OdjeljenjeStavka)
                                            .ThenInclude(u=>u.Ucenik)
                                            .Select(s=>s.DodjeljenPredmet.OdjeljenjeStavka.Ucenik.ImePrezime).ToList()
                }).ToList()
            };
            return View(model);
        }
        public IActionResult Dodaj(int id)
        {
            var listaPredmeta = _context.Predmet.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Naziv
            }).ToList();
            var listaSkola = _context.Skola.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Naziv
            }).ToList();
            var nastavnik = _context.Nastavnik.Find(id);
            var odjeljenje = _context.Odjeljenje.Where(o => o.RazrednikID == nastavnik.Id).FirstOrDefault();
            var skolska = _context.SkolskaGodina.Find(odjeljenje.SkolskaGodinaID);
            OdrzanaNastavaDodajVM model = new OdrzanaNastavaDodajVM
            {
                Predmeti = listaPredmeta,
                Skole = listaSkola,
                NastavnikId = id,
                Nastavnik=nastavnik.Ime+' '+nastavnik.Prezime,
                SkolskaGodina=skolska.Naziv,
                OdjeljenjeId=odjeljenje.Id
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Dodaj(OdrzanaNastavaDodajVM m)
        {
            PredajePredmet p = new PredajePredmet
            {
                NastavnikID = m.NastavnikId,
                PredmetID = m.PredmetId,
                OdjeljenjeID = m.OdjeljenjeId
            };
            _context.PredajePredmet.Add(p);
            _context.SaveChanges();
            MaturskiIspit novi = new MaturskiIspit
            {
                SkolaId = m.SkolaId,
                PredajePredmetID = p.Id,
                Datum = m.Datum
            };
            _context.MaturskiIspit.Add(novi);
            _context.SaveChanges();
            var dPredmet = _context.DodjeljenPredmet.Where(dp => dp.PredmetId == m.PredmetId).ToList();
            foreach(var pr in dPredmet)
            {
                MaturskiIspitStavke stavke = new MaturskiIspitStavke
                {
                    MaturskiIspitId = novi.Id,
                    DodjeljenPredmetId = pr.Id,
                    Pristupio = false
                };
                _context.MaturskiIspitStavke.Add(stavke);
                _context.SaveChanges();
            }
            return Redirect("/OdrzanaNastava/Odaberi?id=" + m.NastavnikId);
        }
        public IActionResult Uredi(int id)
        {
            var ispit = _context.MaturskiIspit.Find(id);
            var PredajePredmet = _context.PredajePredmet.Find(ispit.PredajePredmetID);
            var predmet = _context.Predmet.Find(PredajePredmet.PredmetID);
            var nast = _context.Nastavnik.Find(PredajePredmet.NastavnikID);
            OdrzanaNastavaUrediVM model = new OdrzanaNastavaUrediVM
            {
                Id = ispit.Id,
                Predmet = predmet.Naziv,
                Datum = ispit.Datum,
                NastavnikId=nast.Id
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Uredi(OdrzanaNastavaUrediVM m)
        {
            var zaUrediti = _context.MaturskiIspit.Find(m.Id);
            zaUrediti.Napomena = m.Napomena;
            _context.MaturskiIspit.Update(zaUrediti);
            _context.SaveChanges();
            return Redirect("/OdrzanaNastava/Odaberi?id=" + m.NastavnikId);
        }
        public IActionResult ListaStavke(int id)
        {
            OdrzanaNastavaListaStavkeVM model = new OdrzanaNastavaListaStavkeVM
            {
                Id = id,
                MatStavke = _context.MaturskiIspitStavke.Where(m => m.MaturskiIspitId == id).Select(s => new OdrzanaNastavaListaStavkeVM.Stavke
                {
                    StavkaId=s.Id,
                    Ucenik=s.DodjeljenPredmet.OdjeljenjeStavka.Ucenik.ImePrezime,
                    Prosjek=_context.DodjeljenPredmet.Where(dp=>dp.Id==s.DodjeljenPredmetId).Average(o=>o.ZakljucnoKrajGodine),
                    Bodovi=s.Bodovi,
                    Pristupio=s.Pristupio
                }).ToList()
            };
            return PartialView(model);
        }
        public IActionResult UcenikJePrisutan(int id)
        {
            var stavka = _context.MaturskiIspitStavke.Find(id);
            stavka.Pristupio = true;
            _context.MaturskiIspitStavke.Update(stavka);
            _context.SaveChanges();
            return Redirect("/OdrzanaNastava/ListaStavke?id=" + stavka.MaturskiIspitId);
        }
        public IActionResult UcenikJeOdsutan(int id)
        {
            var stavka = _context.MaturskiIspitStavke.Find(id);
            stavka.Pristupio = false;
            _context.MaturskiIspitStavke.Update(stavka);
            _context.SaveChanges();
            return Redirect("/OdrzanaNastava/ListaStavke?id=" + stavka.MaturskiIspitId);
        }
        public IActionResult UrediStavku(int id)
        {
            var stavka = _context.MaturskiIspitStavke.Find(id);
            var dPredmet = _context.DodjeljenPredmet.Find(stavka.DodjeljenPredmetId);
            var odStavka = _context.OdjeljenjeStavka.Find(dPredmet.OdjeljenjeStavkaId);
            var ucenik = _context.Ucenik.Find(odStavka.UcenikId);
            OdrzanaNastavaUrediStavkuVM model = new OdrzanaNastavaUrediStavkuVM
            {
                Id = stavka.Id,
                Ucenik = ucenik.ImePrezime,
                Bodovi = stavka.Bodovi
            };
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult UrediStavku(OdrzanaNastavaUrediStavkuVM m)
        {
            var stavka = _context.MaturskiIspitStavke.Find(m.Id);
            stavka.Bodovi = m.Bodovi;
            _context.MaturskiIspitStavke.Update(stavka);
            _context.SaveChanges();
            return Redirect("/OdrzanaNastava/ListaStavke?id="+stavka.MaturskiIspitId);
        }
    }
}