using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	
	static public Vector3 cameraPos;
	static public float camMaxSpeed = 1f;
	static public float camMinSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		//transform.Translate(0,0,-100);
		//transform.LookAt(motionScape.motionScapeCenter);
		//transform.Translate (0, 0, -1000f);
		//cameraPos = new Vector3 (0, 0, -1000f);

		transform.Translate (0, 0, -1000f);
		cameraPos = new Vector3 (0, 0, -1000f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Interface.zooming != Interface.oldZooming){
			transform.LookAt (Vector3.zero);
			transform.Translate (0, 0, -Interface.oldZooming);
			transform.Translate (0, 0, Interface.zooming);
			Interface.oldZooming = Interface.zooming;
		}
		
		if (Interface.camAutoState == 1) {
			if (transform.position.z < -200)
				transform.Translate (0, 0, camMaxSpeed);
			else if (transform.position.z >= -200 && transform.position.z < -100)
				transform.Translate (0, 0, camMaxSpeed * (-transform.position.z - 100) / 100 + camMinSpeed);
			else if (transform.position.z >= -100)
				Interface.camAutoState = -1;
		}
			
		
		cameraPos = transform.position;
		
		transform.LookAt(motionScape.motionScapeCenter);
	}
}
