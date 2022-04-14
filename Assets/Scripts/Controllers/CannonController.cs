using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CannonController
    {
        private Transform _muzzleTransform;
        private Transform _targetTransform;

        private Vector3 _direction;
        private float _angle;
        private Vector3 _axis;

        public CannonController(Transform muzzleTransform, Transform _playerTransform)
        {
            _muzzleTransform = muzzleTransform;
            _targetTransform = _playerTransform;
        }
        public void Update()
        {
            _direction = _targetTransform.position - _muzzleTransform.position;
            _angle = Vector3.Angle(Vector3.down, _direction);
            _axis = Vector3.Cross(Vector3.down, _direction);
            
            _muzzleTransform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }
    }
}