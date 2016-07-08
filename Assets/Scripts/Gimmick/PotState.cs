using UnityEngine;
using System.Collections;

public class PotState : PlayerMover {
    
    /*
    private Rigidbody m_RigidBody;
    [SerializeField]
    private GameObject m_Pot;
    [SerializeField]
    private GameObject m_Player;
    private Vector3 m_PotPos;
    private Vector3 m_PlayerPos;
    private Vector3 m_Pos;
    private State m_State = State.NoTacch;
    private PlayerMover m_PlayerMover;
    */


    /*
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_PotPos = m_Pot.transform.position;
        m_PlayerMover = GetComponent<PlayerMover>();
        m_Player = GameObject.FindWithTag("Player");
        m_PotPos = m_Pot.transform.position;
    }
    void FixedUpdate()
    {
        m_PlayerPos = m_Player.transform.position;
        m_Pos = m_PlayerPos - m_PotPos;
        if (m_Pos.x < 2 && m_Pos.x > -2 ||
            m_Pos.z < 2 && m_Pos.z > -2)
        {
            Pos();
        }
    }
    void Pos()
    {

//////////////////プレイヤーと壺が当たっているか判定///////////
        if (m_State == State.NoTacch)
        {
            if (m_Pos.x == 1 && m_Pos.z == 0)
            {
                Input.ResetInputAxes();
                ChangeState(State.LeftTacch);
            }
            else if (m_Pos.x == -1 && m_Pos.z == 0)
            {
                Input.ResetInputAxes();
                ChangeState(State.RightTacch);
            }
            else if (m_Pos.z == 1 && m_Pos.x == 0)
            {
                Input.ResetInputAxes();
                ChangeState(State.DownTacch);
            }
            else if (m_Pos.z == -1 && m_Pos.x == 0)
            {
                Input.ResetInputAxes();
                ChangeState(State.UpTacch);
            }
        }

//////////////////当たっている状態でキーを入力したとき///////////////

        if (m_State == State.RightTacch)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                m_Pot.transform.position = new Vector3(100, 100, 100);
                m_PotPos = m_Pot.transform.position;
                Input.ResetInputAxes();
               // m_PlayerMover.Update();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) ||
                    Input.GetKeyDown(KeyCode.UpArrow) ||
                    Input.GetKeyDown(KeyCode.DownArrow))
            {
                ChangeState(State.WasTacch);
            }
        }
        if (m_State == State.LeftTacch) {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                m_Pot.transform.position = new Vector3(100, 100, 100);
                m_PotPos = m_Pot.transform.position;
                Input.ResetInputAxes();
                //m_PlayerMover.Update();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     Input.GetKeyDown(KeyCode.DownArrow))
            {
                ChangeState(State.WasTacch);
            }
        }
        if (m_State == State.UpTacch) {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_Pot.transform.position = new Vector3(100, 100, 100);
                m_PotPos = m_Pot.transform.position;
                Input.ResetInputAxes();
                //m_PlayerMover.Update();

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) ||
                     Input.GetKeyDown(KeyCode.RightArrow) ||
                     Input.GetKeyDown(KeyCode.DownArrow))
            {
                ChangeState(State.WasTacch);
            }
        }
        if (m_State == State.DownTacch)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                m_Pot.transform.position = new Vector3(100, 100, 100);
                m_PotPos = m_Pot.transform.position;
                Input.ResetInputAxes();
                //m_PlayerMover.Update();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeState(State.WasTacch);
            }
        }
//////////////もしどれかのキーを押したときに壺の位置が変わっていたら処理//////////    

        if (m_State == State.WasTacch)
        {
            if (m_Pos.z != -1 && m_Pos.x != 0 ||
                m_Pos.z != 1 && m_Pos.x != 0 ||
                m_Pos.x != -1 && m_Pos.z != 0 ||
                m_Pos.x != 1 && m_Pos.z != 0)
            {
                ChangeState(State.NoTacch);
            }
        }
    }*/
    void OnCollisionEnter(Collision C)
    {
        if(C.gameObject.tag == "Player")
        {
            Destroy(C.gameObject);
        }
    }

    /*
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
    */
}
