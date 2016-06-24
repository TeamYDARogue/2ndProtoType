using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


	private Vector3 originPosition = Vector3.zero;
	private TweenMove tweenMove = null;

	private GameObject m_target;
	private float m_time = 0;

	void Start()
	{
		m_target = serchTag(gameObject, "Player");

		originPosition = transform.position;
		tweenMove = Tween.Play ("EnemyTween") as TweenMove;
		tweenMove.Stop ();
	}

	void Update()	
	{
			m_time += Time.deltaTime;

			if (m_time >= 1.0f) {
				m_target = serchTag (gameObject, "Player");
			
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

		if (Input.GetKeyDown (KeyCode.A)) 
		{
			Debug.Log("TWeen!");
			OriginEnemy();
		}
	}

	void OriginEnemy()
	{
		tweenMove.startPosition = transform.position; 
		tweenMove.targetPosition = originPosition;    
		tweenMove.Play ();
	}

	GameObject serchTag(GameObject nowObj,string tagName){
		float tmpDis = 0;
		float nearDis = 0;

		GameObject targetObj = null;
		
		foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName)) {
			tmpDis = Vector3.Distance (obs.transform.position, nowObj.transform.position);
			
			if (nearDis == 0 || nearDis > tmpDis) {
				nearDis = tmpDis;
				targetObj = obs;
			}
		}
		
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
