using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer
{
    public class QuestStoryController : IQuestStory
    {
        private List<IQuest> _quests = new List<IQuest>();
        public bool IsDone => _quests.All(value => value.IsCompleted);

        public QuestStoryController(List<IQuest> quests)
        {
            _quests = quests;
            Subscribe();
            Reset(0);
        }
        
        private void Subscribe()
        {
            foreach (var quest in _quests)
            {
                quest.Completed += OnQuestCompleted;
            }
        }
        
        private void UnSubscribe()
        {
            foreach (var quest in _quests)
            {
                quest.Completed -= OnQuestCompleted;
            }
        }
        private void OnQuestCompleted(object sender, IQuest quest)
        {
            int index = _quests.IndexOf(quest);
            if (IsDone)
            {
                Debug.Log("End of the story");
            }
            else
            {
                Reset(++index);
            }
        }

        private void Reset(int index)
        {
            if (index < 0 || index >=_quests.Count)
            {
                return;
            }

            IQuest nextQuest = _quests[index];

            if (nextQuest.IsCompleted)
            {
                OnQuestCompleted(this, nextQuest);
            }
            else
            {
                _quests[index].Reset();
            }
        }
        
        public void Dispose()
        {
            UnSubscribe();
            foreach (var quest in _quests)
            {
                quest.Dispose();
            }
        }
    } 
}
