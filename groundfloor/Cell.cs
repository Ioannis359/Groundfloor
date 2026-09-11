using Math;

const float EatCost = 1.0;
const float DailyInsanityDecrease = -0.004;
const float DailySatietyDecrease = -(1/12);
const float DailyWakefulnessDecrease = -0.033;

enum CellAction {
Suicide = 10000,
Kill = 20000,
Work = 30000,
Sleep = 40000,
Entertain = 50000,
Eat = 60000,
Teach = 70000,
Distract = 80000,
Disstance = 90000, 
}

float ValueAdd(float v, float inc) {
    return Math.TanH(v + inv) * 1.31304;
}

class Cell {
    public float Insanity { get; private set; };
    public float Knowledge { get; private set; };
    public float Wakefulness { get; private set; };
    public float Satiety { get; private set; };
    public float Money { get; private set; };
    
    public Cell() {
        this.Insanity = 0.0;
        this.Knowledge = 0.0;
        this.Wakefulness = 1.0;
        this.Satiety = 1.0;
        this.Money = 1.0;
    }

    public void TickCost() { //A single tick is supposed to represent a day.
        this.Insanity = ValueAdd(this.Insanity, DailyInsanityDecrease);
        this.Satiety = ValueAdd(this.Satiety, DailySatietyDecrease); //The average Person in germany in a day eats fom 95 minutes.
        this.Wakefulness = ValueAdd(this.Wakefulness, DailyWakefulnessDecrease); //Since the average person sleeps for a third of the day and we cant have multiple actions in a day we decrease the sleep value by a third.
    }

    public bool Eat() {
        //..
    }
}