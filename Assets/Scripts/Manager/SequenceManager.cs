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

    private delegate void GameProc();
    private Dictionary<GameState, GameProc> gameProc = new Dictionary<GameState, GameProc>();

    private EnemyState enemyState = EnemyState.eIdle;
    private PlayerState playerState = PlayerState.eIdle;

    public static GameState NowState
    {
        get;
        set;
    }


    void Awake()
    {
        gameProc.Add(GameState.eKeyInput, KeyInput);

        gameProc.Add(GameState.ePlayerMover, PlayerMove);
        gameProc.Add(GameState.ePlayerAction, PlayerAction);
        gameProc.Add(GameState.ePlayerActionEnd, PlayerActionEnd);

        gameProc.Add(GameState.eEnemyAI, EnemyAIReq);
        gameProc.Add(GameState.eEnemyMove, EnemyMove);
        gameProc.Add(GameState.eEnemyAction, EnemyAction);
        gameProc.Add(GameState.eEnemyActionEnd, EnemyActionEnd);

        gameProc.Add(GameState.eTurnEnd, TurnEnd);

        NowState = GameState.eKeyInput;
    }

    void Update()
    {
        gameProc[NowState]();
    }


    void KeyInput()
    {
        Debug.Log("キー入力待機");

        /// 移動選択
        if(Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical") != 0)
        {
            playerState = PlayerState.eMove;
            NowState = GameState.eEnemyAI;
        }

        /// 攻撃選択
        if(Input.GetKeyDown(KeyCode.Z))
        {
            playerState = PlayerState.eAction;
            NowState = GameState.ePlayerAction;
        }

        NowState = GameState.eKeyInput;
    }

    void PlayerAction()
    {
        //Debug.Log(NowState.ToString());
        Debug.Log("Playerの攻撃");

        NowState = GameState.ePlayerActionEnd;
    }

    void PlayerMove()
    {
        //Debug.Log(NowState.ToString());
        Debug.Log("Playerの移動");

        NowState = GameState.ePlayerActionEnd;
    }

    void PlayerActionEnd()
    {
        //Debug.Log(NowState.ToString());
        Debug.Log("Playerの行動終了");

        if(playerState == PlayerState.eAction)
        {
            playerState = PlayerState.eTurnEnd;
            NowState = GameState.eEnemyAI;
        }

        NowState = GameState.eEnemyMove;
    }

    void EnemyAIReq()
    {
        //Debug.Log(NowState.ToString());
        Debug.Log("敵AI選択");
        Debug.Log("Aキー : 移動");
        Debug.Log("Sキー : 攻撃");


        if (Input.GetKeyDown(KeyCode.A))
        {
            enemyState = EnemyState.eMove;

            if(playerState == PlayerState.eMove) { NowState = GameState.ePlayerMover; }

            NowState = GameState.eEnemyMove;

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            enemyState = EnemyState.eAction;

            if(playerState == PlayerState.eMove) { NowState = GameState.ePlayerMover; }

            NowState = GameState.eEnemyAction;

        }
        NowState = GameState.eEnemyAI;
    }

    void EnemyMove()
    {
        if(enemyState == EnemyState.eAction) { NowState = GameState.eEnemyAction; }

        Debug.Log("Enemyの移動");
        NowState = GameState.eEnemyActionEnd;
    }

    void EnemyAction()
    {
        Debug.Log("Enemyの攻撃");
        NowState = GameState.eEnemyActionEnd;
    }

    void EnemyActionEnd()
    {
        Debug.Log("Enemyの行動終了");

        NowState = GameState.eTurnEnd;
    }

    void TurnEnd()
    {
        Debug.Log("ターン終了");

        NowState = GameState.eKeyInput;
    }
}
