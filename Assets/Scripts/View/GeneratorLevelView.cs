using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer
{
    public class GeneratorLevelView : MonoBehaviour
    {
        [SerializeField]private Tilemap tilemap;
        [SerializeField]private Tile tile;
        [SerializeField]private int mapWidth;
        [SerializeField]private int mapHeight;
        [SerializeField]private bool borders;
        [SerializeField][Range(0, 100)] private int fillPercent;
        [SerializeField][Range(0, 100)] private int factorSmooth;
        [SerializeField][Range(0, 100)] private float bridgePercent;
        [SerializeField][Range(0, 100)] private int cannonPercent;
        [SerializeField][Range(0, 100)] private int ratioTrapsAndGround;
        [SerializeField]private List<GameObject> obstacelsObjects;
        [SerializeField]private List<GameObject> trapsObjects;
        
        public int CannonPercent
        {
            get => cannonPercent;
            set => cannonPercent = value;
        }
        
        public float BridgePercent
        {
            get => bridgePercent;
            set => bridgePercent = value;
        }
        public int RatioTrapsAndGround
        {
            get => ratioTrapsAndGround;
            set => ratioTrapsAndGround = value;
        }
        public List<GameObject> ObstacelsObjects
        {
            get => obstacelsObjects;
            set => obstacelsObjects = value;
        }
        
        public List<GameObject> TrapsObjects
        {
            get => trapsObjects;
            set => trapsObjects = value;
        }
        public Tilemap Tilemap
        {
            get => tilemap;
            set => tilemap = value;
        }
        public Tile Tile
        {
            get => tile;
            set => tile = value;
        }

        public int MapWidth
        {
            get => mapWidth;
            set => mapWidth = value;
        }

        public int MapHeight
        {
            get => mapHeight;
            set => mapHeight = value;
        }

        public bool Borders
        {
            get => borders;
            set => borders = value;
        }

        public int FillPercent
        {
            get => fillPercent;
            set => fillPercent = value;
        }


        public int FactorSmooth
        {
            get => factorSmooth;
            set => factorSmooth = value;
        }
    }
}