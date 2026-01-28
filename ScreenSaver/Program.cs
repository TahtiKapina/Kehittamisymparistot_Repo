namespace ScreenSaver
{
    using System.Numerics;
    using Raylib_cs;
    internal class Program
    {
        static void Main(string[] args)
        {
            int screen_width = 800;
            int screen_height = 800;

           

            Raylib.InitWindow(screen_height, screen_width, "ScreenSaver");

            Vector2 A = new Vector2(screen_width / 2, 40);
            Vector2 B = new Vector2(40, screen_height / 2);
            Vector2 C = new Vector2(screen_width - 40, screen_height * 0.75f);

            Vector2 dirA = new Vector2(1, 1);
            Vector2 dirB = new Vector2(1, -1);
            Vector2 dirC = new Vector2(-1, 1);

            float speed = 200.0f;

            while (Raylib.WindowShouldClose() == false)
            {

            }

            Raylib.CloseWindow();
        }
    }
}
