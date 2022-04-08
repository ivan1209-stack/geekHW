using UnityEngine;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private int _animationSpeed = 15;
        [SerializeField] private LevelObjectView _playerView;
        
        private SpriteAnimatorController _playerAnimator;
        
        void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimatorConfig");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _playerAnimator.StartAnimation(_playerView.SpriteRenderer, AnimState.Run, true, _animationSpeed );
        }
        
        void Update()
        {
            _playerAnimator.Update();
        }
    } 
}

