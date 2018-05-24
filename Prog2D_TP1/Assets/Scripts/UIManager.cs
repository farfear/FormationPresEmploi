using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject m_PlayerTextBox;
    public GameObject m_NPCTextBox;
    public GameObject m_NPCInteractionIcon;

    private Text m_NPCTextField;
    private Text m_PlayerTextField;

	private void Start ()
    {
        m_NPCTextField = m_NPCTextBox.GetComponent<Text>();
        m_PlayerTextField = m_PlayerTextBox.GetComponent<Text>();
	}
	
	private void Update ()
    {
		
	}

}
