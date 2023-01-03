using System;
using System.Collections.Generic;

namespace Narciarze_v_2.Models
{
    public partial class Stoki
    {
        public Stoki()
        {
            CenaKarneties = new HashSet<CenaKarnety>();
            Wyciagis = new HashSet<Wyciagi>();
        }

        public int Id { get; set; }
        public string Nazwa { get; set; } = null!;

        public virtual ICollection<CenaKarnety> CenaKarneties { get; set; }
        public virtual ICollection<Wyciagi> Wyciagis { get; set; }
    }
}
