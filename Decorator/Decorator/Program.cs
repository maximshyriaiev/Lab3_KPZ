using System;
using System.Collections.Generic;

namespace Decorator
{
    // Абстрактний клас героя
    public abstract class Hero
    {
        public abstract void Show();

        public abstract int GetPower();
    }

    // Конкретний герой - Воїн
    public class Warrior : Hero
    {
        public override void Show()
        {
            Console.WriteLine("Warrior");
        }

        public override int GetPower()
        {
            return 10;
        }
    }

    // Конкретний герой - Маг
    public class Mage : Hero
    {
        public override void Show()
        {
            Console.WriteLine("Mage");
        }

        public override int GetPower()
        {
            return 8;
        }
    }

    // Конкретний герой - Паладин
    public class Paladin : Hero
    {
        public override void Show()
        {
            Console.WriteLine("Paladin");
        }

        public override int GetPower()
        {
            return 12;
        }
    }

    // Декоратор для інвентарю
    public abstract class InventoryDecorator : Hero
    {
        protected Hero _hero;

        public InventoryDecorator(Hero hero)
        {
            _hero = hero;
        }

        public override void Show()
        {
            _hero.Show();
        }

        public override int GetPower()
        {
            return _hero.GetPower();
        }
    }

    // Конкретний декоратор - зброя
    public class WeaponDecorator : InventoryDecorator
    {
        public WeaponDecorator(Hero hero) : base(hero) { }

        public override void Show()
        {
            base.Show();
            Console.WriteLine("Weapon");
        }

        public override int GetPower()
        {
            return base.GetPower() + 5; // Покращення сили через зброю
        }
    }

    // Конкретний декоратор - броня
    public class ArmorDecorator : InventoryDecorator
    {
        public ArmorDecorator(Hero hero) : base(hero) { }

        public override void Show()
        {
            base.Show();
            Console.WriteLine("Armor");
        }

        public override int GetPower()
        {
            return base.GetPower() + 3; // Покращення сили через броню
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення героя Воїна
            Hero warrior = new Warrior();
            Console.WriteLine("Warrior:");
            warrior.Show();
            Console.WriteLine("Power: " + warrior.GetPower());

            // Додавання зброї до героя
            Hero warriorWithWeapon = new WeaponDecorator(warrior);
            Console.WriteLine("\nWarrior with Weapon:");
            warriorWithWeapon.Show();
            Console.WriteLine("Power: " + warriorWithWeapon.GetPower());

            // Додавання броні до героя
            Hero warriorWithArmor = new ArmorDecorator(warrior);
            Console.WriteLine("\nWarrior with Armor:");
            warriorWithArmor.Show();
            Console.WriteLine("Power: " + warriorWithArmor.GetPower());

            // Додавання інвентарю (броні та зброї) до героя
            Hero warriorWithFullInventory = new WeaponDecorator(new ArmorDecorator(warrior));
            Console.WriteLine("\nWarrior with Full Inventory:");
            warriorWithFullInventory.Show();
            Console.WriteLine("Power: " + warriorWithFullInventory.GetPower());
        }
    }
}
