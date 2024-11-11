using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamningsuppgift3_24111_
{
    public class Bok
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public Författare Författare { get; set; }
        public string Genre { get; set; }
        public int PubliceringsÅr { get; set; }
        public string Isbn { get; set; }
        public List<int> Recensioner { get; set; } = new List<int>();

        public void LäggTillBetyg(int betyg)
        {
            if (betyg >= 1 && betyg <= 5)
            {
                Recensioner.Add(betyg);
            }
        }

        public double BeräknaGenomsnittligtBetyg()
        {
            return Recensioner.Any() ? Recensioner.Average() : 0.0;
        }
    }
}
