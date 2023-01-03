using System;
using System.Collections.Generic;

namespace Narciarze_v_2.Models
{
    public partial class Cennik
    {
        public Cennik()
        {
            Bileties = new HashSet<Bilety>();
            Karneties = new HashSet<Karnety>();
        }

        public int Id { get; set; }
        public int? IdCenaKarnet { get; set; }
        public int? IdCenaBilet { get; set; }
        public DateTime DataRozp { get; set; }
        public DateTime? DataZak { get; set; }

        public virtual CenaBilety? IdCenaBiletNavigation { get; set; }
        public virtual CenaKarnety? IdCenaKarnetNavigation { get; set; }
        public virtual ICollection<Bilety> Bileties { get; set; }
        public virtual ICollection<Karnety> Karneties { get; set; }
    }
}
