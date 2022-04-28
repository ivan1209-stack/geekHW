using System;
using UnityEngine;

namespace Platformer
{
    public class LevelObjectView : MonoBehaviour
    {
        public Transform Transform;
        public SpriteRenderer SpriteRenderer;
        public Collider2D Collider;
        public Rigidbody2D RigidBody;

        public Action<LevelObjectView> OnLevelObjectContact { get; set; }

        private void OnTriggerEnter2D(Collider2D col)
        {
            LevelObjectView levelObject = col.gameObject.GetComponent<LevelObjectView>();
            
            OnLevelObjectContact?.Invoke(levelObject);
        }
    }
}