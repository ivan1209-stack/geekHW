using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(LineRenderer))]
    public class HookView : MonoBehaviour
    {
        public TargetJoint2D targetJoint2D;
        public LineRenderer lineRenderer;
        
    } 
}

