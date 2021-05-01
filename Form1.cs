using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xoxfrm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rasgele = new Random();
        string[,] btnisim = new string[3, 3]; // buton isimleri
        string[,] tablo = new string[3, 3]; // oyun durumu        
        string o;
        int i = 0, d = 0;
        bool drm = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            // Başlangıç rasgele oyunumuzu başlatalım.
            i = 0; d = 0;
            // buton isimleri diziye aktarılıyor.
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    btnisim[i,d] = c.Name.ToString();
                    d++;
                }

                if (d == 3)
                {
                    d = 0;
                    i++;
                }
            }

            // Form Açılış otomatik başlama
            nextMove(); // Bilgisayarın oynucağı yeni adım            
            tabloUpdate();
            
        }

        private void nextMove()  // Bilgisayarın oynucağı yeni adım
        {
            bool drm = true;
            while (drm)
            {
                int x = rasgele.Next(0, 3);
                int y = rasgele.Next(0, 3);

                if (tablo[x, y] == "X" || tablo[x, y] == "O") // işaretlenmiş yeri işaretlememek için.
                    drm = true; // drm yanlış
                else
                {
                    this.Controls.Find(btnisim[x, y], true)[0].Text = "O"; // kısa yöntem
                    drm = false;
                    #region uzun kod
                    /*o = btnisim[x, y]; // buton ismi alınıyor.                    
                    foreach (Control c in this.Controls) // tüm butonların isimleri kontrol ediliyor
                    {
                        if (c.GetType() == typeof(Button))
                        {

                            if (c.Name == o) // işaretlenecek buton
                                c.Text = "O";
                        }
                    }*/
                    #endregion
                }

            }
            
        }
        
        private string playCheck(string player) // Oyun Kontrolü
        {
            
            string krk = "", drmply = "";
            int dogrulama = 0, oyundurumu = 0;
            // oyunun bitip bitmediğinin kontrolü
            for (i = (tablo.Length / 3) - 1; i >= 0; i--)
            {
                krk = tablo[i, 0];
                for (d = (tablo.Length / 3) - 1; d >= 0; d--)
                {
                    if (tablo[i, d] == "X" || tablo[i, d] == "O")
                    {
                        oyundurumu++;
                    }
                }
            }
            // tüm tablo doluysa oyun bitmiştir.
            if (oyundurumu >= 9)
            {
                drmply = "Oyun Bitti"; // berabere kalındı durumu
            }

            //yatay kontrol
            /*
                str1 |0|1|2|
                str2 | | | |
                str3 | | | |
            */
            for (i = (tablo.Length / 3) - 1; i >= 0; i--)
            {
                for (d = (tablo.Length / 3) - 1; d >= 0; d--)
                {
                    if(tablo[i, d] == player) // "X"
                    {
                        dogrulama++;
                    }
                    if (dogrulama == 3)
                    {
                        drmply = "WON";
                        break;
                    }
                }
                dogrulama = 0;
            }
            
            if (dogrulama != 3)
                dogrulama = 0;

            //dikey kontrol
            /*
                str1 |0| | |
                str2 |1| | |
                str3 |2| | |
            */
            for (i = (tablo.Length / 3) - 1; i >= 0; i--)
            {
                for (d = (tablo.Length / 3) - 1; d >= 0; d--)
                {
                    if (tablo[d, i] == player) 
                    {
                        dogrulama++;
                    }
                    if (dogrulama == 3)
                    {
                        drmply = "WON";
                        break;
                    }
                }
                dogrulama = 0;
            }

            if (dogrulama != 3)
                dogrulama = 0;

            //capraz sag dogru
            d = 2;
            for (i = (tablo.Length / 3) - 1; i >= 0; i--)
            {
                /*
                str1 | | |2|
                str2 | |1| |
                str3 |0| | |
                */
                if (tablo[i, d] == player)
                {                    
                    dogrulama++;
                }
                if (dogrulama == 3)
                {
                    drmply = "WON";
                    break;
                }
                d--;
            }

            if (dogrulama != 3)
                dogrulama = 0;

            //capraz sol dogru
            d = 0;
            for (i = (tablo.Length / 3) - 1; i >= 0; i--)
            {
                /*
                str1 |0| | |
                str2 | |1| |
                str3 | | |2|
                */

                //MessageBox.Show(tablo[i, d]);
                if (tablo[i, d] == player)
                {
                    dogrulama++;
                }
                if (dogrulama == 3)
                {
                    drmply = "WON";
                    break;
                }
                d++;
            }

            

            return drmply;           
        }

        private void tabloUpdate() // tablo güncellemesi
        {
            i = 0; d = 0;
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    tablo[i, d] = c.Text.ToString();
                    d++;
                }

                if (d == 3)
                {
                    d = 0;
                    i++;
                }
            }
        }

        private void tabloYazdir() // test amaçlı tablo ekrana yazdırma
        {

            for (i = (tablo.Length / 3) - 1; i >= 0; i--)
            {
                for (d = (tablo.Length / 3) - 1; d >= 0; d--)
                {
                    MessageBox.Show(tablo[i, d]); 
                }
            }
        }

        private void oyunuSifirla() 
        {
            // butonların textini silme
            foreach (Control c in this.Controls)
                if (c.GetType() == typeof(Button))
                    c.Text = "";

            tablo = new string[3, 3];// tabloyu sıfırlama            

            nextMove(); // bilgisayarı ilk oynasını yaptırma
            tabloUpdate();

            
        }

        string drmoyn = "";
        private void btnEvents(object sender, EventArgs e) // tüm butonların tıklanma eventi
        {

            if (sender.GetType() == typeof(Button)) // Sadece buton tipindeki nesneler olduğunda if bloguna girer.
            {
                if (((Button)sender).Text == "") // butonun içinde yazı yoksa gir.
                {
                    drmoyn = ""; // oyun durumunu sıfırla
                    ((Button)sender).Text = "X"; // benim oynadığım.
                    tabloUpdate(); // tablo durumunu güncelle

                    drmoyn = playCheck("X"); // ben oynadıktan sonra kontrol
                    if (drmoyn == "WON")
                    {
                        MessageBox.Show("X Kazandı");
                        oyunuSifirla();
                        return;
                    }
                    
                    // Bilgisayar oynuyor Yani "O"
                    nextMove(); 
                    tabloUpdate(); // tablo güncelleniyor

                    drmoyn = playCheck("O");// Bilgisayar oynadıktan sonra kontrol.
                    if (drmoyn == "WON")
                    {
                        MessageBox.Show("O Kazandı");
                        oyunuSifirla();
                    }
                    else if (drmoyn == "Oyun Bitti")
                    {
                        MessageBox.Show("Berabere Kalındı.");
                        oyunuSifirla();
                    }

                }
                    
            }
        }
    }
}
