using System;


namespace dcqe_test
{
    
    //Ebben vannak az alapveto optikai es valoszinusegi szamitasok, amik a szimulaciohoz kellenek
    static class DCQE_calculations
    {
        #region Seged valtozok es kontansok

        //kepernyo x szerinti eloszlas fuggveny integraljai az egyes esetekhez
        static double[] intdiffract;
        static double dx = 0.001;   //az integral tomb lepeskoze
        static int N = 1000; //az ertelmezesi tartomany 0-tol 1-ig (1-et nem beleertve), ezert N = 1/dx legyen

        //a kettosres restavolsag konstansa (dimenziomentes) az interferencia eloszlashoz
        static double k = 3.0;

        #endregion

        #region Eloszlasi fuggvenyek a signal detektornal (ha nincs es ha van interferencia)

        //Fuggveny definiciok:
        // Mindegyik ert.tartomanya [-1 +1], ertekkeszlete [0 1]

        //Sima  diffrakcios eloszlas (nincs interferencia eloszlasi gorbe)
        public static double f_diffract(double x)
        {
            if (x < 0) x = -x;
            x = x * Math.PI;
            return Math.Sin(x) * Math.Sin(x) / (x * x);
        }

        //interferencia elszolas (interferencia eloszlasi gorbe)
        public static double f_interference(double x)
        {
            if (x < 0) x = -x;
            x = x * Math.PI;
            return Math.Sin(x) * Math.Sin(x) / (x * x) * Math.Cos(k * x) * Math.Cos(k * x);
        }

        //anti-interferencia elszolas (ellentetes interferencia elszolasi gorbe) - nem hasznlaja, csak a szepseg kedveert hagytam bent
        public static double f_interference_reverse(double x)
        {
            if (x < 0) x = -x;
            x = x * Math.PI;
            return Math.Sin(x) * Math.Sin(x) / (x * x) * Math.Sin(k * x) * Math.Sin(k * x);
        }

        #endregion

        #region A fo szamolo fuggvenyek

        //Segedfuggveny az inverz integral szamitashoz, intfx tartalmazza a szamolt integral fuggvenyt, ifx a 0-1 koze eso eloszlasi ertek
        private static double InverseIntagralFx(double[] intfx, double ifx)
        {
            //normalizaljuk az ifx-et (mert 0 es 1 kozott van, de az integral fuggvenytomb nem normalizalt)
            double ifx_norm = ifx * intfx[N - 1];
            //megkeressuk, hogy hova esik az fx az integral fuggvenytombben
            int i = Array.BinarySearch(intfx, ifx_norm);
            //ha valami csoda folytan pont megtalalna a tombben (nagyon ritka)
            if (i >= 0)
            {
                return (double)i * dx;
            }
            //kulonben a visszaadott negatív szamb absz erteke - 1 az az index, ami az elso NAGYOBB erteket tarolja (Microsoft talalta ki igy)
            i = -i - 1;
            //linearis interpolacio a ket tombertek koze
            i = i - 1;
            double x = (double)i * dx;
            double fx1 = intfx[i];
            double fx2 = intfx[i + 1];
            x = (ifx_norm - fx1) / (fx2 - fx1) * dx + x;
            return x;
        }

        //A sima egyenletes eloszlasu veletlen 0-1 erteket attranszormalja a diffrakcios eloszlasi gorbe szerinti 0 es +1 kozotti ertekke (0-ra szimmetrikus)
        public static double SignalPhotonDistrubution(double p)
        {
            //ha meg nem szamoltuk ki, akkor kiszamoljuk a sima diffrakcios eloszlas integraljat (nem normalizalt)
            if (intdiffract == null)
            {
                //a tomb, amiben 0.001 lepeskozzel taroljuk a numerikusan szamolt integralt
                intdiffract = new double[N];
                double fx_prev = 1.0; //x = 0 helyen 1 legyen az erteke a diffrakcios fuggvenynek (mert amugy nem szamolhato)
                //numerikus integralas
                for (int i = 1; i < N; i++)
                {
                    double x = (double)i * dx;
                    double fx = f_diffract(x);
                    intdiffract[i] = intdiffract[i - 1] + dx * 0.5 * (fx + fx_prev);
                    fx_prev = fx;
                }
            }
            //az integral fuggveny inverze adja meg, hogy 0-1 tartomanyban hogyan oszlik el
            double result = InverseIntagralFx(intdiffract, p);
            return result;
        }


        //Annak valoszinuseget szamolja, hogy ha a signal foton x helyre ert a kepernyon (-1 es +1 kozott), akkor az idler atmegy-e a radiron (polarizatoron)
        public static double ErasingPossibility(double x)
        {
            if (f_diffract(x) <= 0) return 0;
            double p = f_interference(x) / f_diffract(x);
            return p;
        }

        #endregion
    }
}
