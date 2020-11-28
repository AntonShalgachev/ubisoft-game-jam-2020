using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class SimulatedObject : MonoBehaviour
    {
        [SerializeField] private Vector2 m_initialDirection = Vector2.up;
        [SerializeField] private float m_initialSpeed = 5.0f;

        private class SimulatedState
        {
            public Vector2 position;
            public Vector2 velocity;
            public Vector2 acceleration;
        }

        private Vector2 m_initialVelocity => m_initialDirection * m_initialSpeed * m_simulationManager.timeScale;

        private Rigidbody2D m_body => GetComponent<Rigidbody2D>();
        private SimulatedState m_simulationState = null;
        private bool m_isSimulationActive => m_simulationState != null;

        private SimulationManager m_simulationManager => GameComponentsLocator.Get<SimulationManager>();

        public float mass => m_body.mass;
        public Vector2 position => m_isSimulationActive ? m_simulationState.position : currentPosition;
        public Vector2 velocity => m_isSimulationActive ? m_simulationState.velocity : currentVelocity;

        public Vector2 currentPosition => transform.position;
        public Vector2 currentVelocity => m_body.velocity;

        private void Start()
        {
            m_body.velocity = m_initialVelocity;
        }

        public void BeginSimulation()
        {
            m_simulationState = new SimulatedState();
            m_simulationState.position = currentPosition;
            m_simulationState.velocity = Application.isPlaying ? currentVelocity : m_initialVelocity;
        }

        public void UpdateObject(float dt)
        {
            if (m_isSimulationActive)
            {
                var newVelocity = velocity + m_simulationState.acceleration * dt;
                var newPosition = position + newVelocity * dt;

                m_simulationState.position = newPosition;
                m_simulationState.velocity = newVelocity;
                m_simulationState.acceleration = Vector2.zero;
            }
        }

        public void EndSimulation()
        {
            m_simulationState = null;
        }

        public void ApplyForce(Vector2 force)
        {
            if (m_isSimulationActive)
            {
                m_simulationState.acceleration = force / mass;
            }
            else
            {
                m_body.AddForce(force);
            }
        }
    }
}
