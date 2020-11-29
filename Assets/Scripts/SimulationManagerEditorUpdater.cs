using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    [ExecuteInEditMode]
    public class SimulationManagerEditorUpdater : MonoBehaviour
    {
        [SerializeField] private bool m_recreatePath = false;

        private void Update()
        {
            // if (m_recreatePath && !Application.isPlaying)
            //     GetComponent<SimulationManager>().RecreatePlayerTruePath();
        }
    }
}
