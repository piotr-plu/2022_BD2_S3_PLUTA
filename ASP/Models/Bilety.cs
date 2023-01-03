using System;
using System.Collections.Generic;

namespace Narciarze_v_2.Models
{
    public partial class Bilety
    {
        public int Id { get; set; }
        public int IdKlient { get; set; }
        public int IdWyciag { get; set; }
        public int IloscZjazdow { get; set; }
        public int IdCennik { get; set; }

        public virtual Cennik IdCennikNavigation { get; set; } = null!;
        public virtual Klient IdKlientNavigation { get; set; } = null!;
        public virtual Wyciagi IdWyciagNavigation { get; set; } = null!;
    }
}
