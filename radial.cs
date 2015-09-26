using UnityEngine;
using System.Collections;

public class radial : MonoBehaviour {
	
	private ParticleSystem.Particle[] points;
	private Vector3[] restoredPosition;
	
	//motion properties
	private float[] rTheta;
	private float[] waveTheta;

	// Use this for initialization
	public void reset () {
		points = new ParticleSystem.Particle[Interface.pointAmount];
		restoredPosition = new Vector3[Interface.pointAmount];

		waveTheta = new float[Interface.pointAmount];
		rTheta = new float[Interface.pointAmount];
		
		//initializing positions
		for (int i = 0; i < Interface.pointAmount; i += Interface.trailPointAmount){
			Vector2 randomPos = Random.insideUnitCircle;
			float randomRadius = Random.Range (0.2f, Interface.radium);
			//points[i].position = new Vector3( randomPos.x * Interface.radium, randomPos.y * Interface.radium, Random.Range(-Interface.thickness, Interface.thickness));
			//rTheta[i] = Mathf.Atan2(randomPos.y, randomPos.x);
			rTheta[i] = Random.Range (0f, 2 * Mathf.PI);
			waveTheta[i] = Random.Range(0f, 6.28318531f);
			points[i].position = new Vector3( randomRadius * Mathf.Cos (rTheta[i]), randomRadius * Mathf.Sin (rTheta[i]), Random.Range(-Interface.thickness, Interface.thickness));
			//rTheta[i] = Mathf.Atan2(randomPos.y, randomPos.x);
			restoredPosition[i] = new Vector3(points[i].position.x, points[i].position.y, points[i].position.z);
		}
		
		particleSystem.SetParticles (points, points.Length);
	
	}
	
	// Update is called once per frame
	public void run () {
		Vector3 pos;
		
		particleSystem.SetParticles (points, points.Length);
		
		for (int i = 0; i < Interface.pointAmount; i+= Interface.trailPointAmount){
			float dist;
			float thresh = 0.2f;
			Vector3 center;
			int dir = 1;
			
			//pos = points[i].position;
			pos = restoredPosition[i];

			center = new Vector3 (0, 0, pos.z);
			
			dist = Vector3.Distance (pos, center);
			
			if (Interface.speed >= 0) dir = 1; 
			else if (Interface.speed < 0) dir = -1;

			pos.x += dir * Mathf.Abs(Interface.speed) * Mathf.Cos (rTheta[i]);
			pos.y += dir * Mathf.Abs(Interface.speed) * Mathf.Sin (rTheta[i]);



			
			//pos.z += Interface.waveAmp * Mathf.Sin( waveTheta[i] );

			//radial out
			if (dir == 1) {
				//Wavyness
				waveTheta[i] += Interface.waveSpeed;
				//pos.y += waveAmp * Mathf.Sin( waveTheta[i]);
				pos.x += Interface.waveAmp * Mathf.Sin( waveTheta[i] ) * Mathf.Cos( rTheta[i] + Mathf.PI/2 );
				pos.y += Interface.waveAmp * Mathf.Sin( waveTheta[i] ) * Mathf.Sin( rTheta[i] + Mathf.PI/2 );
				if ( dist >= Interface.radium ) {
					//thresh = 0.2f + dist - Interface.radium;
					//pos.x = thresh * Mathf.Cos( rTheta[i] );
					//pos.y = thresh * Mathf.Sin( rTheta[i] );
					dist = 0.2f;
					pos.x = dist * Mathf.Cos( rTheta[i] );
					pos.y = dist * Mathf.Sin( rTheta[i] );
				}
			}
			//sucking in
			else if (dir == -1) {
				//Wavyness
				waveTheta[i] += Interface.waveSpeed;
				//pos.y += waveAmp * Mathf.Sin( waveTheta[i]);

				if ( dist <= Mathf.Abs (Interface.speed) ) {
					//thresh = Interface.radium;
					//pos.x = thresh * Mathf.Cos( rTheta[i] );
					//pos.y = thresh * Mathf.Sin( rTheta[i] );
					dist = Interface.radium;
					pos.x = dist * Mathf.Cos( rTheta[i] );
					pos.y = dist * Mathf.Sin( rTheta[i] );
				}

			}

			restoredPosition[i] = pos;
			//points[i].position = pos;
			pos.x += Interface.waveAmp * Mathf.Sin( waveTheta[i] ) * Mathf.Cos( rTheta[i] + Mathf.PI/2 );
			pos.y += Interface.waveAmp * Mathf.Sin( waveTheta[i] ) * Mathf.Sin( rTheta[i] + Mathf.PI/2 );

			//points[i].position = pos;
			points[i].position = pos;
			points[i].size = Interface.size;
			points[i].color = new Color( Interface.blackness, Interface.blackness, Interface.blackness, Interface.opacity);
			
			//trail
			if (Interface.trailPointAmount > 1){
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
