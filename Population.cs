using System;

namespace VirusSimulation
{
    class Population
    {
        private List<person> _people = new List<person>();
        private int _size;
        public int Size { get => _size; set => _size = value; }
        internal List<person> People { get => _people; set => _people = value; }

        public Population(int size)
        {
            _size = size;

            for (int i = 0; i < size; i++)
            {
                person p = new person();
                p.Condition = state.uninfected;
                _people.Add(p);
            }
        }

        public void updatePopulation(virus Virus)
        {
            foreach (person p in _people)
            {
                p.move(_people);
                p.Draw();
                p.update(Virus);
            }
        }
    }
}