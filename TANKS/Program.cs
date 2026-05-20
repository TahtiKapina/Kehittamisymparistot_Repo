namespace TANKS
{
    using System.Numerics;
    using System.Collections.Generic;
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

            // Save starting positions so we can snap back to them on a hit
            Vector2 p1Start = new Vector2(100, 300);
            Vector2 p2Start = new Vector2(700, 300);

            Tank player1 = new Tank(p1Start, Color.Red);
            Tank player2 = new Tank(p2Start, Color.Blue);

            List<Wall> walls = new List<Wall>();
            walls.Add(new Wall(new Vector2(400, 300), new Vector2(40, 200)));

            while (Raylib.WindowShouldClose() == false)
            {
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Gray);

                
                foreach (Wall wall in walls) wall.Draw();
                player1.Draw();
                player2.Draw();

               
                foreach (Bullet b in player1.bullets) b.Draw();
                foreach (Bullet b in player2.bullets) b.Draw();

                
                Raylib.DrawText($"{player1.score} : {player2.score}", 370, 20, 30, Color.White);

                Raylib.EndDrawing();


                
                bool player1moving = false;
                bool player2moving = false;

                // Player 1 Controls (WASD)
                if (Raylib.IsKeyDown(KeyboardKey.W) && player1moving == false)
                {
                    Rectangle nextRect = new Rectangle(player1.position.X - 20, player1.position.Y - 22, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;
                    if (Raylib.CheckCollisionRecs(nextRect, player2.GetRect())) hit = true; // Added Tank-to-Tank check

                    if (!hit) player1.position.Y -= 2;
                    player1.direction = new Vector2(0, -1);
                    player1moving = true;
                }
                if (Raylib.IsKeyDown(KeyboardKey.S) && player1moving == false)
                {
                    Rectangle nextRect = new Rectangle(player1.position.X - 20, player1.position.Y - 18, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;
                    if (Raylib.CheckCollisionRecs(nextRect, player2.GetRect())) hit = true; // Added Tank-to-Tank check

                    if (!hit) player1.position.Y += 2;
                    player1.direction = new Vector2(0, 1);
                    player1moving = true;
                }
                if (Raylib.IsKeyDown(KeyboardKey.A) && player1moving == false)
                {
                    Rectangle nextRect = new Rectangle(player1.position.X - 22, player1.position.Y - 20, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;
                    if (Raylib.CheckCollisionRecs(nextRect, player2.GetRect())) hit = true; // Added Tank-to-Tank check

                    if (!hit) player1.position.X -= 2;
                    player1.direction = new Vector2(-1, 0);
                    player1moving = true;
                }
                if (Raylib.IsKeyDown(KeyboardKey.D) && player1moving == false)
                {
                    Rectangle nextRect = new Rectangle(player1.position.X - 18, player1.position.Y - 20, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;
                    if (Raylib.CheckCollisionRecs(nextRect, player2.GetRect())) hit = true; // Added Tank-to-Tank check

                    if (!hit) player1.position.X += 2;
                    player1.direction = new Vector2(1, 0);
                    player1moving = true;
                }

                // Player 2 Controls (Nuolinäppäimet)
                if (Raylib.IsKeyDown(KeyboardKey.Up) && player2moving == false)
                {
                    Rectangle nextRect = new Rectangle(player2.position.X - 20, player2.position.Y - 22, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;
                    if (Raylib.CheckCollisionRecs(nextRect, player1.GetRect())) hit = true; // Added Tank-to-Tank check

                    if (!hit) player2.position.Y -= 2;
                    player2.direction = new Vector2(0, -1);
                    player2moving = true;
                }
                if (Raylib.IsKeyDown(KeyboardKey.Down) && player2moving == false)
                {
                    Rectangle nextRect = new Rectangle(player2.position.X - 20, player2.position.Y - 18, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;
                    if (Raylib.CheckCollisionRecs(nextRect, player1.GetRect())) hit = true; // Added Tank-to-Tank check

                    if (!hit) player2.position.Y += 2;
                    player2.direction = new Vector2(0, 1);
                    player2moving = true;
                }
                if (Raylib.IsKeyDown(KeyboardKey.Left) && player2moving == false)
                {
                    Rectangle nextRect = new Rectangle(player2.position.X - 22, player2.position.Y - 20, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;
                    if (Raylib.CheckCollisionRecs(nextRect, player1.GetRect())) hit = true; // Added Tank-to-Tank check

                    if (!hit) player2.position.X -= 2;
                    player2.direction = new Vector2(-1, 0);
                    player2moving = true;
                }
                if (Raylib.IsKeyDown(KeyboardKey.Right) && player2moving == false)
                {
                    Rectangle nextRect = new Rectangle(player2.position.X - 18, player2.position.Y - 20, 40, 40);
                    bool hit = false;
                    foreach (Wall w in walls) if (Raylib.CheckCollisionRecs(nextRect, w.rect)) hit = true;
                    if (Raylib.CheckCollisionRecs(nextRect, player1.GetRect())) hit = true; // Added Tank-to-Tank check

                    if (!hit) player2.position.X += 2;
                    player2.direction = new Vector2(1, 0);
                    player2moving = true;
                }

                
                if (Raylib.IsKeyPressed(KeyboardKey.Space)) player1.Shoot();
                if (Raylib.IsKeyPressed(KeyboardKey.Enter)) player2.Shoot();

                foreach (Bullet b in player1.bullets) b.Update(walls);
                foreach (Bullet b in player2.bullets) b.Update(walls);

                foreach (Bullet b in player1.bullets)
                {
                    if (b.active && Raylib.CheckCollisionPointRec(b.pos, player2.GetRect()))
                    {
                        b.active = false;
                        player1.score++;
                        player1.Reset(p1Start);
                        player2.Reset(p2Start);
                        break; 
                    }
                }

                foreach (Bullet b in player2.bullets)
                {
                    if (b.active && Raylib.CheckCollisionPointRec(b.pos, player1.GetRect()))
                    {
                        b.active = false;
                        player2.score++;
                        player1.Reset(p1Start);
                        player2.Reset(p2Start);
                        break;
                    }
                }

                player1.bullets.RemoveAll(b => !b.active);
                player2.bullets.RemoveAll(b => !b.active);
            }
        }

        class Tank
        {
            public Vector2 position;
            public Vector2 direction;
            public Vector2 tankSize;
            public Vector2 turretSize;
            Color color;

            public List<Bullet> bullets = new List<Bullet>();
            double lastShootTime = 0;
            float shootInterval = 0.4f;

            // Added: Scoring property
            public int score = 0;

            public Tank(Vector2 startPos, Color startColor)
            {
                position = startPos;
                color = startColor;

                tankSize = new Vector2(40, 40);
                turretSize = new Vector2(20, 20);
                direction = new Vector2(1, 0);
            }

            public Rectangle GetRect()
            {
                return new Rectangle(position.X - tankSize.X / 2.0f, position.Y - tankSize.Y / 2.0f, tankSize.X, tankSize.Y);
            }

            public void Reset(Vector2 startPos)
            {
                position = startPos;
                bullets.Clear();
            }

            public void Shoot()
            {
                if (Raylib.GetTime() - lastShootTime > shootInterval)
                {
                    Bullet newBullet = new Bullet();
                    newBullet.Fire(position, direction);
                    bullets.Add(newBullet);

                    lastShootTime = Raylib.GetTime();
                }
            }

            public void Draw()
            {
                Vector2 topLeft = position - tankSize / 2.0f;
                Raylib.DrawRectangleV(topLeft, tankSize, color);

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

                if (pos.X < 0 || pos.X > 800 || pos.Y < 0 || pos.Y > 600) active = false;

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