using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamningsuppgift3_24111_
{
    using System.Text.Json;

    public class Bibliotek
    {
        private List<Bok> Böcker { get; set; } = new List<Bok>();
        private List<Författare> Författare { get; set; } = new List<Författare>();

        private const string Filnamn = "LibraryData.json";

        public void LäggTillBok(Bok bok) => Böcker.Add(bok);
        public void LäggTillFörfattare(Författare författare) => Författare.Add(författare);

        public Bok HämtaBok(int id) => Böcker.FirstOrDefault(b => b.Id == id);
        public Författare HämtaFörfattare(int id) => Författare.FirstOrDefault(f => f.Id == id);

        public List<Bok> HämtaAllaBöcker() => Böcker;
        public List<Författare> HämtaAllaFörfattare() => Författare;

        public int GenereraNyBokId()
        {
            return Böcker.Any() ? Böcker.Max(b => b.Id) + 1 : 1;
        }

        public int GenereraNyFörfattarId()
        {
            return Författare.Any() ? Författare.Max(f => f.Id) + 1 : 1;
        }

        public void TaBortBok(int id)
        {
            var bok = HämtaBok(id);
            if (bok != null)
            {
                Böcker.Remove(bok);
            }
        }

        public void TaBortFörfattare(int id)
        {
            var författare = HämtaFörfattare(id);
            if (författare != null)
            {
                Författare.Remove(författare);
                // Tar även bort böcker av författaren om de finns
                Böcker.RemoveAll(b => b.Författare.Id == id);
            }
        }

        public List<Bok> SökOchFiltreraBöcker(Func<Bok, bool> filter)
        {
            return Böcker.Where(filter).ToList();
        }

        public void SparaData()
        {
            var data = new { Böcker, Författare };
            string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Filnamn, jsonString);
        }

        public void LaddaData()
        {
            if (File.Exists(Filnamn))
            {
                string jsonString = File.ReadAllText(Filnamn);
                var data = JsonSerializer.Deserialize<BibliotekData>(jsonString);
                Böcker = data?.Böcker ?? new List<Bok>();
                Författare = data?.Författare ?? new List<Författare>();
            }
        }
    }

    // En hjälpklass för deserialisering av JSON-data
    public class BibliotekData
    {
        public List<Bok> Böcker { get; set; } = new List<Bok>();
        public List<Författare> Författare { get; set; } = new List<Författare>();
    }
}
