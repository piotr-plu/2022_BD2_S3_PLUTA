using System;
using System.Collections.Generic;

namespace Narciarze_v_2.Models
{
    public partial class CenaKarnety
    {
        public CenaKarnety()
        {
            Cenniks = new HashSet<Cennik>();
        }

        public int Id { get; set; }
        public decimal Cena { get; set; }
        public int Czas { get; set; }
        public int IdStok { get; set; }

        public virtual Stoki IdStokNavigation { get; set; } = null!;
        public virtual ICollection<Cennik> Cenniks { get; set; }
    }
}
