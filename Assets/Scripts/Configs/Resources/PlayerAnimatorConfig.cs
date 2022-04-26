using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public enum PlayerAnimState
    {
        Idle = 0,
        Run = 1,
        Jump = 2,
        Attack = 3,
        Hurt = 4,
        Die = 5
    }
    
    [CreateAssetMenu(fileName = "PlayerAnimatorConfig", menuName = "Configs/Animations/player" ,order = 1)]
    public class PlayerAnimatorConfig : ScriptableObject
    {
        [Serializable]
        public sealed class SpriteSequence
        {
            public PlayerAnimState Track;
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Sequences = new List<SpriteSequence>();
    }
}