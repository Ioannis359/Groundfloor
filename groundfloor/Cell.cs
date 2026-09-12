using System;

namespace Cell
{

    enum CellAction
    {
        Suicide = 10000,
        Kill = 20000,
        Work = 30000,
        Sleep = 40000,
        Entertain = 50000,
        Eat = 60000,
        Teach = 70000,
        Distract = 80000,
    }


    class Cell
    {

        public const float EatCost = 1.0f;
        public const float EatSatietyIncrease = 1.0f;
        public const float DailyInsanityDecrease = -0.004f;
        public const float DailySatietyDecrease = -(float)(1 / 12); //The average person in germany eats for 95 Minutes a day.
                                                                    //Nach float Literale müssen von einen f gefolget werden da c# sonst annihmt das sind doubles und doubles zu floats zu konvertieren kann zu Geneuichkeitsverlost führen.
                                                                    //Da ich diese folgende f nicht nach einer Klammer packen kann muss ich es typecasten.
        public const float DailyWakefulnessDecrease = -0.033f; //Most people sleep for a third of the day.
        public const float WorkMoneyIncrease = 1.0f;
        public const float WorkVitalityDecrease = -0.2f;
        public const float EntertainVitalityIncrease = 0.5f;
        //The average person works for 5 days and then rests for 2 days.
        public const float WorkSatietyDecrease = -0.05f;
        public const float InsanityWorkCutOff = 0.5f;
        public const float PersoDeviation = 0.10f;
        public const float SleepWakeFulnessIncrease = 1.0f;
        //Determines how mush the personality factors can deviate from 1.0;

        public static float ValueAdd(float v, float inc)
        {
            return (float)Math.Tanh(v + inc) * 1.31304f;
        }

        public float Insanity { get; private set; }
        public float Knowledge { get; private set; }
        public float Wakefulness { get; private set; }
        public float Satiety { get; private set; }
        public float Money { get; private set; }
        public float Vitality { get; private set; }

        public float[] Perso { get; private set; }
        //Holds personality factors.

        private const int PersoDailyInsanityDecreaseIndex = 0;
        private const int PersoDailyWakefulnessDecreaseIndex = 1;
        private const int PersoDailySatietyDecreaseIndex = 2;
        private const int PersoWorkMoneyIncreaseIndex = 3;
        private const int PersoWorkMoneyDecreaseIndex = 4;
        private const int PersoEatSatietyIncreaseIndex = 5;
        private const int PersoWorkVitalityDecreaseIndex = 6;
        private const int PersoWorkSatietyDecreaseIndex = 7;
        private const int PersoSleepWakefulnessIncrease = 8;

        private const int AmountOfPersoFactors = 9;

        private static Random RndObj;
        //Since Objects handeling randomness are costly to create I am using a single static one

        public Cell()
        {
            this.Insanity = 0.0f;
            this.Knowledge = 0.0f;
            this.Wakefulness = 1.0f;
            this.Satiety = 1.0f;
            this.Money = 1.0f;
            this.Vitality = 1.0f;

            if (Cell.RndObj == null)
            {
                Cell.RndObj = new Random();
            }

            this.Perso = new float[AmountOfPersoFactors];

            for (int i = 0; i < Perso.Length; i++)
            {
                this.Perso[i] = 1 + Cell.RndObj.NextInt64(0 - (Int64)(Int64.MaxValue * PersoDeviation), 1 + (Int64)(Int64.MaxValue * PersoDeviation));
                //The 1 is correct since NectInt64 will never return a number equal to maxValue (the second parameter), it will only return numbers smaller than maxValue.
            }

        }

        public void TickCost()
        { //A single tick is supposed to represent a day.
            this.Insanity = ValueAdd(this.Insanity, DailyInsanityDecrease) * this.Perso[Cell.PersoDailyInsanityDecreaseIndex];
            this.Satiety = ValueAdd(this.Satiety, DailySatietyDecrease) * this.Perso[Cell.PersoDailySatietyDecreaseIndex];
            this.Wakefulness = ValueAdd(this.Wakefulness, DailyWakefulnessDecrease) * this.Perso[Cell.PersoDailyWakefulnessDecreaseIndex];
        }

        public bool Eat()
        {
            if (this.Money < EatCost) { return false; }
            this.Money -= EatCost * this.Perso[PersoWorkMoneyDecreaseIndex];
            this.Satiety = ValueAdd(this.Satiety, EatSatietyIncrease) * this.Perso[Cell.PersoEatSatietyIncreaseIndex];
            return true;
        }

        public bool Work()
        {
            if (this.Insanity < InsanityWorkCutOff) { return false; }
            this.Vitality = ValueAdd(this.Vitality, WorkVitalityDecrease) * this.Perso[Cell.PersoWorkVitalityDecreaseIndex]; 
            this.Money += WorkMoneyIncrease * this.Perso[PersoWorkMoneyIncreaseIndex];
            this.Satiety = ValueAdd(this.Satiety, WorkSatietyDecrease) * this.Perso[Cell.PersoWorkSatietyDecreaseIndex];
            return true;
        }

        public bool Sleep()
        {
            this.Wakefulness = ValueAdd(this.Wakefulness, SleepWakeFulnessIncrease) * this.Perso[Cell.PersoSleepWakefulnessIncrease];
        }
    }

}