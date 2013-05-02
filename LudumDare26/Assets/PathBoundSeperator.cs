using UnityEngine;
using System.Collections;

public class PathBoundSeperator : MonoBehaviour, ISelectableTarget {

	// Use this for initialization
	void Start () {
	
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
