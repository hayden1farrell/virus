using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusSimulation
{


    class Program
    {


        static void Main(string[] args)
        {

            string DName = "";
            int DInfectionRate = 0;
            int DIllDays = 0;
            int DstartIll = 0;
            int PopulationSize = 0;
            char choice;
            do
            {
                Console.WriteLine("Suggest you maximize screen for large population");
                Console.Write("Use Corona Virus Simulation(S) or create your own disease(D)? :");
                choice = Console.ReadLine().ToUpper()[0];
                switch (choice)
                {
                    case 'D':
                        GetDiseaseDetails(ref DName, ref DInfectionRate, ref DIllDays, ref PopulationSize, ref DstartIll);
                        break;
                    case 'S':
                        DName = "Covid 19";
                        DInfectionRate = 70;
                        DIllDays = 14;
                        PopulationSize = 500;
                        DstartIll = 1;
                        break;
                    default:
                        Console.WriteLine("Not recognised choice.Hit Enter to try again");
                        Console.ReadLine();
                        break;
                }

            } while (choice != 'D' && choice != 'S');
            virus userVirus = new virus(DName, DIllDays, DInfectionRate);
            Population pop = new Population(PopulationSize);
            Simulation pandemic = new Simulation(userVirus, pop, DstartIll);

            Console.WriteLine("PRESS ENTER TO START SIMULATION");
            Console.ReadLine();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();

            Console.CursorVisible = false;

            pandemic.Run();


            Console.Clear();
            Console.ReadLine();

        }



        static void GetDiseaseDetails(ref string Name, ref int infection, ref int days, ref int people, ref int start)
        {
            Console.Write("Enter Disease Name :");
            Name = Console.ReadLine();

            infection = -1;
            do
            {
                Console.Write("Enter Probability of Infection from contact between 0 (0%) and 100 (100%):");

                try
                {
                    infection = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    infection = -1;
                }
            } while (infection < 0 && infection > 100);
            days = 0;
            do
            {
                Console.Write("Enter duration of illness in days (min 7):");

                try
                {
                    days = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    days = 0;
                }
            } while (days < 7);
            people = 0;
            do
            {
                Console.Write("Enter Number of people in population (min 50):");

                try
                {
                    people = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    people = 0;
                }
            } while (people < 50);
            start = 0;
            do
            {
                Console.Write("Enter Number of people starting with infection (min 1):");

                try
                {
                    start = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    start = 0;
                }
            } while (start < 1);
        }
    }

    //------------------------------------------------------------------------

    //------------------------------------------------------------------------

    public class virus
    {
        public string Name;
        public int days;
        public int infectivity;


        public virus(string _name, int _days, int _infectivity)
        {
            Name = _name;
            days = _days;
            infectivity = _infectivity;
        }
    }

}