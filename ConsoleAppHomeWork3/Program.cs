using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppHomeWork3
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            List<Monster> monsters = new List<Monster>();
            int Damage = 20;
            
            string input = null;
            Console.WriteLine("Приветствую, рыцарь! Подвиги ждут тебя!");
            
            do
            {

                OpenMenu();
                input = Console.ReadLine();

                if (input == "1")
                {
                    Monster monster = AddMonster();
                    if(monster != null)
                    monsters.Add(monster);
                }
                else if (input == "2")
                {
                    Monster upgradeMonster = UpgradeMonster(monsters);
                    if (upgradeMonster != null)
                    {
                        monsters.RemoveAt(upgradeMonster.Index);
                        monsters.Insert(upgradeMonster.Index, upgradeMonster);
                    }
                }
                else if (input == "3")
                {
                    Monster hitMonster = HitMonster(monsters);
                    if (hitMonster != null)
                    {
                        hitMonster.TakeDamage(Damage);
                        monsters.RemoveAt(hitMonster.Index);
                        if(hitMonster.Health != 0)
                        { 
                            monsters.Insert(hitMonster.Index, hitMonster); 
                        }
                        else
                        {
                            Console.WriteLine(hitMonster.Type + " " + hitMonster.Name + " уничтожен!");
                        }

                        if (monsters.Count == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("ПОЗДРАВЛЯЕМ, ВСЕ МОНСТРЫ УНИЧТОЖЕНЫ!!!");
                        }
                    }
                    
                }
                else if (input == "4")
                {
                    Monster destroyMonster = DestroyMonster(monsters);
                    if (destroyMonster != null)
                    monsters.RemoveAt(destroyMonster.Index);
                }
                else if (input == "5")
                {
                    if (monsters.Count > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Живые монстры: ");
                        foreach (Monster monster in monsters)
                        {
                            Console.WriteLine(monster.Type + " " + monster.Name + ", Здоровье: " + monster.Health + ", " + monster.Armore + ", " + monster.Invisibility);
                        }
                        Console.WriteLine();
                    }
                    else 
                    { 
                        Console.WriteLine();
                        Console.WriteLine("Нет живых монстров");
                    }
                }

            } while (input?.Trim().ToLower() != "q");
        }

        private static void OpenMenu()
        {
            Console.WriteLine();
            Console.WriteLine("МЕНЮ (выберете один из вариантов)");
            Console.WriteLine("1 Добавить монстра");
            Console.WriteLine("2 Улучшить монстра");
            Console.WriteLine("3 Нанести урон монстру");
            Console.WriteLine("4 Уничтожить монстра");
            Console.WriteLine("5 Вывести информацию о текущих монстрах");
            Console.WriteLine("Для выхода из игры введите q");
            Console.WriteLine();
        }

        private static Monster AddMonster()
        {
            string inputMonster = null;

            do
            {
                MonsterMenu();

                inputMonster = Console.ReadLine();

                if ( inputMonster == "1")
                {
                    Monster _skelet = AddSkelet();
                    return _skelet;
                }
                else if (inputMonster == "2")
                {
                    Monster _zombi = AddZombi();
                    return _zombi;
                }
                else if (inputMonster == "3")
                {
                    Monster _werewolf = AddWerewolf();
                    return _werewolf;
                }
                else if(inputMonster == "q")
                {
                    return null;
                }
                
            }while(true);
            
        }

        private static void MonsterMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1 Добавить скелета");
            Console.WriteLine("2 Добавить зомби");
            Console.WriteLine("3 Добавить оборотня");
            Console.WriteLine();
        }

        private static Monster AddSkelet()
        {
            Random rnd = new Random();
            Monster skelet = new Monster();
            int randomIndex = rnd.Next(skelet.skeletName.Length);
            skelet.Type = "Скелет";
            skelet.Name = skelet.skeletName[randomIndex];
            Console.WriteLine();
            Console.WriteLine("Скелет " + skelet.Name + " добавлен! Атакуйте!");
            return skelet;
        }

        private static Monster AddZombi()
        {
            Random rnd = new Random();
            Monster zombi = new Monster();
            int randomIndex = rnd.Next(zombi.zombiName.Length);
            zombi.Type = "Зомби";
            zombi.Name = zombi.zombiName[randomIndex];
            Console.WriteLine();
            Console.WriteLine("Зомби " + zombi.Name + " добавлен! Атакуйте!");
            return zombi;
        }

        private static Monster AddWerewolf()
        {
            Random rnd = new Random();
            Monster werewolf = new Monster();
            int randomIndex = rnd.Next(werewolf.werewolfName.Length);
            werewolf.Type = "Оборотень";
            werewolf.Name = werewolf.werewolfName[randomIndex];
            Console.WriteLine();
            Console.WriteLine("Оборотень " + werewolf.Name + " добавлен! Атакуйте!");
            return werewolf;
        }

        private static Monster UpgradeMonster(List<Monster> monsters)
        {
            string inputUpMonster = null;
            Monster _upgradeMonster = new Monster();
            int _monsterNumber;
            bool _getMonster = false;

            if (monsters.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите монстра для улучшения:");
                Console.WriteLine();


                do
                {
                    while (!_getMonster)
                    {

                        foreach (Monster monster in monsters)
                        {
                            _monsterNumber = monsters.IndexOf(monster) + 1;
                            Console.WriteLine(_monsterNumber + " " + monster.Type + " " + monster.Name + ", Здоровье: " + monster.Health + ", " + monster.Armore + ", " + monster.Invisibility);
                            Console.WriteLine();
                        }

                        inputUpMonster = Console.ReadLine();
                        int index = int.Parse(inputUpMonster) - 1;
                        if (index >= 0 && index < monsters.Count)
                        {
                            _upgradeMonster = monsters[index];
                            _upgradeMonster.Index = index;
                            _getMonster = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Не верно введен номер монстра, повторите ввод");
                            Console.WriteLine();
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("Выберите улучшение:");
                    Console.WriteLine("1 Добавить броню");
                    Console.WriteLine("2 Добавить невидимость");
                    Console.WriteLine();

                    inputUpMonster = Console.ReadLine();

                    if (inputUpMonster == "1")
                    {
                        _upgradeMonster.IsArmore = true;
                        _upgradeMonster.Armore = "с броней";
                    }
                    else if (inputUpMonster == "2")
                    {
                        _upgradeMonster.IsInvisibility = true;
                        _upgradeMonster.Invisibility = "невидимый";
                    }
                    Console.WriteLine();
                    Console.WriteLine(_upgradeMonster.Type + " " + _upgradeMonster.Name + " улучшен");
                    return _upgradeMonster;

                } while (true);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Нет текущих монстров");
                return null;
            }
        }

        private static Monster HitMonster(List<Monster> monsters)
        {
            string inputHitMonster = null;
            Monster _hitMonster = new Monster();
            int _monsterNumber;
            bool _getHit = false;
            bool _getMonster = false;

            if (monsters.Count > 0)
            {
                while (!_getHit)
                {
                    Console.WriteLine();
                    Console.WriteLine("Выберите нанесение урона:");
                    Console.WriteLine("1 Определенному монстру");
                    Console.WriteLine("2 Случайному монстру");
                    Console.WriteLine();

                    inputHitMonster = Console.ReadLine();

                    if (inputHitMonster == "1")
                    {
                        _getHit = true;
                        Console.WriteLine();
                        Console.WriteLine("Выберите монстра для нанесения урона:");
                        Console.WriteLine();

                        do
                        {
                            while (!_getMonster)
                            {

                                foreach (Monster monster in monsters)
                                {
                                    _monsterNumber = monsters.IndexOf(monster) + 1;
                                    Console.WriteLine(_monsterNumber + " " + monster.Type + " " + monster.Name + ", Здоровье: " + monster.Health + ", " + monster.Armore + ", " + monster.Invisibility);
                                    Console.WriteLine();
                                }

                                inputHitMonster = Console.ReadLine();
                                int index = int.Parse(inputHitMonster) - 1;
                                if (index >= 0 && index < monsters.Count)
                                {
                                    _hitMonster = monsters[index];
                                    _hitMonster.Index = index;
                                    _getMonster = true;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Не верно введен номер монстра, повторите ввод");
                                    Console.WriteLine();
                                }
                            }
                            Console.WriteLine();
                            Console.WriteLine(_hitMonster.Type + " " + _hitMonster.Name + " получил урон");
                            return _hitMonster;                    
                        } while (true);
                        
                    }
                    else if (inputHitMonster == "2")
                    {
                        _getHit = true;
                        Random rnd = new Random();
                        int index = rnd.Next(0, monsters.Count);
                        _hitMonster = monsters[index];
                        _hitMonster.Index = index;
                        Console.WriteLine();
                        Console.WriteLine(_hitMonster.Type + " " + _hitMonster.Name + " получил урон");
                        return _hitMonster;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Не верный ввод");
                        Console.WriteLine();
                    }
                    
                }

                
                return _hitMonster;

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Нет текущих монстров");
                return null;
            }
        }

        private static Monster DestroyMonster(List<Monster> monsters)
        {
            string inputDestMonster = null;
            Monster _destroyMonster = new Monster();
            int _monsterNumber;
            bool _getMonster = false;

            if (monsters.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите монстра для уничтожения:");
                Console.WriteLine();

                do
                {
                    while (!_getMonster)
                    {

                        foreach (Monster monster in monsters)
                        {
                            _monsterNumber = monsters.IndexOf(monster) + 1;
                            Console.WriteLine(_monsterNumber + " " + monster.Type + " " + monster.Name + ", Здоровье: " + monster.Health + ", " + monster.Armore + ", " + monster.Invisibility);
                            Console.WriteLine();
                        }

                        inputDestMonster = Console.ReadLine();
                        int index = int.Parse(inputDestMonster) - 1;
                        if (index >= 0 && index < monsters.Count)
                        {
                            _destroyMonster = monsters[index];
                            _destroyMonster.Index = index;
                            _getMonster = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Не верно введен номер монстра, повторите ввод");
                            Console.WriteLine();
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine(_destroyMonster.Type + " " + _destroyMonster.Name + " уничтожен");
                    return _destroyMonster;

                } while (true);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Нет текущих монстров");
                return null;
            }

        }
               
    }

    public class Monster
    {
        public string[] skeletName = new string[] { "Боб", "Ден", "Билл", "без имени" };
        public string[] zombiName = new string[] { "Дон", "Бутч", "Айзек", "без имени" };
        public string[] werewolfName = new string[] { "Тони", "Стен", "Кайл", "без имени" };
        public string Type { get; set; }
        public string Name { get; set; }

        public int Health = 100;

        public string Armore = "без брони";

        public bool IsArmore = false;

        public string Invisibility = "видимый";

        public bool IsInvisibility = false;

        public int Index;
                
        public void TakeDamage(int damage)
        {
            if(this.IsArmore == false)
            {
                this.Health -= damage;
            }
            else
            {
                this.Health -= damage / 2;
            }
        }
    }
}
