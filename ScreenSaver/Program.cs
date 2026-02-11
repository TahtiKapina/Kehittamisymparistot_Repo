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
            Raylib.SetTargetFPS(30);

            Vector2 A = new Vector2(screen_width / 2, 40);
            Vector2 B = new Vector2(40, screen_height / 2);
            Vector2 C = new Vector2(screen_width - 40, screen_height * 0.75f);

            Vector2 dirA = new Vector2(1, 1);
            Vector2 dirB = new Vector2(1, -1);
            Vector2 dirC = new Vector2(-1, 1);

            float speed = 200.0f;
            float radius = 6.0f;

            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib_cs.Color.Black);

                float dt = Raylib.GetFrameTime();
                A += dirA * speed * 1.23f * dt;
                B += dirB * speed * 0.86f * dt;
                C += dirC * speed * 1.52f * dt;

                (A, dirA) = Bounce(A, dirA, radius, screen_width, screen_height);
                (B, dirB) = Bounce(B, dirB, radius, screen_width, screen_height);
                (C, dirC) = Bounce(C, dirC, radius, screen_width, screen_height);

                Raylib.DrawCircleV(A, (int)radius, Raylib_cs.Color.Red);
                Raylib.DrawCircleV(B, (int)radius, Raylib_cs.Color.Blue);
                Raylib.DrawCircleV(C, (int)radius, Raylib_cs.Color.Green);

                Raylib.DrawLineV(A, B, Raylib_cs.Color.Purple);
                Raylib.DrawLineV(B, C, Raylib_cs.Color.SkyBlue);
                Raylib.DrawLineV(C, A, Raylib_cs.Color.Lime);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        private static (Vector2 pos, Vector2 dir) Bounce(Vector2 pos, Vector2 dir, float radius, int screenWidth, int screenHeight)
        {
            if (pos.X - radius <= 0.0f)
            {
                pos.X = radius;
                dir.X = -dir.X;
            }
            else if (pos.X + radius >= screenWidth)
            {
                pos.X = screenWidth - radius;
                dir.X = -dir.X;
            }

            if (pos.Y - radius <= 0.0f)
            {
                pos.Y = radius;
                dir.Y = -dir.Y;
            }
            else if (pos.Y + radius >= screenHeight)
            {
                pos.Y = screenHeight - radius;
                dir.Y = -dir.Y;
            }

            return (pos, dir);
        }
    }
}