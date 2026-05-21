using System.Numerics;
using Raylib_cs;

namespace Csharp_Kirjasto
{
    public class Movement
    {
        int speed;
        public Vector2 position;
        public Vector2 direction;

        KeyboardKey up;
        KeyboardKey down;
        KeyboardKey left;
        KeyboardKey right;

        public Movement(int speed, Vector2 position, Vector2 direction, KeyboardKey up, KeyboardKey down, KeyboardKey left, KeyboardKey right)
        {
            this.speed = speed;
            this.position = position;
            this.direction = direction;
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }
        public void move()
        {
            bool moving = false;


            if (Raylib.IsKeyDown(up) && moving == false)
            {
                direction = new Vector2(0, -1);
                moving = true;
            }
            if (Raylib.IsKeyDown(down) && moving == false)
            {
                direction = new Vector2(0, 1);
                moving = true;
            }
            if (Raylib.IsKeyDown(left) && moving == false)
            {
                direction = new Vector2(-1, 0);
                moving = true;
            }
            if (Raylib.IsKeyDown(right) && moving == false)
            {
                direction = new Vector2(1, 0);
                moving = true;
            }
            if(moving == true)
            {
                position += direction * speed * Raylib.GetFrameTime();
            }
        }
    }
}