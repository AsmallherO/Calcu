using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GruntTestRosNeft
{
    /// <summary>
    /// В этом классе хранятся статичные поля для типов грунта 
    /// Данные взяты из Таблицы Б.1 Приложения Б
    /// Так же есть публичные методы формул
    /// </summary>
    internal class StaticData
    {
        public float Formula_TBF(float A,float B,float Dsal,float ltot, float Wtot, float Wm)
        {
            float TBF;
            float Cps = Formula_CPS(Dsal,ltot,Wtot,Wm);
            float Cps2 = Cps*Cps;
            TBF = A - B * (53 * Cps + 40 * Cps2);
            return TBF;
        }
        public float Formula_CPS(float Dsal,float ltot,float Wtot,float Wm)
        {
            float W = 0;
            if (ltot<= 0.4f)
            {
                W = Wtot;
            }
            else if(ltot > 0.4f)
            {
                W = Wm;
            }
            float Cps = Dsal / (Dsal+100*W);
            return Cps;
        }
        //Тип грунта
        private readonly float Peski = -0.1f; 
        private static readonly float Suspeci = -0.15f; 
        private static readonly float SuGlinok = -0.2f; 
        private static readonly float Glina = -0.25f; 
        public float Peski1
        {
            get { return Peski; }
        }
        public float Suspeci1
        {
            get { return Suspeci; }
        }
        public float SuGlenok
        {
            get { return SuGlinok; }
        }
        public float GLina1
        {
            get { return Glina; }
        }
    }
}
