using System;
using System.Collections.Generic;

namespace Narciarze_v_2.Models
{
    public partial class Harmonogram
    {
        public int Id { get; set; }
        public int IdWyciagi { get; set; }
        public bool Stan { get; set; }
        public DateTime DataRozp { get; set; }
        public DateTime? DataZak { get; set; }

        public virtual Wyciagi IdWyciagiNavigation { get; set; } = null!;
    }
}
