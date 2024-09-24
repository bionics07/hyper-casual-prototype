using UnityEngine;

namespace Assets.Assets.Scripts.Character
{
    public class GroundCheckerHelper
    {
        private const float SPHERE_RADIOUS = 0.1f;

        private MonoBehaviour _parent;
        private float _groundCheckerDistance;
        private LayerMask _layerMask;
        private Vector3 _spherePosition;

        public GroundCheckerHelper(MonoBehaviour parent, float groundCheckerDistance, LayerMask layerMask)
        {
            _parent = parent;
            _groundCheckerDistance = groundCheckerDistance;
            _layerMask = layerMask;
        }

        public void DrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_spherePosition, SPHERE_RADIOUS);
        }

        public bool IsGoingToHitGroundNow(Vector3 direction)
        {
            direction *= _groundCheckerDistance;
            
            _spherePosition = _parent.transform.position + direction;

            bool isHitting = Physics.CheckSphere(_spherePosition, SPHERE_RADIOUS, _layerMask,
                QueryTriggerInteraction.Ignore);

            return isHitting;
        }
    }
}