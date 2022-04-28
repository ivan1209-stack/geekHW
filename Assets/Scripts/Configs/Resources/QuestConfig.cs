using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "QuestConfig", menuName = "config/quests/QuestConfig", order = 0)]
    public class QuestConfig : ScriptableObject
    {
        public int id;
        public QuestType QuestType;
    }

    public enum QuestType
    {
        item = 0,
        kill = 1,
        find = 2
    }
}