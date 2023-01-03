using System;
using System.Collections.Generic;

namespace Narciarze_v_2.Models
{
    public partial class CenaBilety
    {
        public CenaBilety()
        {
            Cenniks = new HashSet<Cennik>();
        }

        public int Id { get; set; }
        public decimal CenaPrzejazd { get; set; }
        public int IdWyciag { get; set; }

        public virtual Wyciagi IdWyciagNavigation { get; set; } = null!;
        public virtual ICollection<Cennik> Cenniks { get; set; }
    }
}
