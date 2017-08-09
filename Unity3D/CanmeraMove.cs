using UnityEngine;
using System.Collections;

public class CanmeraMove : MonoBehaviour
{
	// 按键移动速度
	public float moveSpeed = 70;
	// 鼠标滚动速度
	public float mouseMoveSpeed = 360;

	public KeyCode forwardKey = KeyCode.W;
	public KeyCode backwardKey = KeyCode.S;
	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;


	// 是否用鼠标右键旋转相机
	public bool isRoteForRightMouseButton = true;
	//绕X轴旋转的最小度数限制
	public float xAxisRotateMin = -80f;
	//绕X轴旋转的最大度数限制
	public float xAxisRotateMax = 80f;
	//绕X轴旋转的速度
	public float xRotateSpeed = 3f;
	//绕Y轴旋转的速度
	public float yRotateSpeed = 5f;



	// Update is called once per frame
	void Update ()
	{
		moveForKeyboard ();
		moveForMouse ();

		roateForMouse ();

		// Zoom
		// zoom();
	}

	private void moveForMouse ()
	{
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			forward (mouseMoveSpeed);
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			backward (mouseMoveSpeed);
		}
	}

	private void moveForKeyboard ()
	{
		if (Input.GetKey (forwardKey)) {
			forward (moveSpeed);
		}
		if (Input.GetKey (backwardKey)) {
			backward (moveSpeed);
		}
		if (Input.GetKey (leftKey)) {
			left (moveSpeed);
		}
		if (Input.GetKey (rightKey)) {
			right (moveSpeed);
		}
	}

	//摄像机移动
	public void forward (float speed)
	{
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}

	public void backward (float speed)
	{
		transform.Translate (Vector3.forward * Time.deltaTime * -speed);
	}

	public void left (float speed)
	{
		transform.Translate (Vector3.right * Time.deltaTime * -speed);
	}

	public void right (float speed)
	{
		transform.Translate (Vector3.right * Time.deltaTime * speed);
	}


	private void zoom ()
	{
		//Zoom in  
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {  
			zoomIn ();
		}
		//Zoom out  
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			zoomOut ();
		}
	}

	public void zoomOut ()
	{

		if (Camera.main.fieldOfView <= 100)
			Camera.main.fieldOfView += 2;  
		if (Camera.main.orthographicSize <= 20)
			Camera.main.orthographicSize += 0.5F;  
	}

	public void zoomIn ()
	{
		if (Camera.main.fieldOfView > 2)
			Camera.main.fieldOfView -= 2;  
		if (Camera.main.orthographicSize >= 1)
			Camera.main.orthographicSize -= 0.5F;  
	}



	private void roateForMouse ()
	{
		if (isRoteForRightMouseButton) {
			if (Input.GetMouseButton (1)) {
				rotate ();
			} 
		} else {
			if (Input.GetMouseButton (0)) {
				rotate ();
			}
		}
	}

	float yRotateAngle;
	float xRotateAngle;
	Vector3 vecRotateAngle = new Vector3 ();

	public void rotate ()
	{
		yRotateAngle += Input.GetAxis ("Mouse X") * yRotateSpeed;
		xRotateAngle -= Input.GetAxis ("Mouse Y") * xRotateSpeed;
		if (xRotateAngle < xAxisRotateMin) {
			xRotateAngle = xAxisRotateMin;
		}
		if (xRotateAngle > xAxisRotateMax) {
			xRotateAngle = xAxisRotateMax;
		}

		vecRotateAngle.x = xRotateAngle;
		vecRotateAngle.y = yRotateAngle;
		vecRotateAngle.z = 0;
			
			
		//设置绕Z轴旋转为0，保证了垂直方向的不倾斜
		transform.rotation = Quaternion.Euler (vecRotateAngle);
	}
		
}
