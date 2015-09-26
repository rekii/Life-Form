using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {
	//presets:
	static public int presetId = 2;
	static public int oldPresetId = 0;
	static public string[] presetStrings = new string[] {"Local Effect 01", 
		"Local Effect 02", "Ambient Effect", "Speed", "Path Curvature 01", "Path Curvature 02", 
		"Direction UP", "Direction DOWN", "Inward: Linear", "Outward: Linear", "Inward: Spherical", "Outward: Spherical","View point"}; 

	static public string[] speedStrings = new string[] {"Slow", "Fast"};

	//particle properties
	static public int agentAmount = 10000;
	static public int pointAmount = 10000;
	static public int trailPointAmount = 1;
	static public int oldPointAmount = 10000;
	static public float size = 2f;
	static public float opacity = 1f;
	static public float blackness = 1f;
	
	//scale
	static public float scaleX = 500f;
	static public float scaleY = 500f;
	static public float scaleZ = 500f;
	
	static public float oldScaleX = 500f;
	static public float oldScaleY = 500f;
	static public float oldScaleZ = 500f;
	
	static public float radium = 500f;
	static public float oldRadium = 500f;
	static public float thickness = 50f;
	
	//zooming
	static public float zooming = -800f;
	static public float oldZooming = -800f;
	
	//transforming hole system
	static public float rotationX = 0f;
	static public float rotationY = 0f;
	static public float rotationZ = 0f;
	
	static public float oldRotationX = 0f;
	static public float oldRotationY = 0f;
	static public float oldRotationZ = 0f;
	
	static public float translateX = 0f;
	static public float translateY = 0f;
	static public float translateZ = 0f;
	
	static public float oldTranslateX = 0f;
	static public float oldTranslateY = 0f;
	static public float oldTranslateZ = 0f;
	
	//motion selection
	static public string[] motionStrings = new string[] {"Linear", "Radial", "Spherical", "Circular"}; 
	static public int selMotionInt = 0; //linear
	static public int oldSelMotionInt = 0;

	static public int selSpeedInt = 1; 
	
	//motion properties
	static public float speed = -0.2f;
	static public float rotateSpeed = 0.01f;
	
	static public int pathCurvature = 1;
	static public float waveAmp = 0.2f;
	static public float waveSpeed = 0.015f;
	static public float angAmp = 0f;
	static public float angSpeed = 0f;
	
	private int menuMargin = 10;
	private int menuWidth = 200;
	private int menuSliderHeight = 20;
	
	private Rect appearanceWindowRect;
	private Rect motionWindowRect;
	private Rect scaleWindowRect;
	private Rect rotationWindowRect;
	private Rect translationWindowRect;
	private Rect motionPickRect;
	private Rect presetPickRect;
	private Rect environmentWindowRect;
	
	//Interaction factors
	static public int responsive = 1;
	static public int autoState =  1;
	static public int camAutoState = 1;
	public int showMenu = 1;
	
	public int screenShotId = 1;
	
	//Terrain Data
	static public float terrainY = -1;
	static public float fogDensity = 0.1f;
	static public float fogWhiteness = 0.5f;
	static public float ambientLightness = 0.0f;
	
	
	void Start () {
		appearanceWindowRect = new Rect( menuMargin, 60, 2*menuMargin + menuWidth, 240); 
		motionWindowRect = new Rect( Screen.width - 3*menuMargin - menuWidth, 60, 2*menuMargin + menuWidth, 200);
		environmentWindowRect = new Rect( Screen.width - 6*menuMargin - 2 * menuWidth, 60, 2*menuMargin + menuWidth, 120);

		rotationWindowRect = new Rect( Screen.width - 3*menuMargin - menuWidth, 60 + 220, 2*menuMargin + menuWidth, 160);
		translationWindowRect = new Rect ( Screen.width - 3*menuMargin - menuWidth, 60 + 400, 2*menuMargin + menuWidth, 160);
		scaleWindowRect = new Rect( menuMargin, 60 + 240 + menuMargin, 2*menuMargin + menuWidth, 240);
		motionPickRect = new Rect ( 4 * menuMargin + menuWidth, 60, 2*menuMargin + menuWidth, 140);
		presetPickRect = new Rect ( 4 * menuMargin + menuWidth, 200 + menuMargin, 2*menuMargin + menuWidth, 340);

		pointAmount = agentAmount * trailPointAmount;
		//System.IO.File.WriteAllText("yourtextfile.txt", "This is text that goes into the text file");
	}

	void OnGUI () {

		if (autoState == -1 && showMenu == 1){
			appearanceWindowRect = GUI.Window(0, appearanceWindowRect, appearanceWindowFunction, "Appearance");
			motionWindowRect = GUI.Window(1, motionWindowRect, motionWindowFunction, "Motion Properties");
			rotationWindowRect = GUI.Window(2, rotationWindowRect, rotationWindowFunction, "Rotation");
			translationWindowRect = GUI.Window(3, translationWindowRect, translationWindowFunction, "Center Position");
			scaleWindowRect = GUI.Window(4, scaleWindowRect, scaleWindowFunction, "Scale");
			motionPickRect = GUI.Window(5, motionPickRect, motionPickWindowFunction, "Shape");
			environmentWindowRect = GUI.Window(6, environmentWindowRect, environmentWindowFunction, "Environment");
			presetPickRect = GUI.Window(7, presetPickRect, presetPickWindowFunction, "Presets");

		}



		if (Input.GetKey (KeyCode.P)) {
			Application.CaptureScreenshot ("Shots/sample" + screenShotId +".jpg");
			screenShotId ++;
			
		}

		
		if (Input.GetKeyDown (KeyCode.S)) {
			showMenu = 1;

		}
		if (Input.GetKeyDown (KeyCode.H)) {
			showMenu = -1;
		}

	}
	
	void appearanceWindowFunction (int windowId) {
		
		GUI.Label (new Rect (menuMargin, 2 * menuMargin, menuWidth, menuSliderHeight), "Size " + size);
		size = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + menuSliderHeight, menuWidth, menuSliderHeight), size, 0.1f, 200.0f);
		
		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 2 * menuSliderHeight, 50, menuSliderHeight), "Opacity");
		opacity = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 3 * menuSliderHeight, menuWidth, menuSliderHeight), opacity, 0f, 1f);
		
		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 4 * menuSliderHeight, 50, menuSliderHeight), "Color");
		blackness = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 5 * menuSliderHeight, menuWidth, menuSliderHeight), blackness, 0f, 1f);
		
		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 6 * menuSliderHeight, 100, menuSliderHeight), "Amount " + agentAmount);
		agentAmount = (int)GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 7 * menuSliderHeight, menuWidth, menuSliderHeight), agentAmount, 100, 10000);
		
		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 8 * menuSliderHeight, 100, menuSliderHeight), "Tail " + trailPointAmount);
		trailPointAmount = (int)GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 9 * menuSliderHeight, menuWidth, menuSliderHeight), trailPointAmount, 1, 100);
		
		pointAmount = agentAmount * trailPointAmount;


	}
	
	void motionWindowFunction (int windowId) {
		if (selMotionInt!=3){
			GUI.Label (new Rect (menuMargin, 2 * menuMargin, menuWidth, menuSliderHeight), "Speed " + speed);
			speed = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + menuSliderHeight, menuWidth, menuSliderHeight), speed, -12f, 12f);
			//if(presetId == 3) selSpeedInt = GUI.SelectionGrid (new Rect (menuMargin, 3 * menuMargin + 2 * menuSliderHeight, menuWidth, menuSliderHeight), selSpeedInt, speedStrings, 2);
		}
		else{
			GUI.Label (new Rect (menuMargin, 2 * menuMargin, menuWidth, menuSliderHeight), "Rotation Speed " + rotateSpeed);
			rotateSpeed = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + menuSliderHeight, menuWidth, menuSliderHeight), rotateSpeed, -0.1f, 0.1f);
		}
		
		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 4 * menuSliderHeight, menuWidth, menuSliderHeight), "Wave Amp " + waveAmp);
		waveAmp = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 5 * menuSliderHeight, menuWidth, menuSliderHeight), waveAmp, 0f, 5f);
		
		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 6 * menuSliderHeight, menuWidth, menuSliderHeight), "Wave Speed " + waveSpeed);
		waveSpeed = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 7 * menuSliderHeight, menuWidth, menuSliderHeight), waveSpeed, 0f, 0.5f);

	}
	
	void translationWindowFunction (int windowId) {
		//GUI.Label (new Rect (menuMargin, 2 * menuMargin, menuWidth, menuSliderHeight), "X " + translateX);
		//translateX = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + menuSliderHeight, menuWidth, menuSliderHeight), translateX, -180f, 180f);
		
		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 2 * menuSliderHeight, menuWidth, menuSliderHeight), "Y" + translateY);
		translateY = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 3 * menuSliderHeight, menuWidth, menuSliderHeight), translateY, -180f, 180f);
		
		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 4 * menuSliderHeight, menuWidth, menuSliderHeight), "Z " + translateZ);
		translateZ = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 5 * menuSliderHeight, menuWidth, menuSliderHeight), translateZ, -180f, 180f);

	}
	
	void rotationWindowFunction (int windowId) {
		GUI.Label (new Rect (menuMargin, 2 * menuMargin, menuWidth, menuSliderHeight), "X " + rotationX);
		rotationX = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + menuSliderHeight, menuWidth, menuSliderHeight), rotationX, -180f, 180f);
		
		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 2 * menuSliderHeight, menuWidth, menuSliderHeight), "Y " + rotationY);
		rotationY = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 3 * menuSliderHeight, menuWidth, menuSliderHeight), rotationY, -180f, 180f);
		
		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 4 * menuSliderHeight, menuWidth, menuSliderHeight), "Z " + rotationZ);
		rotationZ = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 5 * menuSliderHeight, menuWidth, menuSliderHeight), rotationZ, -180f, 180f);

	}

	void scaleWindowFunction (int windowId) {
		if (selMotionInt == 0){

			GUI.Label (new Rect (menuMargin, 2 * menuMargin, menuWidth, menuSliderHeight), "Width (x) " + scaleX);
			scaleX = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + menuSliderHeight, menuWidth, menuSliderHeight), scaleX, 5f, 500f);
			
			GUI.Label (new Rect (menuMargin, 2 * menuMargin + 2 * menuSliderHeight, menuWidth, menuSliderHeight), "Height (y)" + scaleY);
			scaleY = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 3 * menuSliderHeight, menuWidth, menuSliderHeight), scaleY, 5f, 500f);
		}

		else{
			GUI.Label (new Rect (menuMargin, 2 * menuMargin, menuWidth, menuSliderHeight), "Radium " + radium);
			radium = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + menuSliderHeight, menuWidth, menuSliderHeight), radium, 5f, 1500f);
		}
		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 4 * menuSliderHeight, menuWidth, menuSliderHeight), "Depth (z) " + scaleZ);
		scaleZ = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 5 * menuSliderHeight, menuWidth, menuSliderHeight), scaleZ, 5f, 500f);


		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 6 * menuSliderHeight, menuWidth, menuSliderHeight), "Zooming " + zooming);
		zooming = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 7 * menuSliderHeight, menuWidth, menuSliderHeight), zooming, -100f, -1000f);

	}
	
	void motionPickWindowFunction (int windowId) {
		GUI.Label (new Rect (menuMargin, 2 * menuMargin, menuWidth, menuSliderHeight), "Current: " + (selMotionInt + 1));
		selMotionInt = GUI.SelectionGrid (new Rect (menuMargin, 3 * menuMargin + menuSliderHeight, menuWidth, 2 * menuSliderHeight), selMotionInt, motionStrings, 2);
	}

	void environmentWindowFunction (int windowId) {
		GUI.Label (new Rect (menuMargin, 2 * menuMargin, menuWidth, menuSliderHeight), "Ambient Light:" + ambientLightness);
		ambientLightness = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + menuSliderHeight, menuWidth, menuSliderHeight), ambientLightness, 0.0f, 1.0f);
		GUI.Label (new Rect (menuMargin, 2 * menuMargin + 2 * menuSliderHeight, menuWidth, menuSliderHeight), "Fog Brighteness " + fogWhiteness);
		fogWhiteness = GUI.HorizontalSlider (new Rect (menuMargin, 2 * menuMargin + 3 * menuSliderHeight, menuWidth, menuSliderHeight), fogWhiteness, 0.0f, 1.0f);

	}

	void presetPickWindowFunction (int windowId){
		GUI.Label (new Rect (menuMargin, 2 * menuMargin, menuWidth, menuSliderHeight), "Current: " + (presetId + 1));
		presetId = GUI.SelectionGrid (new Rect (menuMargin, 3 * menuMargin + menuSliderHeight, menuWidth, 13 * menuSliderHeight), presetId, presetStrings, 1);
	}


}