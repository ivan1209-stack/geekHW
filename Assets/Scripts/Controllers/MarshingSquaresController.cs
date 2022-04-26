using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer
{
    /*
    #region 1
    public class MarshingSquaresController
    {
        private SquareGrid _squareGrid;
        private Tilemap _tilemap;
        private Tile _tile;

        public void GeneratorGrid(int[,] map, float squareSize)
        {
            _squareGrid = new SquareGrid(map, squareSize);
        }

        public void DrawTilesOnMap(Tilemap tilemap, Tile tile)
        {
            if (_squareGrid == null)
            {
                return;
            }

            _tilemap = tilemap;
            _tile = tile;

            for (int x = 0; x < _squareGrid.Squares.GetLength(0); x++)
            {
                for (int y = 0; y < _squareGrid.Squares.GetLength(1); y++)
                {
                    DrawTile(_squareGrid.Squares[x, y].TopLeft.Active, _squareGrid.Squares[x,y].TopLeft.position);
                    DrawTile(_squareGrid.Squares[x, y].TopRight.Active, _squareGrid.Squares[x,y].TopRight.position);
                    DrawTile(_squareGrid.Squares[x, y].BottomLeft.Active, _squareGrid.Squares[x,y].BottomLeft.position);
                    DrawTile(_squareGrid.Squares[x, y].BottomRight.Active, _squareGrid.Squares[x,y].BottomRight.position);
                }
            }
        }

        public void DrawTile(bool active, Vector3 position)
        {
            if (active)
            {
                Vector3Int pos = new Vector3Int((int)position.x, (int)position.y, 0);
                _tilemap.SetTile(pos, _tile);
            }
        }
    }

    public class Node
    {
        public Vector3 position;

        public Node(Vector3 pos)
        {
            position = pos;
        }
    }

    public class ControlNode : Node
    {
        public bool Active;

        public ControlNode(Vector3 pos, bool active) : base(pos)
        {
            Active = active;
        }
    }

    public class Square
    {
        public ControlNode TopLeft, TopRight, BottomLeft, BottomRight;

        public Square(ControlNode topLeft, ControlNode topRight, ControlNode bottomLeft, ControlNode bottomRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }
    }

    public class SquareGrid
    {
        public Square[,] Squares;

        public SquareGrid(int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);

            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            float size = squareSize / 2;

            float width = -mapWidth / 2;
            float height = -mapHeight / 2;

            ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    Vector3 position = new Vector3(width + x * squareSize + size, height + y * squareSize + size, 0);
                    controlNodes[x, y] = new ControlNode(position, map[x, y] == 1);
                }
            }
            Squares = new Square[nodeCountX - 1, nodeCountY - 1];

            for (int x = 0; x < nodeCountX - 1; x++)
            {
                for (int y = 0; y < nodeCountY - 1; y++)
                {
                    Squares[x, y] = new Square(controlNodes[x, y + 1], controlNodes[x + 1, y + 1],
                        controlNodes[x + 1, y], controlNodes[x, y]);
                }
            }
        }
    }
    #endregion*/
    
    public class GridGenerate
    {
        public Square[,] grid;
        private int[,] _map;
        private Tilemap _tilemap;
        private int nodeCountX;
        private int nodeCountY;
        
        public GridGenerate(int[,] map, float squareSize, Tilemap tilemap)
        {
            _tilemap = tilemap;
            _map = map;
            nodeCountX = map.GetLength(0);
            nodeCountY = map.GetLength(1);
            grid = new Square[nodeCountX, nodeCountY];

            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            float size = squareSize / 2;

            float width = -mapWidth / 2;
            float height = -mapHeight / 2;
            
            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    if (map[x, y] == 1)
                    {
                        DrawTile(x, y);
                    }
                }
            }
        }

        private void DrawTile(int x, int y)
        {
            Tile ChoicedTile;
            bool TopLeftConect = false, TopRightConect = false, BottomLeftConect = false, BottomRightConect = false;
            List<Square> targetForChoice = Resources.Load<Tiles>("TilesConfig").squares;
            if (x == 0 || y == 0 || y == nodeCountY-1 || x == nodeCountX-1) 
            { 
                TopLeftConect = true; 
                BottomRightConect = true;
                BottomLeftConect = true; 
                TopRightConect = true;
            }
            else 
            {
                if (grid[x-1, y-1].TopRight.Active
                    || grid[x, y-1].TopLeft.Active
                    || grid[x - 1, y].BottomRight.Active
                    || _map[x-1, y-1] == 1 
                    || _map[x-1, y] == 1 
                    || _map[x, y-1] == 1)
                { 
                    BottomLeftConect = true;
                }

                if (grid[x - 1, y].TopRight.Active
                    || grid[x - 1, y + 1].BottomRight.Active
                    || grid[x, y + 1].BottomLeft.Active
                    || _map[x-1, y] == 1 
                    || _map[x-1, y+1] == 1 
                    || _map[x, y+1] == 1)
                { 
                    TopLeftConect = true;
                }
                        

                if (grid[x, y + 1].BottomRight.Active
                    || grid[x + 1, y + 1].BottomLeft.Active
                    || grid[x + 1, y].TopLeft.Active 
                    || _map[x+1, y+1] == 1 
                    || _map[x+1, y] == 1 
                    || _map[x, y+1] == 1) 
                { 
                    TopRightConect = true;
                }

                if (grid[x + 1, y].BottomLeft.Active 
                    || grid[x + 1, y - 1].TopLeft.Active
                    || grid[x, y-1].TopRight.Active
                    || _map[x+1, y-1] == 1 
                    || _map[x+1, y] == 1 
                    || _map[x-1, y-1] == 1)
                {
                    BottomRightConect = true;
                }
                        
            }

            Vector3Int pos = new Vector3Int(x, y, 0);
            foreach (var square in targetForChoice)
            {
                if (square.Check(TopLeftConect, TopRightConect,
                        BottomLeftConect, BottomRightConect))
                {
                    int index = Random.Range(0, square.tile.Count);
                    _tilemap.SetTile(pos, square.tile[index]);
                    grid[x, y].BottomLeft.Active = BottomLeftConect;
                    grid[x, y].BottomRight.Active = BottomRightConect;
                    grid[x, y].TopRight.Active = TopRightConect;
                    grid[x, y].TopLeft.Active = TopLeftConect;
                    break;
                }
            }
            
        }
    }
}