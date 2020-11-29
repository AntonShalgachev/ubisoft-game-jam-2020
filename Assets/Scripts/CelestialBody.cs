using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class CelestialBody : MonoBehaviour
    {
        [SerializeField] private CelestialBodyContainer m_celestialBodyGroup = null;

        public SimulatedObject simulatedObject => GetComponent<SimulatedObject>();

        private SimulationManager m_simulationManager => GameComponentsLocator.Get<SimulationManager>();

        public Vector2 CalculateForce(SimulatedObject targetObject)
        {
            var targetObjectPos = targetObject.position;
            var selfPos = simulatedObject.position;
            var r2 = (targetObjectPos - selfPos).sqrMagnitude;

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
    }
}
