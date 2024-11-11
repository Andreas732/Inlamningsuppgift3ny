using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamningsuppgift3_24111_
{
    public class Författare
    {
        public int Id { get; set; }
        public string Namn { get; set; }
        public string Land { get; set; }

        public List<Bok> HämtaBöcker(List<Bok> böcker)
        {
            return böcker.Where(b => b.Författare.Id == Id).ToList();
        }
    }
}
