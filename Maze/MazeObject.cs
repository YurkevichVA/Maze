using System;
using System.Drawing;

namespace Maze
{
    class MazeObject
    {
        public enum MazeObjectType { HALL, WALL, MEDAL, ENEMY, CHAR, AIDKIT, FINISH };

        public Bitmap[] images = {
            new Bitmap(@"C:\Users\ovsan\source\repos\Maze\Maze\pics\hall.png"),
            new Bitmap(@"C:\Users\ovsan\source\repos\Maze\Maze\pics\wall.png"),
            new Bitmap(@"C:\Users\ovsan\source\repos\Maze\Maze\pics\medal.png"),
            new Bitmap(@"C:\Users\ovsan\source\repos\Maze\Maze\pics\enemy.png"),
            new Bitmap(@"C:\Users\ovsan\source\repos\Maze\Maze\pics\player.png"),
            new Bitmap(@"C:\Users\ovsan\source\repos\Maze\Maze\pics\aidkit.png")};

        public MazeObjectType type;
        public int width;
        public int height;
        public Image texture;

        public MazeObject(MazeObjectType type)
        {
            this.type = type;
            width = 16;
            height = 16;
            if ((int)type == 6)
                texture = images[0];
            else
                texture = images[(int)type];
        }

    }
}
