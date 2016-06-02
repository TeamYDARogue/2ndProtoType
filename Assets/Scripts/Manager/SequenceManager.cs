using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// --------------------------------------------------------------------------------
/// 2016.06.02
/// 制作者: Kei Yukawa
/// --------------------------------------------------------------------------------
/// シーケンス
/// --------------------------------------------------------------------------------
/// プレイヤーが移動を選択した場合:
///     KeyInput -> EnemyAI -> PlayerMove -> EnemyMove -> EnemyAction -> TurnEnd
/// プレイヤーが行動を選択した場合:
///     KeyInput -> PlayerAction -> EnemyAI -> EnemyAction -> EnemyMove -> TurnEnd
/// ---------------------------------------------------------------------------------
/// </summary>
/// 
public enum EnemyState
{
    eIdle,
    eAction,
    eMove,
    eTurnEnd,

}
public enum PlayerState
{
    eIdle,
    eAction,
    eMove,
    eTurnEnd,

}


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

    private EnemyState enemyState = EnemyState.eIdle;
    private PlayerState playerState = PlayerState.eIdle;

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
        //Debug.Log(NowState.ToString());
        Debug.Log("キー入力待機");

        /// 移動選択
        if(Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical") != 0)
        {
            playerState = PlayerState.eMove;
            return GameState.eEnemyAI;
        }

        /// 攻撃選択
        if(Input.GetKeyDown(KeyCode.Z))
        {
            playerState = PlayerState.eAction;
            return GameState.ePlayerAction;
        }

        return GameState.eKeyInput;
    }

    GameState PlayerAction()
    {
        //Debug.Log(NowState.ToString());
        Debug.Log("Playerの攻撃");

        return GameState.ePlayerActionEnd;
    }

    GameState PlayerMove()
    {
        //Debug.Log(NowState.ToString());
        Debug.Log("Playerの移動");

        return GameState.ePlayerActionEnd;
    }

    GameState PlayerActionEnd()
    {
        //Debug.Log(NowState.ToString());
        Debug.Log("Playerの行動終了");

        if(playerState == PlayerState.eAction)
        {
            playerState = PlayerState.eTurnEnd;
            return GameState.eEnemyAI;
        }

        return GameState.eEnemyMove;
    }

    GameState EnemyAIReq()
    {
        //Debug.Log(NowState.ToString());
        Debug.Log("敵AI選択");
        Debug.Log("Aキー : 移動");
        Debug.Log("Sキー : 攻撃");


        if (Input.GetKeyDown(KeyCode.A))
        {
            enemyState = EnemyState.eMove;

            if(playerState == PlayerState.eMove) { return GameState.ePlayerMover; }

            return GameState.eEnemyMove;

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            enemyState = EnemyState.eAction;

            if(playerState == PlayerState.eMove) { return GameState.ePlayerMover; }

            return GameState.eEnemyAction;

        }
        return GameState.eEnemyAI;
    }

    GameState EnemyMove()
    {
        //Debug.Log(NowState.ToString());

        if(enemyState == EnemyState.eAction) { return GameState.eEnemyAction; }

        Debug.Log("Enemyの移動");
        return GameState.eEnemyActionEnd;
    }

    GameState EnemyAction()
    {
        //Debug.Log(NowState.ToString());
        Debug.Log("Enemyの攻撃");
        return GameState.eEnemyActionEnd;
    }

    GameState EnemyActionEnd()
    {
        //Debug.Log(NowState.ToString());
        Debug.Log("Enemyの行動終了");

        return GameState.eTurnEnd;
    }

    GameState TurnEnd()
    {
        //Debug.Log(NowState.ToString());

        Debug.Log("ターン終了");

        return GameState.eKeyInput;
    }
}
