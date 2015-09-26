using UnityEngine;
using System.Collections;

public class circular : MonoBehaviour {
	
	public ParticleSystem.Particle[] points;
	
	//motion properties
	private float[] sThetaHorizontal;
	private float[] sThetaVertical;
	private float[] waveTheta;
	private float[] rPoints;

	// Use this for initialization
	public void reset () {
		points = new ParticleSystem.Particle[Interface.pointAmount];
		
		waveTheta = new float[Interface.pointAmount];
		sThetaHorizontal = new float[Interface.pointAmount];
		sThetaVertical = new float[Interface.pointAmount];
		rPoints = new float[Interface.pointAmount];
		//rTheta = new float[Interface.pointAmount];
		
		for (int i = 0; i < Interface.pointAmount; i += Interface.trailPointAmount){
			float r = Random.Range(0.2f, Interface.radium);
			points[i].position =  Random.onUnitSphere * Interface.radium;
			
			sThetaHorizontal[i] = Random.Range(0f, 2 * Mathf.PI);
			sThetaVertical[i] = Random.Range(0f, 2 * Mathf.PI);
			
			//points[i].position = new Vector3(r * Mathf.Cos (sThetaVertical[i]) * Mathf.Cos (sThetaHorizontal[i]), r * Mathf.Sin (sThetaVertical[i]), r * Mathf.Cos (sThetaVertical[i]) * Mathf.Sin (sThetaHorizontal[i]));
			
			rPoints[i] = Vector2.Distance (new Vector2 (points[i].position.x, points[i].position.z), new Vector2 (0, 0));
			//sThetaHorizontal[i] = Mathf.Asin (points[i].position.y / Vector3.Distance (points[i].position, new Vector3 (0f, 0f, 0f)));
			
			//sThetaVertical[i] = Mathf.Atan2 (points[i].position.z, points[i].position.x);
			waveTheta[i] = Random.Range(0f, 6.28318531f);



			
			//Initilize trail points
			for (int j = 0; j < Interface.trailPointAmount; j++){
				points[i + j].position = points[i].position;
				points[i + j].color = new Color(Interface.blackness, Interface.blackness, Interface.blackness, Interface.opacity); 
				points[i + j].size = Interface.size;
			}
			//points[i].
		}
		
		particleSystem.SetParticles(points, points.Length);
	
	}
	
	// Update is called once per frame
	public void run () {
		
		particleSystem.SetParticles(points, points.Length);
		
		for (int i = 0; i < Interface.pointAmount; i+= Interface.trailPointAmount){ 
			Vector3 pos;
			float dist;
			float thresh = 0.2f;
			int dir = 1;
			float theta;
			float dSpeed;
			float rotationAngle;
			
			//Determine direction
			if (Interface.rotateSpeed >= 0) dir = 1;
			else if (Interface.speed <0) dir = -1;
			
			
			pos = points[i].position;
			
			//dist = Vector3.Distance (pos, new Vector3 (0, pos.y, 0));
			
			theta = Mathf.Atan2 (pos.z, pos.x);
			
			dSpeed = rPoints[i] * (Mathf.Cos (Mathf.PI / 2 - Interface.rotateSpeed / 2));
			
			rotationAngle = theta + Mathf.PI / 2 + Interface.rotateSpeed / 2;
			
			pos.x += dSpeed * Mathf.Cos (rotationAngle);
			
			pos.z += dSpeed * Mathf.Sin (rotationAngle);
			
			
			 
			//Boundary detection
			//Rotate counter clockwise
			/*
			if (dir == 1) {
				if (dist >= Interface.radium) {
					dist = Interface.speed;
					sThetaHorizontal[i] = Random.Range(0f, 2 * Mathf.PI);
					sThetaVertical[i] = Random.Range(0f, 2 * Mathf.PI);
					pos = new Vector3(dist * Mathf.Cos (sThetaVertical[i]) * Mathf.Cos (sThetaHorizontal[i]), dist * Mathf.Sin (sThetaVertical[i]), dist * Mathf.Cos (sThetaVertical[i]) * Mathf.Sin (sThetaHorizontal[i]));
			
				}
			}
			
			else if (dir == -1) {
				if (dist < Mathf.Abs (Interface.speed)) {
					dist = Interface.radium;
					sThetaHorizontal[i] = Random.Range(0f, 2 * Mathf.PI);
					sThetaVertical[i] = Random.Range(0f, 2 * Mathf.PI);
					pos = new Vector3(dist * Mathf.Cos (sThetaVertical[i]) * Mathf.Cos (sThetaHorizontal[i]), dist * Mathf.Sin (sThetaVertical[i]), dist * Mathf.Cos (sThetaVertical[i]) * Mathf.Sin (sThetaHorizontal[i]));
			
				}
			}
			*/

			//Wavyness
			waveTheta[i] += Interface.waveSpeed;
			
			//pos.y += waveAmp * Mathf.Sin( waveTheta[i]);
			pos.x += Interface.waveAmp * Mathf.Sin( waveTheta[i] ) * Mathf.Cos( sThetaHorizontal[i] + Mathf.PI/2 );
			pos.y += Interface.waveAmp * Mathf.Sin( waveTheta[i] ) * Mathf.Sin( sThetaHorizontal[i] + Mathf.PI/2 );

			 
			points[i].size = Interface.size;
			points[i].position = pos;
			
			points[i].color = new Color ( Interface.blackness, Interface.blackness, Interface.blackness, Interface.opacity);
			
			//green
			//points[i].color = new Color( 0f, Interface.blackness, 0f, Interface.opacity);
			//Update trail position 
			if (Interface.trailPointAmount > 1){
				//Make the first particle invisible
				points[i].color = new Color( Interface.blackness, Interface.blackness, Interface.blackness, 0);
				for (int j = Interface.trailPointAmount - 1; j > 0; j --){  
					//yield break;
					points[i + j].position = points[i + j - 1].position;
					//if (j % 8 == 0 && j >= 1) points[i + j].size = 3 * Interface.size;
					points[i + j].size = Interface.size;
					points[i + j].color = new Color( Interface.blackness, Interface.blackness, Interface.blackness, Interface.opacity - Interface.opacity * j / (Interface.trailPointAmount - 1));
					//green
					//points[i + j].color = new Color( 0f, Interface.blackness, 0f, Interface.opacity);
				}
			}
		}
	
	}
}
