using System.Drawing;

namespace PGM2B_SemestralniPrace_Zalesak.Models
{
    public class Kolecko
    {
        public Kolecko(int kolPozX, int kolPozY, int polomer)
        {
            KolPozX = kolPozX;
            KolPozY = kolPozY;
            Polomer = polomer;
        }
        public int KolPozX { get; set; }
        public int KolPozY { get; set; }
        public int Polomer { get; set; }
    }   
}
