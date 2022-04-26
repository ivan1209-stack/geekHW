using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class BulletEmittorController
    {
        private List<BulletController> _bullets = new List<BulletController>();
        private Transform _bulletTransform;
        private Transform _targetTransform;
        private int _index;
        private float _timeTillNextBullet;
        private float delay = 1;
        private float _startSpeed = 3f;

        public BulletEmittorController(List<LevelObjectView> bulletViews, Transform transform, Transform target)
        {
            _targetTransform = target;
            _bulletTransform = transform;
            foreach (var BulletView in bulletViews)
            {
                _bullets.Add(new BulletController(BulletView)); 
            }
        }

        public void Update()
        {
            if (_timeTillNextBullet > 0)
            {
                _bullets[_index].Active(false);
                _timeTillNextBullet -= Time.fixedDeltaTime;
            }
            else
            {
                _timeTillNextBullet = delay;
                if ((_targetTransform.position - _bulletTransform.position).magnitude < 30f)
                {
                    _bullets[_index].Throw(_bulletTransform.position,
                        (_targetTransform.position - _bulletTransform.position) * _startSpeed);
                    _index++;

                    if (_index >= _bullets.Count)
                    {
                        _index = 0;
                    }
                }
            }
        }
    }
}