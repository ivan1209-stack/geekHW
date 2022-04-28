using System;
using UnityEngine;

namespace Platformer
{
    public class QuestObjectView : LevelObjectView
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private Color _completeColor;
        [SerializeField] private Color _defaultColor;

        public int Id
        {
            get => id;
            set => id = value;
        }

        private void Awake()
        {
            _defaultColor = SpriteRenderer.color;
        }

        public void Activate()
        {
            SpriteRenderer.color = _completeColor;
            
        }

        public void Deactivate()
        {
            SpriteRenderer.color = _defaultColor;
        }
    }
}