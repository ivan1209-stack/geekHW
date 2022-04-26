using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Platformer
{
    public class LevelGeneratorController
    {
        private readonly Tilemap _tilemap;
        private readonly Tile _tile;
        private readonly int _mapWidth;
        private readonly int _mapHeight;
        private readonly bool _borders;
        private readonly int _fillPercent;
        private readonly int _factorSmooth;
        private readonly int _ratioTrapsAndGround;
        private readonly int _countWall = 4;
        private LevelObjectView _playerView;
        private List<GameObject> _trapsObjects;
        private List<GameObject> _obstacelsObjects;
        private List<CannonView> _cannons;

        private int[,] _map;


        private int Clean;
        //private MarshingSquaresController _marshingSquaresController;

        public LevelGeneratorController(GeneratorLevelView view, LevelObjectView playerView)
        {
            _ratioTrapsAndGround = view.RatioTrapsAndGround;
            _obstacelsObjects = view.ObstacelsObjects;
            _trapsObjects = view.TrapsObjects;
            _playerView = playerView;
            _tilemap = view.Tilemap;
            _tile = view.Tile;
            _mapWidth = view.MapWidth;
            _mapHeight = view.MapHeight;
            _borders = view.Borders;
            _fillPercent = view.FillPercent;
            _factorSmooth = view.FactorSmooth;
            _map = new int[_mapWidth, _mapHeight];
            _cannons = new List<CannonView>();
        }


        public List<CannonView> GetCannons()
        {
            return _cannons;
        }
        public void Init()
        {
            FillMap();
            for (int i = 0; i < _factorSmooth; i++)
            {
                SmoothMap();
            }

            GridGenerate grid = new GridGenerate(_map, 1, _tilemap);
            fillTraps();
            fillBridges();
            //_marshingSquaresController = new MarshingSquaresController();
            //_marshingSquaresController.GeneratorGrid(_map, 1);
            //_marshingSquaresController.DrawTilesOnMap(tilemap, tile);
            //DrawTiles();
        }

        private void FillMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (x == 0 || x == _mapWidth - 1 || y == 0 || y == _mapHeight - 1)
                    {
                        if (_borders)
                        {
                            _map[x, y] = 1;
                        }
                    }
                    else
                    {
                        if (Random.Range(0, 100) < _ratioTrapsAndGround)
                        {
                            _map[x, y] = Random.Range(0, 100) < _fillPercent ? 2 : 3;
                        }
                        else
                        {
                            _map[x, y] = Random.Range(0, 100) < _fillPercent ? 1 : 0;
                        }
                        
                    }
                }
            }
        }

        private void fillTraps()
        {
            Vector3 pos = new Vector3(0, 0, 0);
            for (var x = 0; x < _mapWidth; x++)
            {
                pos.x = x;
                for (var y = 0; y < _mapHeight; y++)
                {
                    pos.y = y;
                    if (_map[x, y] == 2)
                    {
                        var trap = Object.Instantiate(_trapsObjects[Random.Range(0, _trapsObjects.Count)]);
                        trap.transform.position = pos;
                        if (trap.TryGetComponent(out CannonView cannon))
                        {
                            _cannons.Add(cannon);
                        }
                    }
                }
            }
        }
        
        private void fillBridges()
        {
            Vector3 pos = new Vector3(0, 0, 0);
            for (var x = 0; x < _mapWidth; x++)
            {
                pos.x = x;
                for (var y = 0; y < _mapHeight; y++)
                {
                    pos.y = y;
                    if (_map[x, y] == 3 && _map[x-1, y] == 1)
                    {
                        Clean = 0;
                        for (int i = x+1; _map[i, y] == 0 && i != _mapWidth-1 ; i++)
                            {
                                Clean++;
                            }
                        if (Clean > 4)
                        {
                            List<GameObject> generated = new List<GameObject>();
                            pos.x += 0.25f;
                            for (int i = 0; i < Clean*2; i++)
                            {
                                generated.Add(Object.Instantiate(_obstacelsObjects[Random.Range(0, _obstacelsObjects.Count)]));
                                generated[i].AddComponent<DistanceJoint2D>();
                                generated[i].transform.position = pos;
                                pos.x += 0.5f;
                            }
                            connectBridge(generated);
                        }
                    }
                }
            }
        }

        private void connectBridge(List<GameObject> objects)
        {
            for (var i = 1; i < objects.Count; i++)
            {
                objects[i].GetComponent<DistanceJoint2D>().connectedBody = objects[i - 1].GetComponent<Rigidbody2D>();
                objects[i].GetComponent<DistanceJoint2D>().distance = 0.5f;
            }

            objects[0].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            objects[objects.Count-1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

    private void SmoothMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    int neighbor = GetNeighbor(x, y);
                    if (neighbor > _countWall)
                    {
                        _map[x, y] = 1;
                    }
                    else if (neighbor < _countWall)
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
                    if (gridx >= 0 && gridx < _mapWidth && gridy >= 0 && gridy < _mapHeight)
                    {
                        if (gridx != x || gridy != y)
                        {
                            if(_map[gridx, gridy] == 0 || _map[gridx, gridy] == 1) counter += _map[gridx, gridy];
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
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(-_mapWidth / 2 + x, -_mapHeight / 2 + y, 0);
                    if (_map[x, y] == 1)
                    {
                        _tilemap.SetTile(tilePosition, _tile);
                    }
                }
            }
            
        }

        public void SpawnPlayer()
        {
            Vector3 spawnPoint = new Vector3(0, 0, 0);
            for (int x = 0; x < _mapWidth; x++)
            {
                spawnPoint.x++;
                for (int y = 0; y < _mapHeight; y++)
                {
                    spawnPoint.y++;
                    if (_map[x, y] == 0)
                    {
                        _playerView.Transform.position = spawnPoint;
                        return;
                    }
                }
            }
        }
    }
}