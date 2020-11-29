using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class CelestialBody : MonoBehaviour
    {
        [SerializeField] private CelestialBodyContainer m_celestialBodyGroup = null;
        [SerializeField] private float m_radius = 0.0f;

        public SimulatedObject simulatedObject => GetComponent<SimulatedObject>();

        private SimulationManager m_simulationManager => GameComponentsLocator.Get<SimulationManager>();

        public Vector2 CalculateForce(SimulatedObject targetObject)
        {
            var targetObjectPos = targetObject.position;
            var selfPos = simulatedObject.position;
            var r = (targetObjectPos - selfPos).magnitude - m_radius;
            var r2 = r * r;

            var forceDirection = (selfPos - targetObjectPos).normalized;
            var forceMagnitude = m_simulationManager.gravitationalConstant * simulatedObject.mass * targetObject.mass / r2;

            return forceDirection * forceMagnitude;
        }

        private void OnEnable()
        {
            Debug.Log($"Regestering self");
            RegisterBody();
        }

        private void OnDisable()
        {
            UnregisterBody();
        }

        public void RegisterBody()
        {
            if (m_celestialBodyGroup != null)
            {
                m_celestialBodyGroup.RegisterBody(this);
            }
        }

        public void UnregisterBody()
        {
            if (m_celestialBodyGroup != null)
            {
                m_celestialBodyGroup.UnregisterBody(this);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            GizmosHelper.DrawCircle(transform.position, m_radius);
        }
    }
}
