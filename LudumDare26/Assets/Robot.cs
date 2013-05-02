using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour {

	public float walkSpeed = 5;
	private float damping = 2.5f;
	public Transform target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		damping = walkSpeed/8;
		
		Transform _target = target;
		if(_target!=null){
			//transform.LookAt(_target);	
			var rotation = Quaternion.LookRotation(_target.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
			
		}
		
		//var _rotation = Quaternion.LookRotation(destination - transform.position);
		//transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.deltaTime * damping);
		//transform.rotation = _rotation;
		
		
		
		transform.Translate(Vector3.forward * Time.deltaTime*walkSpeed);
	}
}
