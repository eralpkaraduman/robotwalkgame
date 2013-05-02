using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	private Vector3 initCameraPos;
	private Vector3 initTargetPos;
	public Transform target;
	// Use this for initialization
	void Start () {
		initCameraPos = transform.position;
		initTargetPos = target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		transform.position.Set(
			target.transform.position.x,
			initCameraPos.y,
			//target.transform.position.z - (initTargetPos.z - initCameraPos.z)
			target.transform.position.z
		);
		*/
		Vector3 _target = initCameraPos;
		_target.x = target.transform.position.x;
		//_target.
		_target.z = target.transform.position.z - (initTargetPos.z - initCameraPos.z);
		
		
		transform.position = _target;
		/*
		transform.position.Set(
			target.transform.position.x,
			target.transform.position.y,
			//target.transform.position.z - (initTargetPos.z - initCameraPos.z)
			target.transform.position.z
		);
		*/
	}
}
