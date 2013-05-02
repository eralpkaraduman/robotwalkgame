using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserInteraction : MonoBehaviour {
	
	private ISelectableTarget selection1;
	private ISelectableTarget selection2;
	public LineRenderer lineRenderer1;
	public LineRenderer lineRenderer2;
	public LineRenderer betweenLineRenderer;
	private LineRenderer lineRendererToPlayer;
	private float drawCircleOffset = 0;
	public float circleTurnSpeed = 4;
	public float circleDiameter = 5;
	public float circleNumberOfLines = 8;
	public Robot robot;
	public GameObject centerOfSelections;
	private const float center_move_easing = 30.0f;
	
	// Use this for initialization
	void Start () {
		
		lineRenderer1.SetVertexCount(20);
		lineRenderer2.SetVertexCount(20);
		lineRendererToPlayer = gameObject.GetComponent<LineRenderer>();
		lineRendererToPlayer.SetVertexCount(2);
	}
	
	// Update is called once per frame
	void Update () {
		
		// select targets
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast(ray,out hit,1000)){
				if(hit.collider.tag == "selectableTarget"){
					//ISelectableTarget selectableTarget = (ISelectableTarget)GetComponent(typeof(ISelectableTarget));
					
					ISelectableTarget selectableTarget = hit.collider.gameObject.GetComponent(typeof(ISelectableTarget)) as ISelectableTarget;
					/*
					ISelectableTarget selectableTarget = (ISelectableTarget)hit.collider.gameObject.GetComponent<ISelectableTarget>();
					gameObject.getco
					*/
					addOrRemoveTarget(selectableTarget);
				}
			}
		}
		
		//draw circle 1
		if(selection1!=null){
			for(int i=1; i<=circleNumberOfLines; i++){
				Vector3 pos = selection1.getPosition();
				pos.x = (pos.x + Mathf.Sin(((360/circleNumberOfLines)*i*Mathf.Deg2Rad) + drawCircleOffset )*circleDiameter);
				pos.z = (pos.z + Mathf.Cos(((360/circleNumberOfLines)*i*Mathf.Deg2Rad) + drawCircleOffset )*circleDiameter);
				lineRenderer1.SetPosition(i-1,pos);
			}
			lineRenderer1.enabled = true;
		}else{
			lineRenderer1.enabled = false;	
		}
		
		//draw circle 2
		if(selection2!=null){
			for(int i=1; i<=circleNumberOfLines; i++){
				Vector3 pos = selection2.getPosition();
				pos.x = (pos.x + Mathf.Sin(((360/circleNumberOfLines)*i*Mathf.Deg2Rad) - drawCircleOffset )*circleDiameter);
				pos.z = (pos.z + Mathf.Cos(((360/circleNumberOfLines)*i*Mathf.Deg2Rad) - drawCircleOffset )*circleDiameter);
				lineRenderer2.SetPosition(i-1,pos);
			}
			lineRenderer2.enabled = true;
		}else{
			lineRenderer2.enabled = false;	
		}
		
		//draw line between and to player
		if(selection1!=null && selection2!=null){
			lineRendererToPlayer.SetPosition(0,selection1.getPosition());
			
			Vector3[] vectors = new Vector3[2];
			vectors[0] = selection1.getPosition();
			vectors[1] = selection2.getPosition();
			Vector3 center = CenterOfVectors(vectors);
			
			lineRendererToPlayer.SetPosition(0,robot.transform.position);
			lineRendererToPlayer.SetPosition(1,center);
			
			betweenLineRenderer.SetPosition(0,selection1.getPosition());
			betweenLineRenderer.SetPosition(1,selection2.getPosition());
			
			// if reached target, clear selections
			if(center.z <= robot.transform.position.z){
				selection1 = selection2 = null;
			}else{
				//centerOfSelections.transform.position = center;
				// smooth
				centerOfSelections.transform.position += (center-centerOfSelections.transform.position)/center_move_easing;
			}
			
			centerOfSelections.renderer.enabled = false;
			betweenLineRenderer.renderer.enabled = true;
			lineRendererToPlayer.enabled = true;
		}else{
			
			Vector3 _tempPos = robot.transform.position;
			_tempPos.z += 30;
			//centerOfSelections.transform.position = _tempPos;
			
			//smooth
			centerOfSelections.transform.position += (_tempPos-centerOfSelections.transform.position)/center_move_easing;
			
			centerOfSelections.renderer.enabled = false;
			betweenLineRenderer.renderer.enabled = false;
			lineRendererToPlayer.enabled = false;
		}
		
		// animate circle
		drawCircleOffset += Time.deltaTime*circleTurnSpeed;
	}
	
	void addOrRemoveTarget(ISelectableTarget target){
		// if has 2 selected and selecting an new one, clear old selections
		if(target!=null && selection1!=null && selection2!=null){
			selection1 = selection2 = null;
		}
		
		// if has no first selection select it if it isnt selected as second
		if(selection1 == null){
			if(selection2!=target){
				selection1 = target;
				return;
			}
			
		}else if(selection1 == target){
			selection1 = null;
			return;
		}
		
		// if has no second selection select it if it isnt selected as first
		if(selection2 == null){
			if(selection1!=target){
				selection2 = target;
				return;
			}			
		}else if(selection2 == target){
			selection2 = null;
			return;
		}
		
	}
	/*
	void selectObject(ISelectableTarget target, int slot){
		if(slot == 1){
			foreach (ISelectableTarget targ in selectedHistory1)
			{
			    targ.setSelected(false);
			}
			selectedHistory1.Clear();
			if(target!=null){
				target.setSelected(true);
				selectedHistory1.Add(target);
			}
		}else if(slot == 2){
			foreach (ISelectableTarget targ in selectedHistory2)
			{
			    targ.setSelected(false);
			}
			selectedHistory2.Clear();
			if(target!=null){
				target.setSelected(true);
				selectedHistory2.Add(target);
			}
		}
	}
	*/
	/*
	void deSelectObject(ISelectableTarget target, int slot){ 
		if(slot == 1){
		
		}else if(slot == 2){
		
		}
	}
	*/
	public Vector3 CenterOfVectors( Vector3[] vectors ) // jason 0531 782 61 01
	{
	    Vector3 sum = Vector3.zero;
	    if( vectors == null || vectors.Length == 0 )
	    {
	        return sum;
	    }
	 
	    foreach( Vector3 vec in vectors )
	    {
	        sum += vec;
	    }
	    return sum/vectors.Length;
	}
}
