using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace UnityPrototype
{
    public class DesynchronizationManager : MonoBehaviour
    {
        [SerializeField] private Transform m_ghostPlayer = null;
        [SerializeField] private SimulatedObject m_player = null;

        [Header("Desynchronization calculation")]
        [SerializeField] private float m_minOffset = 1.0f;
        [SerializeField] private float m_maxOffset = 2.0f;
        [SerializeField] private float m_extremeDesyncDuration = 5.0f;

        [ShowNativeProperty] private float m_maxDesyncRate => 1.0f / m_extremeDesyncDuration;
        [ShowNonSerializedField] private float m_desynchronization = 0.0f;

        [ShowNativeProperty] private float m_currentDesyncRate => GetCurrentDesyncRate();

        private void FixedUpdate()
        {
            var dt = Time.fixedUnscaledDeltaTime;
            UpdateDesynchronization(dt);
        }

        private float GetCurrentDesyncRate()
        {
            var truePosition = m_ghostPlayer.position;
            var actualPosition = m_player.position;

            var offset = Vector2.Distance(truePosition, actualPosition);

            return GetDesyncRate(offset);
        }

        private void UpdateDesynchronization(float dt)
        {
            m_desynchronization += GetCurrentDesyncRate() * dt;
        }

        private float GetDesyncRate(float offset)
        {
            if (offset < m_minOffset)
                return 0.0f;

            offset = Mathf.Min(offset, m_maxOffset);

            var t = Mathf.InverseLerp(m_minOffset, m_maxOffset, offset);
            return Mathf.Lerp(0.0f, m_maxDesyncRate, t);
        }

        private void OnDrawGizmos()
        {
            if (m_ghostPlayer)
            {
                Gizmos.color = Color.green;
                GizmosHelper.DrawCircle(m_ghostPlayer.position, m_minOffset);
                Gizmos.color = Color.red;
                GizmosHelper.DrawCircle(m_ghostPlayer.position, m_maxOffset);
            }
        }
    }
}