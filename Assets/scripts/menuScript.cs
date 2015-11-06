using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour {

	// Use this for initialization
	void OnGUI () {
		int height = 60;
		int width = 84;
		Rect rect = new Rect (Screen.width/2- width/2,2*Screen.height/3 - height/3,width,height);
		if (GUI.Button (rect, "start"))
			Application.LoadLevel ("level1");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
