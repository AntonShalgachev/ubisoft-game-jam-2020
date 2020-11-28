using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace UnityPrototype
{
    public class SimulationManager : MonoBehaviour
    {
        [SerializeField] private CelestialBodyContainer m_planets = null;
        [SerializeField] private SimulatedObject m_player = null;
        // [SerializeField] private TruePath m_targetPath = null;
        [SerializeField] private float m_maxTime = 10.0f;
        [SerializeField] private float m_gravitationalConstant = 1.0f;

        public float gravitationalConstant => m_gravitationalConstant;

        private TruePath m_targetPath = null;

        [ShowNativeProperty] private TruePath targetPath => m_targetPath;

        private void Simulate(SimulatedObject target)
        {
            var dt = Time.fixedDeltaTime;
            var time = 0.0f;

            m_targetPath = ScriptableObject.CreateInstance<TruePath>();

            m_player.BeginSimulation();
            if (!Application.isPlaying)
                foreach (var planet in FindObjectsOfType<CelestialBody>())
                    planet.RegisterBody();

            m_targetPath.Clear();
            RecordState(target.position);

            while (time < m_maxTime)
            {
                SimulateStep(dt, target);
                RecordState(target.position);

                time += dt;
            }

            if (!Application.isPlaying)
                foreach (var planet in FindObjectsOfType<CelestialBody>())
                    planet.UnregisterBody();
            m_player.EndSimulation();
        }

        private void SimulateStep(float dt, SimulatedObject target)
        {
            var force = Vector2.zero;
            foreach (var planet in m_planets.bodies)
            {
                force += planet.CalculateForce(target);
            }

            var oldVelocity = target.velocity;
            var oldPosition = target.position;

            var a = force / target.mass;
            var newVelocity = oldVelocity + a * dt;
            var newPosition = oldPosition + newVelocity * dt;

            target.UpdateSimulatedState(newPosition, newVelocity);
        }

        private void RecordState(Vector2 position)
        {
            m_targetPath.AddPoint(position);
        }

        [Button("Simulate")]
        private void SimulatePlayer()
        {
            Simulate(m_player);
        }

        private void OnDrawGizmos()
        {
            if (m_targetPath != null)
            {
                Gizmos.color = Color.red;
                m_targetPath.DrawGizmos();
            }
        }
    }
}
