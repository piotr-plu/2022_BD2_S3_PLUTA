using System;
using System.Collections.Generic;

namespace Narciarze_v_2.Models
{
    public partial class Klient
    {
        public Klient()
        {
            Bileties = new HashSet<Bilety>();
            Karneties = new HashSet<Karnety>();
        }

        public int Id { get; set; }
        public string Imie { get; set; } = null!;
        public string Nazwisko { get; set; } = null!;
        public string Haslo { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Bilety> Bileties { get; set; }
        public virtual ICollection<Karnety> Karneties { get; set; }
    }
}
