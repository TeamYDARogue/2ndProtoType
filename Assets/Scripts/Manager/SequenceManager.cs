using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequenceManager : MonoBehaviour {

    public enum GameState
    {
        eKeyInput = 0,      //キー入力
        ePlayerAction,      //プレイヤーの行動
        ePlayerMover,       //プレイヤーの移動
        ePlayerActionEnd,   //プレイヤーの行動終了
        eEnemyAI,           //敵のAIリクエスト
        eEnemyAction,       //敵の行動
        eEnemyMove,         //敵の移動
        eEnemyActionEnd,    //敵の行動終了
        eMenu,              //メニューを開いている
        eTurnEnd,           //全体のターン終了
        eNextFloor,         //次の階層へ移動
    }

    private delegate GameState GameProc();
    private Dictionary<GameState, GameProc> m_gameProc = new Dictionary<GameState, GameProc>();

    public static GameState NowState
    {
        get;
        set;
    }


    void Awake()
    {
        m_gameProc.Add(GameState.eKeyInput, KeyInput);

        m_gameProc.Add(GameState.ePlayerMover, PlayerMove);
        m_gameProc.Add(GameState.ePlayerAction, PlayerAction);
        m_gameProc.Add(GameState.ePlayerActionEnd, PlayerActionEnd);

        m_gameProc.Add(GameState.eEnemyAI, EnemyAIReq);
        m_gameProc.Add(GameState.eEnemyMove, EnemyMove);
        m_gameProc.Add(GameState.eEnemyAction, EnemyAction);
        m_gameProc.Add(GameState.eEnemyActionEnd, EnemyActionEnd);

        m_gameProc.Add(GameState.eTurnEnd, TurnEnd);

        NowState = GameState.eKeyInput;
    }

    void Update()
    {
        NowState = m_gameProc[NowState]();
    }


    GameState KeyInput()
    {
        Debug.Log(NowState.ToString());

        if(Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical") != 0)
        {
            return GameState.ePlayerMover;
        }
        return GameState.eKeyInput;
    }

    GameState PlayerAction()
    {
        Debug.Log(NowState.ToString());

        return GameState.ePlayerAction;
    }

    GameState PlayerMove()
    {
        Debug.Log(NowState.ToString());

        return GameState.ePlayerMover;
    }

    GameState PlayerActionEnd()
    {
        Debug.Log(NowState.ToString());

        return GameState.ePlayerActionEnd;
    }

    GameState EnemyAIReq()
    {
        Debug.Log(NowState.ToString());

        return GameState.eEnemyAI;
    }

    GameState EnemyMove()
    {
        Debug.Log(NowState.ToString());

        return GameState.eEnemyMove;
    }

    GameState EnemyAction()
    {
        Debug.Log(NowState.ToString());

        return GameState.eEnemyAction;
    }

    GameState EnemyActionEnd()
    {
        Debug.Log(NowState.ToString());

        return GameState.eEnemyActionEnd;
    }

    GameState TurnEnd()
    {
        Debug.Log(NowState.ToString());

        return GameState.eTurnEnd;
    }
}
