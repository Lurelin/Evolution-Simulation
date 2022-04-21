using System;

namespace Helloworld
{    class Entity {
        public int health = 3;
        public int aggressiveness = 10;

        public Entity(int aggressivenessInt) {
            if (aggressivenessInt >= 100) {
                aggressiveness = 100;
            } else {
                aggressiveness = aggressivenessInt;
            }
        }
        public void Attack(Entity target) {
            Random RNG = new Random();
            int CoinFlip = RNG.Next(1, 3);
            if (CoinFlip == 1) {
                health -= 2;
            } else {
                target.health--;
                health++;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Random Rand = new Random();
            List<Entity> EntityList = new List<Entity>();
            for (int day = 0; day < 5; day++) {
                Console.WriteLine("Day " + day);
                for (int i = 0; i < 5; i++) {
                    EntityList.Add(new Entity(50));
                }
                Console.WriteLine("Entities: " + EntityList.Count);
                foreach (Entity TargetEntity in EntityList) {
                    if (Rand.Next(1, 101) <= TargetEntity.aggressiveness) {
                        Entity AttackEntity = new Entity(10);
                        while (true) {
                            AttackEntity = EntityList[Rand.Next(0, EntityList.Count)];
                            if (EntityList.IndexOf(AttackEntity) != EntityList.IndexOf(TargetEntity)) {
                                Console.WriteLine("Entity" + EntityList.IndexOf(TargetEntity) + " attacked " + "Entity" + EntityList.IndexOf(AttackEntity));
                                break;
                            }
                        }
                        TargetEntity.Attack(AttackEntity);
                    }
                }
                for (int x = 0; x < EntityList.Count; x++) {
                    if (EntityList[x].health <= 0) {
                        Console.WriteLine("Entity" + x + " Died");
                        EntityList.Remove(EntityList[x]);
                        x--;
                    } else {
                        Console.WriteLine("Health of Entity" + x + ": " + EntityList[x].health);
                    }
                }
            }
        }
    }
}