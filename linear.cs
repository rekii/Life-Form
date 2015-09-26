using UnityEngine;
using System.Collections;

public class linear : MonoBehaviour {
	
	private ParticleSystem.Particle[] points;
	
	//motion properties
	private float[] waveTheta;
	
	// Use this for initialization
	public void reset () {
		
		points = new ParticleSystem.Particle[Interface.pointAmount];
		
		waveTheta = new float[Interface.pointAmount];
		
		for (int i = 0; i < Interface.pointAmount; i += Interface.trailPointAmount){
			points[i].position = new Vector3( Random.Range(-Interface.scaleX, Interface.scaleX), Random.Range (-Interface.scaleY, Interface.scaleY), Random.Range(-Interface.scaleZ, Interface.scaleZ));
				
			waveTheta[i] = Random.Range(0f, 6.28318531f);
			
			//Initilize trail points
			for (int j = 0; j < Interface.trailPointAmount; j++){
				points[i + j].position = points[i].position;
				points[i + j].color = new Color(1f,1f,1f,0.1f);
				points[i + j].size = Interface.size;
			}
			//points[i].
		}
		
		
		particleSystem.SetParticles(points, points.Length);
		//particleSystem.transform.Rotate(Interface.rotationX, Interface.rotationY, Interface.rotationZ);

	}
	
	// Update is called once per frame
    public void run () {
		Vector3 pos;
		
		particleSystem.SetParticles(points, points.Length);
		
		if (Interface.oldRotationX != Interface.rotationX || Interface.oldRotationY != Interface.rotationY || Interface.oldRotationZ != Interface.rotationZ){
			//particleSystem.transform.Rotate(-Interface.oldRotationX, -Interface.oldRotationY, -Interface.oldRotationZ);
			particleSystem.transform.Rotate(Interface.rotationX-Interface.oldRotationX, Interface.rotationY-Interface.oldRotationY, Interface.rotationZ-Interface.oldRotationZ);
			Interface.oldRotationX = Interface.rotationX;
			
			Interface.oldRotationY = Interface.rotationY;
			Interface.oldRotationZ = Interface.rotationZ;
		}
		
		for (int i = 0; i < Interface.pointAmount; i+= Interface.trailPointAmount){ 
			pos = points[i].position;
			pos.z += Interface.speed; 
			
			points[i].size = Interface.size;
			points[i].color = new Color( Interface.blackness, Interface.blackness, Interface.blackness, Interface.opacity);
			
			
			//check boundaries
			if (pos.z > Interface.scaleZ) {
				pos.z = pos.z - 2*Interface.scaleZ;
			}
			
			else if (pos.z < -Interface.scaleZ) {
				pos.z = pos.z + 2*Interface.scaleZ;
			}
			
			if (pos.x > Interface.scaleX) {
				pos.x = pos.x - 2*Interface.scaleX;
			}
			
			else if (pos.x < -Interface.scaleX) {
				pos.x = pos.x + 2*Interface.scaleX;
			}
			
			if (pos.y > Interface.scaleY) {
				pos.y = pos.y - 2*Interface.scaleY;
			}
			
			else if (pos.y < -Interface.scaleY) {
				pos.y = pos.y + 2*Interface.scaleY;
			}
			
			
		
			//Wavy path curvature	
			if (Interface.responsive == 1) {
				if (Interface.pathCurvature == 1) {
					waveTheta[i] += Interface.waveSpeed;
					pos.y += Interface.waveAmp * Mathf.Sin( waveTheta[i]);
					points[i].color = new Color( Interface.blackness, Interface.blackness, Interface.blackness, Mathf.Sin( waveTheta[i]) * Interface.opacity);
				}
			}
			
			else if (Interface.responsive == -1) {
				if (Interface.pathCurvature == 1) {
					//particleSystem.transform.Rotate(-Interface.oldRotationX, -Interface.oldRotationY, -Interface.oldRotationZ);
					float farest = 50f;
					float blowUp = 6f;
					//float closeness = Vector2.Distance (new Vector2 (cameraController.cameraPos.x, cameraController.cameraPos.z), new Vector2 (pos.x, pos.z));
					float closeness = Vector3.Distance (cameraController.cameraPos, pos);
					if ( closeness <= farest) {
						waveTheta[i] += ((farest - closeness) / farest) * Interface.waveSpeed;
						pos.z -= Interface.speed;
						pos.z += (closeness / farest) * 0.5f * Interface.speed;
						pos.y += ((farest - closeness) / farest) * 5 * Interface.waveAmp * Mathf.Sin( waveTheta[i]);
						points[i].size = ((farest - closeness) / farest) * blowUp * Interface.size;
						points[i].color = new Color( Interface.blackness, Interface.blackness, Interface.blackness, (closeness / farest) * Interface.opacity);
						//points[i].color = new Color( Interface.blackness, Interface.blackness, Interface.blackness, Interface.opacity);
					}
					
					//particleSystem.transform.Rotate(Interface.oldRotationX, Interface.oldRotationY, Interface.oldRotationZ);
				}
			}
			
			points[i].position = pos;
			
			
			
			//green
			//points[i].color = new Color( 0f, Interface.blackness, 0f, Interface.opacity);
			//Update trail position 
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
