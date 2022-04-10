using System.Security.Cryptography.X509Certificates;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Platformer
{
    public class PlayerTransformController
    {
        private const float _speed = 5f;
        private const float _animationSpeed = 8f;
        private const float _jumpSpeed = 9f;
        private const float _g = -9.8f;
        private const float _movingTresh = 0.1f;
        private const float _jumpTresh = 1f;
        private const float _groundLevel  = 0.2f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private float _yVelocity;
        private bool _inJump;
        private float _xAxisInput;
        private bool Move;

        private LevelObjectView _view;
        private PlayerAnimatorController _animatorController;
        
        public PlayerTransformController(LevelObjectView view, PlayerAnimatorController controller)
        {
            _view = view;
            _animatorController = controller;
            _animatorController.StartAnimation(_view.SpriteRenderer, PlayerAnimState.Idle, true, _animationSpeed);
        }

        public void Update()
        {
            _animatorController.Update();
            _inJump = Input.GetAxis("Vertical") > 0;
            _xAxisInput = Input.GetAxis("Horizontal");
            Move = Mathf.Abs(_xAxisInput) > _movingTresh;
            
            if (IsGrounded())
            {
                
                if (Move)
                {
                    MoveTowards();
                    _animatorController.StartAnimation(_view.SpriteRenderer, Move? PlayerAnimState.Run : PlayerAnimState.Idle,
                        true , _animationSpeed);
                }

                if (_inJump && _yVelocity == 0)
                {
                    _yVelocity = _jumpSpeed;
                }
                else if (_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _view.transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                if (Move) MoveTowards();

                if (Mathf.Abs(_yVelocity) > _jumpTresh)
                {
                    _animatorController.StartAnimation(_view.SpriteRenderer, PlayerAnimState.Jump, true,
                        _animationSpeed);
                }

                _yVelocity += _g * Time.fixedDeltaTime;
                _view.transform.position +=  Vector3.up * Time.fixedDeltaTime * _yVelocity;
            }
        }

        public void MoveTowards()
        {
            _view.transform.position += Vector3.right * Time.fixedDeltaTime * _speed * (_xAxisInput < 0 ? -1 : 1);
            _view.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }
        
        public bool IsGrounded()
        {
            return _view.transform.position.y <= _groundLevel && _yVelocity <= 0;
        }
        
    }
}