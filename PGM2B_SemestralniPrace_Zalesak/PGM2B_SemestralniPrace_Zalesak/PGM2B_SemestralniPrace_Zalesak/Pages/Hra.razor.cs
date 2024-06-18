using Microsoft.JSInterop;
using PGM2B_SemestralniPrace_Zalesak.Models;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace PGM2B_SemestralniPrace_Zalesak.Pages
{
    public partial class Hra
    {
        private int platnoW = 800;
        private int platnoH = 300;

        private int stranaA = 250;
        private int stranaB = 100;

        private int pocet;

        private int x => 250 + (stranaA / 2);
        private int y1;
        private int y2;
        private int y3 => stranaB + y1;

        private bool Zmena = false;
        private bool Konec = false;
        private bool Stop = false;

        private int kolX2 = 325; //pozice X 1. zleva
        private int kolX3 = 350; //pozice X 2. zleva
        private int kolX4 = 450; //pozice X 3. zleva
        private int kolX1 = 475; //pozice X 4. zleva

        private int kolY1 = 55; //pozice Y 1. zhora
        private int kolY2 = 85; //pozice Y 2. zhora

        private int kolPozX;
        private int kolPozY;

        private int polomer = 10;

        private List<Obdelnik> ObdelnikyList { get; set; } = new List<Obdelnik>();
        private List<Kolecko> KoleckaList { get; set; } = new List<Kolecko>();
        private List<Postava> PostavyList { get; set; } = new List<Postava>();

        private List<string> Barvy = new List<string>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            InicializaceHry();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Task.Run(() => Prichod());
                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }    

        private void InicializaceHry()
        {
            var postava = new Postava(obrazek: "../img/Koza.png", 150);
            postava.PridejPozici(pozX: 45, pozY: 500, velikostObrazku: 85);
            postava.PridejPozici(pozX: 1150, pozY: 500, velikostObrazku: 85);
            PostavyList.Add(postava);

            var postava2 = new Postava(obrazek: "../img/Zeli.png", 100);
            postava2.PridejPozici(pozX: 45, pozY: 400, velikostObrazku: 85);
            postava2.PridejPozici(pozX: 1150, pozY: 400, velikostObrazku: 85);
            PostavyList.Add(postava2);

            var postava3 = new Postava(obrazek: "../img/Vlk.png", 150);
            postava3.PridejPozici(pozX: 45, pozY: 300, velikostObrazku: 85);
            postava3.PridejPozici(pozX: 1150, pozY: 300, velikostObrazku: 85);
            PostavyList.Add(postava3);

            var postava4 = new Postava(obrazek: "../img/Prevoznik.png", 200);
            postava4.PridejPozici(pozX: 45, pozY: 125, velikostObrazku: 85);
            postava4.PridejPozici(pozX: 1150, pozY: 125, velikostObrazku: 85);
            PostavyList.Add(postava4);

            var postava5 = new Postava(obrazek: "../img/Lod.png", 200);
            postava5.PridejPozici(pozX: 500, pozY: 325, velikostObrazku: 85);
            PostavyList.Add(postava5);

            Vykresleni();

        }

        private void Pusunuti(int pozice)
        {
            if (pozice == 3)
            {
                PostavyList[3].Presun();
                Zmena = true;
            }
            else
            {
                if ((!PostavyList[pozice].Vpravo && !PostavyList[3].Vpravo) || (PostavyList[pozice].Vpravo && PostavyList[3].Vpravo))
                {
                    PostavyList[pozice].Presun();
                    PostavyList[3].Presun();
                    Zmena = true;
                }
                else
                {
                    Zmena = false;
                }
            }

            Rozhodnuti();

            if (Zmena && !Konec)
            {
                Vykresleni();
            }
        }

        private void Vykresleni()
        {
            int pozX = 250;
            int pozY = -120;

            platnoH += 150;

            y1 = 30;
            y2 = stranaB + 30;
            
            pocet++;

            for (int i = 0; i < pocet; i++)
            {
                pozY += 150;

                if (i > 0)
                {
                    y2 += 150;
                }

                var obdelnik = new Obdelnik(pozX, pozY, stranaA, stranaB);
                ObdelnikyList.Add(obdelnik);

            }

            for (int k = 0; k < PostavyList.Count - 1; k++)
            {
                if (PostavyList[k].Vpravo)
                {

                    if (k == 0 || k == 1)
                    {
                        kolPozX = kolX1; 
                    }
                    else if (k == 2 || k == 3)
                    {
                        kolPozX = kolX4; 
                    }
                }
                else
                {
                    if (k == 0 || k == 1)
                    {
                        kolPozX = kolX3; 
                    }
                    else if (k == 2 || k == 3)
                    {
                        kolPozX = kolX2; 
                    }
                }

                if (k == 0 || k == 2)
                {
                    kolPozY = kolY1;
                }
                else if (k == 1 || k == 3)
                {
                    kolPozY = kolY2;
                }

                Barvy.Add("white");
                Barvy.Add("green");
                Barvy.Add("brown");
                Barvy.Add("blue");

                var kolecko = new Kolecko(kolPozX, kolPozY, polomer);
                KoleckaList.Add(kolecko);
            }

            kolY1 += 150;
            kolY2 += 150;
        }

        private async Task Rozhodnuti()
        {
            string zprava;

            if ((!PostavyList[0].Vpravo && !PostavyList[1].Vpravo && !PostavyList[2].Vpravo && PostavyList[3].Vpravo) || (PostavyList[0].Vpravo && PostavyList[1].Vpravo && PostavyList[2].Vpravo && !PostavyList[3].Vpravo))
            {
                zprava = "Koza sežrala zelí a vlk sežral kozu, spusťe hru znovu";
                await Task.Delay(800);
                await JS.InvokeVoidAsync("alert", zprava);
                Konec = true;
                Stop = true;
            }
            else if ((PostavyList[0].Vpravo && PostavyList[1].Vpravo && !PostavyList[3].Vpravo) || (!PostavyList[0].Vpravo && !PostavyList[1].Vpravo && PostavyList[3].Vpravo))
            {
                zprava = "Koza sežrala zelí, spusťe hru znovu";
                await Task.Delay(800);
                await JS.InvokeVoidAsync("alert", zprava);
                Konec = true;
                Stop = true;
            }
            else if ((PostavyList[0].Vpravo && PostavyList[2].Vpravo && !PostavyList[3].Vpravo) || (!PostavyList[0].Vpravo && !PostavyList[2].Vpravo && PostavyList[3].Vpravo))
            {
                zprava = "Vlk sežral kozu, spusťe hru znovu";
                await Task.Delay(800);
                await JS.InvokeVoidAsync("alert", zprava);
                Konec = true;
                Stop = true;
            }
            else if (PostavyList[0].Vpravo && PostavyList[1].Vpravo && PostavyList[2].Vpravo && PostavyList[3].Vpravo)
            {
                zprava = "Vyhráli jste! Dostali jste všechny postavy na druhý břeh bez toho, aby se sežrali";
                await Task.Delay(800);
                await JS.InvokeVoidAsync("alert", zprava);
                Konec = true;
                Stop = true;
            }

            if (Stop)
            {
                for (int i = 0; i < PostavyList.Count - 1; i++)
                    PostavyList[i].Stop = true;
            }
        }

        private void Prichod()
        {
            foreach (var item in PostavyList)
            {
                item.Presun();
            }
        }
    }
}


