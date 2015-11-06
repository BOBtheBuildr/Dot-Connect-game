using UnityEngine;
using System.Collections.Generic;






public class lineDraw : MonoBehaviour {
	private LineRenderer line;
	private bool isMousePressed;
	private bool mousePresedAgain;
	private List<Vector3> pointsList;
	private List<int> elements;
	private Vector3 mousePos;
	private circles[] circleList;
	private GameObject[] circleClone;
	private GameObject circle;
	private List<int> hitList;
	private levelScript ls;
	private float startTime;
	private string x;
	private int flag =0;
	private bool timerRun;
	private bool isGamePlayeble=true;
	private int next=0;
	private int prv = 0;
	private int retVal;
	private int score=0;
	int dots;
	// Structure for line points
	struct myLine
	{
		public Vector3 StartPoint;
		public Vector3 EndPoint;
	};
	//	-----------------------------------
	//structure for circles

	struct circles
	{
		public float x;
		public float y;
		public float rad;
		
	};
	void Awake ()
	{
		// Create line renderer component and set its property

		elements = new List<int> ();
		line = gameObject.AddComponent<LineRenderer> ();
		line.material = new Material (Shader.Find ("Particles/Additive"));
		line.SetVertexCount (0);
		line.SetWidth (0.05f, 0.05f);
		//line.SetColors (Color.white, Color.white);
		line.useWorldSpace = true;
		line.sortingOrder = 1;
		isMousePressed = false;
	    pointsList = new List<Vector3> ();
		hitList = new List<int> ();
		circleClone = new GameObject[100];
		circleList = new circles[100];
		ls = new levelScript();
		startTime= Time.time;
		timerRun = true;
		dots = 0;

		x = Application.loadedLevelName;

		circleClone = ls.instantiate ((int)x[x.Length-1]-48);
		dots = ls.arrayLength();
		//Debug.Log (x[x.Length-1]);
		//-------initialization complete---//



		for (int i=1; i<=dots;i++) {
			elements.Add(i);
			circleList[i-1].rad =circleClone[i-1].GetComponent<CircleCollider2D> ().radius;
			circleList[i-1].x = circleClone[i-1].GetComponent<Renderer> ().bounds.center.x + circleClone[i-1].GetComponent<CircleCollider2D> ().offset.x;
			circleList[i-1].y = circleClone[i-1].GetComponent<Renderer> ().bounds.center.y + circleClone[i-1].GetComponent<CircleCollider2D> ().offset.y;
			//Instantiate(GameObject.Find("Sphere"),new Vector3(circleList[i-1].x,circleList[i-1].y,0),Quaternion.identity);
		}

		redCircle ();

	}
	//	-----------------------------------	









	void Update ()
	{
		if (isGamePlayeble == true)
			gameControl ();
    }


	void gameControl()
	{

		
		if (Input.GetMouseButtonDown (0)) {
			
			
			if(mousePresedAgain==true){
				flag=1;
				Debug.Log("game over");
			}
			
			isMousePressed = true;
			line.SetVertexCount (0);
			pointsList.RemoveRange (0, pointsList.Count);
			line.SetColors (Color.green, Color.green);
		}
		
		
		
		
		if (Input.GetMouseButtonUp (0)) {
			isMousePressed = false;
		}
		
		
		
		
		// Drawing line when mouse is moving(presses)
		if (isMousePressed) {
			
			mousePresedAgain = true;
			Vector2 pos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePos.z = 0;
			
			WithInWindow(pos);
			for(int i=1;i<=dots;i++){
				if(isCircleIntersect(i)==true)
				{
				if(i==next){
					score+=10;
					greyCircle(next);
					Debug.Log("correct hit!!");
					redCircle();
					break;
					}
				else if(!hitList.Contains(i) || i!=prv)
						flag=1;
				}
			}
			
			
			if (!pointsList.Contains (mousePos)) {
				pointsList.Add (mousePos);
				line.SetVertexCount (pointsList.Count);
				line.SetPosition (pointsList.Count - 1, (Vector3)pointsList [pointsList.Count - 1]);
				if (isLineCollide ()) {
					isMousePressed = false;
					line.SetColors (Color.red, Color.red);
				}
			}
		}

	} 









	private bool isLineCollide ()
	{
		if (pointsList.Count < 2)
			return false;
		int TotalLines = pointsList.Count - 1;
		myLine[] lines = new myLine[TotalLines];
		if (TotalLines > 1) {
			for (int i=0; i<TotalLines; i++) {
				lines [i].StartPoint = (Vector3)pointsList [i];
				lines [i].EndPoint = (Vector3)pointsList [i + 1];
			}
		}
		for (int i=0; i<TotalLines-10; i++) {
			myLine currentLine;
			currentLine.StartPoint = (Vector3)pointsList [pointsList.Count - 2];
			currentLine.EndPoint = (Vector3)pointsList [pointsList.Count - 1];
			if (isLinesIntersect (lines [i], currentLine)){ 
				flag=1;
				return true;
			}
		}
		return false;
	}



	//	-----------------------------------	
	//	Following method checks whether given two points are same or not
	//	-----------------------------------	

	private bool checkPoints (Vector3 pointA, Vector3 pointB)
	{
		return (pointA.x == pointB.x && pointA.y == pointB.y);
	}
	//	-----------------------------------	
	//	Following method checks whether given two line intersect or not
	//	-----------------------------------	

	private bool isLinesIntersect (myLine L1, myLine L2)
	{
		if (checkPoints (L1.StartPoint, L2.StartPoint) ||
		    checkPoints (L1.StartPoint, L2.EndPoint) ||
		    checkPoints (L1.EndPoint, L2.StartPoint) ||
		    checkPoints (L1.EndPoint, L2.EndPoint))
			return false;
		
		return((Mathf.Max (L1.StartPoint.x, L1.EndPoint.x) >= Mathf.Min (L2.StartPoint.x, L2.EndPoint.x)) &&
		       (Mathf.Max (L2.StartPoint.x, L2.EndPoint.x) >= Mathf.Min (L1.StartPoint.x, L1.EndPoint.x)) &&
		       (Mathf.Max (L1.StartPoint.y, L1.EndPoint.y) >= Mathf.Min (L2.StartPoint.y, L2.EndPoint.y)) &&
		       (Mathf.Max (L2.StartPoint.y, L2.EndPoint.y) >= Mathf.Min (L1.StartPoint.y, L1.EndPoint.y)) 
		       );
	}

	private bool isCircleIntersect(int i)
	{
		if ((circleList[i-1].x - mousePos.x) * (circleList[i-1].x - mousePos.x) + (circleList[i-1].y - mousePos.y) * (circleList[i-1].y - mousePos.y) < circleList[i-1].rad * circleList[i-1].rad)
			return true;
		else
			return false;

	}

	private void redCircle()
	{
		if (hitList.Count == dots) {
			flag = 2;
			return;	
		}
			
		
			int	rand= elements[(int)Random.Range (0, elements.Count-1)];

			if(!hitList.Contains(rand))
			{
				hitList.Add (rand);
				elements.Remove(rand);
			    prv = next;
				next =rand;
				Renderer r;
				r = circleClone [rand - 1].GetComponent<Renderer> ();
				r.material.color = Color.yellow;
			}
	
	}


	private void greyCircle(int curr)
	{
		Renderer r;

		r = circleClone[curr-1].GetComponent<Renderer> ();
		r.material.color = Color.grey;
	}

	void OnGUI(){

		GUI.Label (new Rect (10, Screen.height-50, 800, 800),"Score : "+score.ToString("00"));

		if (timerRun == true) {
			var tmr = Time.time - startTime;
			string min = Mathf.Floor (tmr / 60).ToString ("00");
			string sec = Mathf.Floor (tmr % 60).ToString ("00");
			string t = min + ":" + sec;
			GUI.Label (new Rect (Screen.width-100, 10, 800, 800), t);
		}

		if (flag == 1) {
			GUI.Label (new Rect (10, 40, 500, 500), "Game Over!!");
			timerRun = false;
			Rect window =new Rect (Screen.width/2-100, 10, 250, 250);
			 GUI.Window(0,window,myWindow,"Game over");
			 isGamePlayeble = false;
			if(retVal==1)
				Application.LoadLevel(Application.loadedLevel);
			else if(retVal == 2)
						Application.Quit();
		} 
		else if (flag == 2) {
		//	GUI.Label (new Rect (10, 10, 500, 500), "Congratulations you won!!");
			Rect window =new Rect (Screen.width/2-100, 10, 250, 250);
			GUI.Window(0,window,winWindow,"Well Done!!");
			isGamePlayeble = false;
			timerRun = false;
			int num =(int)x[x.Length-1]-47;
			if(num<5 && retVal==3)
				Application.LoadLevel("level"+num.ToString());	
		}

	}
	
	void winWindow(int windowId)
	{
		if (GUI.Button (new Rect (75, 50, 100, 40), "Next Level!!"))
			retVal = 3;
	}
	

	void myWindow(int windowId)
	{
		if (GUI.Button (new Rect (75, 50, 100, 40), "restart"))
			retVal = 1;
		else if (GUI.Button (new Rect (75, 110, 100, 40), "exit"))
			retVal = 2;

		GUI.Label(new Rect (75, 180, 100, 40), "Your Score = "+score);
		return;
	}

	


	private void WithInWindow(Vector2 pos)
	{
		if (!((pos.x>=0 && pos.x<=Screen.width) && (pos.y>=0 && pos.y<=Screen.height))) {
			flag = 1;
		}


	}

}