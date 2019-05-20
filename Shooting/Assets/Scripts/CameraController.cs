using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

	public Camera mainCamera;
	private Vector2 lastMousePosition;
	private Vector3 initAngle;
	private Vector3 rotationSpeed;
	private Vector3 currentAngle;
	private Vector3 currentBase;
	private Vector3 offset;

	void Start()
    {
		offset = transform.position - player.transform.position;
		rotationSpeed = new Vector3(0.1f, 0.2f, 0.1f);
	}

    void LateUpdate()
    {
		// カメラの視点移動：LeftShift
		if (Input.GetKey(KeyCode.LeftShift)) {
			transform.position = player.transform.position;
		} else {
			transform.position = player.transform.position + offset;
		}

		// 右ドラッグにより視点移動：カメラ軸（ボタン離したら正面に戻す）
		if (Input.GetMouseButtonDown(0)) {
			// 初期のカメラ角度を格納（カメラリセット用）
			initAngle = mainCamera.transform.localEulerAngles;
			// カメラの角度を変数に格納
			currentAngle = mainCamera.transform.localEulerAngles;
			lastMousePosition = Input.mousePosition;
		}
		// 右ドラッグしている間
		else if(Input.GetMouseButton(0)) { 
			// Y軸の回転：マウスドラッグ方向に視点回転
			currentAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * rotationSpeed.y;
			// x軸の回転：マウスドラッグ方向に視点回転
			currentAngle.x -= (lastMousePosition.y - Input.mousePosition.y) * rotationSpeed.x;
			// z軸は初期0のまま

			// currentAngleの角度をカメラ角度に格納
			mainCamera.transform.localEulerAngles = currentAngle;
			// マウス座標を変数lastMousePositionに格納
			lastMousePosition = Input.mousePosition;
		}else if (Input.GetMouseButtonUp(0)) {
			// 初期のカメラ角度に戻す
			//mainCamera.transform.localEulerAngles = initAngle;
		}
	}
}
