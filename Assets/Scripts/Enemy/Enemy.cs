using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	
	struct Point2{
		public int x;
		public int z;
		public Point2(int x = 0, int z = 0){
			this.x = x;
			this.z = z;
		}

		public void Set(int x,int z){
			this.x = x;
			this.z = z;
		}
	}

	//A-Starノード
	class ANode{
		enum eStatus{
			None,
			Open,
			Closed,
		}

		//ステータス
		eStatus m_status = eStatus.None;
		//実コスト
		int m_cost = 0;
		//ヒューリスティック・コスト
		int m_heuristic = 0;
		//親ノード
		ANode m_parent = null;

		//座標
		int m_x = 0;
		int m_z = 0;

		public int X {
			get{ return m_x;}
		}
		public int Z{
			get{return m_z;}
		}
		public int Cost{
			get{return m_cost;}
		}

		//コンストラクタ
		public ANode(int x,int z){
			m_x = x;
			m_z = z;
		}

		//スコアを計算する
		public int GetScore(){
			return m_cost + m_heuristic;
		}

		//ヒューリスティック・コストの計算
		public void CalcHeuristic(bool allowdiag,int xgoal,int zgoal){

			if (allowdiag) {
				var dx = (int)Mathf.Abs (xgoal - X);
				var dz = (int)Mathf.Abs (zgoal - Z);
				m_heuristic = dx > dz ? dx : dz;
			} else {
				//縦横移動のみ
				var dx = Mathf.Abs(xgoal - X);
				var dz = Mathf.Abs(zgoal - Z);
				m_heuristic = (int)(dx + dz);
			}
			Dump ();
		}

		//ステータスかNoneかどうか
		public bool IsNone(){
			return m_status == eStatus.None;
		}

		//ステータスをOpenにする
		public void Open(ANode parent,int cost){
			Debug.Log (string.Format ("Open:({0},{1})", X, Z));
			m_status = eStatus.Open;
			m_cost = cost;
			m_parent = parent;
		}

		//ステータスをClosedにする
		public void Close(){
			Debug.Log (string.Format ("Closed:({0},{1})", X, Z));
			m_status = eStatus.Closed;
		}

		//パスを取得する
		public void GetPath(List<Point2> pList){
			pList.Add (new Point2 (X, Z));
			if (m_parent != null) {
				m_parent.GetPath(pList);
			}
		}

		public void Dump(){
			Debug.Log (string.Format ("({0},{1})[{2}] cost = {3} heuris = {4} score = {5}", X, Z, m_status, m_cost, m_heuristic, GetScore ()));
		}

		public void DumpRecursive(){
			Dump ();
			if (m_parent != null) {
				//再帰的にダンプする
				m_parent.DumpRecursive();
			}
		}
	}

	//A-starノード管理
	class ANodeMgr{

		//地形レイヤー
		Layer2D m_layer;
		//斜め移動を許可するかどうか
		bool m_allowdiag = true;
		//オープンリスト
		List<ANode> m_openList = null;
		//ノードインスタンス管理
		Dictionary<int,ANode> m_pool = null;

		//ゴール座標
		int m_xgoal = 0;
		int m_zgoal = 0;

		public ANodeMgr(Layer2D layer,int xgoal,int zgoal,bool allowdiag = true){
			m_layer = layer;
			m_allowdiag = allowdiag;
			m_openList = new List<ANode>();
			m_pool = new Dictionary<int,ANode>();
			m_xgoal = xgoal;
			m_zgoal = zgoal;
		}

		//ノード生成する
		public ANode GetNode(int x,int z){
			var idx = m_layer.Get(x, z);
			if (m_pool.ContainsKey (idx)) {
				//既に存在しているのでプーリングから取得
				return m_pool[idx];
			}

			//ないので新規作成
			var node = new ANode (x, z);
			m_pool [idx] = node;
			//ヒューリスティック・コストを計算する
			node.CalcHeuristic (m_allowdiag, m_xgoal, m_zgoal);
			return node;
		}

		//ノードをオープンリストに追加
		public void AddOpenList(ANode node){
			m_openList.Add (node);
		}

		//ノードをオープンリストから削除
		public void RemoveOpenList(ANode node){
			m_openList.Remove (node);
		}

		//指定の座標にあるノードをオープンする
		public ANode OpenNode(int x,int z,int cost,ANode parent){
			//座標をチェック
			if (m_layer.IsOutOfRange (x, z)) {
				//領域外
				return null;
			}
			if (m_layer.Get (x, z) > 1) {
				//通過出来ない
				return null;
			}
			//ノードを取得する
			var node = GetNode (x, z);
			if (node.IsNone () == false) {
				//既にOpenしているから何もしない
				return null;
			}

			//Openする
			node.Open (parent, cost);
			AddOpenList (node);

			return node;
		}

		//周りをOpenする
		public void OpenAround(ANode parent){
			var xbase = parent.X;  //基準座標X
			var zbase = parent.Z;  //基準座標Z
			var cost = parent.Cost;    //コスト
			cost += 1;       //一歩進むので+1する
			if (m_allowdiag) {
				//8方向を開く
				for (int j = 0; j < 3; j++) {
					for (int i = 0; i < 3; i++) {
						var x = xbase + i - 1;
						var z = zbase + j - 1;
						OpenNode (x, z, cost, parent);
					}
				}
			} else {
				//4方向を開く
				var x = xbase;
				var z = zbase;
				OpenNode(x-1,z,cost,parent);       //右
				OpenNode(x,z-1,cost,parent);       //上
				OpenNode(x+1,z,cost,parent);       //左
				OpenNode(x,z+1,cost,parent);       //下
			}
		}

		//最小スコアのノードを取得
		public ANode SearchMinScoreNodeFromOpenList(){

			//最小スコア
			int min = 9999;
			//最小実コスト
			int minCost = 9999;
			ANode minNode = null;
			foreach (ANode node in m_openList) {
				int score = node.GetScore();
				if(score > min){
					//スコアが大きい
					continue;
				}
				if(score == min && node.Cost >= minCost){
					//スコアが同じときは実コストも比較する
				continue;
				}

				//最小値更新
				min = score;
				minCost = node.Cost;
				minNode = node;
			}
			return minNode;
		}

	}

	//チップ上のX座標を取得
	float GetChipX(int i){
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0, 0));
		return i;
	}

	//チップ上のZ座標を取得
	float GetChipZ(int j){
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		return j;
	}

	//ランダムな座標を取得
	Point2 GetRandomPosition(Layer2D layer){
		Point2 p;
		while (true) {
			p.x = Random.Range(0, layer.Width);
			p.z = Random.Range(0,layer.Height);
			if(layer.Get (p.x,p.z) == 1){
				//通過可能
				break;
			}
		}
		return p;
	}

	//状態
	enum eState{

		Exec,     //実行中
		Walk,     //移動中
		End,      //終わり
	}

	eState m_state = eState.Exec;
	
	IEnumerator Start(){

		//地形データのロード
		var map = new MapNumericValue ();
		map.Create ("Levels/Stage02_Main");
		map.Create("Levels/Stage02_Ground");
		var layer = map.GetLayer (0);

		for (int j = 0; j < layer.Height; j++) {
			for(int i = 0; i < layer.Width; i++){
				var v = layer.Get (i,j);
				var x = GetChipX(i);
				var z = GetChipZ(j);
				FieldManager.Add(v,x,z);
			}
		}

		yield return new WaitForSeconds (0.1f);

		var pList = new List<Point2> ();
		Token enemy = null;

		//A-starの実行
		{
			//スタート地点
			Point2 pStart = GetRandomPosition(layer);
			enemy = Util.CreateToken(GetChipX(pStart.x),GetChipZ(pStart.z),"Enemy_a");
			//ゴール地点
			Point2 pGoal = GetRandomPosition(layer);
			var player = Util.CreateToken(GetChipX(pGoal.x),GetChipZ(pGoal.z),"Player");
			//斜め移動を許可
			var allowdiag = true;
			var mgr = new ANodeMgr (layer, pGoal.x, pGoal.z, allowdiag);

			//スタート地点のノード取得
			//スタート地点だからコストは0
			ANode node = mgr.OpenNode (pStart.x, pStart.z, 0, null);
			mgr.AddOpenList (node);

			//試行回数 1000回超えたら強制中断
			int cnt = 0;
			while (cnt < 1000) {
				mgr.RemoveOpenList (node);
				//周囲を開く
				mgr.OpenAround (node);
				//最小スコアのノードを探す
				node = mgr.SearchMinScoreNodeFromOpenList ();
				if (node == null) {
					Debug.Log ("Not found path.");
					break;
				}
				if (node.X == pGoal.x && node.Z == pGoal.z) {
					//プレイヤーにたどり着いた
					Debug.Log ("接触");
					mgr.RemoveOpenList (node);
					node.DumpRecursive ();
					//パスを取得
					node.GetPath (pList);
					//反転する
					pList.Reverse ();
					break;
				}

				yield return new WaitForSeconds (0.01f);
			}
		}

		m_state = eState.Walk;
		//Enemyを移動させる
		foreach (var p in pList) {
			var x = GetChipX(p.x);
			var z = GetChipZ(p.z);
			enemy.X = x;
			enemy.Z = z;
			yield return new WaitForSeconds (0.2f);
		}
		//終了
		m_state = eState.End;
	}
	
	/*
	private GameObject m_target;  //最も近いオブジェクト
	private float m_time = 0;     //経過時間

	void Start(){
		//最も近いオブジェクトを取得
		this.m_target = this.serchTag (gameObject, "Player");
	}

	void Update(){

		this.m_time += Time.deltaTime;   //経過時間を取得

		if (this.m_time >= 1.0f) {
			//最も近いオブジェクトを取得
			this.m_target = this.serchTag (gameObject, "Player");
			//経過時間を初期化
			this.m_time = 0;

			//移動のスピード
			this.changeDirection (this.m_target);  //向かう方向に向く
			this.transform.Translate (Vector3.forward * 2.00f);    //向かう方向に向かう
		}
	}

	//Playerの座標に応じて向きを変える
	void changeDirection(GameObject player){

		//PlayerとEnemyの座標差分を取得
		int m_xDistance = Mathf.RoundToInt (this.transform.position.x - player.transform.position.x);
		int m_zDistance = Mathf.RoundToInt (this.transform.position.z - player.transform.position.z);

		//向きたい角度
		int m_rotateDir = 0;

		//x座標とy座標の差分から向きたい角度を取得
		//PlayerとEnemyに距離がある場合
		if (m_xDistance == 0) {
			//x座標が同じ場合,z座標のみ向きを取得
			m_rotateDir = this.getDirection (m_zDistance, "z");
		} else if (m_zDistance == 0) {
			//z座標が同じ場合,x座標のみ向きを取得
			m_rotateDir = this.getDirection (m_xDistance, "x");
		} else {
			//どちらも差がある場合,ランダムで進む向きを取得
			int m_rand = UnityEngine.Random.Range (0, 2);
			if (m_rand == 0) {
				//z座標
				m_rotateDir = this.getDirection (m_zDistance, "z");
			} else {
				//x座標
				m_rotateDir = this.getDirection (m_xDistance, "x");
			}
		}

		//取得した方向にオブジェクトの向きを変える
		this.transform.rotation = Quaternion.Euler (0, m_rotateDir, 0);

	}

	//向きの角度を取得
	int getDirection(int distance,string axis){
		//距離がプラスかマイナスを取得
		int flag = distance > 0 ? 1 : 0;

		//角度を返す
		if (axis == "x") {
			return flag == 1 ? 270 : 90;
		} else {
			return flag == 1 ? 180 : 0;
		}
	}


	GameObject serchTag(GameObject nowObj,string tagName){

		float tmpDis = 0;     //距離用の一時変数
		float nearDis = 0;    //最も近いオブジェクトの距離
		GameObject targetObj = null;     //オブジェクト

		//タグ指定されたオブジェクトを配列で取得
		foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName)) {
			//Enemyと取得したオブジェクトの距離を取得
			tmpDis = Vector3.Distance (obs.transform.position, nowObj.transform.position);

			//オブジェクトの距離が近いか,距離0であればオブジェクト名を取得
			//一時変数に距離を格納
			if (nearDis == 0 || nearDis > tmpDis) {
				nearDis = tmpDis;
				targetObj = obs;
			}
		}

		//最も近いオブジェクトを返す
		return targetObj;
	}

	//当たり判定
	void OnTriggerEnter(Collider col){
		//Playerに当たった時
		if (col.gameObject.tag == "Player") {
			Debug.Log ("攻撃");
		}
	}


*/

}
