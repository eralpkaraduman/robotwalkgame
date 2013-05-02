using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathSpawner : MonoBehaviour {
	
	public float distanceFromRobot = 525;
	public float offsetBehindRobot = 100;
	private float totalZgenerated = 0;
	public float minRoadWidth = 374;
	private float generationStartZ;
	private float generationStartX;
	private float generationStartY;
	public float boundPostInterval = 100;
	public Robot robot;
	public PathBoundSeperator pathBoundSeperator;
	private const int boundPostLimit = 50;
	private List<PathBoundSeperator> boundPostList = new List<PathBoundSeperator>();
	
	void Start () {
		Vector3 _v = robot.transform.position;
		_v.z += distanceFromRobot;
		transform.position = _v;
		
		generationStartZ = robot.transform.position.z - offsetBehindRobot;
		generationStartY = robot.transform.position.y;
		generationStartX = robot.transform.position.x;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//generation
		while(totalZgenerated<(transform.position.z - generationStartZ)){
			
			float roadWidth = minRoadWidth;
			
			// left
			PathBoundSeperator post = (PathBoundSeperator)Instantiate(pathBoundSeperator);
			boundPostList.Add(post);
			
			post.transform.position = new Vector3(
				generationStartX - roadWidth/2,
				generationStartY,
				generationStartZ + totalZgenerated
			);
			
			//right
			PathBoundSeperator post2 = (PathBoundSeperator)Instantiate(pathBoundSeperator);
			boundPostList.Add(post2);
			
			post2.transform.position = new Vector3(
				generationStartX + roadWidth/2,
				generationStartY,
				generationStartZ + totalZgenerated
			);
			
			// iterate;
			totalZgenerated += boundPostInterval;
		}
		
		// bezier here
		transform.position = new Vector3(
			transform.position.x,
			transform.position.y,
			robot.transform.position.z+distanceFromRobot
		);
		
		
		//collect
		for(int i=0; i<boundPostList.Count; i++){
			PathBoundSeperator _post = boundPostList[i];
			if(_post.transform.position.z<=robot.transform.position.z-offsetBehindRobot){
				boundPostList.RemoveAt(i);
				Destroy(_post.gameObject);
			}
		}
		
		/*
		List<PathBoundSeperator> _list = new List<PathBoundSeperator>();
		foreach (PathBoundSeperator oldBoundPost in boundPostList)
		{
			
			if(oldBoundPost.transform.position.z<=robot.transform.position.z-offsetBehindRobot){
				//_list.Remove(oldBoundPost);
				//Destroy(oldBoundPost);
				
				_list.Add(oldBoundPost);
			}
		}
		*/
		
		/*
		for(int i=0; i<_list.Count; i++){
			
			//Destroy(_list[i]);
			//print("destroyed "+_list[i]);
			//boundPostList.Remove(_list[i]);
			
			_list[i].transform.position =  new Vector3(
				_list[i].transform.position.x,
				50,
				_list[i].transform.position.z
			);
			
			
		}
		*/
	}
}
