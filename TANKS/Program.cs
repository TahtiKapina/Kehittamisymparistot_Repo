namespace TANKS
{
    using System.Numerics;
    using Raylib_cs;

    internal class Program
    {
        static void Main(string[] args)
        {
            Program TANKS = new Program();
            TANKS.run();
        }

        void run()
        {
            Raylib.InitWindow(800, 600, "TANKS");
            Raylib.SetTargetFPS(60);

            Tank player1 = new Tank(new Vector2(100, 300), Color.Red);
            Tank player2 = new Tank(new Vector2(700, 300), Color.Blue);

            List<Wall> walls = new List<Wall>();
            walls.Add(new Wall(new Vector2(400, 300), new Vector2(40, 200)));

            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Gray);

                foreach (Wall wall in walls) wall.Draw();
                player1.Draw();
                player2.Draw();

                player1.bullet.Update(walls);
                player2.bullet.Update(walls);

                bool player1moveing = false;
                bool player2moveing = false;

                if (Raylib.IsKeyDown(KeyboardKey.W) && player1moveing == false)
                {
                    // Check if moving Up hits a wall
                    Rectangle nextRect = new Rectangle(player1.position.X - 20, player1.position.Y - 22, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;

                    if (!hit) player1.position.Y -= 2;
                    player1.direction = new Vector2(0, -1);
                    player1moveing = true;
                }
                if (Raylib.IsKeyDown(KeyboardKey.S) && player1moveing == false)
                {
                    Rectangle nextRect = new Rectangle(player1.position.X - 20, player1.position.Y - 18, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;

                    if (!hit) player1.position.Y += 2;
                    player1.direction = new Vector2(0, 1);
                    player1moveing = true;
                }
                if (Raylib.IsKeyDown(KeyboardKey.A) && player1moveing == false)
                {
                    Rectangle nextRect = new Rectangle(player1.position.X - 22, player1.position.Y - 20, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;

                    if (!hit) player1.position.X -= 2;
                    player1.direction = new Vector2(-1, 0);
                    player1moveing = true;
                }
                if (Raylib.IsKeyDown(KeyboardKey.D) && player1moveing == false)
                {
                    Rectangle nextRect = new Rectangle(player1.position.X - 18, player1.position.Y - 20, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;

                    if (!hit) player1.position.X += 2;
                    player1.direction = new Vector2(1, 0);
                    player1moveing = true;
                }

                if (Raylib.IsKeyDown(KeyboardKey.Up) && player2moveing == false)
                {
                    Rectangle nextRect = new Rectangle(player2.position.X - 20, player2.position.Y - 22, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;

                    if (!hit) player2.position.Y -= 2;
                    player2.direction = new Vector2(0, -1);
                    player2moveing = true;
                }
                if (Raylib.IsKeyDown(KeyboardKey.Down) && player2moveing == false)
                {
                    Rectangle nextRect = new Rectangle(player2.position.X - 20, player2.position.Y - 18, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;

                    if (!hit) player2.position.Y += 2;
                    player2.direction = new Vector2(0, 1);
                    player2moveing = true;
                }
                if (Raylib.IsKeyDown(KeyboardKey.Left) && player2moveing == false)
                {
                    Rectangle nextRect = new Rectangle(player2.position.X - 22, player2.position.Y - 20, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;

                    if (!hit) player2.position.X -= 2;
                    player2.direction = new Vector2(-1, 0);
                    player2moveing = true;
                }
                if (Raylib.IsKeyDown(KeyboardKey.Right) && player2moveing == false)
                {
                    Rectangle nextRect = new Rectangle(player2.position.X - 18, player2.position.Y - 20, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;

                    if (!hit) player2.position.X += 2;
                    player2.direction = new Vector2(1, 0);
                    player2moveing = true;
                }

                if (Raylib.IsKeyDown(KeyboardKey.Space))
                {
                    player1.Shoot();
                }

                if (Raylib.IsKeyDown(KeyboardKey.Enter))
                {
                    player2.Shoot();
                }

                Raylib.EndDrawing();
            }
        }

        class Tank
        {
            public Vector2 position;
            public Vector2 direction;

            public Vector2 tankSize;
            public Vector2 turretSize;

            Color color;

            public Bullet bullet = new Bullet();
            double lastShootTime = 0;
            float shootInterval = 1.0f;

            public Tank(Vector2 startPos, Color startColor)
            {
                position = startPos;
                color = startColor;

                tankSize = new Vector2(40, 40);
                turretSize = new Vector2(20, 20);
                direction = new Vector2(1, 0);
            }

            public void Shoot()
            {
                if (Raylib.GetTime() - lastShootTime > shootInterval)
                {
                    bullet.Fire(position, direction);
                    lastShootTime = Raylib.GetTime();
                }
            }

            public void Draw()
            {
                // Tank hull
                Vector2 topLeft = position - tankSize / 2.0f;
                Raylib.DrawRectangleV(topLeft, tankSize, color);

                // Tank turret is positioned using direction
                // Size.X and Y are expected to be same
                Vector2 turretPos = position + direction * (tankSize.X / 2.0f + turretSize.X / 2.0f);
                Vector2 turretTopLeft = turretPos - turretSize / 2.0f;

                Raylib.DrawRectangleV(turretTopLeft, turretSize, Color.Black);
            }
        }

        class Bullet
        {
            public Vector2 pos;
            public Vector2 dir;
            public bool active = false;
            float speed = 5.0f;

            public void Fire(Vector2 startPos, Vector2 direction)
            {
                pos = startPos;
                dir = direction;
                active = true;
            }

            public void Update(List<Wall> walls)
            {
                if (!active) return;

                pos += dir * speed;

                // Deactivate if hits screen edge
                if (pos.X < 0 || pos.X > 800 || pos.Y < 0 || pos.Y > 600) active = false;

                // Deactivate if hits wall
                foreach (Wall wall in walls)
                {
                    if (Raylib.CheckCollisionPointRec(pos, wall.rect)) active = false;
                }
            }

            public void Draw()
            {
                if (active)
                {
                    Raylib.DrawCircleV(pos, 5, Color.Magenta);
                }
            }
        }

        class Wall
        {
            public Rectangle rect;
    
            public Wall(Vector2 pos, Vector2 size)
            {
                rect = new Rectangle(pos.X - size.X / 2, pos.Y - size.Y / 2, size.X, size.Y);
            }

            public void Draw()
            {
                Raylib.DrawRectangleRec(rect, Color.Purple);
            }
        }
    }
}
