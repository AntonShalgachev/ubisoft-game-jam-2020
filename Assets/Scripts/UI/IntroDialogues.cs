using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

namespace UnityPrototype
{
    public class IntroDialogues : MonoBehaviour
    {
        [SerializeField] private DialogManager m_dialogueManager = null;
        [SerializeField] private string[] m_lines;

        private void Start()
        {
            var dialogTexts = new List<DialogData>();

            foreach (var line in m_lines)
                dialogTexts.Add(new DialogData(line, "CrewMember"));

            m_dialogueManager.Show(dialogTexts);
        }
    }
}
