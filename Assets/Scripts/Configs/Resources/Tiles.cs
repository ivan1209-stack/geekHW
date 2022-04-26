using System;
using System.Collections.Generic;
using Platformer;
using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "TilesConfig", menuName = "config/TilesConfig", order = 2)]
    public class Tiles : ScriptableObject
    {
        public List<Square> squares;
    }
}