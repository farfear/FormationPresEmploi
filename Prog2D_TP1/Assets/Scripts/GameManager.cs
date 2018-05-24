using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController m_player;
    public NPCController m_NPC;
    public GameObject m_button1;
    public GameObject m_button2;

    private Text m_button1Text;
    private Text m_button2Text;


    private string m_nPCGreetingsStrings = "Hey, 'sup Guybrush? I think you're in the wrong game./" +
        "This is some low-budget cheap-ass Blade Runner rip-off. Anyway, since you're already here.../" +
        "Wanna buy some magic?";
    private string m_nPCYesStrings = "Wonderful! Let me show you my beautiful non-sexual androids.";
    private string m_nPCNoStrings = "Pffff! Go back to Stan's Previously Own Vessels you filthy pirate peasant.";

    private string m_playerSelectionString1 = "Yes, of course !";
    private string m_playerSelectionString2 = "Nope-a-reno.";

    private void Awake()
    {
        m_button1Text = m_button1.GetComponentInChildren<Text>();
        m_button2Text = m_button2.GetComponentInChildren<Text>();

    }
    private void Start ()
    {
		
	}
	
	private void Update ()
    {
		
	}

    public void StartDialog(int aDialogNumber)
    {
        switch(aDialogNumber)
        {
            case 1:
                m_NPC.Speak(m_nPCGreetingsStrings);
                break;
            case 2:
                EnableButtons();
            break;
            case 3:
                DisableButtons();
                m_NPC.Speak(m_nPCYesStrings);
                break;
            case 4:
                DisableButtons();
                m_NPC.Speak(m_nPCNoStrings);
                break;

        }
    }

    public void DisableButtons()
    {
        m_button1.SetActive(false);
        m_button2.SetActive(false);
    }

    public void EnableButtons()
    {
        m_button1.SetActive(true);
        m_button1Text.text = m_playerSelectionString1;
        m_button2.SetActive(true);
        m_button2Text.text = m_playerSelectionString2;
    }

    public void OnBtn1Click()
    {
        DisableButtons();
        StartDialog(3);
        DisableButtons();
    }

    public void OnBtn2Click()
    {
        DisableButtons();
        StartDialog(4);
        DisableButtons();
    }

    public void ResetScene()
    {
        m_player.ResetScene();
        m_NPC.ResetScene();
        DisableButtons();
    }
}
