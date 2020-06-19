using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using LearnCSharp.libGLFW;
using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class TileMap : IDrawable, IUpdatable
    {
        public enum Tile
        {
            None,
            Wall,
            PickUp,
            Finish
        }

        SKPaint paint = new SKPaint
        {
            Color = SKColors.Green,
            IsAntialias = true
        };


        Tile[,] tiles = new Tile[Grid.Width, Grid.Height];

        public TileMap()
        {
        }

        public void Draw(SKCanvas canvas)
        {
            // Fix transparent edges
            var px = 1f/canvas.TotalMatrix.ScaleX;

            for (int i = 0; i < Grid.Width; ++i)
            {
                for (int j = 0; j < Grid.Height; ++j)
                {
                    var tile = tiles[i, j];
                    switch (tile)
                    {
                        case Tile.Wall:
                            canvas.DrawRect(Grid.Left + i - px/2f, Grid.Top + j - px/2f, 1 + px, 1 + px, paint);
                            break;
                    }

                }
            }
        }

        public void Update(float deltaTimeInSeconds, InputState input)
        {
            int i = (int)(input.MousePositionX - Grid.Left);
            int j = (int)(input.MousePositionY - Grid.Top);
            if (i >= 0 && j >= 0 && i < Grid.Width && j < Grid.Height)
            {
                if (input.IsMouseDown(MouseButton.Left))
                {
                    tiles[i, j] = Tile.Wall;
                }
                if (input.IsMouseDown(MouseButton.Right))
                {
                    tiles[i, j] = Tile.None;
                }
            }

            if (input.IsKeyTapped(Key.O)) 
            {
                var formatter = new BinaryFormatter();
                using var stream = File.Create("tilemap1.bin");
                formatter.Serialize(stream, tiles);

            }
            if (input.IsKeyTapped(Key.I)) 
            {
                var formatter = new BinaryFormatter();
                using var stream = File.OpenRead("tilemap1.bin");
                tiles = (Tile[,])formatter.Deserialize(stream);

            }
        }
    }

}