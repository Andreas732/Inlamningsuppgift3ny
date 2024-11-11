using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamningsuppgift3_24111_
{
    using System;
    using System.Linq;

    public class Program
    {
        static void Main(string[] args)
        {
            Bibliotek bibliotek = new Bibliotek();
            bibliotek.LaddaData();

            bool avsluta = false;
            while (!avsluta)
            {
                Console.WriteLine("\n--- Bibliotekshanteringssystem ---");
                Console.WriteLine("1. Lägg till ny bok");
                Console.WriteLine("2. Lägg till ny författare");
                Console.WriteLine("3. Uppdatera bokdetaljer");
                Console.WriteLine("4. Uppdatera författardetaljer");
                Console.WriteLine("5. Ta bort bok");
                Console.WriteLine("6. Ta bort författare");
                Console.WriteLine("7. Lista alla böcker och författare");
                Console.WriteLine("8. Sök och filtrera böcker");
                Console.WriteLine("9. Avsluta och spara data");
                Console.Write("Välj ett alternativ: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        LäggTillNyBok(bibliotek);
                        break;
                    case "2":
                        LäggTillNyFörfattare(bibliotek);
                        break;
                    case "3":
                        UppdateraBokDetaljer(bibliotek);
                        break;
                    case "4":
                        UppdateraFörfattarDetaljer(bibliotek);
                        break;
                    case "5":
                        TaBortBok(bibliotek);
                        break;
                    case "6":
                        TaBortFörfattare(bibliotek);
                        break;
                    case "7":
                        ListaAllaBöckerOchFörfattare(bibliotek);
                        break;
                    case "8":
                        SökOchFiltreraBöcker(bibliotek);
                        break;
                    case "9":
                        bibliotek.SparaData();
                        avsluta = true;
                        Console.WriteLine("Data sparad. Avslutar programmet...");
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }
            }
        }

        // --- Metoder för menyval ---

        static void LäggTillNyBok(Bibliotek bibliotek)
        {
            Console.Write("Ange boktitel: ");
            string titel = Console.ReadLine();

            Console.Write("Ange författarens ID (eller skapa en ny författare först): ");
            int författarId = int.Parse(Console.ReadLine());
            var författare = bibliotek.HämtaFörfattare(författarId);
            if (författare == null)
            {
                Console.WriteLine("Författare hittades inte. Skapa författare först.");
                return;
            }

            Console.Write("Ange genre: ");
            string genre = Console.ReadLine();

            Console.Write("Ange publiceringsår: ");
            int år = int.Parse(Console.ReadLine());

            Console.Write("Ange ISBN: ");
            string isbn = Console.ReadLine();

            Bok nyBok = new Bok
            {
                Id = bibliotek.GenereraNyBokId(),
                Titel = titel,
                Författare = författare,
                Genre = genre,
                PubliceringsÅr = år,
                Isbn = isbn
            };

            bibliotek.LäggTillBok(nyBok);
            Console.WriteLine("Bok tillagd!");
        }

        static void LäggTillNyFörfattare(Bibliotek bibliotek)
        {
            Console.Write("Ange författarens namn: ");
            string namn = Console.ReadLine();

            Console.Write("Ange författarens land: ");
            string land = Console.ReadLine();

            Författare nyFörfattare = new Författare
            {
                Id = bibliotek.GenereraNyFörfattarId(),
                Namn = namn,
                Land = land
            };

            bibliotek.LäggTillFörfattare(nyFörfattare);
            Console.WriteLine("Författare tillagd!");
        }

        static void UppdateraBokDetaljer(Bibliotek bibliotek)
        {
            Console.Write("Ange bok-ID för den bok som ska uppdateras: ");
            int bokId = int.Parse(Console.ReadLine());
            var bok = bibliotek.HämtaBok(bokId);
            if (bok == null)
            {
                Console.WriteLine("Bok hittades inte.");
                return;
            }

            Console.Write("Ange ny titel (lämna tomt för att behålla befintlig titel): ");
            string nyTitel = Console.ReadLine();
            if (!string.IsNullOrEmpty(nyTitel))
            {
                bok.Titel = nyTitel;
            }

            Console.Write("Ange ny genre (lämna tomt för att behålla befintlig genre): ");
            string nyGenre = Console.ReadLine();
            if (!string.IsNullOrEmpty(nyGenre))
            {
                bok.Genre = nyGenre;
            }

            Console.Write("Ange nytt publiceringsår (lämna tomt för att behålla befintligt år): ");
            string årInput = Console.ReadLine();
            if (int.TryParse(årInput, out int nyttÅr))
            {
                bok.PubliceringsÅr = nyttÅr;
            }

            Console.WriteLine("Bok uppdaterad!");
        }

        static void UppdateraFörfattarDetaljer(Bibliotek bibliotek)
        {
            Console.Write("Ange författar-ID för den författare som ska uppdateras: ");
            int författarId = int.Parse(Console.ReadLine());
            var författare = bibliotek.HämtaFörfattare(författarId);
            if (författare == null)
            {
                Console.WriteLine("Författare hittades inte.");
                return;
            }

            Console.Write("Ange nytt namn (lämna tomt för att behålla befintligt namn): ");
            string nyttNamn = Console.ReadLine();
            if (!string.IsNullOrEmpty(nyttNamn))
            {
                författare.Namn = nyttNamn;
            }

            Console.Write("Ange nytt land (lämna tomt för att behålla befintligt land): ");
            string nyttLand = Console.ReadLine();
            if (!string.IsNullOrEmpty(nyttLand))
            {
                författare.Land = nyttLand;
            }

            Console.WriteLine("Författare uppdaterad!");
        }

        static void TaBortBok(Bibliotek bibliotek)
        {
            Console.Write("Ange bok-ID för den bok som ska tas bort: ");
            int bokId = int.Parse(Console.ReadLine());
            bibliotek.TaBortBok(bokId);
            Console.WriteLine("Bok borttagen!");
        }

        static void TaBortFörfattare(Bibliotek bibliotek)
        {
            Console.Write("Ange författar-ID för den författare som ska tas bort: ");
            int författarId = int.Parse(Console.ReadLine());
            bibliotek.TaBortFörfattare(författarId);
            Console.WriteLine("Författare borttagen!");
        }

        static void ListaAllaBöckerOchFörfattare(Bibliotek bibliotek)
        {
            Console.WriteLine("--- Böcker ---");
            foreach (var bok in bibliotek.HämtaAllaBöcker())
            {
                Console.WriteLine($"ID: {bok.Id}, Titel: {bok.Titel}, Författare: {bok.Författare.Namn}, Genre: {bok.Genre}, År: {bok.PubliceringsÅr}");
            }

            Console.WriteLine("--- Författare ---");
            foreach (var författare in bibliotek.HämtaAllaFörfattare())
            {
                Console.WriteLine($"ID: {författare.Id}, Namn: {författare.Namn}, Land: {författare.Land}");
            }
        }

        static void SökOchFiltreraBöcker(Bibliotek bibliotek)
        {
            Console.Write("Ange filterkriterium (Genre, Författarens namn, eller År): ");
            string kriterium = Console.ReadLine().ToLower();

            var resultat = kriterium switch
            {
                "genre" => bibliotek.SökOchFiltreraBöcker(b => b.Genre.Equals(kriterium, StringComparison.OrdinalIgnoreCase)),
                "författarens namn" => bibliotek.SökOchFiltreraBöcker(b => b.Författare.Namn.Equals(kriterium, StringComparison.OrdinalIgnoreCase)),
                "år" => bibliotek.SökOchFiltreraBöcker(b => b.PubliceringsÅr == int.Parse(kriterium)),
                _ => null
            };

            if (resultat != null)
            {
                foreach (var bok in resultat)
                {
                    Console.WriteLine($"ID: {bok.Id}, Titel: {bok.Titel}, Författare: {bok.Författare.Namn}");
                }
            }
            else
            {
                Console.WriteLine("Inga böcker hittades med det angivna kriteriet.");
            }
        }
    }
}