using UnityEngine;

namespace Platformer
{
    public class CameraController
    {
        private LevelObjectView _player;
        private Transform _playerTransform;
        private Transform _cameraTransform;
        private float _camSpeed = 1.2f;

        private float x;
        private float y;

        private float offsetX;
        private float offsetY;

        private float _xAxisInput;
        private float _yAxisVelocity;

        private float _treshold;
        
        public CameraController(LevelObjectView player, Transform camera)
        {
            _player = player;
            _cameraTransform = camera;
            _playerTransform = _player.transform;
            _treshold = 0.2f;
        }
        public void Update()
        {
            _xAxisInput = Input.GetAxis("Horizontal");
            _yAxisVelocity = _player.RigidBody.velocity.y;

            x = _playerTransform.position.x;
            y = _playerTransform.position.y;

            OffsetSetter();

            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position,
                new Vector3(x + offsetX, y + offsetY, _cameraTransform.position.z), 
                Time.fixedDeltaTime * _camSpeed);
        }

        private void OffsetSetter()
        {
            if (_xAxisInput > _treshold)
            {
                offsetX = 4;
            }
            else if (_xAxisInput < -_treshold)
            {
                offsetX = -4;
            }
            else
            {
                offsetX = 0;
            }
            
            if (_yAxisVelocity > _treshold)
            {
                offsetY = 4;
            }
            else if (_yAxisVelocity < -_treshold)
            {
                offsetY = -4;
            }
            else
            {
                offsetY = 0;
            }
        }
    }
}