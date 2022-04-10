using UnityEngine;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private PlayerAnimatorConfig _playerConfig;
        [SerializeField] private EnemyAnimatorConfig _enemyConfig;
        [SerializeField] private int _animationSpeed = 15;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private LevelObjectView _EnemyView;
        
        private PlayerAnimatorController _playerAnimator;
        private EnemyAnimatorController _enemyAnimator;
        private PlayerTransformController _playerController;

        void Awake()
        {
            _playerConfig = Resources.Load<PlayerAnimatorConfig>("PlayerAnimatorConfig");
            _playerAnimator = new PlayerAnimatorController(_playerConfig);
            _playerController = new PlayerTransformController(_playerView, _playerAnimator);
            
            _enemyConfig =  Resources.Load<EnemyAnimatorConfig>("EnemyAnimationConfig");
            _enemyAnimator = new EnemyAnimatorController(_enemyConfig);
            _enemyAnimator.StartAnimation(_EnemyView.SpriteRenderer, EnemyAnimState.Idle, true, _animationSpeed);
            
        }
        
        void FixedUpdate()
        {
            _playerController.Update();
            _enemyAnimator.Update();
        }
    } 
}

