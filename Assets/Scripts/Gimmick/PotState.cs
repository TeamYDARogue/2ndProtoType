using UnityEngine;
using System.Collections;

public class PotState : PlayerMover {

    private Rigidbody m_RigidBody;
    [SerializeField]
    private GameObject m_Pot;
    [SerializeField]
    private GameObject m_Player;
    private Vector3 m_PotPos;
    private Vector3 m_Playerpos;
    private Vector3 m_poss;
    private State m_State = State.NoTacch;
    private PlayerMover m_PlayerMover;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_PotPos = m_Pot.transform.position;
        m_PlayerMover = GetComponent<PlayerMover>();
        
    }
    void FixedUpdate()
    {
        pos();
        m_Playerpos = m_Player.transform.position;
    }
    void pos()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_Pot.transform.Translate(0, 0, -1);
        }
        m_Playerpos = m_Player.transform.position;
        m_PotPos = m_Pot.transform.position;
        m_poss = m_Playerpos - m_PotPos;

        Debug.Log(m_poss);
        Debug.Log(m_State);

        if (m_State == State.NoTacch)
        {
            if (m_poss.x == 1 && m_poss.z == 0)
            {
                Input.ResetInputAxes();
                ChangeState(State.LeftTacch);
            }
            else if (m_poss.x == -1 && m_poss.z == 0)
            {
                Input.ResetInputAxes();
                ChangeState(State.RightTacch);
            }
            else if (m_poss.z == 1 && m_poss.x == 0)
            {
                Input.ResetInputAxes();
                ChangeState(State.DownTacch);
            }
            else if (m_poss.z == -1 && m_poss.x == 0)
            {
                Input.ResetInputAxes();
                ChangeState(State.UpTacch);
            }
        }
        switch (m_State)
        {
            case State.RightTacch:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    m_Pot.SetActive(false);
                    //m_RigidBody.constraints = (RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ);
                    m_Pot.transform.position = new Vector3(100, 100, 100);
                    m_PotPos = m_Pot.transform.position;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) ||
                        Input.GetKeyDown(KeyCode.UpArrow) ||
                        Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ChangeState(State.WasTacch);
                }
                break;
            case State.LeftTacch:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    m_Pot.SetActive(false);
                    m_Pot.transform.position = new Vector3(100, 100, 100);
                    m_PotPos = m_Pot.transform.position;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) ||
                         Input.GetKeyDown(KeyCode.UpArrow) ||
                         Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ChangeState(State.WasTacch);
                }
                break;
            case State.UpTacch:
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    m_Pot.SetActive(false);
                    m_Pot.transform.position = new Vector3(100, 100, 100);
                    m_PotPos = m_Pot.transform.position;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) ||
                         Input.GetKeyDown(KeyCode.RightArrow) ||
                         Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ChangeState(State.WasTacch);
                }
                break;
            case State.DownTacch:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    //m_RigidBody.constraints = (RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ);
                    //m_Pot.transform.position = new Vector3(100, 100, 100);
                    m_Pot.SetActive(false);
                    m_PotPos = m_Pot.transform.position;
                    ChangeState(State.NoTacch);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) ||
                         Input.GetKeyDown(KeyCode.UpArrow) ||
                         Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ChangeState(State.WasTacch);
                }
                break;
            default:
                break;
        }
        if (m_State == State.UpTacch ||
           m_State == State.DownTacch ||
           m_State == State.RightTacch ||
           m_State == State.LeftTacch)
        {
            if (m_poss.x == 100 || m_poss.x == -100 ||
                m_poss.z == 100 || m_poss.z == -100)
            {
                ChangeState(State.NoTacch);
                //m_RigidBody.constraints = (RigidbodyConstraints.None);
                //m_PlayerMover.Update();
            }
        }
        if (m_State == State.WasTacch)
        {
            
            if (m_poss.z != -1 && m_poss.x != 0 || m_poss.z != 1 && m_poss.x != 0 ||
                m_poss.x != -1 && m_poss.z != 0 || m_poss.x != 1 && m_poss.z != 0)
            {
                //m_RigidBody.constraints = (RigidbodyConstraints.None);
                ChangeState(State.NoTacch);
            }

        }
    }
    void OnCollisionEnter(Collision c)
    {
        Debug.Log("bbbbbBBBBBBBBBBBBBBBBBB");

        if (c.gameObject.tag == "Player")
        {
            Debug.Log("CCCCCCCCCCCCCCCCC");
            m_Pot = c.gameObject;
            Debug.Log("Aaaaaaaaaaaaaaaaaa");
        }

    }
    public enum State
    {
        UpTacch,
        DownTacch,
        LeftTacch,
        RightTacch,
        NoTacch,
        WasTacch,
    }
    public void ChangeState(State s)
    {
        m_State = s;
    }
}
