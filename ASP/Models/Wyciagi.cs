using System;
using System.Collections.Generic;

namespace Narciarze_v_2.Models
{
    public partial class Wyciagi
    {
        public Wyciagi()
        {
            Bileties = new HashSet<Bilety>();
            CenaBileties = new HashSet<CenaBilety>();
            Harmonograms = new HashSet<Harmonogram>();
        }

        public int Id { get; set; }
        public int IdHarmonogram { get; set; }
        public int IdStok { get; set; }
        public string Nazwa { get; set; } = null!;
        public int Dlugosc { get; set; }
        public double WysBezwzgl { get; set; }

        public virtual Stoki IdStokNavigation { get; set; } = null!;
        public virtual ICollection<Bilety> Bileties { get; set; }
        public virtual ICollection<CenaBilety> CenaBileties { get; set; }
        public virtual ICollection<Harmonogram> Harmonograms { get; set; }
    }
}
