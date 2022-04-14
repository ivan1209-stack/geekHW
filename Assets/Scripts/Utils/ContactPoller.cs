using UnityEngine;

namespace Platformer
{
    public class ContactPoller
    {
        private ContactPoint2D[] _contacts = new ContactPoint2D[10];
        private Collider2D _collider;
        private int _contactCount;
        private float _treshhold;

        public bool IsGrounded { get; private set; }
        public bool leftContact { get; private set; }
        public bool rightContact { get; private set; }

        public ContactPoller(Collider2D collider)
        {
            _collider = collider;
        }
        public void Update()
        {
            IsGrounded = false;
            leftContact = false;
            rightContact = false;

            _contactCount = _collider.GetContacts(_contacts);

            for (int i = 0; i < _contactCount; i++)
            {
                if (_contacts[i].normal.y > _treshhold) IsGrounded = true;
                if (_contacts[i].normal.x > _treshhold) leftContact = true;
                if (_contacts[i].normal.x > -_treshhold) rightContact = true;
            }
        }
    }
}