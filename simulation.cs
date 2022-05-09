namespace VirusSimulation{
    class Simulation{
        private int _daysIn;
        private Population _simPop;
        private virus _Virus;
        private int _infected = 0;
        private int _recoverd = 0;
        private int totalInfected = 0;

        private List<int> _dailyInfected = new List<int>();
        public int Details { get => _daysIn; set => _daysIn = value; }
        public virus Virus { get => _Virus; set => _Virus = value; }
        public int Infected { get => _infected; set => _infected = value; }
        public int Recoverd { get => _recoverd; set => _recoverd = value; }
        internal Population SimPop { get => _simPop; set => _simPop = value; }
   
        public Simulation(virus Virus, Population p, int startInfections){
            _Virus = Virus;
            _daysIn = 0;
            _infected = startInfections;
            _simPop = p;

            for(int i = 0; i < startInfections; i++){
                _simPop.People[i].Condition = state.infected;
            }
        }

        public void SimInfection(){
            foreach (person infector in _simPop.People)
            {
                foreach (person infectee in _simPop.People)
                {
                    infector.Infect(infectee, _Virus);
                }
            }
        }

        public void AddUpNumbers(){
            totalInfected = 0;
            _infected = 0;
            _recoverd = 0;
            foreach (person p in _simPop.People)
            {
                if(p.BeenInfected) totalInfected++;
                if(p.Condition == state.infected) _infected++;
                if(p.Condition == state.recovered) _recoverd++;
            }
        }
        public void Summary(){
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{_Virus.Name} is over");
            double popInfected = Math.Round(((double)totalInfected / (double)_simPop.Size) * 100,2);
            Console.WriteLine($"Duration: {_daysIn}\nTotal infected = {totalInfected} ({popInfected}%)");

            Console.WriteLine("Daily infected amount of people");
            foreach (int infectedNum in _dailyInfected)
            {
                Console.Write(infectedNum + ", ");
            }

            Console.ReadLine();
        }

        public void Run(){
            while(_infected > 0){
                Console.Clear();
                _daysIn += 1;
                _simPop.updatePopulation(_Virus);
                SimInfection();
                AddUpNumbers();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0,0);
                Console.Write($"Pop: {_simPop.Size} | DayNum: {_daysIn} Infected: {_infected} REcoverd: {_recoverd}, Disease: {_Virus.Name}");
                _dailyInfected.Add(_infected);
                System.Threading.Thread.Sleep(500);

                if(Console.KeyAvailable){
                    ConsoleKeyInfo k = Console.ReadKey(false);
                    if(k.Key == ConsoleKey.Escape){
                        break;
                    }
                }
            }
            Summary();
        }
    }
}