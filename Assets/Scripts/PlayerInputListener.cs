using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityPrototype
{
    public class PlayerInputListener : MonoBehaviour
    {
        [SerializeField] private float m_thrusterMaxForce = 1.0f;

        private SimulatedObject m_simulatedObject => GetComponent<SimulatedObject>();
        private SimulationManager m_simulationManager => GameComponentsLocator.Get<SimulationManager>();

        private Vector2 m_lastInput = Vector2.zero;

        public void OnMove(InputAction.CallbackContext context)
        {
            m_lastInput = context.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            var force = m_lastInput * m_thrusterMaxForce;
            force *= m_simulationManager.timeScale * m_simulationManager.timeScale;
            m_simulatedObject.ApplyForce(force);
        }
    }
}
