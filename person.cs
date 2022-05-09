namespace VirusSimulation
{
    enum state{
        uninfected,
        infected,
        recovered
    }
    class person{

        
        private int yPos;
        private int xPos;

        private int YVelocity;

        private int XVelocity;
        private int daysSafe;
        private int daysInfected;
        private bool _beenInfected = false;
        private state condition;

        static Random rng = new Random();

        public int YPos { get => yPos; set => yPos = value; }
        public int XPos { get => xPos; set => xPos = value; }
        public int YVelocity1 { get => YVelocity; set => YVelocity = value; }
        public int XVelocity1 { get => XVelocity; set => XVelocity = value; }
        public int DaysInfected { get => daysInfected; set => daysInfected = value; }
        internal state Condition { get => condition; set => condition = value; }
        public int DaysSafe { get => daysSafe; set => daysSafe = value; }
        public bool BeenInfected { get => _beenInfected; set => _beenInfected = value; }

        public person(){
            Condition = state.infected;
            XPos = rng.Next(2, Console.WindowWidth - 4);
            XVelocity1 = rng.Next(0, 3) - 1;

            YPos = rng.Next(2, Console.WindowHeight - 4);
            YVelocity1 = rng.Next(0, 3) - 1;
            DaysInfected = 0;
        }
        public void update(virus Virus){
            if(Condition == state.infected){
                DaysInfected += 1;

                if(DaysInfected >= Virus.days){
                    daysSafe += 1;
                    Condition = state.recovered;
                }
            }
            if(condition == state.recovered){
                daysSafe +=1;
            }
            if(daysSafe > 20){
                Condition = state.uninfected;
            }
        }

        public void move(List<person> p){
            if(XPos >= Console.WindowWidth - 3 || XPos <= 2){
                XVelocity1 = - XVelocity1;
                YVelocity1 = rng.Next(0,3) - 1;
            }

            if(YPos >= Console.WindowHeight - 3 || YPos <= 2){
                YVelocity1 = - YVelocity1;
                XVelocity1 = rng.Next(0,3) - 1;
            }

            XPos += XVelocity1;
            YPos += YVelocity1;
        }

        private (int, int) FindNearestInfected(List<person> p)
        {
            int nearestX = int.MaxValue;
            int nearestY = int.MaxValue;
            foreach (var person in p)
            {
                if(Math.Abs(XPos - person.XPos) + Math.Abs(YPos - person.YPos) < (nearestX + nearestY)){
                    nearestX = person.xPos;
                    nearestY = person.YPos;
                }
            }

            return (nearestX,nearestY);
        }

        public void Draw(){
            try{Console.SetCursorPosition(XPos, YPos);} catch{ }

            switch(Condition){
                case state.infected:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case state.uninfected:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case state.recovered:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.Write("#");
        }

        public void Infect(person other, virus Virus){
            if(Condition != state.infected) return;
            if(Math.Abs(XPos - other.XPos) > 1) return;
            if(Math.Abs(YPos - other.YPos) > 1) return;
            if(other.Condition != state.uninfected) return;
            if(rng.Next(1, 101) < Virus.infectivity){
                other._beenInfected = true;
                other.Condition = state.infected;
            }
        }
    }
}