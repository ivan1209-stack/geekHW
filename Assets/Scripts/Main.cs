using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private PlayerAnimatorConfig _playerConfig;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private GeneratorLevelView _LevelView;
        [SerializeField] private QuestView _questView;
        
        private PlayerAnimatorController _playerAnimator;
        private PlayerTransformController _playerController;
        private CameraController _cameraController;
        private LevelGeneratorController _levelGeneratorController;
        private List<CannonController> _cannonControllers;
        private List<BulletEmittorController> _bulletControllers;
        private QuestConfiguratorController _questConfiguratorController;

        void Awake()
        {
            _cannonControllers = new List<CannonController>();
            _bulletControllers = new List<BulletEmittorController>();
            _playerConfig = Resources.Load<PlayerAnimatorConfig>("PlayerAnimatorConfig");
            _playerAnimator = new PlayerAnimatorController(_playerConfig);
            _playerController = new PlayerTransformController(_playerView, _playerAnimator);

            _levelGeneratorController = new LevelGeneratorController(_LevelView, _playerView);
            _levelGeneratorController.Init();
            _levelGeneratorController.SpawnPlayer();
            _questConfiguratorController = new QuestConfiguratorController(_questView);
            _questConfiguratorController.Init();

            foreach (var cannonView in _levelGeneratorController.GetCannons())
            {
                _cannonControllers.Add(new CannonController(cannonView._muzzleTransform, _playerView.transform));
                _bulletControllers.Add(new BulletEmittorController(cannonView._bullets, cannonView._emitterTransform, _playerView.transform));
                
            }
            
            _cameraController = new CameraController(_playerView, Camera.main.transform);
            
        }
        
        void FixedUpdate()
        {
            foreach (var controller in _cannonControllers)
            {
                controller.Update();
            }

            foreach (var controller in _bulletControllers)
            {
                controller.Update();
            }
            _cameraController.Update();
            _playerController.Update();
        }
    } 
}

