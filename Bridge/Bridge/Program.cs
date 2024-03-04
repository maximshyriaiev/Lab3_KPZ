using System;

namespace Bridge
{
    // Абстрактний клас фігури
    public abstract class Shape
    {
        // Посилання на об'єкт інтерфейсу, що відповідає за рендеринг
        protected IRenderer renderer;

        // Конструктор, який приймає об'єкт інтерфейсу для рендерингу
        public Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        // Метод для відображення фігури
        public abstract void Draw();
    }

    // Конкретна фігура - Коло
    public class Circle : Shape
    {
        public Circle(IRenderer renderer) : base(renderer) { }

        public override void Draw()
        {
            Console.WriteLine("Drawing Circle " + renderer.Render());
        }
    }

    // Конкретна фігура - Квадрат
    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer) { }

        public override void Draw()
        {
            Console.WriteLine("Drawing Square " + renderer.Render());
        }
    }

    // Конкретна фігура - Трикутник
    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer) { }

        public override void Draw()
        {
            Console.WriteLine("Drawing Triangle " + renderer.Render());
        }
    }

    // Інтерфейс для рендерингу
    public interface IRenderer
    {
        string Render();
    }

    // Конкретний клас для векторного рендерингу
    public class VectorRenderer : IRenderer
    {
        public string Render()
        {
            return "as vector";
        }
    }

    // Конкретний клас для растрового рендерингу
    public class RasterRenderer : IRenderer
    {
        public string Render()
        {
            return "as pixels";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення об'єктів для рендерингу
            IRenderer vectorRenderer = new VectorRenderer();
            IRenderer rasterRenderer = new RasterRenderer();

            // Створення фігур та виклик методу Draw() для кожної фігури з різним рендерером
            Shape circle = new Circle(vectorRenderer);
            circle.Draw();

            Shape square = new Square(rasterRenderer);
            square.Draw();

            Shape triangle = new Triangle(vectorRenderer);
            triangle.Draw();
        }
    }
}
