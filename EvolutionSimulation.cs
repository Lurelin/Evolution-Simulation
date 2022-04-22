using System;
using System.IO;

namespace Helloworld
{  
    static public class Entities {
        static public int Rabbits = 10;
        static public int Day = 1;

        static public int NamesUsed = 0;

        static public List<Hunter> HunterList = new List<Hunter>();
        static public List<Hunter> NewHunterList = new List<Hunter>();

    }  
    public class Hunter 
    {
        public float speed = 4;
        public float sight = 4;
        public float stealth = 4;
        public float food = 0;

        public int name;
        public Hunter(float speedFloat, float sightFloat, float stealthFloat, int nameInt) {
            speed = speedFloat;
            sight = sightFloat;
            stealth = stealthFloat;
            name = nameInt;
        }
        public void Hunt() {
            Random RNG = new Random();
            int Dice = RNG.Next(1, 7);
            float Chance = 0;
            if (speed > Bunny.speed) {
                Chance += speed - Bunny.speed;
            }
            if (sight > Bunny.stealth) {
                Chance += sight - Bunny.stealth;
            }
            if (stealth > Bunny.vigilance) {
                Chance += stealth - Bunny.vigilance;
            }
            int Bunnies = Entities.Rabbits;
            for (int i = 0; i < Bunnies; i++) {
                Dice = RNG.Next(1, 7);
                if (Chance > Dice) {
                    Entities.Rabbits--;
                    food++;
                }
            }
        }
        public void Reproduce() {
            if (food >= 1) {
                Entities.NewHunterList.Add(new Hunter(4, 4, 4, name));
            }
            if (food >= 2) {
                for (int z = 1; z < food; z++) {
                    float NewSpeed = speed - 0.25f;
                    float NewSight = sight - 0.25f;
                    float NewStealth = stealth - 0.25f;
                    Random TraitRandom = new Random();
                    for (int t = 0; t < 3; t++) {
                        int RandomInt = TraitRandom.Next(0, 3);
                        if (RandomInt == 0) {
                            NewSpeed += 0.25f;
                        }
                        if (RandomInt == 1) {
                            NewSight += 0.25f;
                        }
                        if (RandomInt == 2) {
                            NewStealth += 0.25f;
                        }
                    }
                    Entities.NewHunterList.Add(new Hunter(NewSpeed, NewSight, NewStealth, Entities.NamesUsed + 1));
                    Entities.NamesUsed++;
                }
            }
        }
    }
    public static class Bunny {
        static public float speed = 2;
        static public float vigilance = 5;
        static public float stealth = 3;
    }
    class Program
    {
        static void Main(string[] args)
        {
            File.Delete("EvolutionSimulationOutput.txt");
            using StreamWriter outputFile = new StreamWriter("EvolutionSimulationOutput.txt");

            Entities.HunterList.Add(new Hunter(4, 4, 4, 0));
            for (int y = 1; y < 21; y++) {
                Entities.Day = y;
                outputFile.WriteLine(string.Concat(Enumerable.Repeat("-", 100)));
                outputFile.WriteLine("Day: " + Entities.Day + "      Entities: " + Entities.HunterList.Count);
                outputFile.WriteLine(string.Concat(Enumerable.Repeat("-", 100)));
                Entities.Rabbits = 10;
                Entities.NewHunterList.Clear();
                ShuffleHunterList();
                foreach (Hunter Target in Entities.HunterList) {
                    Target.Hunt();
                    outputFile.WriteLine("Name: " + Target.name + " - Food: " + Target.food + " - Speed: " + Target.speed + " - Sight: " + Target.sight + " - Stealth: " + Target.stealth);
                    Target.Reproduce();
                }
                Entities.HunterList.Clear();
                Entities.HunterList.AddRange(Entities.NewHunterList);
                float AverageSpeed = 0;
                float AverageSight = 0;
                float AverageStealth = 0;
                foreach (Hunter Target in Entities.HunterList) {
                    AverageSpeed += Target.speed;
                    AverageSight += Target.sight;
                    AverageStealth += Target.stealth;
                }
                AverageSpeed = AverageSpeed / Entities.HunterList.Count;
                AverageSight = AverageSight / Entities.HunterList.Count;
                AverageStealth = AverageStealth / Entities.HunterList.Count;
                outputFile.WriteLine("Average Speed: " + AverageSpeed);
                outputFile.WriteLine("Average Sight: " + AverageSight);
                outputFile.WriteLine("Average Stealth: " + AverageStealth);
            }
        }
        static void ShuffleHunterList() {
            Random rnglistorder = new Random();
            Entities.HunterList.OrderBy(a => rnglistorder.Next());
        }
    }
}