using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer
{
    [Serializable]
    public struct Square
    {
        public Vector3 pos;
        public Node TopLeft, TopRight, BottomLeft, BottomRight;
        public List<Tile> tile;
        public bool Check(bool topLeft, bool topRight, bool bottomLeft, bool bottomRight)
        {
            bool resault = topLeft.Equals(TopLeft.Active)
                           && bottomLeft.Equals(BottomLeft.Active)
                           && topRight.Equals(TopRight.Active)
                           && bottomRight.Equals(BottomRight.Active);
            return resault;
            
        }
    }
    
    [Serializable]
    public struct Node
    {
        public Vector2 position;
        public bool Active;
    }
    
    
}