using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamelogic.Extensions;

namespace UnityPrototype
{
    public class PathFollower : MonoBehaviour
    {
        private TruePath m_path = null;
        private float m_time = 0.0f;

        public bool hasPath => m_path != null;

        public void SetPath(TruePath path)
        {
            m_path = path;
            m_time = 0.0f;
        }

        private void Update()
        {
            if (!hasPath)
                return;

            var point = m_path.GetPoint(m_time);

            var position = point.position;
            transform.SetXY(position.x, position.y);
            transform.SetRotationZ(Vector2.SignedAngle(Vector2.up, point.velocity));

            m_time += Time.deltaTime;
        }
    }
}
