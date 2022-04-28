using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    
    [CreateAssetMenu(fileName = "QuestStoryConfig", menuName = "config/quests/QuestStoryConfig", order = 0)]
    public class QuestStoryConfig : ScriptableObject
    {
        public List<QuestConfig> quests;

        public QuestStoryType QuestStoryType;
    }

    public enum QuestStoryType
    {
        Common, 
        Ressettable,
    }
}
