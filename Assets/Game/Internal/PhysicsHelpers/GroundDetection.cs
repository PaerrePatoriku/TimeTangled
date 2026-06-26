using UnityEngine;

namespace Game.Internal.PhysicsHelpers
{
    internal class GroundDetection <T> where T : MonoBehaviour
    {
        bool _grounded;
        T _originObject;
        Vector3 _castOffset;
        LayerMask _layerMask;
        float _castWidth;
        RaycastHit hit;
        public GroundDetection(T originObject, Vector3 castOffset, float castWidth, LayerMask layerMask)
        {
            _originObject = originObject;
            _castWidth = castWidth;
            _castOffset = castOffset;
            _layerMask = layerMask;
        }
        public void CastDetection()
        {
            Vector3 halfExtents = new Vector3(_castWidth, _castWidth, _castWidth);
            Vector3 start = _originObject.transform.position + _castOffset + (Vector3.down * _castWidth);
            Collider[] colliders = UnityEngine.Physics.OverlapBox(start, halfExtents, Quaternion.identity, _layerMask);
            _grounded = colliders.Length > 0;
            UnityEngine.Debug.DrawRay(start, Vector3.down *  _castWidth, Color.red);
            UnityEngine.Debug.DrawRay(start, halfExtents, Color.green);

        }
        public bool isGrounded()
        {
            return _grounded;
        }

    }
}
