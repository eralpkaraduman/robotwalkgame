using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour, ISelectableTarget {

	private GameObject selectionCircle;
	public bool selected = false;
	// Use this for initialization
	void Start () {
		selectionCircle = transform.Find("SelectionCircle").gameObject;
		selectionCircle.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void setSelected(bool selected){
		//selectionCircle.renderer.enabled = selected;
	}
	
	public Vector3 getPosition(){
		return transform.position;
	}
	
	public Transform getTransform(){
		return transform;
	}
}
