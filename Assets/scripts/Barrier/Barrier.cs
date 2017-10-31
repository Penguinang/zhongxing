﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.ComponentModel.Design;
using System.Runtime.Serialization.Formatters;
using System.Xml.Linq;
//using UnityEngine.XR.Tango;
using NUnit.Framework;

public class Barrier : MonoBehaviour
{
	public LineRenderer debugLine;
	public float R = 2;
	List<Vector3> vertices;

	public void Init(int[] starsIDs){
		StartCoroutine (BarrierAnimation (starsIDs));
	}

	IEnumerator BarrierAnimation(int[] starsIDs){
		int count = starsIDs.Length;
		for(int i = 1;i<count;i++){
			float x = 0;
			while (x <= 1) {
				List<Vector3> starsPositions = new List<Vector3> ();
				for (int k = 0; k <= i; k++) {
					starsPositions.Add (PlanetManager.GetPlanet (starsIDs[k]).transform.position);
				}
				Vector3 targetPosition = starsPositions[starsPositions.Count-1];
				starsPositions.RemoveAt (starsPositions.Count-1);
				Vector3 lastPosition = starsPositions[starsPositions.Count-1];
				x += 0.05f;
				starsPositions.Add (lastPosition*(1-x)+targetPosition*x);
				yield return StartCoroutine (Calculate (starsPositions));
//				yield return 100;
			}
		}
	}

	public IEnumerator Calculate(List<Vector3> starsPositions){
		vertices = new List<Vector3> ();

		//----------------------------------------找出凸多边形--------------------------------------------
		Vector3 current = new Vector3 (),next = new Vector3(9999,9999,9999),start;
		Vector3 currentLine = new Vector3 (0, 1, 0);
		//--------找出起始点
		float maxX= starsPositions [0].x;
		for (int i = 0; i < starsPositions.Count; i++) {
			float tempX = starsPositions [i].x;
			if (tempX > maxX) {
				maxX = tempX;
			} 
		}
		float minY = 99999;
		List<Vector3> temp = new List<Vector3> ();
		for (int i = 0; i < starsPositions.Count; i++) {
			if (starsPositions [i].x == maxX) {
				temp.Add (starsPositions [i]);
				if (starsPositions [i].y < minY) {
					minY = starsPositions [i].y;
					current = starsPositions[i];
				}
			}
		}
		start = current;
		System.Collections.Generic.IComparer<Vector3> comp = new PointCompareDis (start); 
		temp.Sort (comp);
		for (int i = 0; i < temp.Count; i++) {
			vertices.Add (temp[i]);
		}
		current = temp [temp.Count - 1];
		bool startMask = true;
		//--------开始循环
		while ((!start.Equals (current))||startMask) {
			if(startMask) startMask = false;
			//找到最小角
			float minAngle = 9999;
			for (int i = 0; i < starsPositions.Count; i++) {
				if (starsPositions [i].Equals (current))
					continue;
				Vector3 tempLine = starsPositions [i] - current;
				float tempAngle = Angle (currentLine, tempLine);
				if (tempAngle< minAngle&&tempAngle!=0) {
					minAngle = tempAngle;
				}
			}
			//找到最小角方向上的所有点
			List<Vector3> PointsOnLine = new List<Vector3> ();
			for (int i = 0; i < starsPositions.Count; i++)
				if (Angle (currentLine, starsPositions [i] - current) == minAngle)
					PointsOnLine.Add (starsPositions [i]);
			PointsOnLine.Sort ((IComparer<Vector3>)new PointCompareDis (current));
			//依距离将此直线上的所有点按顺序加入顶点数组，更新当前点为最远点和当前方向为最小角方向
			for (int i = 0; i < PointsOnLine.Count; i++) {
				vertices.Add (PointsOnLine [i]);
			}
			currentLine = PointsOnLine[0]-current;
			current = PointsOnLine [PointsOnLine.Count - 1];
		}
		//----------------------------------------找出凸多边形--------------------------------------------

		//----------------------------------------对边的坐标修正--------------------------------------------
		vertices.RemoveAt (vertices.Count-1);
		for (int i = 0; i < vertices.Count; i+=3) {
			int nextVertex = (i+1)%vertices.Count;
			int prevVertex = (i - 1 + vertices.Count) % vertices.Count;
			Vector3 leftEdge = vertices[prevVertex] - vertices [i];
			Vector3 rightEdge = vertices [nextVertex] - vertices [i];
			Vector3 right = new Vector3(rightEdge.y,-rightEdge.x);
			Vector3 left = new Vector3 (-leftEdge.y, +leftEdge.x);
			Vector3 rightPoint = vertices [i] + right.normalized * R;
			Vector3 leftPoint = vertices [i] + left.normalized * R;
			vertices.Insert (i,leftPoint);
			vertices.Insert (i+2,rightPoint);
		}
		Vector3 head = vertices [0];
		vertices.Add (head);
		//----------------------------------------对边的坐标修正--------------------------------------------

		//DEBUG
		//用找到的顶点画多边形
//		debugLine.positionCount = vertices.Count;
//		debugLine.SetPositions (vertices.ToArray ());

		Render ();
		yield return 0;
	}

	public void Render(){
		vertices.Reverse ();
		//----------------------------------------分割三角形--------------------------------------------
		int vertexNumber = vertices.Count-1;
		int sunkenVertexNumber = vertexNumber / 3;
		List<int> indexes = new List<int> (3*(vertexNumber-1));
		//-------------内部凹点组成的多边形网格
		for (int i = 0; i < sunkenVertexNumber-2; i++) {
			indexes.Add (2);
			indexes.Add (i*3+5);
			indexes.Add (i*3+8);
		}
		//-------------外部凸点组成的长方形网格
		for (int i = 0; i < sunkenVertexNumber-1; i++) {
			indexes.Add (i*3+2);
			indexes.Add (i*3+3);
			indexes.Add (i*3+4);

			indexes.Add (i*3+2);
			indexes.Add (i*3+4);
			indexes.Add (i*3+5);
		}
		indexes.Add (vertices.Count-2);
		indexes.Add (0);
		indexes.Add (1);

		indexes.Add (vertices.Count-2);
		indexes.Add (1);
		indexes.Add (2);
		//----------------------------------------分割三角形--------------------------------------------

		//----------------------------------------添加圆角--------------------------------------------
		float dtheta =10f;
		for (int i = 0; i < sunkenVertexNumber; i++) {
			Vector3 origin = vertices [3 * i + 2];
			Vector3 left = vertices [3 * i+1];
			Vector3 right = vertices [3 * i + 3];
			Vector3 current = left - origin;

			current = Rotate (current, -dtheta);
			Vector3 result = current + origin;
			vertices.Add (result);
			indexes.Add (3 * i + 2);
			indexes.Add (3 * i+1); 
			indexes.Add (vertices.Count-1);

			while(Angle (right - origin,current)>dtheta){
//				Debug.Log ("right \\/ current"+Angle (right - origin,current));
				current = Rotate (current, -dtheta);
				result = current + origin;
				vertices.Add (result);
				indexes.Add (3 * i + 2);
				indexes.Add (vertices.Count-2);
				indexes.Add (vertices.Count-1);
//				Debug.Log ("vertices .count is "+vertices.Count);
			}

 			indexes.Add (3 * i + 2);
			indexes.Add (vertices.Count-1);
			indexes.Add (3 * i+3); 
		}
		//----------------------------------------添加圆角--------------------------------------------

		//----------------------------------------添加网格然后渲染--------------------------------------------
		Mesh mesh = gameObject.GetComponent<MeshFilter> ().mesh;
		Debug.Log (vertices.Count);
		mesh.triangles = indexes.ToArray ();
		mesh.vertices = this.vertices.ToArray ();
		mesh.RecalculateNormals();  
		mesh.RecalculateBounds(); 
		//----------------------------------------添加网格然后渲染--------------------------------------------
	}

	private float Angle(Vector3 start,Vector3 end){
		//XXX not very sure whether this will result in any bug
		if (start.Equals (new Vector3 ())||end.Equals (new Vector3()))
			return 0;
		float sign = Vector3.Dot (Vector3.Cross (start, end), new Vector3 (0,0,-1));
		if (sign > 0)
			return -1*Vector3.Angle (start, end);
		else
			return Vector3.Angle (start, end);
	}

	private Vector3 Rotate(Vector3 current,float theta){
		theta = Mathf.PI * theta / 180;
		return  new Vector3 (current.x * Mathf.Cos (theta) - current.y * Mathf.Sin (theta), current.y * Mathf.Cos (theta) + current.x * Mathf.Sin (theta));
	}

	//DEBUG
	public GameObject[] stars;
	void Start(){
//		int[] IDs = new int[]{0,1,2,3,4,5,6};
//		StartCoroutine (Calculate (IDs));
	}
	public GameObject getStar(int ID){
		if (ID < stars.Length)
			return stars [ID];
		else
			return null;
	}

}

class PointCompareDis : System.Collections.Generic.IComparer<Vector3>{
	Vector3 referencePoint;
	public PointCompareDis(Vector3 _referencePoint){
		this.referencePoint = _referencePoint;
	}
	int IComparer<Vector3>.Compare(Vector3 x,Vector3 y){
		return (int)((x - referencePoint).magnitude - (y - referencePoint).magnitude);
	}
}