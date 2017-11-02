using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.ComponentModel.Design;
using System.Runtime.Serialization.Formatters;
using System.Xml.Linq;
using System.Net.Sockets;
using System;
using NUnit.Framework.Constraints;

public class Barrier : MonoBehaviour
{
	public LineRenderer debugLine;
	public PolygonCollider2D polygonCollider;
	public float R = 2;
	List<Vector3> vertices;
	Color color = new Color(1,1,0,0.5f);

	/// <summary>
	/// 每一个星球之间形成防护层的间隔的延迟，s
	/// </summary>
	public float PlanetsProtectionInterval = 0;
	/// <summary>
	/// 单个防护层总时间为 36×OneProtectionInterval s
	/// </summary>
	public float OneProtectionInterval = 0.02f;

	void OnTriggerEnter2D(Collider2D collider){
//		if (collider.tag == "stone") {
//			Destroy (collider);
//		} else if (collider.tag == "ship") {
		Destroy (collider.gameObject);
		Debug.Log ("collider : " + collider.name);

//		}
	}

	public void Init(int[] starsIDs){
		StartCoroutine (BarrierAnimation (starsIDs));
		//DEBUG
//		List<Vector3> stars = new List<Vector3>();
//		foreach (int i in new int[]{0,2,4}) {
//			stars.Add (PlanetManager.GetPlanet (i).transform.position);
//		}
//		StartCoroutine (Calculate (stars));
	}

	IEnumerator BarrierAnimation(int[] starsIDs){
		int count = starsIDs.Length;
		if (count > 1) {
			for (int i = 1; i < count; i++) {
				float x = 0;
				while (x <= 1) {
					List<Vector3> starsPositions = new List<Vector3> ();
					for (int k = 0; k <= i; k++) {
						starsPositions.Add (PlanetManager.GetPlanet (starsIDs [k]).transform.position);
					}
					Vector3 targetPosition = starsPositions [starsPositions.Count - 1];
					starsPositions.RemoveAt (starsPositions.Count - 1);
					Vector3 lastPosition = starsPositions [starsPositions.Count - 1];
					x += 0.05f;
					starsPositions.Add (lastPosition * (1 - x) + targetPosition * x);
					yield return StartCoroutine (Calculate (starsPositions));
					yield return new WaitForSeconds (PlanetsProtectionInterval/20);
				}
			}
		} else {
			Debug.Log ("draw one-point mesh for planet : "+starsIDs[0]);
			float currTheta = Mathf.PI/18;
			float dTheta = Mathf.PI / 18;
			Vector3 position = PlanetManager.GetPlanet (starsIDs [0]).transform.position;
			while (currTheta < Mathf.PI*2) {
				ProtectionForOnePlayer (position,currTheta);
				currTheta += dTheta;
				yield return new WaitForSeconds (OneProtectionInterval);
			}
		}

		StartCoroutine (FadeAway (starsIDs.Length));
		Debug.Log ("start fade away color");
	}

	/// <summary>
	/// 只适用于两个以上星球
	/// </summary>
	/// <param name="starsPositions">Stars positions.</param>
	public IEnumerator Calculate(List<Vector3> starsPositions){
		vertices = new List<Vector3> ();
		polygonCollider.pathCount = 0;

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
		while ((!start.Equals (current))||startMask) {yield return null;if(startMask) startMask = false;
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
		bool startFlag = true;
		for (int i = 0; i < vertices.Count; i+=3) {
			int nextVertex = (i+1)%vertices.Count;
			int prevVertex = (i - 1 + vertices.Count) % vertices.Count;
//			Vector3 leftEdge = vertices[prevVertex] - vertices [i];
//			Vector3 rightEdge = vertices [nextVertex] - vertices [i];
//			Vector3 right = new Vector3(rightEdge.y,-rightEdge.x);
//			Vector3 left = new Vector3 (-leftEdge.y, +leftEdge.x);
//			Vector3 rightPoint = vertices [i] + right.normalized * R;
//			Vector3 leftPoint = vertices [i] + left.normalized * R;
//			vertices.Insert (i,leftPoint);
//			vertices.Insert (i+2,rightPoint);

			//DEBUG
			if (startFlag)
				startFlag = false;
			else {
				nextVertex = (i+2)%vertices.Count;
				prevVertex = (i - 2 + vertices.Count) % vertices.Count;
			}
				
			Vector3 leftEdge = vertices[nextVertex] - vertices [i];
			Vector3 rightEdge = vertices [prevVertex] - vertices [i];
			Vector3 right = new Vector3(-rightEdge.y,rightEdge.x);
			Vector3 left = new Vector3 (leftEdge.y, -leftEdge.x);
			Vector3 rightPoint = vertices [i] + right.normalized * R;
			Vector3 leftPoint = vertices [i] + left.normalized * R;
			vertices.Insert (i,rightPoint);
			vertices.Insert (i+2,leftPoint);
			yield return null;
		}
		Vector3 head = vertices [0];
		vertices.Add (head);
		//----------------------------------------对边的坐标修正--------------------------------------------

		// 多边形Collider
		List<Vector2> colliderPoint = new List<Vector2>();
		foreach (Vector3 point in vertices) {
			colliderPoint.Add (point);
		}
		AddPolygonColliderElement (colliderPoint.ToArray ());

		//DEBUG
		//用找到的顶点画多边形
				debugLine.positionCount = vertices.Count;
				debugLine.SetPositions (vertices.ToArray ());

		Render ();
	}

	public void Render(){
		// 上面得到的是逆时针方向绕点，根据unity中mesh的要求，必须为逆时针，所以反向
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
//			if (i == 0)
//				continue;
			Vector3 origin = vertices [3 * i + 2];
			Vector3 left = vertices [3 * i+1];
			Vector3 right = vertices [3 * i + 3];
			Vector3 current = left - origin;
			int baseCount = vertices.Count;

			current = Rotate (current, -dtheta);
			Vector3 result = current + origin;
			vertices.Add (result);
			indexes.Add (3 * i + 2);																														// 中间凹点
			indexes.Add (3 * i+1); 																														// 左上角点
			indexes.Add (vertices.Count-1);																									// 新得到的弧线点

			while(Angle (right - origin,current)>dtheta){
				current = Rotate (current, -dtheta);
				result = current + origin;
				vertices.Add (result);
				indexes.Add (3 * i + 2);																													// 中间凹点
				indexes.Add (vertices.Count-2);																								// 上一个弧线点
				indexes.Add (vertices.Count-1);																								// 新得到的弧线点
			}

			indexes.Add (3 * i + 2);																														// 中间凹点
			indexes.Add (vertices.Count-1);																									// 新的到的弧线点
			indexes.Add (3 * i+3); 																														// 右上角点

			// 多边形Collider
			List<Vector2> colliderPoint = new List<Vector2> ();
			colliderPoint.Add (vertices[3 * i + 2]);																						// 左上角点
			for(int k = baseCount;k<vertices.Count;k++){
				colliderPoint.Add (vertices[k]);																									// 中间弧线点
			}

			colliderPoint.Add (vertices[3 * i + 3]);																						// 右上角点

			AddPolygonColliderElement (colliderPoint.ToArray ());
		}
		//----------------------------------------添加圆角--------------------------------------------

		//----------------------------------------添加网格然后渲染--------------------------------------------
		Mesh mesh = new Mesh();
		GetComponent<MeshFilter> ().mesh = mesh;
		mesh.vertices = this.vertices.ToArray ();
		mesh.triangles = indexes.ToArray ();
		mesh.RecalculateNormals();  
		mesh.RecalculateBounds(); 
		//----------------------------------------添加网格然后渲染--------------------------------------------
	}

	//----------------------------------------添加多边形Collider--------------------------------------------
	private void AddPolygonColliderElement(Vector2[] points){
		polygonCollider.pathCount = polygonCollider.pathCount + 1;
		polygonCollider.SetPath (polygonCollider.pathCount-1,points);
	}
	//----------------------------------------添加多边形Collider--------------------------------------------

	public void ProtectionForOnePlayer(Vector3 position,float targetTheta){
		List<Vector3> points = new List<Vector3> ();
		points.Add (position);
		float dtheta = Mathf.PI / 18;
		for(float theta = 0;theta>=-targetTheta;theta-=dtheta){
			Vector3 result = R * new Vector3 (Mathf.Cos (theta),Mathf.Sin (theta),0)+position;
			points.Add (result);
		}
//		points.Add (position+new Vector3(R,0,0));

		List<int> indices = new List<int> ();
		for (int i = 1; i < points.Count-1; i++) {
			indices.Add (0);
			indices.Add (i);
			indices.Add (i+1);
		}
//		indices.Add (0);
//		indices.Add (points.Count-1);
//		indices.Add (1);


		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;
		mesh.vertices = points.ToArray ();
		mesh.triangles = indices.ToArray ();
		mesh.RecalculateNormals();  
		mesh.RecalculateBounds(); 

		List<Vector2> colliderPoints = new List<Vector2> ();
		foreach (Vector3 point in points) {
			colliderPoints.Add (point);
		}
		colliderPoints.RemoveAt (0);//0 is origin point
//		colliderPoints.Add (colliderPoints[0]);//1 is first point
		polygonCollider.pathCount += 1;
		polygonCollider.SetPath (polygonCollider.pathCount-1,colliderPoints.ToArray ());
	}

	IEnumerator FadeAway(int planetsNumber){
		float time = planetsNumber * 4;
		float intervalTime = 0.3f;
		float dColorRatio = intervalTime / time;
		Material material = GetComponent<Renderer> ().material;
		Debug.Log ("planet number is : "+planetsNumber+" , and will be destroyed after "+time);
		while (time > 0) {
			time -= intervalTime;
			material.color += new Color (0, 0, dColorRatio, -dColorRatio * 0.5f);
			yield return new WaitForSeconds (intervalTime);
		}

		Destroy (gameObject);
	}

	//XXX 效果不够好，暂时放弃
	IEnumerator OnCollision(){
		float animationTime = 0.3f;
		float dTime = 0.05f;
		Color ColorChange = new Color(0,0,0.00f);
		Color dcolor = ColorChange * dTime / animationTime;
		Material material = GetComponent<Renderer> ().material;
		while (animationTime > 0) {
			animationTime -= dTime;
//			material.color += dcolor;
//			if (material.color.b > 1)
//				material.color = new Color (material.color.r,material.color.g,1f,material.color.a);
			Debug.Log ("now material color is : "+material.color);
			yield return new WaitForSeconds (dTime);
		}

		material.color -= ColorChange;
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

	/// <summary>
	/// Rotate the Vector3 current by theta in anticlockwise.
	/// </summary>
	/// <param name="current">Current.</param>
	/// <param name="theta">Theta.In Radian</param>
	private Vector3 Rotate(Vector3 current,float theta){
		theta = Mathf.PI * theta / 180;
		return  new Vector3 (current.x * Mathf.Cos (theta) - current.y * Mathf.Sin (theta), current.y * Mathf.Cos (theta) + current.x * Mathf.Sin (theta));
	}

	//DEBUG
	public GameObject[] stars;
	void Start(){
		GetComponent<Renderer> ().material.color = color;
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