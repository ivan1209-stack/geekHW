using System.Security.Cryptography.X509Certificates;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Platformer
{
    public class PlayerTransformController
    {
        private const float _speed = 500f;
        private const float _jumpSpeed = 9f;
        private const float _animationSpeed = 8f;
        private const float _movingTresh = 0.1f;
        private const float _jumpTresh = 1f;

        private bool flag;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private float _xVelocity;
        private bool _inJump;
        private float _xAxisInput;
        private bool Move;

        private LevelObjectView _view;
        private PlayerAnimatorController _animatorController;
        private ContactPoller _contactPoller;
        
        public PlayerTransformController(LevelObjectView view, PlayerAnimatorController controller)
        {
            _view = view;
            _animatorController = controller;
            _animatorController.StartAnimation(_view.SpriteRenderer, PlayerAnimState.Idle, true, _animationSpeed);
            flag = true;
            _contactPoller = new ContactPoller(_view.Collider);
        }

        public void Update()
        {
            _animatorController.Update();
            _contactPoller.Update();

            _inJump = Input.GetAxis("Vertical") > 0;
            _xAxisInput = Input.GetAxis("Horizontal");
            Move = Mathf.Abs(_xAxisInput) > _movingTresh;

            if (!Move && flag)
            {
                _animatorController.StartAnimation(_view.SpriteRenderer, PlayerAnimState.Idle,
                    true, _animationSpeed);
                flag = false;
            }

            if (Move)
            {
                MoveTowards();
            }
            if (_contactPoller.IsGrounded)
            {
                _animatorController.StartAnimation(_view.SpriteRenderer, PlayerAnimState.Run,
                    true, _animationSpeed);
                flag = true;
                if (_inJump && _view.RigidBody.velocity.y <= _jumpTresh)
                {
                    _view.RigidBody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }
                else
                {
                    if (Move) MoveTowards();
                    if (Mathf.Abs(_view.RigidBody.velocity.y) > _jumpTresh)
                    {
                        _animatorController.StartAnimation(_view.SpriteRenderer, PlayerAnimState.Jump, true,
                            _animationSpeed);
                        flag = true;
                    }
                    
                    
                }
            }
        }

        public void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _speed * (_xAxisInput < 0 ? -1 : 1);
            _view.RigidBody.velocity = _view.RigidBody.velocity.Change(x: _xVelocity);
            _view.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }
    }
}