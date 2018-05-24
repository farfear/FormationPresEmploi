using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameManager m_gameManager;
    public AudioManager m_audioManager;
    public float m_walkingSpeed = 2f;
    public NPCController m_NPC;
    [HideInInspector]
    public bool m_canSpeak = false;

    private Animator m_animator;
    private int m_currentState = 0;
    private Vector3 m_newPosition = new Vector3();
    private Vector3 m_originalPosition;
    private SpriteRenderer m_spriteRenderer;
    private bool m_dialogActive = false;
    private bool m_collisionTracked = false;
    private int m_dialogNumber = 1;

    private enum m_states
    { 
        Idle = 0,
        Walk = 1
    };

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start ()
    {
        m_originalPosition = transform.localPosition;
        m_newPosition = transform.localPosition;
    }

    private void Update ()
    {
        m_collisionTracked = false;
        Move();
        CheckSpeakInput();
	}

    public void IncreaseDialogNumber()
    {
        m_dialogNumber++;
    }

    public void IncreaseDialogNumber(int aCount)
    {
        m_dialogNumber += aCount;
    }

    public void ResetScene()
    {
        m_currentState = 0;
        m_canSpeak = false;
        m_dialogActive = false;
        m_dialogNumber = 1;
        m_newPosition = m_originalPosition;
        transform.position = m_newPosition;
        m_spriteRenderer.flipX = false;
    }

    public void ResetDialog()
    {
        m_dialogActive = false;
        m_dialogNumber = 1;
    }

    private void CheckSpeakInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && m_canSpeak && !m_dialogActive && m_dialogNumber == 1)
        {
            m_dialogActive = true;
            m_canSpeak = false;
            m_NPC.HideNPCInteractionIcon();
            m_NPC.SetDialogTriggerZone(false);
            m_gameManager.StartDialog(m_dialogNumber);
        }

        else if (Input.GetKeyDown(KeyCode.E) && m_canSpeak && m_dialogNumber == 2)
        {
            m_dialogActive = true;
            m_canSpeak = false;
            m_gameManager.StartDialog(m_dialogNumber);
        }

        else if (Input.GetKeyDown(KeyCode.E) && m_canSpeak && m_dialogNumber == 3)
        {
            ResetDialog();
            m_NPC.SetDialogTriggerZone(true);
        }
    }

    private void Move()
    {
        if(Input.GetKey(KeyCode.D) && !m_dialogActive)
        {
            m_spriteRenderer.flipX = false;
            m_currentState = (int)m_states.Walk;
            m_animator.SetInteger("State", m_currentState);
            m_newPosition.x += m_walkingSpeed;
        }

        else if(Input.GetKey(KeyCode.A) && !m_dialogActive)
        {
            m_spriteRenderer.flipX = true;
            m_currentState = (int)m_states.Walk;
            m_animator.SetInteger("State", m_currentState);
            m_newPosition.x -= m_walkingSpeed;
        }

        else
        {
            m_currentState = (int)m_states.Idle;
            m_animator.SetInteger("State", m_currentState);
        }

        transform.position = m_newPosition;
    }

    private void OnTriggerEnter2D(Collider2D aCol)
    {
        Debug.Log("OnTriggerEnter");

        if(aCol.tag == "NPC" && !m_dialogActive)
        {
            m_collisionTracked = true;
            m_NPC.DisplayNPCInteractionIcon();
            m_canSpeak = true;
        }
    }

    private void OnTriggerExit2D(Collider2D aCol)
    {
        Debug.Log("OnTriggerExit");

        if (aCol.tag == "NPC" && !m_dialogActive)
        {
            m_NPC.HideNPCInteractionIcon();
            m_canSpeak = false;
        }
    }
}
