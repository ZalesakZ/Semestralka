namespace PGM2B_SemestralniPrace_Zalesak.Models
{
    public class Obdelnik
    {
        public Obdelnik(int pozX, int pozY, int stranaA, int stranaB)
        {
            PozX = pozX;
            PozY = pozY;
            StranaA = stranaA;
            StranaB = stranaB;
        }

        public int PozX { get; set; }
        public int PozY { get; set; }
        public int StranaA { get; set; }
        public int StranaB { get; set; }
      
    }
}
