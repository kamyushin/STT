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

    public float runSpeed;				// 移動速度
	public float walkSpeed;				// 歩・走の速度比の調整
	public float jumpPower;				// ジャンプ力ぅ…ですかね…
	public Transform CameraTransform;	// 横の視点移動（MainCameraをアタッチ）
	public float rotateRate;

    PlayerController(){
		HP = 10;
		Attack = 1;
		runSpeed = 4.0f;
		walkSpeed = 1.0f;
    }

    void Start(){     
    }

	void Update() {

		// キー入力用変数
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");
		bool hold = Input.GetKey(KeyCode.LeftShift);
		bool shot = Input.GetKey(KeyCode.J);
		bool jump = Input.GetKey(KeyCode.Space);
		
		// 進行方向制御用変数
		float angleDir = CameraTransform.transform.eulerAngles.y * (Mathf.PI / 180.0f);		// ワールド座標に対するオイラー角
		Vector3 dirVertical = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));		// カメラ正面方向をベクトルで取得
		Vector3 dirHorizontal = new Vector3(Mathf.Cos(angleDir), 0, -Mathf.Sin(angleDir));  // カメラ右正面方向をベクトルで取得

		// Playerの回転制御

		
		// 走る：デフォルト
		if (!hold) {
			this.transform.position += dirVertical * vertical * runSpeed * Time.deltaTime;
			this.transform.position += dirHorizontal * horizontal * runSpeed * Time.deltaTime;
		}

		// 構える・歩く。"J"で射撃（未実装）。
		if (hold) {
			this.transform.position += dirVertical * vertical * walkSpeed * Time.deltaTime;
			this.transform.position += dirHorizontal * horizontal * walkSpeed * Time.deltaTime;

			if (Input.GetKey(KeyCode.J)) {
				Debug.Log("射撃");
			}
		}
	}
}
