using UnityEngine;
using System.Collections;

public class gameSpaceManager : MonoBehaviour {
	
	public Transform brick;
	Transform[] cubes;
	
	private int[] ifHit;
	
	void Start() {
		cubes = new Transform[20];
		ifHit = new int[20];
		
		for (int i = 0; i < 20; i++) {
			cubes[i] = (Transform)Instantiate(brick, new Vector3(i - 10, 0, 0), Quaternion.identity);
			cubes[i].name = "cube" + i;
			ifHit[i] = 0;
		}
		
	}
	
	void Update() {
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Mouse is down");
			
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) 
			{
				Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				
				for (int i = 0; i < 20; i++){
					if (cubes[i].gameObject.name == hitInfo.transform.gameObject.name){
						cubes[i].position += Vector3.up * 1.0F;
						Debug.Log("Hit " + i);
					}
				}
			} else {
				Debug.Log("No hit");
			}
		}
	}
	
	void OnGUI() {
		
	}
}