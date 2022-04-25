using System;
using System.IO;

namespace EvolutionSimulation
{  
    public class Hunter {
        public float speed = 1;
        public float energy = 15;
        public int name;

        public Hunter(float SpeedInt, int nameInt) {
            speed = SpeedInt;
            name = nameInt;
        }
        
        public void Exist() {
            energy -= speed;
        }

        public void Hunt() {
            Random rnd = new Random();
            if (rnd.Next(1, 101) <= 1 * PubVars.Rabbits) {
                if (speed >= 1) {
                    float RabbitDistance = 2;
                    for (int i = 0; i < 3; i++) {
                        RabbitDistance -= speed - 1;
                        if (RabbitDistance <= 0) {
                            energy += 30;
                            PubVars.Rabbits--;
                            break;
                        } else {
                            energy -= speed;
                        }
                    }
                    if (RabbitDistance <= speed * energy) {
                        energy += 5;
                        PubVars.Rabbits--;
                    }
                }
            }
        }
    }
    public static class PubVars {
        public static float RabbitsStartingValue = 10;
        public static float Rabbits = RabbitsStartingValue;
        public static int names = 0;

        public static List<Hunter> HunterList = new List<Hunter>();
    }
    class Program
    {
        static void Main(string[] args) {
            Random rand = new Random();
            for (int e = 0; e < 10; e++) {
                PubVars.HunterList.Add(new Hunter(rand.Next(75, 125) / 100, e));
            }
            for (int day = 0; day < 20; day++) {
                Console.WriteLine("Day: " + day);
                DoStuff();
            }
        }
        static void DoStuff()
        {
            PubVars.Rabbits = PubVars.RabbitsStartingValue;
            var NewHunterList = from hunter in PubVars.HunterList orderby hunter.speed select hunter;
            PubVars.HunterList = NewHunterList.ToList();
            foreach (Hunter Target in PubVars.HunterList) {
                Target.Exist();
            }
            for (int y = 0; y < PubVars.RabbitsStartingValue; y++) {
                foreach (Hunter Target in PubVars.HunterList) {
                    if (!(PubVars.Rabbits <= 0)) {
                        Target.Hunt();
                    } else {
                        break;
                    }
                }
                if (PubVars.Rabbits <= 0) {
                    break;
                }
            }
            foreach (Hunter Target in PubVars.HunterList) {
                Console.WriteLine("Name: " + Target.name + "    Energy: " + Target.energy + "    Speed: " + Target.speed);
            }
        }
    }
}