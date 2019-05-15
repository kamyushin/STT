using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
	public Camera mainCamera;
	private Vector2 rotationSpeed = new Vector2(0.1f,0.2f);
	private Vector2 lastMousePosition;
	private Vector2 currentAngle = new Vector2(0, 0);
	private Vector3 offset;

	void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
		// カメラの視点移動：LeftShift
		transform.position = player.transform.position + offset;
		if (Input.GetKey(KeyCode.LeftShift)) {
			transform.position = player.transform.position;
		}

		// ドラッグによる画面移動
		if (Input.GetMouseButtonDown(0)) {
			//カメラの角度を変数に格納
			currentAngle = mainCamera.transform.localEulerAngles;
			lastMousePosition = Input.mousePosition;
		}
		// 左ドラッグしている間
		else if(Input.GetMouseButton(0)){ 
			// Y軸の回転：マウスドラッグ方向に視点回転
			currentAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * rotationSpeed.y;

			// x軸の回転：マウスドラッグ方向に視点回転
			currentAngle.x -= (lastMousePosition.y - Input.mousePosition.y) * rotationSpeed.x;

			// currentAngleの角度をカメラ角度に格納
			mainCamera.transform.localEulerAngles = currentAngle;
			// マウス座標を変数lastMousePositionに格納
			lastMousePosition = Input.mousePosition;
		}
	}
}
