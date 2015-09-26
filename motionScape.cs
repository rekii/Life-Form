using UnityEngine;
using System.Collections;

public class motionScape : MonoBehaviour {
	
	public int particleAmount = 1000;
	
	public float xScale = 50f;
	public float yScale = 50f;
	public float zScale = 50f;
	
	static public Vector3 motionScapeCenter;
		
	//private ParticleSystem.Particle[] points;
	
	private linear linearMotion;
	private radial radialMotion;
	private spherical sphericalMotion;
	private circular circularMotion;

	// Use this for initialization
	void Start () {
		
		linearMotion = GetComponent<linear>();
		radialMotion = GetComponent<radial>();
		sphericalMotion = GetComponent<spherical>();
		circularMotion = GetComponent<circular>();
		
		motionScapeCenter = new Vector3 (Interface.translateX, Interface.translateY, Interface.translateZ); 
		
		switch (Interface.selMotionInt) {
			case 0:
				linearMotion.reset ();
				break;
			case 1:
				radialMotion.reset ();
				break;
			case 2:
				sphericalMotion.reset ();
				break;
			case 3:
				circularMotion.reset ();
				break;
		}
	}
		
	
	// Update is called once per frame
	void Update () {
		if (Interface.presetId != Interface.oldPresetId){
			//Interface.rotationX = 0f;
			//Interface.rotationY = 0f;
			//Interface.rotationZ = 0f;
			switchPreset ();

			//translate particle system
			if (Interface.oldTranslateX != Interface.translateX || Interface.oldTranslateY != Interface.translateY || Interface.oldTranslateZ != Interface.translateZ){
				//particleSystem.transform.Rotate(-Interface.oldRotationX, -Interface.oldRotationY, -Interface.oldRotationZ);
				//particleSystem.transform.Rotate(Interface.rotationX-Interface.oldRotationX, Interface.rotationY-Interface.oldRotationY, Interface.rotationZ-Interface.oldRotationZ);
				transform.Translate (Interface.translateX - Interface.oldTranslateX, Interface.translateY - Interface.oldTranslateY, Interface.translateZ - Interface.oldTranslateZ);
				Interface.oldTranslateX = Interface.translateX;
				Interface.oldTranslateY = Interface.translateY;
				Interface.oldTranslateZ = Interface.translateZ;
				
			}

			if (Interface.oldRotationX != Interface.rotationX || Interface.oldRotationY != Interface.rotationY || Interface.oldRotationZ != Interface.rotationZ){
				//transform.Rotate(Interface.rotationX - Interface.oldRotationX, Interface.rotationY - Interface.oldRotationY, Interface.rotationZ - Interface.oldRotationZ);
				//transform.Rotate(- Interface.oldRotationX, - Interface.oldRotationY,  - Interface.oldRotationZ);
				transform.Rotate(0, 0,  - Interface.oldRotationZ);
				transform.Rotate(0, - Interface.oldRotationY,  0);
				transform.Rotate(- Interface.oldRotationX, 0,  0);

				transform.Rotate(Interface.rotationX, 0,  0);
				transform.Rotate(0, Interface.rotationY,  0);
				transform.Rotate(0, 0,  Interface.rotationZ);

				//transform.Rotate(Interface.rotationX, Interface.rotationY, Interface.rotationZ);

			}
			Interface.oldRotationX = Interface.rotationX;
			Interface.oldRotationY = Interface.rotationY;
			Interface.oldRotationZ = Interface.rotationZ;


		
		}
		//Camera looks at
		motionScapeCenter.x = Interface.translateX;
		motionScapeCenter.y = Interface.translateY;
		motionScapeCenter.z = Interface.translateZ;

		//fog
		//RenderSettings.fogDensity = Interface.fogDensity;
		RenderSettings.fogColor = new Color( 0, Interface.fogWhiteness * 0.75f, Interface.fogWhiteness);
		RenderSettings.ambientLight = new Color(Interface.ambientLightness, Interface.ambientLightness, Interface.ambientLightness );
		//RenderSettings.fogColor = new Color( 0f, 0.2f, 0.4f);

		if (Interface.camAutoState == -1 && Interface.autoState ==1) {
			if (Interface.translateY < 180)
				Interface.translateY += 0.1f;
			else {
				Interface.autoState = -1;
				Interface.translateY = 180f;
			}

		}
		
		//translate particle system
		if (Interface.oldTranslateX != Interface.translateX || Interface.oldTranslateY != Interface.translateY || Interface.oldTranslateZ != Interface.translateZ){
			//particleSystem.transform.Rotate(-Interface.oldRotationX, -Interface.oldRotationY, -Interface.oldRotationZ);
			//particleSystem.transform.Rotate(Interface.rotationX-Interface.oldRotationX, Interface.rotationY-Interface.oldRotationY, Interface.rotationZ-Interface.oldRotationZ);
			transform.Translate (Interface.translateX - Interface.oldTranslateX, Interface.translateY - Interface.oldTranslateY, Interface.translateZ - Interface.oldTranslateZ);
			Interface.oldTranslateX = Interface.translateX;
			Interface.oldTranslateY = Interface.translateY;
			Interface.oldTranslateZ = Interface.translateZ;
			
		}
		
		//rotate particle system
		if (Interface.oldRotationX != Interface.rotationX || Interface.oldRotationY != Interface.rotationY || Interface.oldRotationZ != Interface.rotationZ){
			//particleSystem.transform.Rotate(-Interface.oldRotationX, -Interface.oldRotationY, -Interface.oldRotationZ);
			//particleSystem.transform.Rotate(Interface.rotationX-Interface.oldRotationX, Interface.rotationY-Interface.oldRotationY, Interface.rotationZ-Interface.oldRotationZ);
			transform.Rotate(Interface.rotationX - Interface.oldRotationX, Interface.rotationY - Interface.oldRotationY, Interface.rotationZ - Interface.oldRotationZ);
			Interface.oldRotationX = Interface.rotationX;
			Interface.oldRotationY = Interface.rotationY;
			Interface.oldRotationZ = Interface.rotationZ;
			
		}
			
		
		
		//Update motions
		if (Interface.selMotionInt != Interface.oldSelMotionInt) {
			switch (Interface.selMotionInt) {
				case 0:
					linearMotion.reset ();
					break;
				case 1:
					radialMotion.reset ();
					break;
				case 2:
					sphericalMotion.reset ();
					break;
				case 3:
					circularMotion.reset ();
					break;
			}
			
			Interface.oldSelMotionInt = Interface.selMotionInt;
		}
		
		//When points amount or trail length is changed
		if (Interface.pointAmount != Interface.oldPointAmount) {
			switch (Interface.selMotionInt) {
				case 0:
					linearMotion.reset ();
					break;
				case 1:
					radialMotion.reset ();
					break;
				case 2:
					sphericalMotion.reset ();
					break;
				case 3:
					circularMotion.reset ();
					break;
			}
			
			Interface.oldPointAmount = Interface.pointAmount;
		}
		
		//When scale is changed
		if (Interface.scaleX != Interface.oldScaleX || Interface.scaleY != Interface.oldScaleY || Interface.scaleZ != Interface.oldScaleZ){
			switch (Interface.selMotionInt) {
				case 0:
					linearMotion.reset ();
					break;
				case 1:
					radialMotion.reset ();
					break;
				case 2:
					sphericalMotion.reset ();
					break;
				case 3:
					circularMotion.reset ();
					break;
			}
			
			Interface.oldScaleX = Interface.scaleX;
			Interface.oldScaleY = Interface.scaleY;
			Interface.oldScaleZ = Interface.scaleZ;	
		}
		
		if (Interface.radium != Interface.oldRadium) {
			switch (Interface.selMotionInt) {
				case 0:
					break;
				case 1:
					radialMotion.reset ();
					break;
				case 2:
					sphericalMotion.reset ();
					break;
				case 3:
					circularMotion.reset ();
					break;
			}
			Interface.oldRadium = Interface.radium;
		}
		
		switch (Interface.selMotionInt) {
				case 0:
					linearMotion.run ();
					break;
				case 1:
					radialMotion.run ();
					break;
				case 2:
					sphericalMotion.run ();
					break;
				case 3:
					circularMotion.run ();
					break;
		}
		
	}

	void switchPreset(){

		switch (Interface.presetId){
		case 0:

			Interface.size = 2f;
			Interface.opacity = 1f;

			Interface.selMotionInt = 2;
			Interface.speed = 0.5f;

			Interface.radium = 200f;
			
			Interface.trailPointAmount = 20;
			Interface.agentAmount = 1000;


			Interface.waveAmp = 0.0f;
			Interface.waveSpeed = 0.0f;

			Interface.translateX = 0f;
			Interface.translateY = 0f;
			Interface.translateZ = 0f;

			Interface.rotationX = 0f;
			Interface.rotationY = 0f;
			Interface.rotationZ = 0f;

			Interface.zooming = -300;
			break;
		case 1:
			Interface.size = 2f;
			Interface.opacity = 1f;

			Interface.selMotionInt = 3;
			Interface.speed = 0.5f;

			Interface.radium = 200f;
			
			Interface.trailPointAmount = 20;
			Interface.agentAmount = 1000;

			Interface.waveAmp = 0.0f;
			Interface.waveSpeed = 0.0f;

			Interface.translateX = 0f;
			Interface.translateY = 0f;
			Interface.translateZ = 0f;
			
			Interface.rotationX = 90f;
			Interface.rotationY = 0f;
			Interface.rotationZ = 0f;
			Interface.zooming = -300;
			break;
		case 2: // Ambient Effect
			Interface.size = 2f;
			Interface.opacity = 1f;

			Interface.selMotionInt = 0;
			Interface.speed = 0.4f;

			Interface.agentAmount = 10000;
			Interface.trailPointAmount = 1;

			Interface.waveAmp = 0.3f;
			Interface.waveSpeed = 0.02f;

			Interface.translateX = 0f;
			Interface.translateY = 0f;
			Interface.translateZ = 0f;

			Interface.rotationX = 0f;
			Interface.rotationY = 90f;
			Interface.rotationZ = 0f;
			Interface.zooming = -800;
			break;
		case 3: // Speed up
			Interface.selMotionInt = 0;
			Interface.size = 2f;
			Interface.opacity = 1f;

			Interface.speed = 2.5f;

			Interface.agentAmount = 10000;
			Interface.trailPointAmount = 6;

			Interface.translateX = 0f;
			Interface.translateY = 0f;
			Interface.translateZ = 0f;
			
			Interface.rotationX = 0f;
			Interface.rotationY = 90f;
			Interface.rotationZ = 0f;
			Interface.zooming = -100;
			break;
			
		case 4: // Path 01
			Interface.size = 2f;
			Interface.opacity = 1f;

			Interface.selMotionInt = 0;
			Interface.speed = 1.5f;

			Interface.agentAmount = 1000;
			Interface.trailPointAmount = 25;

			Interface.waveAmp = 0.5f;
			Interface.waveSpeed = 0.2f;

			Interface.translateX = 0f;
			Interface.translateY = 0f;
			Interface.translateZ = 0f;
			
			Interface.rotationX = 0f;
			Interface.rotationY = 90f;
			Interface.rotationZ = 0f;
			Interface.zooming = -100;
			break;
			
		case 5: // Path 02
			Interface.size = 2f;
			Interface.opacity = 0.5f;

			Interface.selMotionInt = 2;
			Interface.speed = 1.5f;

			Interface.agentAmount = 1000;
			Interface.trailPointAmount = 25;

			Interface.waveAmp = 1.5f;
			Interface.waveSpeed = 0.2f;

			Interface.translateX = 0f;
			Interface.translateY = 0f;
			Interface.translateZ = 0f;
			
			Interface.rotationX = 0f;
			Interface.rotationY = 0f;
			Interface.rotationZ = 0f;
			Interface.zooming = -100;
			break;
		case 6: // Direction UP
			Interface.size = 2f;
			Interface.opacity = 1f;

			Interface.selMotionInt = 0;
			Interface.speed = 0.5f;

			Interface.agentAmount = 10000;
			Interface.trailPointAmount = 1;
			Interface.waveAmp = 0.3f;
			Interface.waveSpeed = 0.02f;

			Interface.translateX = 0f;
			Interface.translateY = 0f;
			Interface.translateZ = 0f;
			
			Interface.rotationX = -90f;
			Interface.rotationY = -0f;
			Interface.rotationZ = 90f;
			Interface.zooming = -100;
			break;
		case 7: // Direction Down
			Interface.size = 2f;
			Interface.opacity = 1f;

			Interface.selMotionInt = 0;
			Interface.speed = 0.5f;

			Interface.agentAmount = 10000;
			Interface.trailPointAmount = 1;

			Interface.waveAmp = 0.3f;
			Interface.waveSpeed = 0.02f;

			Interface.translateX = 0f;
			Interface.translateY = 0f;
			Interface.translateZ = 0f;
			
			Interface.rotationX = 90f;
			Interface.rotationY = 0f;
			Interface.rotationZ = 90f;

			Interface.zooming = -100;
			break;

		case 8: // Inward
			Interface.size = 1f;
			Interface.opacity = 1f;

			Interface.selMotionInt = 0;
			Interface.speed = 2.5f;

			Interface.agentAmount = 2000;
			Interface.trailPointAmount = 20;

			Interface.waveAmp = 0f;
			Interface.waveSpeed = 0f;

			Interface.translateX = 0f;
			Interface.translateY = 0f;
			Interface.translateZ = 0f;
			
			Interface.rotationX = 0f;
			Interface.rotationY = 0f;
			Interface.rotationZ = 0f;
			Interface.zooming = -200;
			break;

		case 9: // Outward
			Interface.size = 2f;
			Interface.opacity = 1f;

			Interface.selMotionInt = 0;
			Interface.speed = -2.5f;

			Interface.agentAmount = 2000;
			Interface.trailPointAmount = 20;


			Interface.waveAmp = 0f;
			Interface.waveSpeed = 0f;

			Interface.translateX = 0f;
			Interface.translateY = 0f;
			Interface.translateZ = 0f;
			
			Interface.rotationX = 0f;
			Interface.rotationY = 0f;
			Interface.rotationZ = 0f;
			Interface.zooming = -200;
			break;

		case 10: // Inward spherical
			
			Interface.size = 2f;
			Interface.opacity = 1f;
			
			Interface.selMotionInt = 2;
			Interface.speed = -0.5f;
			
			Interface.radium = 200f;
			
			Interface.trailPointAmount = 20;
			Interface.agentAmount = 1000;
			
			
			Interface.waveAmp = 0.0f;
			Interface.waveSpeed = 0.0f;

			Interface.translateX = 0f;
			Interface.translateY = 0f;
			Interface.translateZ = 0f;

			Interface.rotationX = 0f;
			Interface.rotationY = 0f;
			Interface.rotationZ = 0f;
			
			Interface.zooming = -300;
			break;
		
		case 11: // // Outward spherical
			
			Interface.size = 2f;
			
			Interface.selMotionInt = 2;
			Interface.speed = 0.5f;
			
			Interface.radium = 200f;
			
			Interface.opacity = 0.5f;
			
			Interface.trailPointAmount = 20;
			Interface.agentAmount = 1000;
			
			Interface.waveAmp = 0f;
			Interface.waveSpeed = 0f;

			Interface.translateX = 0f;
			Interface.translateY = 0f;
			Interface.translateZ = 0f;
			
			Interface.rotationX = 0f;
			Interface.rotationY = 0f;
			Interface.rotationZ = 0f;
			
			Interface.zooming = -300;
			break;
		case 12: // View point
			
			Interface.size = 2f;
			
			Interface.selMotionInt = 2;
			Interface.speed = 0.5f;

			Interface.radium = 200f;

			Interface.opacity = 0.5f;
			
			Interface.trailPointAmount = 40;
			Interface.agentAmount = 1000;
			
			Interface.waveAmp = 1.5f;
			Interface.waveSpeed = 0.1f;
			
			Interface.rotationX = 0f;
			Interface.rotationY = 0f;
			Interface.rotationZ = 0f;
			
			Interface.zooming = -800;
			break;
		}

	
		Interface.oldPresetId = Interface.presetId;
	}
}
