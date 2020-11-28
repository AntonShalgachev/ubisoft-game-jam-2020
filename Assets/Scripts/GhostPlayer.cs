using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPrototype
{
    public class GhostPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject m_visual = null;

        private void Update()
        {
            m_visual.SetActive(GetComponent<PathFollower>().hasPath);
        }
    }
}
