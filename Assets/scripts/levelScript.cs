using UnityEngine;
using System.Collections;

public class levelScript : MonoBehaviour {

	// Use this for initialization
	GameObject[] circleClone = new GameObject[100];
	int dots = 0;
	GameObject c; 
	public GameObject[] instantiate(int level) {
		Debug.Log (level);
		switch(level)
		{
		case 1:
			 c = Resources.Load ("circle 1") as GameObject;
			generateSquaredCircles(3,3,1f,1f,1.5f,0,0,c);
			//generateTriangularCircles(3,1f,1f,1.5f,c);
			break;
		case 2:
			c = Resources.Load ("circle 2") as GameObject;
			generateSquaredCircles(4,4,2f,1.5f,1.2f,0,0,c);
			//randomScatter();
			break;
		case 3:
			c = Resources.Load ("circle 2") as GameObject;
			generateTriangularCircles(4,1f,1f,1.2f,c);
			//randomScatter();
			break;
		case 4:
			c = Resources.Load ("circle 2") as GameObject;
			generateSquaredCircles(4,5,2f,0.5f,1.2f,-1,0,c);
			break;

		case 5:
			c = Resources.Load ("circle 2") as GameObject;
			generatePattern(4,1f,1f,1.2f,c);
			break;

		case 6:
			c = Resources.Load ("circle 2") as GameObject;
			generateCircle(4f,30.0f,c);
			generateCircle(2.0f,45.0f,c);
			//generateCircle(0.8f,90.0f,c);
			break;
			

		
		 }
	
		return circleClone;
}

	private void generateSquaredCircles(int length1,int length2,float xoffSet,float yoffSet,float offSet,int y1,int x1,GameObject c)
	{
		for (int y = y1; y < length1-Mathf.Abs(y1); y++) {
			for (int x = x1; x < length2-Mathf.Abs(x1); x++) {
				

				circleClone [dots++] = Instantiate (c, new Vector3 (1.5f * (x - xoffSet) * offSet, 1.5f * (y - yoffSet) * offSet, 0.5f), Quaternion.identity) as GameObject; 

			}
		}
	}


	private void generateTriangularCircles(int length,float xoffSet,float yoffSet,float offSet,GameObject c)
	{
		float val = 0.73f;
		float spaceX = 0f;
		for (int y = 0; y < length; y++) {
			for (int x = 0; x < length-y; x++) {
					
				circleClone [dots++] = Instantiate (c, new Vector3 ((1.2f * (x-1+0.5f - xoffSet) * offSet) + spaceX, 1.2f * (y+0.5f - yoffSet) * offSet, 0.5f), Quaternion.identity) as GameObject; 
				
			}
			spaceX+=val;
		}
		spaceX -=val;
		for (int y = -length+1; y < 0; y++) {
			for (int x = 0; x < length-Mathf.Abs(y); x++) {
				
				circleClone [dots++] = Instantiate (c, new Vector3 ((1.2f * (x-1+0.5f - xoffSet) * offSet) + spaceX, 1.2f * (y+0.65f - yoffSet) * offSet, 0.5f), Quaternion.identity) as GameObject; 
				
			}
			spaceX-=val;
		}
		
	}

	private void randomScatter()
	{
		int count =10;
		while (count-->0) {
			float y = Random.Range (-4.0f,4.0f);
			float x = Random.Range(-9.0f,9.0f);
			//Vector3 pos = Camera.main.ScreenToWorldPoint(
				Vector3 pos = 	new Vector3(x,y,0f);
			circleClone[dots++] = Instantiate(c,pos,Quaternion.identity) as GameObject;



		}
	}


	private void generatePattern(int length,float xoffSet,float yoffSet,float offSet,GameObject c)
	{
		float val = 0.73f;
		float spaceX = 0f;
		for (int y = 0; y < length; y++) {
			for (int x = 0; x < length-y; x++) {
				
				circleClone [dots++] = Instantiate (c, new Vector3 ((1.2f * (x-1+0.5f - xoffSet) * offSet) + spaceX, 1.2f * (y+0.5f - yoffSet) * offSet, 0.5f), Quaternion.identity) as GameObject; 
				
			}
			spaceX+=val;
		}
		spaceX -=val*(length+3);
		for (int y = -length+1; y < 0; y++) {
			for (int x = 0; x < length-Mathf.Abs(y); x++) {
				
				circleClone [dots++] = Instantiate (c, new Vector3 ((1.2f * (x-1+0.5f - xoffSet) * offSet) + spaceX, 1.2f * (y+0.65f - yoffSet) * offSet, 0.5f), Quaternion.identity) as GameObject; 
				
			}
			spaceX-=val;
		}

		spaceX =val*(length+1)*2;
		for (int y = -length+1; y < 0; y++) {
			for (int x = 0; x < length-Mathf.Abs(y); x++) {
				
				circleClone [dots++] = Instantiate (c, new Vector3 ((1.2f * (x-1+0.5f - xoffSet) * offSet) + spaceX, 1.2f * (y+0.65f - yoffSet) * offSet, 0.5f), Quaternion.identity) as GameObject; 
				
			}
			spaceX-=val;
		}

	}

	private void generateCircle(float r,float angle,GameObject c)
	{
		float x, y;
		for (float theta = 0; theta<270f; theta+=angle) {
			x= r*Mathf.Cos(theta);
			y=r*Mathf.Sin(theta);
			//print ("hello ");
			circleClone[dots++]= Instantiate(c,new Vector3(x,y,0),Quaternion.identity)as GameObject;
		}
	}




	public int arrayLength()
	{
		return dots;
	}

}
