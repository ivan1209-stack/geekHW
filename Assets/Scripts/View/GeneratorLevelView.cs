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