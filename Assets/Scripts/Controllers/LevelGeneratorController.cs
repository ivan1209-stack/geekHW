using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer
{
    public class LevelGeneratorController
    {
        private Tilemap tilemap;
        private Tile tile;
        private int mapWidth;
        private int mapHeight;
        private bool borders;
        private int fillPercent;
        private int factorSmooth;
        private int countWall = 4;

        private int[,] _map;
        private Dictionary<int, Tile> tiles = new Dictionary<int, Tile>();
        private MarshingSquaresController _marshingSquaresController;

        public LevelGeneratorController(GeneratorLevelView view)
        {
            tilemap = view.Tilemap;
            tile = view.Tile;
            mapWidth = view.MapWidth;
            mapHeight = view.MapHeight;
            borders = view.Borders;
            fillPercent = view.FillPercent;
            factorSmooth = view.FactorSmooth;
            _map = new int[mapWidth, mapHeight];
        }

        public void Init()
        {
            FillMap();
            for (int i = 0; i < factorSmooth; i++)
            {
                SmoothMap();
            }
            _marshingSquaresController = new MarshingSquaresController();
            _marshingSquaresController.GeneratorGrid(_map, 1);
            _marshingSquaresController.DrawTilesOnMap(tilemap, tile);
            //DrawTiles();
        }

        private void FillMap()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (x == 0 || x == mapWidth - 1 || y == 0 || y == mapHeight - 1)
                    {
                        if (borders)
                        {
                            _map[x, y] = 1;
                        }
                    }
                    else
                    {
                        _map[x,y] = Random.Range(0, 100) < fillPercent ? 1 : 0;
                    }
                }
            }
        }

        private void SmoothMap()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    int neighbor = GetNeighbor(x, y);
                    if (neighbor > countWall)
                    {
                        _map[x, y] = 1;
                    }
                    else if (neighbor < countWall)
                    {
                        _map[x, y] = 0;
                    }
                }
            } 
        }

        private int GetNeighbor(int x, int y)
        {
            int counter = 0;
            for (int gridx = x-1; gridx <= x+1; gridx++)
            {
                for (int gridy = y-1; gridy <= y+1; gridy++)
                {
                    if (gridx >= 0 && gridx < mapWidth && gridy >= 0 && gridy < mapHeight)
                    {
                        if (gridx != x || gridy != y)
                        {
                            counter += _map[gridx, gridy];
                        }
                    }
                    else
                    {
                       counter++;
                    }
                }
            }
            return counter;
        }

        private void DrawTiles()
        {
            if(_map==null) return;
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(-mapWidth / 2 + x, -mapHeight / 2 + y, 0);
                    if (_map[x, y] == 1)
                    {
                        tilemap.SetTile(tilePosition, tile);
                    }
                }
            }
            
        }
    }
}