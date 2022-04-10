using UnityEngine;
using System;
using System.Collections.Generic;

namespace Platformer
{
    public enum EnemyAnimState
        {
            Idle = 0,
            Run = 1,
            attack = 2
        }
    [CreateAssetMenu(fileName = "EnemyAnimationConfig", menuName = "Configs/Animations/enemy", order = 0)] 
    public class EnemyAnimatorConfig : ScriptableObject
    {
        [Serializable]
        public sealed class SpriteSequence
        { 
            public EnemyAnimState Track; 
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Sequences = new List<SpriteSequence>();
    }
        
}