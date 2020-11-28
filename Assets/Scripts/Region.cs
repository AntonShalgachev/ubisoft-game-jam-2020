using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class Region : MonoBehaviour
    {
        [SerializeField] private float m_radius = 5.0f;
        [SerializeField] private Color m_gizmoColor = Color.black;

        private Vector2 m_center => transform.position;

        public bool IsInside(Vector2 position)
        {
            var d2 = (position - m_center).sqrMagnitude;
            return d2 <= m_radius * m_radius;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = m_gizmoColor;
            GizmosHelper.DrawCircle(m_center, m_radius, 64);
        }
    }
}
