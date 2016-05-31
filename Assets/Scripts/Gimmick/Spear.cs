using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour {

    private Rigidbody m_Rigidbody;
    private Vector3 m_GoalPos;
    private Vector3 m_StartPos;
    private Vector3 m_Pos;
    [SerializeField]
    private float m_Speed = 1.0f;
    private bool m_PosFlg = false;

	// Use this for initialization
	void Start () {
        m_Rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        //スタート地点を更新する
        m_StartPos = gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (m_PosFlg == false)
            {
                //ゴール地点を-5にする
                m_GoalPos.y = -5.0f;
                m_PosFlg = true;
            }
            else
            {
                //ゴール地点を0にする
                m_GoalPos.y = 0.0f;
                m_PosFlg = false;
            }
        }
        //Vector3.MoveTowards(スタート地点,ゴール地点,スピード)
        //スタート地点からゴール地点まで変数に入っているスピードで動く
        m_Pos = Vector3.MoveTowards(m_StartPos, m_GoalPos, m_Speed);
        m_Rigidbody.MovePosition(m_Pos);
    }
}
