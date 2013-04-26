using UnityEngine;
using System.Collections;

public class CameraView : MonoBehaviour {
	
	public Transform target;
	public float distance = 10f;
	
	// Use this for initialization
	void Start () {
		setNewPos();
	}
	
	// Update is called once per frame
	void Update () {		
		setNewPos();
		
		transform.LookAt(target);
	}
	
	void setNewPos () {
		Vector3 pos = target.transform.position;
		Vector3 camPos = new Vector3(pos.x, pos.y + distance*0.05f, pos.z + distance*0.15f);
		transform.position = camPos;
	}
}
