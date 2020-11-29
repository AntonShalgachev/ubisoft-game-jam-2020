using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class PathVisual : MonoBehaviour
    {
        [SerializeField] private float m_warningThreshold = 0.1f;
        [SerializeField] private Color m_normalColor = Color.white;
        [SerializeField] private Color m_warningColor = Color.yellow;
        [SerializeField] private float m_colorTransitionSpeed = 1.0f;

        Color m_currentColor;

        public void SetPath(TruePath path)
        {
            var lineRenderer = GetComponent<LineRenderer>();

            var positions = path.GetPositions();
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);

            m_currentColor = m_normalColor;
            UpdateColor();
        }

        private void UpdateColor()
        {
            var lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startColor = m_currentColor;
            lineRenderer.endColor = m_currentColor;
        }

        private void Update()
        {
            var desyncManager = GameComponentsLocator.Get<DesynchronizationManager>();

            var warning = desyncManager.currentDesyncRate > m_warningThreshold;
            var targetColor = warning ? m_warningColor : m_normalColor;

            m_currentColor = Color.Lerp(m_currentColor, targetColor, m_colorTransitionSpeed * Time.deltaTime);

            UpdateColor();
        }
    }
}
