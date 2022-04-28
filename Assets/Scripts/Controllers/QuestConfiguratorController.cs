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
        }
    }
}

