using System;
using System.Drawing;
using System.Windows.Forms;

namespace dcqe_test
{
    public partial class Form1 : Form
    {

        #region Munkavaltozok, definiciok, konstansok

        //veletlen generator
        Random rnd;

        //A fo kepek (a harom kiserleti elrendezes: radir nincs, radir bent van, radir ellentetesen van bent)
        Bitmap mainBmp0, mainBmp1, mainBmp2;

        //A fo kepeken az egyes dolgok x,y koordinatai (hol mi van)
        const int sourcePicX = 134;         //indulo hely X
        const int sourcePicY = 324;         //indulo hely Y
        const int signaldetectorPicX = 266; //signal foton detector X helye
        const int idlereraserPicX = 368;    //idler foton radir (polarizator) X helye
        const int idlerdetectorPicX = 400;  //idler foton detektor X helye

        //A meresi kepernyo (signal-detektor) bitkepe (ide rajzoljuk a signal foton pottyoket)
        Bitmap detectorBmp;

        //A radir allapota (0: nincs bent, 1: radir normal beallitas (45 fok), 2: radir ellentetes beallitas (-45 fok)
        enum TEraserState { noeraser, eraser_normal, eraser_anti };
        TEraserState eraserState = TEraserState.noeraser;

        //A detektor kepernyo szinezesi beallitasa (0: automata, 1: nincs, 2: piros, 3: kek)
        enum TColorSetting { auto, none, red, blue };
        TColorSetting colorSetting = TColorSetting.auto;

        //signal foton adat strukturaja
        struct TSignalPhoton
        {
            //aktiv meg? (ha eleri a detektort, akkor nem)
            public bool active;
            //mozgasi koordinatai a kepen (a mainBmp kepben)
            public int picX, picY;
            //a signal detektor kepernyon a becsapodasi helye (miutan elerte a detektort)
            public int detectorX, detectorY;
        }

        //idler foton adat strukturaja
        struct TIdlerlPhoton
        {
            //aktiv meg? (ha eleri a detektort vagy elnyeli a radir, akkor nem)
            public bool active;
            //mozgasi koordinatai a kepen (a mainBmp kepben)
            public int picX, picY;
            //polarizaltsagi allapotat leiro valoszinusegi ertek. Milyen valoszinuseggel megy at +45 fokos polarizatoron (ellentete a -45 fokos polarizator esete)
            public double state;
            //a radir (polarizator) hatasa: 0: nem erte el a radirt vagy nem is volt, 1: a radirozas sikeres volt (atment a polarizatoron), -1: a radir elnyelte a fotont (volt polarizator de nem ment at rajta)
            public int erased;
        }

        //A fotonparok adat tombje (minden signal es idler fotonhoz tartozik egy-egy adat struktura)
        TSignalPhoton[] signals;
        TIdlerlPhoton[] idlers;

        //Fotonok mozgasi sebessege az ablakban: mennyi pixelt tegyen meg egy oraciklus (20 msec) alatt
        int speed_pixel_per_cycle = 2; //igy par masodpercig tart egy szimulalt kiserlet


        #endregion

        #region Inicializalas a program indulasakor

        //INDULASKOR:
        public Form1()
        {
            InitializeComponent();

            //veletlen generator inicializalasa
            rnd = new Random();

            //detektor kepernyo bitkep letrehozasa (ide rajzoljuk majd a signal fotonok detektalasi pontjait)
            detectorBmp = new Bitmap(pictureBoxDetector.Width, pictureBoxDetector.Height);
            Graphics g = Graphics.FromImage(detectorBmp);
            g.Clear(Color.Black);

            //detektor kepernyo kepe az ablakban
            pictureBoxDetector.Image = detectorBmp;
            
            //a fo kepek (kiserleti rendszer) betoltese fajlokbol
            mainBmp0 = new Bitmap("kep0.png");
            mainBmp1 = new Bitmap("kep1.png");
            mainBmp2 = new Bitmap("kep2.png");

            //fo kep az ablakban, kezdetben az elso kep (nincs radir)
            pictureBoxMain.Image = mainBmp0;

            //signal es idler fotonok (1-1 db) letrehozasa, hogy ne dobjon hibat a program kesobb, mikor hivatkozik ra
            //a ket indito gomb fogja majd tenylegesen letrehozni
            signals = new TSignalPhoton[1];
            idlers = new TIdlerlPhoton[1];
        }

        #endregion

        #region Esemenyek kezelese (user gombnyomas, rendszer ujrarajzolas keres)

        //Fo kepet (kiserleti elrendezes) ujra kell rajzolni.
        //Ezt az op.rendszer hivja meg igeny szerint, de a fo munka eljaras is kozvetve ezt hasznalja, mikor keri a kep frissiteset a fotonok mozgasa miatt
        private void pictureBoxMain_Paint(object sender, PaintEventArgs e)
        {
            //fotonok kirajzolasa az ablakba
            Graphics g = e.Graphics;
            DrawPhotonsOnPaint(g);
            //mas dolgunk nincs is, a rendszer gondoskodik a bitkep alapjan a kep frissiteserol ott ahol nem rajzoltunk
        }

        //Ora funkcio: minden masodpercben x50-szer fut le (20 ms-onkent). Ez hivja meg a fo munka eljarast, ami mozgatja a fotonokat, stb.
        private void timer1_Tick(object sender, EventArgs e)
        {
            //A fo munka eljaras meghivasa (fotonok mozgatasa, detektalasa, szamolasok, stb.)
            MainWorkFunction();
        }

        //1 fotonpar inditasa gombot lenyomta a user
        private void buttonStart1_Click(object sender, EventArgs e)
        {
            //uj fotonpar generalasa
            GeneratePhotonPairs(1);
            //a ket indito gomb inaktiv, amig veget nem er a kiserlet
            buttonStart1.Enabled = false;
            buttonStart2.Enabled = false;
        }

        //100 fotonpar inditasa gombot lenyomta a user
        private void buttonStart2_Click(object sender, EventArgs e)
        {
            //100 uj fotonpar generalasa
            GeneratePhotonPairs(100);
            //a ket indito gomb inaktiv, amig veget nem er a kiserlet
            buttonStart1.Enabled = false;
            buttonStart2.Enabled = false;
        }

        //A user megvaltoztatta a radir beallitasat az ablakban (radioButton-okkal)
        private void Eraserbuttons_CheckedChanged(object sender, EventArgs e)
        {
            //Elrendezes kivalasztasa (nincs radir, radir bent, es forditva bent)
            if (radioButtonEraser0.Checked)
            {
                eraserState = TEraserState.noeraser;
                pictureBoxMain.Image = mainBmp0;
            }
            else if (radioButtonEraser1.Checked)
            {
                eraserState = TEraserState.eraser_normal;
                pictureBoxMain.Image = mainBmp1;
            }
            else if (radioButtonEraser2.Checked)
            {
                eraserState = TEraserState.eraser_anti;
                pictureBoxMain.Image = mainBmp2;
            }
        }

        //A user megvaltoztatta a szinezes beallitasat az ablakban (radioButton-okkal)
        private void Coloringbuttons_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonColoring0.Checked)
                colorSetting = TColorSetting.auto;
            else if (radioButtonColoring1.Checked)
                colorSetting = TColorSetting.none;
            else if (radioButtonColoring2.Checked)
                colorSetting = TColorSetting.red;
            else if (radioButtonColoring3.Checked)
                colorSetting = TColorSetting.blue;
        }

        //Signal detektor kepernyo kepenek torlese
        private void buttonCls_Click(object sender, EventArgs e)
        {
            //toroljuk feketevel az egeszet
            Graphics g = Graphics.FromImage(detectorBmp);
            g.Clear(Color.Black);
            //frissiteni kell a kepet az ablakban, hogy a user lassa is a valtozast
            pictureBoxDetector.Invalidate();
        }

        //Signal detektor kepernyon a feher pontok elrejtese
        private void buttonClearWhitePoints_Click(object sender, EventArgs e)
        {
            //melos megoldas: minden pixelen vegigmegyunk, ahol feher, azt csereljuk feketere
            for (int y = 0; y < detectorBmp.Height; y++)
            {
                for (int x = 0; x < detectorBmp.Width; x++)
                {
                    //csak korulmenyesen lehet lekerdetni a szinet, mert amugy a sima osszehasonlitas nem mukodik
                    int c = detectorBmp.GetPixel(x, y).ToArgb() & 0x00FFFFFF;
                    if (c == 0xFFFFFF) //ha feher
                        detectorBmp.SetPixel(x, y, Color.Black);
                }
            }
            //frissiteni kell a kepet az ablakban, hogy a user lassa is a valtozast
            pictureBoxDetector.Invalidate();
        }

        #endregion

        #region Seged eljarasok: fotonparok letrehozasa, a signal detektoron pont rajzolasa, fotonok kirajzolasa az ablakra

        //Uj fotonparok inditasa (num: hany darab legyen) seged eljaras, ezt a start gombok hivjak meg
        private void GeneratePhotonPairs(int num)
        {
            signals = new TSignalPhoton[num];
            idlers = new TIdlerlPhoton[num];

            for (int i = 0; i < num; i++)
            {
                //helye a kepen (amit a user lat) pixelekben
                int d = i / 5; //ha tobb foton van (100 darab), akkor egy hosszabb lancban jonnek egymas utan a fotonok, 20 pixel hosszu legyen egy 100 fotonos lanc
                signals[i].picX = sourcePicX - d;
                signals[i].picY = sourcePicY - d;
                idlers[i].picX = sourcePicX - d;
                idlers[i].picY = sourcePicY + d;
                //aktiv mindegyik
                idlers[i].active = signals[i].active = true;
                //idler foton: 50-50%, hogy atmegy a polarizatoron (radiron) fuggetlenul annak beallitasatol
                idlers[i].state = 0.5;
                //idler foton: meg nem erte el a polarizatort (radirt), nincs torolve
                idlers[i].erased = 0;
            }
        }

        //A signal detektor kepernyo kepre egy detektalasi potty rajzolasa (detectorX,detectorY helyekre c szinnel)
        private void DrawSignalDetector(int detectorX, int detectorY, Color c)
        {
            //2x2 pixel vastag a potty, hogy jol lathato legyen a monitoron
            detectorBmp.SetPixel(detectorX, detectorY, c);
            detectorBmp.SetPixel(detectorX + 1, detectorY, c);
            detectorBmp.SetPixel(detectorX, detectorY + 1, c);
            detectorBmp.SetPixel(detectorX + 1, detectorY + 1, c);
            //frissiteni kell a kepet az ablakban, hogy a user lassa is a valtozast
            pictureBoxDetector.Invalidate();
        }

        //Fotonok rajzolasa a fo kepre az ablakra (nem bitkepbe rajzolja be, hanem az OnPaint esemeny eseten direkt rajzol az ablakra)
        //(Ezt indirekt modon a fo eljaras is meghivja, de az op.rendszer is kerhet frissitest)
        private void DrawPhotonsOnPaint(Graphics g)
        {
            //3 pixel vastag narancs szinu vonalat hasznalunk
            Pen drawpen = new Pen(Color.Orange, 3);

            //Rovid vonalat - gyakorlatilag negyzetet - rajzol ki minden egyes fotonhoz
            //megjegyzes: ez nem valami optimalis megoldas, lehetne rajta javitani, hogy 100 db fotonpar eseten ne rajzoljon x100-szor, de igazbol nem sokat szamit, ugyhogy mindegy is
            for (int i = 0; i < signals.Length; i++)
            {
                if (signals[i].active == false || signals[i].picX < sourcePicX) continue;
                g.DrawLine(drawpen, signals[i].picX - 1, signals[i].picY - 1, signals[i].picX + 1, signals[i].picY + 1);
            }
            for (int i = 0; i < idlers.Length; i++)
            {
                if (idlers[i].active == false || idlers[i].picX < sourcePicX) continue;
                g.DrawLine(drawpen, idlers[i].picX - 1, idlers[i].picY - 1, idlers[i].picX + 1, idlers[i].picY + 1);
            }
        }

        #endregion

        #region **** A FO ELJARAS ****

        //A fo munka eljaras. Ezt az eljaras, amit az timer hiv meg minden ido ciklusban (20 ms-onkent)
        //Kiszamolja, hogy hol tartanak a fotonparok, kirajzolja oket, majd ha elerik a detektort, akkor az alapjan frissit a helyzetet, stb.
        private void MainWorkFunction()        
        {
            //Veget ert mar a kiserlet? Ezt torlni fogja ha van meg aktiv foton (Azert kell, mert a start gombok addig nem mukodnek, mig megy a szimulacio)
            bool finished = true;

            //Signal fotonok kezelese
            for (int i = 0; i < signals.Length; i++)
            {
                //ha nem aktiv, akkor folytatjuk a kovetkezovel
                if (signals[i].active == false) continue;
                
                //van aktiv foton, nincs vege a kiserletnek
                finished = false;

                //a foton mozog tovabb jobbra lefele a kepen
                int oldpicX = signals[i].picX;
                int oldpicY = signals[i].picY;
                signals[i].picX += speed_pixel_per_cycle;
                signals[i].picY += speed_pixel_per_cycle;

                //ujra kell majd rajzolni a foton kepet az ablakban (ld. meg megjegyzest a MainWorkFunction vegen)
                pictureBoxMain.Invalidate(new Rectangle(oldpicX - 2, oldpicY - 2, speed_pixel_per_cycle + 4, speed_pixel_per_cycle + 4));

                //ha eleri a detektort, akkor "osszeomlik a hullamfuggvenye" es egy veletlen helyen megjelenik a kepernyon
                if (signals[i].picX >= signaldetectorPicX)
                {
                    //veletlen detektalasi x, y koordinata szamolasa (elszolas)
                    double x = DCQE_calculations.SignalPhotonDistrubution(rnd.NextDouble());
                    double y = DCQE_calculations.SignalPhotonDistrubution(rnd.NextDouble());
                    //a szimmetria miatt a 0-ra tukorszimmetrikus egyik felet szamolta csak, ezert veletlenul tukrozzuk
                    if (rnd.NextDouble() < 0.5) x = -x;
                    if (rnd.NextDouble() < 0.5) y = -y;

                    //kvantum osszefonodas miatt reszlegesen osszeomlik az idler foton polarizaltsagi allapot hullamfuggvenye is
                    // ezert megvaltozik a polarizaltsagi allapota (annak valoszinusege, hogy atmegy +45 fokos polarizatoron, vagyis a radiron):
                    idlers[i].state = DCQE_calculations.ErasingPossibility(x);

                    //x es y transzformalasa bitkep koordinatakka a rajzolashoz
                    int xsrc, ysrc;
                    xsrc = (int)(x * detectorBmp.Width / 2) + detectorBmp.Width / 2;
                    ysrc = (int)(y * detectorBmp.Height / 2) + detectorBmp.Height / 2;
                    //kirajzoljuk feher pottyel
                    DrawSignalDetector(xsrc, ysrc, Color.White);

                    //taroljuk a jovebeni hasznalatra
                    signals[i].detectorX = xsrc;
                    signals[i].detectorY = ysrc;

                    //ez a foton mar tovabbiakban nem aktiv
                    signals[i].active = false;
                }
            }

            //Idler fotonok kezelese
            for (int i = 0; i < idlers.Length; i++)
            {
                //ha nem aktiv, akkor folytatjuk a kovetkezovel
                if (idlers[i].active == false) continue;

                //van aktiv foton, nincs vege a kiserletnek
                finished = false;

                //a foton mozog tovabb jobbra lefele a kepen
                int oldpicX = idlers[i].picX;
                idlers[i].picX += speed_pixel_per_cycle;
                idlers[i].picY -= speed_pixel_per_cycle;

                //ujra kell majd rajzolni a foton kepet az ablakban (ld. meg megjegyzest a MainWorkFunction vegen)
                pictureBoxMain.Invalidate(new Rectangle(oldpicX - 2, idlers[i].picY - 2, speed_pixel_per_cycle + 4, speed_pixel_per_cycle + 4));

                //Ha bent van a radir (a polarizator) es azt eleri, akkor "osszeomlik" es eldol, hogy atjut-e rajta vagy nem
                if (idlers[i].picX >= idlereraserPicX && eraserState != TEraserState.noeraser && idlers[i].erased == 0)
                {
                    //annak valoszinusege, hogy atmegy az idler foton a polarizatoron (radiron)
                    double prob = idlers[i].state;
                    //anti (-45 fokos) bellitas eseten a valoszinuseg pont az ellentete
                    if (eraserState == TEraserState.eraser_anti) prob = 1.0 - prob;
                    //veletlen "dobas", hogy atmegy-e
                    if (rnd.NextDouble() < prob)
                        idlers[i].erased = 1; //atment, sikeres torles
                    else
                        idlers[i].erased = -1; //nem ment atment, anti-torles
                    //ha nem ment at, akkor elnyeli a polarizator, ezert tovabbiakban mar nem aktiv 
                    if (idlers[i].erased == -1) idlers[i].active = false;
                }

                //Ha eleri a detektort, akkor vege
                if (idlers[i].picX >= idlerdetectorPicX)
                { 
                    //nem aktiv
                    idlers[i].active = false;

                    //signal fotonok kepernyojen a detektalasi pontok szinezes igeny szerint, ha az idler foton elerte a detektort (atment a radiron)
                    if (idlers[i].erased >= 0 && colorSetting != TColorSetting.none)
                    {
                        //a signal foton parjanak detektalasi helye (detektor kepernyo koordinatai a kepen)
                        int xsrc = signals[i].detectorX;
                        int ysrc = signals[i].detectorY;
                        //a szin kivalasztasa a beallitas fuggvenyeben
                        Color c = Color.White;
                        if (colorSetting == TColorSetting.red) c = Color.Red;
                        if (colorSetting == TColorSetting.blue) c = Color.Blue;
                        if (colorSetting == TColorSetting.auto)
                        { 
                            if (eraserState == TEraserState.eraser_normal) c = Color.Red;
                            if (eraserState == TEraserState.eraser_anti) c = Color.Blue;
                        }
                        if (c != Color.White)
                            DrawSignalDetector(xsrc, ysrc, c);
                    }

                }
            }
            //Megjegyzes: Bar a pictureBoxMain.Invalidate()-tel minden fotonra meghivjuk a fotonok ujrarajzolasat, igazabol nem fut le egyesevel, hanem csak egyszer fut le a rajzolas a pictureBoxMain_Paint() eljarassal.
            //Az op.rendszer gondoskodik az optimalizalasrol is: a tobb kisebb frissitett teruletbol csinal egy egysegeset, es arra hivja meg a frissites kerest.

            //Ha veget ert a kiserlet, akkor a Start gombokat ujra lehet hasznalni
            if (finished == true && buttonStart1.Enabled == false)
            {
                buttonStart1.Enabled = true;
                buttonStart2.Enabled = true;
            }
        }

        #endregion 

    }
}
