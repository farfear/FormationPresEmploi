using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public GameObject m_NPCTextBox;
    public Text m_NPCTextField;
    public GameObject m_NPCInteractionIcon;
    public PlayerController m_player;
    public AudioManager m_audioManager;
    public BoxCollider2D m_boxCollider;

    private float m_textApparitionDelay = 0.05f;
    private string[] m_NPCStrings;

    private Animator m_animator;

    private enum m_states
    {
        Idle = 1,
        Speaking = 2
    };

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

	private void Update ()
    {
	}

    public void DisplayNPCInteractionIcon()
    {
        m_NPCInteractionIcon.SetActive(true);
    }

    public void HideNPCInteractionIcon()
    {
        m_NPCInteractionIcon.SetActive(false);
    }

    public void ResetScene()
    {
        StopAllCoroutines();
        m_audioManager.StopSFX();
        m_audioManager.StopMusic();
        m_audioManager.StartMusic();
        HideNPCInteractionIcon();
        HideNPCTextBox();
        SetDialogTriggerZone(true);
        SetTalkingAnimation(false);
    }

    public void SetDialogTriggerZone(bool aValue)
    {
        m_boxCollider.enabled = aValue;
    }

    private void DisplayNPCTextBox()
    {
        m_NPCTextBox.SetActive(true);
    }

    private IEnumerator DisplayTextInDialogBox(string[] aDialogStrings)
    {

        for (int i = 0; i < aDialogStrings.Length; i++)
        {
            string displayedText = "";
            m_NPCTextField.text = displayedText;
            int letterCount = aDialogStrings[i].Length;
            SetTalkingAnimation(true);
            int j = 0;
            m_audioManager.StartSFX();

            while (j < letterCount)
            {
                yield return new WaitForSeconds(m_textApparitionDelay);
                m_NPCTextField.text = displayedText + aDialogStrings[i][j];
                displayedText = m_NPCTextField.text;
                j++;
            }
            SetTalkingAnimation(false);
            m_audioManager.StopSFX();
            DisplayNPCInteractionIcon();
            m_player.m_canSpeak = true;
            if(i == aDialogStrings.Length - 1)
            {
                m_player.IncreaseDialogNumber();
            }

            while (true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    HideNPCInteractionIcon();
                    break;
                }
                yield return null;
            }
        }

        HideNPCTextBox();
    }

    public void HideNPCTextBox()
    {
        m_NPCTextBox.SetActive(false);
    }

    public void Speak(string aNPCString)
    {
        m_NPCStrings = aNPCString.Split('/');
        DisplayNPCTextBox();
        StartCoroutine(DisplayTextInDialogBox(m_NPCStrings));
    }

    public void SetTalkingAnimation(bool aBool)
    {
        m_animator.SetBool("isTalking", aBool);
    }
}
