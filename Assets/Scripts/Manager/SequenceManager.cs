using UnityEngine;
using System.Collections;

public class SequenceManager : MonoBehaviour {

    private enum State
    {
        eKeyInput = 0,      //キー入力
        ePlayerAction,      //プレイヤーの行動
        ePlayerMover,       //プレイヤーの移動
        ePlayerActionEnd,   //プレイヤーの行動終了
        eEnemyAI,           //敵のAIリクエスト
        eEnemyAction,       //敵の行動
        eEnemyActionEnd,    //敵の行動終了
        eMenu,              //メニューを開いている
        eTurnEnd,           //全体のターン終了
        eNextFloor,         //次の階層へ移動
    }

    private State m_nowState = State.eKeyInput;
    private State m_prevState = State.eKeyInput;

    void ChangeState(State _state)
    {
        m_prevState = m_nowState;
        m_nowState = _state;
    }

    void Update()
    {

    }
}
