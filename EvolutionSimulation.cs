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
            Random rnd = new Random();
            energy -= speed * 2;
            if (energy <= 0) {
                PubVars.NewHunterList.Remove(this);
            }
            if (energy > 15) {
                PubVars.NewHunterList.Add(new Hunter(speed + rnd.Next(-25, 26) / 100f, PubVars.names));
                PubVars.names++;
                energy -= 5;
            }
        }

        public void Hunt() {
            Random rnd = new Random();
            if (rnd.Next(1, 101) <= 2 * PubVars.Rabbits) {
                if (speed >= 1) {
                    float RabbitDistance = 2;
                    for (int i = 0; i < 3; i++) {
                        RabbitDistance -= speed - 1;
                        if (RabbitDistance <= 0) {
                            energy += 10;
                            PubVars.Rabbits--;
                            break;
                        } else {
                            energy -= speed / 2;
                        }
                    }
                    if (RabbitDistance <= speed) {
                        energy += 10;
                        PubVars.Rabbits--;
                    }
                }
            }
        }
    }
    public static class PubVars {
        public static int RabbitsStartingValue = 10;
        public static int Rabbits = RabbitsStartingValue;
        public static int names = 0;

        public static List<Hunter> HunterList = new List<Hunter>();
        public static List<Hunter> NewHunterList = new List<Hunter>();
    }
    class Program
    {
        static void Main(string[] args) {
            File.Delete("EvolutionSimulationOutput.txt");
            using StreamWriter outputFile = new StreamWriter("EvolutionSimulationOutput.txt");
            Random rand = new Random();
            for (int e = 0; e < 25; e++) {
                PubVars.HunterList.Add(new Hunter(rand.Next(75, 126) / 100f, PubVars.names));
                PubVars.names++;
            }
            for (int day = 0; day < 10000; day++) {
                outputFile.WriteLine(string.Concat(Enumerable.Repeat("-", 100)));
                outputFile.WriteLine("Day: " + day);
                outputFile.WriteLine(string.Concat(Enumerable.Repeat("-", 100)));
                DoStuff();
                float AverageSpeed = 0;
                foreach (Hunter Target in PubVars.HunterList) {
                    AverageSpeed += Target.speed;
                    outputFile.WriteLine("Name: " + Target.name + "    Energy: " + Target.energy + "    Speed: " + Target.speed);
                }
                AverageSpeed = AverageSpeed / PubVars.HunterList.Count;                
            }
        }
        static void DoStuff()
        {
            PubVars.Rabbits = PubVars.RabbitsStartingValue;
            var HunterList2 = from hunter in PubVars.HunterList orderby hunter.speed descending select hunter;
            PubVars.HunterList = HunterList2.ToList();
            PubVars.NewHunterList.Clear();
            PubVars.NewHunterList.AddRange(PubVars.HunterList);
            foreach (Hunter Target in PubVars.HunterList) {
                Target.Exist();
            }
            PubVars.HunterList.Clear();
            PubVars.HunterList.AddRange(PubVars.NewHunterList);
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
        }
    }
}