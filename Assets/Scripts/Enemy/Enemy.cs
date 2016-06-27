using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


	//private Vector3 originPosition = Vector3.zero;
	//private TweenMove tweenMove = null;

	private GameObject m_target;  //最も近いオブジェクト
	private float m_time = 0;     //経過時間

	void Start()
	{
		//最も近かったオブジェクトを取得する処理
		m_target = serchTag(gameObject, "Player");

		//originPosition = transform.position;
		//tweenMove = Tween.Play ("EnemyTween") as TweenMove;
		//tweenMove.Stop ();
	}

	void Update()	
	{
		    //経過時間を取得する処理
			m_time += Time.deltaTime;

			if (m_time >= 1.0f) {
			//最も近かったオブジェクトを取得する処理
				m_target = serchTag (gameObject, "Player");
			//経過時間を初期化
				m_time = 0;
		}

		/*if(Input.GetKeyDown(KeyCode.A)){
			transform.position = new Vector3 (3.0f, 0.0f, 0.0f);
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			transform.position += new Vector3 (3.0f, 0.0f, 0.0f);
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			transform.position += new Vector3 (0.0f, 0.0f, 3.0f);
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			transform.position -= new Vector3 (0.0f, 0.0f, 3.0f);
		}*/

		//Zキーを押したら
		if (Input.GetKeyDown (KeyCode.Z)) {
			transform.LookAt (m_target.transform);            //キャラの方向に向く
			transform.Translate (Vector3.forward * 2.00f);    //キャラの方に向かう
		}

		/*if (Input.GetKeyDown (KeyCode.A)) 
		{
			Debug.Log("TWeen!");
			OriginEnemy();
		}*/
	}

	/*void OriginEnemy()
	{
		tweenMove.startPosition = transform.position; 
		tweenMove.targetPosition = originPosition;    
		tweenMove.Play ();
	}*/

	//指定されたタグの中で最も近い物を取得する処理
	GameObject serchTag(GameObject nowObj,string tagName){

		float tmpDis = 0;       //距離用一時変数
		float nearDis = 0;      //最も近いオブジェクトとの距離

		GameObject targetObj = null;     //オブジェクト

		//タグ指定されたプレイヤーを配列で取得
		foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName)) {
			//Enemyとプレイヤーの距離を取得
			tmpDis = Vector3.Distance (obs.transform.position, nowObj.transform.position);

			//プレイヤーとの距離が近いか,距離0であればオブジェクト名を取得
			//一時変数に距離を格納
			if (nearDis == 0 || nearDis > tmpDis) {
				nearDis = tmpDis;
				targetObj = obs;
			}
		}

		//最も近かったオブジェクトを返す
		return targetObj;
	}






	/*private Transform m_target;
	private bool m_move;

	protected override void Start()
	{
		m_target = GameObject.FindGameObjectWithTag ("Player").transform;
		base.Start ();
	}

	protected override void AttemptMove<T> (int xDir, int yDir)
	{
		if (m_move) {
			m_move = false;
			return;
		}
		base.AttemptMove <T> (xDir, yDir);
		m_move = true;
	}

	public void MobeEnemy()
	{
		int xDir = 0;
		int yDir = 0;
		if (Mathf.Abs (m_target.position.x - transform.position.x) < float.Epsilon) {
			yDir = m_target.position.y > transform.position.y ? 1 : -1;
		} else {
			xDir = m_target.position.x > transform.position.x ? 1 : -1;
		}

		AttemptMove <Player> (xDir, yDir);
	}

	protected override void OnCantMove <T> (T component)
	{
		Player hitPlayer = component as Player;
	}*/
}
