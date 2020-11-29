using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;
using UnityEngine.Events;

namespace UnityPrototype
{
    public class SuccessDialogues : MonoBehaviour
    {
        [SerializeField] private DialogManager m_dialogueManager = null;
        [SerializeField, TextArea] private string[] m_lines = new string[] { };

        public void ShowFarewell()
        {
            var dialogTexts = new List<DialogData>();

            for (var i = 0; i < m_lines.Length; i++)
            {
                var line = m_lines[i];
                var isLast = i == m_lines.Length - 1;
                UnityAction onClosed = isLast ? OnLastDialogueClosed : (UnityAction)null;
                dialogTexts.Add(new DialogData(line, "CrewMember", onClosed));
            }

            m_dialogueManager.Show(dialogTexts);
        }

        private void OnLastDialogueClosed()
        {
            Debug.Log("Dialogue closed");
            var flowManager = GameComponentsLocator.Get<UnityPrototype.GameFlowManager>();
            flowManager.OnFarewellCompleted();
        }
    }
}
