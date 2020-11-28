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
        }

        private Vector2 m_initialVelocity => m_initialDirection * m_initialSpeed;

        private Rigidbody2D m_body => GetComponent<Rigidbody2D>();
        private SimulatedState m_simulationState = null;

        public float mass => m_body.mass;
        public Vector2 position => m_simulationState != null ? m_simulationState.position : currentPosition;
        public Vector2 velocity => m_simulationState != null ? m_simulationState.velocity : currentVelocity;

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

        public void UpdateSimulatedState(Vector2 position, Vector2 velocity)
        {
            m_simulationState.position = position;
            m_simulationState.velocity = velocity;
        }

        public void EndSimulation()
        {
            m_simulationState = null;
        }
    }
}
