using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class Bezier
{
	/*
	private static Matrix4x4 m = new Matrix4x4() 
		{
		  m00 = -1.0f, m01 =  3.0f, m02 = -3.0f, m03 = 1.0f,
		  m10 =  3.0f, m11 = -6.0f, m12 =  3.0f, m13 = 0.0f,
		  m20 = -3.0f, m21 =  3.0f, m22 =  0.0f, m23 = 0.0f,
		  m30 =  1.0f, m31 =  0.0f, m32 =  0.0f, m33 = 0.0f
		};
	*/
	
	/**
	 * Calculate Bernstein basis
	 */
	private static float Bernstein(int n, int i, float t)
	{
		//float basis;
		float ti; /* t^i */
		float tni; /* (1 - t)^(n - i) */
		
		/* Prevent problems with pow */	
		if (t == 0.0 && i == 0)
			ti = 1.0f; 
		else 
			ti = Mathf.Pow(t, i);
		
		if (n == i && t == 1.0f) 
			tni = 1.0f; 
		else 
			tni = Mathf.Pow((1 - t), (n - i));
	
		//Bernstein basis
		return BinoCache.Get(n, i) * ti * tni;
	}
	
	public static List<Vector3> Bezier2D(List<Vector3> b, int detail)
	{
		int npts = b.Count;
		float step, t, basis;
		List<Vector3> ret = new List<Vector3>(detail);
		Vector3 v;
		
		t = 0;
	    step = 1.0f / (detail - 1);
	
	    for (int i1 = 0; i1 != detail; i1++)
	  	{ 
	        if ((1.0f - t) < 5e-6)
	      	  t = 1.0f;
	
			v = Vector3.zero;
		    for (int i = 0; i < npts; i++)
		    {
		        basis = Bernstein(npts - 1, i, t);
		        v.x += basis * b[i].x;
		        v.z += basis * b[i].z;
		    }
			
			ret.Add(v);
	    	t += step;
	  	}
		return ret;
	}
	
	public static List<Vector3> Bezier3D(List<Vector3> b, int detail)
	{
		int npts = b.Count;
		float step, t, basis;
		List<Vector3> ret = new List<Vector3>(detail);
		Vector3 v;
		
		t = 0;
	    step = 1.0f / (detail - 1);
	
		for (int i1 = 0; i1 != detail; i1++)
		{
	        if ((1.0f - t) < 5e-6)
	      	  t = 1.0f;
	
			v = Vector3.zero;
		    for (int i = 0; i < npts; i++)
		    {
		        basis = Bernstein(npts - 1, i, t);
		        v.x += basis * b[i].x;
				v.y += basis * b[i].y;
		        v.z += basis * b[i].z;
		    }
			
			ret.Add(v);
	    	t += step;
	  	}
		return ret;
	}
	
	/*
	public static List<Vector3> Calc(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, int detail)
	{
		float t;
		float tStep = 1.0f / detail;
		List<Vector3> ret = new List<Vector3>();
	
		// Powers of t
		float t2;
		float t3;
		Vector3 point = new Vector3();
		Vector4 fx = m * new Vector4(p1.x, p2.x, p3.x, p4.x);
		Vector4 fy = m * new Vector4(p1.z, p2.z, p3.z, p4.z);
		
		for(t = 0.0f; t <= 1.0f; t += tStep)
		{
			t2 = t * t;
			t3 = t2 * t;

			point.x = fx[0] * t3 + fx[1] * t2 + fx[2] * t + fx[3];
			point.z = fy[0] * t3 + fy[1] * t2 + fy[2] * t + fy[3];
			
			ret.Add(point);
		}

		return ret;
	}
	*/
}