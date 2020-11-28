using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            var position = m_path.GetPosition(m_time);
            transform.position = position;

            m_time += Time.deltaTime;
        }
    }
}
