using System;
using System.Collections.Generic;
using System.Linq;
using RS1_Ispit_asp.net_core.EntityModels;

namespace RS1_Ispit_asp.net_core.EF
{
    public class MojDBInitializer
    {
        public static void Izvrsi(MojContext context)
        {
            // Look for any Skola.
            if (context.Skola.Any())
            {
                return; // DB has been seeded
            }

            int maxRazredi = 4;
            int maxPredmeti = 10;
            int maxUcenici = 15;
            int maxOdjeljenja = 2;

            var predmeti = new List<Predmet>();
            var odjeljenja = new List<Odjeljenje>();
            var skolskeGodine = new List<SkolskaGodina>();
            var skole = new List<Skola>();
            var odjeljenjeStavke = new List<OdjeljenjeStavka>();
            var ucenici = new List<Ucenik>();
            var predajePredmete = new List<PredajePredmet>();
            var nastavnici = new List<Nastavnik>();
            var dodjeljenPredmet = new List<DodjeljenPredmet>();

            for (int i = 1; i <= maxRazredi; i++)
            {
                for (int j = 0; j < maxPredmeti; j++)
                {
                    predmeti.Add(new Predmet { Naziv = MyRandomExtensions.MyRandomString(5), Razred = i });
                }
            }

            skolskeGodine.Add(new SkolskaGodina { Aktuelna = false, Naziv = "2016/17" });
            skolskeGodine.Add(new SkolskaGodina { Aktuelna = true, Naziv = "2017/18" });

            skole.Add(new Skola() { Naziv = "I Srednja škola Mostar" });
            skole.Add(new Skola() { Naziv = "II Srednja škola Mostar" });


            int nBrojac = 0;
            foreach (Skola s in skole)
            {
                for (int i = 1; i <= 15; i++)
                {
                    nastavnici.Add(new Nastavnik
                    {
                        Ime = MyRandomExtensions.MyRandomString(4),
                        Prezime = MyRandomExtensions.MyRandomString(4),
                    });
                }


                foreach (SkolskaGodina skolskaGodina in skolskeGodine)
                {
                    for (int bRazred = 1; bRazred <= maxRazredi; bRazred++)
                    {
                        for (int bOdjeljenja = 1; bOdjeljenja < maxOdjeljenja; bOdjeljenja++)
                        {
                            Odjeljenje newOdjeljenje =
                                new Odjeljenje()
                                {
                                    SkolskaGodina = skolskaGodina,
                                    Oznaka = bRazred + "-" + bOdjeljenja,
                                    Razred = bRazred,
                                    Skola = s,
                                    Razrednik = nastavnici.MyRandom()
                                };
                            odjeljenja.Add(newOdjeljenje);

                            foreach (Predmet p in predmeti.Where(p => newOdjeljenje.Razred == bRazred))
                            {
                                predajePredmete.Add(new PredajePredmet
                                {
                                    Predmet = p,
                                    Odjeljenje = newOdjeljenje,
                                    Nastavnik = nastavnici.ToList().MyRandom()
                                });
                            }

                            for (int bUcenik = 1; bUcenik <= maxUcenici; bUcenik++)
                            {
                                Ucenik ucenik = new Ucenik()
                                {
                                    ImePrezime = MyRandomExtensions.MyRandomString(5)
                                };

                                ucenici.Add(ucenik);

                                OdjeljenjeStavka odjeljenjeStavka = new OdjeljenjeStavka
                                {
                                    Ucenik = ucenik,
                                    Odjeljenje = newOdjeljenje,
                                    BrojUDnevniku = bUcenik,
                                };
                                odjeljenjeStavke.Add(odjeljenjeStavka);
                                foreach (Predmet p in predmeti.Where(q => q.Razred == bRazred))
                                {
                                    int zakljucnoKrajGodine = MyRandomExtensions.dajOcjenu();


                                    DodjeljenPredmet dp = new DodjeljenPredmet()
                                    {
                                        Predmet = p,
                                        OdjeljenjeStavka = odjeljenjeStavka,
                                        ZakljucnoKrajGodine = zakljucnoKrajGodine,
                                    };
                                    dodjeljenPredmet.Add(dp);
                                }

                            }

                        }

                    }
                }
            }
            context.Nastavnik.AddRange(nastavnici);
            context.Predmet.AddRange(predmeti);
            context.Odjeljenje.AddRange(odjeljenja);
            context.SkolskaGodina.AddRange(skolskeGodine);
            context.Skola.AddRange(skole);
            context.OdjeljenjeStavka.AddRange(odjeljenjeStavke);
            context.Ucenik.AddRange(ucenici);
            context.PredajePredmet.AddRange(predajePredmete);
            context.Nastavnik.AddRange(nastavnici);
            context.DodjeljenPredmet.AddRange(dodjeljenPredmet);
            context.SaveChanges();
        }

       
    }
    public static class MyRandomExtensions
    {
        public static string MyRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public static readonly Random random = new Random();

        public static T MyRandom<T>(this List<T> lista)
        {
            int r = random.Next(0, lista.Count);
            return lista[r];
        }

        public static int dajOcjenu()
        {
            int x = random.Next(1, 20);
            if (x > 1)
                x = x % 4 + 2;
            return x;
        }
    }
}
