using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField] private float m_angularSpeed = 60.0f;
        [SerializeField] private Vector3 m_axis = Vector3.up;

        float m_angle = 0.0f;

        private void Update()
        {
            var dt = Time.deltaTime;
            m_angle += m_angularSpeed * dt;
            transform.rotation = Quaternion.AngleAxis(m_angle, m_axis);
        }
    }
}
