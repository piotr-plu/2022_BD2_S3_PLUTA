using System;
using System.Collections.Generic;

namespace Narciarze_v_2.Models
{
    public partial class Karnety
    {
        public int Id { get; set; }
        public int IdKlient { get; set; }
        public int IdStok { get; set; }
        public string Nazwa { get; set; } = null!;
        public int CzasTrwania { get; set; }
        public byte[]? DataAktywacji { get; set; }
        public bool Status { get; set; }
        public int IdCennika { get; set; }

        public virtual Cennik IdCennikaNavigation { get; set; } = null!;
        public virtual Klient IdKlientNavigation { get; set; } = null!;
    }
}
