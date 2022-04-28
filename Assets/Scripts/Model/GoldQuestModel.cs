using System;
using UnityEngine;

namespace Platformer
{
    public class GoldQuestModel : IQuestModel
    {
        private const string Tag = "Player";
        public bool TryComplete(GameObject actor)
        {
            return actor.CompareTag(Tag);
        }
    } 
}
