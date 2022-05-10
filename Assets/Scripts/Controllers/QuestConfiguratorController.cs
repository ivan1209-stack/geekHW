using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Platformer
{
    public class QuestConfiguratorController
    {
        private QuestObjectView _singleQuestObjectView;
        private QuestController _singleQuestController;
        private GoldQuestModel _model;

        private QuestStoryConfig[] _questStoryConfigs;
        private QuestObjectView[] _questViews;

        private List<IQuestStory> _questStories;

        public QuestConfiguratorController(QuestView view)
        {
            _singleQuestObjectView = view._singleQuestObjectView;
            _model = new GoldQuestModel();

            _questStoryConfigs = view._questStoryConfigs;
            _questViews = view._questViews;
        }
        
        public void Init()
        {
            _singleQuestController = new QuestController(_singleQuestObjectView, _model);
            _singleQuestController.Reset();
            
            _questsStoryfactory.Add(QuestStoryType.Common, quests=> new QuestStoryController(quests));
            
            _questsFabric.Add(QuestType.item, ()=>new GoldQuestModel());

            _questStories = new List<IQuestStory>();

            foreach (QuestStoryConfig questStoryConfig in _questStoryConfigs)
            {
                _questStories.Add(createQuestStory(questStoryConfig));
            }
        }
        private Dictionary<QuestType, Func<IQuestModel>> _questsFabric = new Dictionary<QuestType, Func<IQuestModel>>(1);
        private Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questsStoryfactory = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>(2);
        private IQuest createQuest (QuestConfig config)
        {
            int questId = config.id;
            QuestObjectView questview = _questViews.FirstOrDefault
                (value => value.Id == config.id);
            if (questview == null)
            {
                return null;
            }

            if (_questsFabric.TryGetValue(config.QuestType, out var factory))
            {
                IQuestModel questModel = factory.Invoke();
                return new QuestController(questview, questModel);
            }

            return null;
        }

        private IQuestStory createQuestStory(QuestStoryConfig cfg)
        {
            List<IQuest> quests = new List<IQuest>();

            foreach (QuestConfig questConfig in cfg.quests)
            {
                IQuest quest = createQuest(questConfig);
                if (quest == null) continue;
                
                quests.Add(quest);
            }

            return _questsStoryfactory[cfg.QuestStoryType].Invoke(quests);
        }
        
    }
}

