namespace Pong
{
    using System.Numerics;
    using Raylib_cs;

    internal class Program
    {
        int player1Score = 0;
        int player2Score = 0;

        int PlayerToWall = 60;
        float ballSize = 10;
        Vector2 PlayerSize = new Vector2(40, 200);


        static void Main(string[] args)
        {
            Program pong = new Program();
            pong.Run();
        }

        void Run()
        {
            Raylib.InitWindow(800, 600, "Pong");
            Raylib.SetTargetFPS(60);

            int ScreenHeight = Raylib.GetScreenHeight();
            int ScreenWidth = Raylib.GetScreenWidth();

            Vector2 Player1 = new Vector2(0 + PlayerToWall, ScreenHeight / 2 - PlayerSize.Y / 2);
            Vector2 Player2 = new Vector2(ScreenWidth - PlayerSize.X - PlayerToWall, ScreenHeight / 2 - PlayerSize.Y / 2);

            Vector2 ballPosition = Raylib.GetScreenCenter();
            Vector2 ballDirection = Vector2.Normalize(new Vector2(1, 0.5f));

            float ballSpeed = 640;
             

            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                string scoreText1 = player1Score.ToString();
                string scoreText2 = $"{player2Score}";
                Raylib.DrawText(scoreText1, ScreenWidth / 4, 50, 40, Color.White);
                Raylib.DrawText(scoreText2, (ScreenWidth / 4) * 3, 50, 40, Color.White);

                Raylib.DrawRectangleV(Player1, PlayerSize, Color.Red);
                Raylib.DrawRectangleV(Player2, PlayerSize, Color.Blue);
                Raylib.DrawCircleV(ballPosition, ballSize, Color.White);

                ballPosition = ballPosition + ballDirection * ballSpeed * Raylib.GetFrameTime();

                if (Raylib.CheckCollisionPointRec(ballPosition, new Rectangle(Player1, PlayerSize)))
                {
                    ballDirection.X *= -1;
                }

                if (Raylib.CheckCollisionPointRec(ballPosition, new Rectangle(Player2, PlayerSize)))
                {
                    ballDirection.X *= -1;
                }

                {
                    if (ballPosition.X - ballSize <= 0.0f)
                    {
                        player2Score++;
                        ballPosition = Raylib.GetScreenCenter();
                        ballDirection.X *= -1;
                    }
                    else if (ballPosition.X + ballSize >= ScreenWidth)
                    {
                        player1Score++;
                        ballPosition = Raylib.GetScreenCenter();
                        ballDirection.X *= -1;
                    }

                    if (ballPosition.Y - ballSize <= 0.0f)
                    {
                        ballPosition.Y = ballSize;
                        ballDirection.Y = -ballDirection.Y;
                    }
                    else if (ballPosition.Y + ballSize >= ScreenHeight)
                    {
                        ballPosition.Y = ScreenHeight - ballSize;
                        ballDirection.Y = -ballDirection.Y;
                    }
                }

                if (Raylib.IsKeyDown(KeyboardKey.W))
                {
                    Player1.Y -= 400 * Raylib.GetFrameTime();
                }
                
                if (Raylib.IsKeyDown(KeyboardKey.S))
                {
                    Player1.Y += 400 * Raylib.GetFrameTime();
                }
                
                if (Raylib.IsKeyDown(KeyboardKey.Up))
                {
                    Player2.Y -= 400 * Raylib.GetFrameTime();
                }

                if (Raylib.IsKeyDown(KeyboardKey.Down))
                {
                    Player2.Y += 400 * Raylib.GetFrameTime();
                }

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
