using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharaController
{

	/// <summary>
	/// 利用する際は、
	/// 1.CameraTransformにPlayerとMainCameraをアタッチしてください。
	/// 2.適度にパラメータを調整してください。
	/// </summary>

    public float runSpeed;				// 走る速度
	public float walkSpeed;				// 歩く速度
	public float jumpPower;				// ジャンプ力ぅ…ですかね…
	public float rotateRate;
	public Transform CameraTransform;   // 横の視点移動（MainCameraをアタッチ）


	PlayerController(){
		//仮入力しています
		Routine = CharaRoutine.CHARA_ROUTINE_MOVE;
		HP = 10;
		Attack = 1;
		runSpeed = 4.0f;
		walkSpeed = 1.0f;
    }

    void Start(){     
    }

	override protected void Update() {
		base.Update();
	}

	//移動
	public override void move() {

		// キー入力用変数
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");
		bool hold = Input.GetKey(KeyCode.LeftShift);
		bool shot = Input.GetKey(KeyCode.J);
		bool jump = Input.GetKey(KeyCode.Space);

		// 進行方向制御用変数：
		float angleDir = CameraTransform.transform.eulerAngles.y * (Mathf.PI / 180.0f);     // ワールド座標に対するオイラー角の取得
		Vector3 dirVertical = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));     // カメラ正面方向をベクトルで取得
		Vector3 dirHorizontal = new Vector3(Mathf.Cos(angleDir), 0, -Mathf.Sin(angleDir));  // カメラ右正面方向をベクトルで取得


		
		// 走る or 歩く：カメラの向いている方向に移動
		if (!hold) {
			this.transform.position += dirVertical * vertical * runSpeed * Time.deltaTime;
			this.transform.position += dirHorizontal * horizontal * runSpeed * Time.deltaTime;
		} else {
			this.transform.position += dirVertical * vertical * walkSpeed * Time.deltaTime;
			this.transform.position += dirHorizontal * horizontal * walkSpeed * Time.deltaTime;

			if (Input.GetKey(KeyCode.J)) {
				//Routine = CharaRoutine.CHARA_ROUTINE_SHOT;
				Debug.Log("射撃");
			}
		}

		// Playerの回転制御：カメラの向いている方向に回転
		if (false) {
		}
	}


	//Routineで取ると移動不能となるため、一旦shot部はコメントアウト
	/*public override void shot() {
		//動作確認用出力LOG
		Debug.Log("射撃");

		if (!(Input.GetKey(KeyCode.J))) {
			Routine = CharaRoutine.CHARA_ROUTINE_MOVE;
		}
	}*/

}