using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "QuestItemConfig", menuName = "config/quests/QuestItemConfig", order = 1)]
    public class QuestItemConfig : ScriptableObject
    {
        public int questID;
        public List<int> questItemCollections;
    } 
}

