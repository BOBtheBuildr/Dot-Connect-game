using UnityEngine;
using System.Collections;
using System.Drawing;
public class drawCircle {

	private LineRenderer ln;
	private int radius;
     public drawCircle()
	{

	}


	public void draw()
	{
		for (double i=0.0; i<360.0; i+=0.1) {
			double angle = i* System.Math.PI/180;
			int x =(int) (150 + radius* System.Math.Cos(angle));
			int y = (int) (150 + radius* System.Math.Sin(angle));
		
		}

	}
	// Use this for initialization

	void pixelPut(int x,int y)
	{

	}

}
