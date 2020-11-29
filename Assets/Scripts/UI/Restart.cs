using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityPrototype
{
    public class Restart : MonoBehaviour
    {
        [SerializeField] private GameObject m_button;
        [SerializeField] private GameObject m_text;

        private void Update()
        {
            var canRestart = GameComponentsLocator.Get<GameFlowManager>().canRestart;
            var success = GameComponentsLocator.Get<GameFlowManager>().state == GameFlowManager.State.PostFarewell;
            m_button.SetActive(canRestart);
            m_text.SetActive(success);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
