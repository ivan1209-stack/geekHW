using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Platformer
{
    public class GrapplingHookController
    {
        private HookView _view;
        private LevelObjectView _player;
        private RopeGeneratorController rope;
        private RaycastHit2D hit;
        private bool flag = false;
        private float ropeSegLen = 0.25f;

        public GrapplingHookController(HookView view, LevelObjectView player)
        {
            _player = player;
            _view = view;
            rope = new RopeGeneratorController(_view, 0, 0.1f, ropeSegLen);
        }

        public void Update()
        {
            rope.UpdateEnd(_player.Transform.position);
            ConnectHook();
            if(flag) rope.DrawRope();
        }

        public void FixedUpdate()
        {
            if (flag) rope.Simulate();
        }

        private void ConnectHook()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 posPlayer = _player.Transform.position;
                Ray ray = new Ray(posPlayer+Vector2.up, (pos - posPlayer));
                hit = Physics2D.Raycast(ray.origin, ray.direction, 20f);
                if (hit != null)
                {
                    
                    rope.UpdateSeg((int)((hit.point - posPlayer).magnitude / ropeSegLen));
                    rope.GeneratePoints();
                    _view.targetJoint2D.target = hit.point;
                    _view.targetJoint2D.anchor = _player.Transform.InverseTransformPoint(hit.point);
                    _player.RigidBody.constraints = RigidbodyConstraints2D.None;
                    _view.targetJoint2D.enabled = true;
                    rope.UpdateStart(hit.point);
                    flag = true;
                    rope.ActiveRope(flag);
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                _player.Transform.rotation = Quaternion.Euler(Vector3.zero);
                flag = false;
                _player.RigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                _view.targetJoint2D.enabled = flag;
                rope.ActiveRope(flag);
            }
        }
    }
    
}