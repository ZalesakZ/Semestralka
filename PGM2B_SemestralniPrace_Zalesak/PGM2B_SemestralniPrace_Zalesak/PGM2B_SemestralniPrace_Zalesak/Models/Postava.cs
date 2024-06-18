using System.Drawing;
using System.Runtime.CompilerServices;

namespace PGM2B_SemestralniPrace_Zalesak.Models
{
    public class Postava
    {
        public Postava(string obrazek, int sirkaObrazku)
        {
            Obrazek = obrazek;
            SirkaObrazku = sirkaObrazku;
        }
        private List<Souradnice> PoziceList { get; set; } = new List<Souradnice>();
        public string Obrazek { get; private set; }
        public int AktualniPozice { get; set; } = -1;
        private int SirkaObrazku { get; set; }
        public bool Vpravo { get; set; } = false;
        public bool Stop { get; set; } = false;

        public string Style 
        {
            get
            {
                if (AktualniPozice < 0)
                {
                    return $"width:{SirkaObrazku}px;";
                }
                else
                {
                    var poz = PoziceList[AktualniPozice];
                    return poz.Style + $"width:{SirkaObrazku * poz.VelikostObrazku / 100}px;";
                }
            }
        }

        public bool Dopredu { get; set; } = true;
        private bool HlavaVpravo { get; set; } = true;
        public string TransformRotateY => HlavaVpravo ? "transform: rotateY(0deg);" : "transform: rotateY(180deg);";

        public void PridejPozici(int pozX, int pozY, int velikostObrazku)
        {
            var souradnice = new Souradnice(pozX, pozY, velikostObrazku);
            PoziceList.Add(souradnice);
        }

        public void Presun()
        {
            if (!Stop)
            {
                if (Dopredu)
                {
                    if (AktualniPozice == PoziceList.Count - 1)
                        Dopredu = false;
                }
                else
                {
                    if (AktualniPozice == 0)
                        Dopredu = true;   
                }

                var predchoziPozice = AktualniPozice;

                if (Dopredu)
                {
                   if (AktualniPozice != -1)
                        Vpravo = true;

                   AktualniPozice++;
                }
                else
                {
                   AktualniPozice--;
                   Vpravo = false;
                }

                HlavaVpravo = predchoziPozice < 0 || PoziceList[predchoziPozice].PozX < PoziceList[AktualniPozice].PozX;
            }
        }
    }
}
