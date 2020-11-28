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
        [SerializeField] private float m_maxTime = 10.0f;
        [SerializeField] private float m_gravitationalConstant = 1.0f;
        [SerializeField] private bool m_simulateInRuntime = false;
        [SerializeField] private PathFollower m_ghostPlayer = null;

        public float gravitationalConstant => m_gravitationalConstant;

        private TruePath m_targetPath = null;

        [ShowNativeProperty] private TruePath targetPath => m_targetPath;

        private void Start()
        {
            CreateTruePath(m_player);
            m_ghostPlayer.SetPath(m_targetPath);
        }

        private void FixedUpdate()
        {
            if (m_simulateInRuntime)
                SimulateStep(Time.fixedDeltaTime, m_player);
        }

        private void CreateTruePath(SimulatedObject target)
        {
            var dt = Time.fixedDeltaTime;
            var time = 0.0f;

            m_targetPath = ScriptableObject.CreateInstance<TruePath>();

            m_player.BeginSimulation();
            if (!Application.isPlaying)
                foreach (var planet in FindObjectsOfType<CelestialBody>())
                    planet.RegisterBody();

            m_targetPath.Clear();

            while (time < m_maxTime)
            {
                RecordState(target.position, time);
                SimulateStep(dt, target);
                time += dt;
            }

            RecordState(target.position, time);

            if (!Application.isPlaying)
                foreach (var planet in FindObjectsOfType<CelestialBody>())
                    planet.UnregisterBody();
            m_player.EndSimulation();
        }

        private void SimulateStep(float dt, SimulatedObject target)
        {
            var force = Vector2.zero;
            foreach (var planet in m_planets.bodies)
                force += planet.CalculateForce(target);

            target.ApplyForce(force, dt);
        }

        private void RecordState(Vector2 position, float time)
        {
            m_targetPath.AddPoint(position, time);
        }

        private void OnDrawGizmos()
        {
            if (m_targetPath != null)
            {
                Gizmos.color = Color.red;
                m_targetPath.DrawGizmos();
            }
        }

        [Button("Simulate")]
        private void SimulatePlayer()
        {
            CreateTruePath(m_player);
        }
    }
}
