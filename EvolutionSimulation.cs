using System;

namespace Helloworld
{  
    static public class Prey {
        static public float Rabbits = 10;
        static public float Foxes = 0;
    }  
    class Hunter 
    {
        float speed = 4;
        float sight = 4;
        float stealth = 4;
        Hunter(float speedFloat, float sightFloat, float stealthFloat) {
            speed = speedFloat;
            sight = sightFloat;
            stealth = stealthFloat;
        }
        void Hunt() {
            Prey.Rabbits--;
        }
    }
    class Bunny {
        float speed = 2;
        float vigilance = 5;
        float stealth = 3;
    }
    class Fox {
        float speed = 6;
        float vigilance = 2;
        float stealth = 1;
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}