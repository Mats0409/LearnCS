using System;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using LearnCSharp.libGLFW;
using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class Level
    {

        public static Scene Load()
        {
            var player = new Player
            {
                Position = new Vector2(0, 0)
            };

            var energyBar = new EnergyBar
            {
                Position = new Vector2(Grid.Left, Grid.Top),
                player = player
            };

            var tileMap = new TileMap();

            var scene = new Scene
            {
                player,
                // energyBar,
                tileMap
            };

            return scene;
        }
    }

}