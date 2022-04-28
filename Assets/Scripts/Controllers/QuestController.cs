using System;

namespace Platformer
{
    public class QuestController : IQuest
    {
        public event EventHandler<IQuest> Completed;
        public bool IsCompleted { get; private set; }

        private QuestObjectView _objectView;
        private bool _active;
        private IQuestModel _model;


        public QuestController(QuestObjectView objectView, IQuestModel model)
        {
            _objectView = objectView;
            _model = model;
        }
        public void Reset()
        {
            if (_active)
            {
                return;
            }
            _active = true;
            _objectView.OnLevelObjectContact += OnContact;
            _objectView.Deactivate();
           
        }

        private void OnContact(LevelObjectView arg)
        {
            bool complete = _model.TryComplete(arg.gameObject);
            if (complete)
            {
                Complete();
            }
        }

        private void Complete()
        {
            if (!_active)
            {
                return;
            }

            _active = false;
            _objectView.OnLevelObjectContact -= OnContact;
            _objectView.Activate();
            Completed?.Invoke(this, this);
        }
        
        public void Dispose()
        {
            _objectView.OnLevelObjectContact -= OnContact;
        }
    }  
}