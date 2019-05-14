using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharaController
{

    public float walkSpeed;
	public float runSpeed;
    public Rigidbody rb;

    PlayerController(){
		HP = 10;
		Attack = 1;
		walkSpeed = 0.01f;
		runSpeed = 0.03f;
    }

    void Start(){
        rb = GetComponent<Rigidbody>();     
    }

	void Update() {

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		bool hold = Input.GetKey(KeyCode.LeftShift);

		// 歩く。構え時には動かない(処理も回さない)。
		// TODO: AddForceはTranslateに切り替え
		if (!(horizontal == 0 && vertical == 0) && !hold) {
			float x = horizontal * walkSpeed;
			float z = vertical * walkSpeed;
			rb.AddForce(x, 0, z);
		}
		// 構える。"J"で射撃（未実装）。
		// カメラの視点を変化させる（MainCameraに実装？）。
		else if (hold) {

			if (Input.GetKey(KeyCode.J)) {
				Debug.Log("射撃");
			}
		} else {

		}

	}
}
