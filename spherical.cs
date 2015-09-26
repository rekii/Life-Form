using UnityEngine;
using System.Collections;

public class spherical : MonoBehaviour {
	
	private ParticleSystem.Particle[] points;
	private Vector3[] restoredPosition;
	//motion properties
	private float[] sThetaHorizontal;
	private float[] sThetaVertical;
	private float[] waveTheta;

	// Use this for initialization
	public void reset () {
		points = new ParticleSystem.Particle[Interface.pointAmount];
		restoredPosition = new Vector3[Interface.pointAmount];

		waveTheta = new float[Interface.pointAmount];
		sThetaHorizontal = new float[Interface.pointAmount];
		sThetaVertical = new float[Interface.pointAmount];
		//rTheta = new float[Interface.pointAmount];
		
		for (int i = 0; i < Interface.pointAmount; i += Interface.trailPointAmount){
			float r = Random.Range(0.2f, Interface.radium);
			//points[i].position =  Random.insideUnitSphere * Interface.radium;
			
			sThetaHorizontal[i] = Random.Range(0f, 2 * Mathf.PI);
			sThetaVertical[i] = Random.Range(0f, 2 * Mathf.PI);
			
			
			points[i].position = new Vector3(r * Mathf.Cos (sThetaVertical[i]) * Mathf.Cos (sThetaHorizontal[i]), r * Mathf.Sin (sThetaVertical[i]), r * Mathf.Cos (sThetaVertical[i]) * Mathf.Sin (sThetaHorizontal[i]));
			restoredPosition[i] = new Vector3(points[i].position.x, points[i].position.y, points[i].position.z);
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
			Vector3 center;
			int dir = 1;
			
			//Determine direction
			if (Interface.speed >= 0) dir = 1;
			else if (Interface.speed <0) dir = -1;
			
			
			//pos = points[i].position;
			pos = restoredPosition[i];
			
			pos.x += Interface.speed * Mathf.Cos (sThetaVertical[i]) * Mathf.Cos (sThetaHorizontal[i]);
			pos.y += Interface.speed * Mathf.Sin (sThetaVertical[i]);
			pos.z += Interface.speed * Mathf.Cos (sThetaVertical[i]) * Mathf.Sin (sThetaHorizontal[i]);
			
			dist = Vector3.Distance (pos, new Vector3 (0, 0, 0));
			 
			//Boundary detection
			//Radial out
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
			restoredPosition[i] = pos;
			//Wavyness
			waveTheta[i] += Interface.waveSpeed;
			
			//pos.y += waveAmp * Mathf.Sin( waveTheta[i]);
			pos.x += Interface.waveAmp * Mathf.Sin( waveTheta[i] ) * Mathf.Cos( sThetaHorizontal[i] + Mathf.PI/2 );
			pos.y += Interface.waveAmp * Mathf.Sin( waveTheta[i] ) * Mathf.Sin( sThetaHorizontal[i] + Mathf.PI/2 );

			 
			points[i].size = Interface.size;
			points[i].position = pos;
			
			if (dist <= 50f) points[i].color = new Color ( Interface.blackness, Interface.blackness, Interface.blackness, (dist / 50f) * Interface.opacity);
			else points[i].color = new Color ( Interface.blackness, Interface.blackness, Interface.blackness, Interface.opacity);
			
			//green
			//points[i].color = new Color( 0f, Interface.blackness, 0f, Interface.opacity);
			//Update trail position 
			if (Interface.trailPointAmount > 1){
				//Make the first particle invisible
				points[i].color = new Color( Interface.blackness, Interface.blackness, Interface.blackness, 0);
				for (int j = Interface.trailPointAmount - 1; j > 0; j --){  
					//yield break;
					
					points[i + j].position = points[i + j - 1].position;
					float trailDist = Vector3.Distance (points[i + j].position, new Vector3 (0, 0, 0));
					points[i + j].size = Interface.size;
					if (trailDist <= 50f) points[i + j].color = new Color( Interface.blackness, Interface.blackness, Interface.blackness, (trailDist / 50f) * (Interface.opacity - Interface.opacity * j / (Interface.trailPointAmount - 1)));
					else points[i + j].color = new Color( Interface.blackness, Interface.blackness, Interface.blackness, Interface.opacity - Interface.opacity * j / (Interface.trailPointAmount - 1));
				}
			}
		}
	
	}
}
