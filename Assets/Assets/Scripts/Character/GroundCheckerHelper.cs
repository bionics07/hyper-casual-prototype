using UnityEngine;

namespace Assets.Assets.Scripts.Character
{
    public class GroundCheckerHelper
    {
        private const float SPHERE_RADIOUS = 0.2f;

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
            Vector3 positionWithOffset = new Vector3(
                _parent.transform.position.x,
                _parent.transform.position.y - 0.05f,
                _parent.transform.position.z);


            _spherePosition = positionWithOffset + direction;

            bool isHitting = Physics.CheckSphere(_spherePosition, SPHERE_RADIOUS, _layerMask,
                QueryTriggerInteraction.Ignore);

            return isHitting;
        }
    }
}