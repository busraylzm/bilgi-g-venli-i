using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // textBox1'den metni al
            string metin = textBox1.Text;

            // anahtar numarası gir
            int anahtar;

            if (int.TryParse(textBox2.Text, out anahtar))
            {
                string sifrelimetin = Sifrele(metin, anahtar);
                textBox3.Text = sifrelimetin;
            }
            else
            {
                MessageBox.Show("Geçerli bir anahtar girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Şifrelenmiş metni hesapla
            string sifrelenmisMetin = Sifrele(metin, anahtar);

            // Şifrelenmiş metni textBox3'ye yaz
            textBox3.Text = sifrelenmisMetin;
        }

        // Tüm karakterler için Caesar şifreleme fonksiyonu
        private string Sifrele(string metin, int anahtar)
        {
            char[] karakterler = metin.ToCharArray();
            for (int i = 0; i < karakterler.Length; i++)
            {
                // Her karakteri ASCII tablosuna göre kaydır
                karakterler[i] = (char)((karakterler[i] + anahtar) % 256);
            }
            return new string(karakterler);
        }

        // ASCII kodlarını metin olarak döndüren fonksiyon
        private string GetAsciiKodlari(string metin)
        {
            StringBuilder asciiKodlari = new StringBuilder();
            foreach (char karakter in metin)
            {
                asciiKodlari.Append($"{karakter}: {(int)karakter}, ");
            }
            return asciiKodlari.ToString().TrimEnd(',', ' ');
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // textBox1'den metni al
            string metin = textBox1.Text;

            // ListBox'u temizle (önceki sonuçları sil)
            listBox1.Items.Clear();

            // ASCII bilgilerini listeye ekle
            foreach (var satir in GetKodlamaListe(metin))
            {
                listBox1.Items.Add(satir);
            }
        }
        private List<string> GetKodlamaListe(string metin)
        {
            List<string> liste = new List<string>();

            foreach (char karakter in metin)
            {
                int asciiKod = (int)karakter;
                string binaryKod = Convert.ToString(asciiKod, 2).PadLeft(8, '0'); // Binary (8 bit)
                string octalKod = Convert.ToString(asciiKod, 8);                 // Octal
                string hexKod = Convert.ToString(asciiKod, 16).ToUpper();        // Hexadecimal

                // Formatlı şekilde bilgileri listeye ekle
                string satir = $"Karakter: '{karakter}', ASCII: {asciiKod}, Binary: {binaryKod}, Octal: {octalKod}, Hex: {hexKod}";
                liste.Add(satir);
            }

            return liste;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // textBox1'den metni al
            string metin = textBox1.Text;

            // anahtar numarası gir
            int anahtar;

            if (int.TryParse(textBox2.Text, out anahtar))
            {
                string sifrelimetin = Sifrele(metin, anahtar);
                textBox4.Text = sifrelimetin;
            }
            else
            {
                MessageBox.Show("Geçerli bir anahtar girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Şifrelenmiş metni hesapla (sadece alfabe karakterlerine göre)
            string sifrelenmisMetin = SadeceAlfabeSifrele(metin, anahtar);

            // Şifrelenmiş metni textBox2'ye yaz
            textBox4.Text = sifrelenmisMetin;
        }

        // Şifreleme fonksiyonu
        private string SadeceAlfabeSifrele(string metin, int anahtar)
        {
            char[] karakterler = metin.ToCharArray();
            for (int i = 0; i < karakterler.Length; i++)
            {
                char karakter = karakterler[i];
                if (char.IsLetter(karakter))
                {
                    char offset = char.IsUpper(karakter) ? 'A' : 'a';
                    karakter = (char)(((karakter + anahtar - offset) % 26) + offset);
                }
                karakterler[i] = karakter;
            }
            return new string(karakterler);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // textBox1'den şifrelenecek metni al
            string metin = textBox1.Text;

            // textBox2 ve textBox6 anahtarları al
            if (!int.TryParse(textBox2.Text, out int a) || !int.TryParse(textBox6.Text, out int b))
            {
                MessageBox.Show("Lütfen geçerli bir tam sayı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 'a' anahtarının geçerli olup olmadığını kontrol et
            if (GCD(a, 26) != 1)
            {
                MessageBox.Show("'a' anahtarı 26 ile aralarında asal olmalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Şifrelenmiş metni hesapla
            string sifrelenmisMetin = AffineSifrele(metin, a, b);

            // Şifrelenmiş metni textBox2'ye yaz
            textBox5.Text = sifrelenmisMetin;
        }
        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        private string AffineSifrele(string metin, int a, int b)
        {
            char[] karakterler = metin.ToCharArray();

            for (int i = 0; i < karakterler.Length; i++)
            {
                char karakter = karakterler[i];
                if (char.IsLetter(karakter))
                {
                    char offset = char.IsUpper(karakter) ? 'A' : 'a'; // Büyük/küçük harf kontrolü
                    int x = karakter - offset; // Alfabetik sıra numarasını al
                    int y = (a * x + b) % 26;  // Affine şifreleme formülü
                    karakterler[i] = (char)(y + offset); // Şifrelenmiş karakteri hesapla
                }
            }

            return new string(karakterler);
        }
    }
}
