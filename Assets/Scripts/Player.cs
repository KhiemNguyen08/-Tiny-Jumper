using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 jumpForce;
    public Vector2 jumpForceUp;
    public float minForceX;
    public float maxForceX;
    public float minForceY;
    public float maxForceY;

    [HideInInspector]
    public int lastPlatFormID;

    bool m_didJump;
    bool m_powerSetted;

    Rigidbody2D m_rb;
    Animator m_anim;
    float curPowerBarVal = 0;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (GameManager.Ins.Is_gameStarted)
        {
            SetPower();
            if (Input.GetMouseButtonDown(0))
            {
                SetPower(true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                SetPower(false);
                //m_didJump = false;
            }
        }
    }
    void SetPower()
    {
        if(m_powerSetted && !m_didJump)
        {
            jumpForce.x += jumpForceUp.x * Time.deltaTime;
            jumpForce.y += jumpForceUp.y * Time.deltaTime;
            jumpForce.x = Mathf.Clamp(jumpForce.x, minForceX, maxForceX);
            jumpForce.y = Mathf.Clamp(jumpForce.y, minForceY, maxForceY);
           
            curPowerBarVal += GameManager.Ins.powerBarUp * Time.deltaTime;
            GameGUIManager.Ins.PowerBar(curPowerBarVal , 1);
        }
    }
    public void SetPower(bool isHoldingMouse)
    {
        m_powerSetted = isHoldingMouse;
        if(!m_powerSetted && !m_didJump)
        {
            Jump();
        }
    }
    void Jump()
    {
        if (!m_rb || jumpForce.x <= 0 || jumpForce.y <= 0) return;
        m_rb.velocity = jumpForce;
        m_didJump = true;
        if (!m_anim)
        {
            m_anim.SetBool("didJump", true);
        }
        AudioController.Ins.PlaySound(AudioController.Ins.jump);

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Tagconst.GROUND))
        {
            Platform p = col.transform.root.GetComponent<Platform>();
            if (m_didJump)
            {
                m_didJump = false;
                if (m_anim)
                {
                    m_anim.SetBool("didJump", false);
                }
                if (m_rb)
                {
                    m_rb.velocity = Vector2.zero;
                }
                jumpForce = Vector2.zero;
                curPowerBarVal = 0;
                GameGUIManager.Ins.PowerBar(curPowerBarVal, 1);
            }
            if(p && p.id != lastPlatFormID)
            {
                GameManager.Ins.CreatePlatformAndLerp(transform.position.x);
                lastPlatFormID = p.id;

                GameManager.Ins.AddScore();
            }
        }
        if (col.CompareTag(Tagconst.DEAD_ZONE))
        {
            Destroy(gameObject);
            GameGUIManager.Ins.GameoverDialog();
            AudioController.Ins.PlaySound(AudioController.Ins.gameover);

        }
    }
}
