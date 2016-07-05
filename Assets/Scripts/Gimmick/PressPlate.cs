using UnityEngine;
using System.Collections;

public class PressPlate : MonoBehaviour {


    private Renderer m_Rend;
    private State m_PressState = State.NoTouchPress;    //感圧版の状態管理
    [SerializeField]
    private GameObject spearObj = null;
    private Spear spear = null;

    void Start()
    {
        m_Rend = GetComponent<Renderer>();
        spear = spearObj.GetComponent<Spear>();
    }
    public void UpdateHandler()
    {

    }
    void OnCollisionEnter(Collision C)
    {
        if (C.gameObject.tag == "Player" && m_PressState == State.NoTouchPress)
        {
            Debug.Log("触れている");
            //色を変える
            m_Rend.material.EnableKeyword("_Color");
            m_Rend.material.SetColor("s_Color", new Color(0, 1, 0));
            //感圧版を触れている状態に切り替える
            ChangeState(State.TouchPress);
            //ドアを開ける
            spear.SpearState();
        }
    }
    /*プレイヤーが感圧版に触れていないとき*/
    /*void OnCollisionExit(Collision Exit)
    {
        if (Exit.gameObject.tag == "Player" && m_PressState == State.TouchPress)
        {
            Debug.Log("触れていない");
            //色を変える
            m_Rend.material.EnableKeyword("_Color");
            m_Rend.material.SetColor("_Color", new Color(1, 1, 1));
            //感圧版を触れていない状態に切り替える
            ChangeState(State.NoTouchPress);
            //ドアを閉める
            spear.SpearState();
        }

    }*/
    public enum State
    {
        TouchPress,
        NoTouchPress,
    }
    public void ChangeState(State s)
    {
        m_PressState = s;
    }
}
