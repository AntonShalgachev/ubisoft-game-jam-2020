using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityPrototype
{
    public class DesyncBar : MonoBehaviour
    {
        [SerializeField] private Slider m_bar = null;

        private void Update()
        {
            var desyncManager = GameComponentsLocator.Get<DesynchronizationManager>();
            m_bar.value = desyncManager.desynchronization;
        }
    }
}
