using UnityEngine;
using UnityEngine.Serialization;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private PlayerAnimatorConfig _playerConfig;
        [SerializeField] private EnemyAnimatorConfig _enemyConfig;
        [SerializeField] private int _animationSpeed = 15;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private LevelObjectView _EnemyView;
        [FormerlySerializedAs("_cannon")] [SerializeField] private CannonView _cannonView;
        
        private PlayerAnimatorController _playerAnimator;
        private EnemyAnimatorController _enemyAnimator;
        private PlayerTransformController _playerController;
        private CannonController _cannonController;
        private BulletEmittorController _bulletEmittorController;
        private CameraController _cameraController;

        void Awake()
        {
            _playerConfig = Resources.Load<PlayerAnimatorConfig>("PlayerAnimatorConfig");
            _playerAnimator = new PlayerAnimatorController(_playerConfig);
            _playerController = new PlayerTransformController(_playerView, _playerAnimator);
            
            _enemyConfig =  Resources.Load<EnemyAnimatorConfig>("EnemyAnimationConfig");
            _enemyAnimator = new EnemyAnimatorController(_enemyConfig);
            _enemyAnimator.StartAnimation(_EnemyView.SpriteRenderer, EnemyAnimState.Idle, true, _animationSpeed);

            _cannonController = new CannonController(_cannonView._muzzleTransform, _playerView.transform);
            _bulletEmittorController = new BulletEmittorController(_cannonView._bullets, _cannonView._emitterTransform, _playerView.transform);
            _cameraController = new CameraController(_playerView, Camera.main.transform);


        }
        
        void FixedUpdate()
        {
            _cameraController.Update();
            _playerController.Update();
            _enemyAnimator.Update();
            _cannonController.Update();
            _bulletEmittorController.Update();
        }
    } 
}

