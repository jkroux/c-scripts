using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=rQG9aUWarwE
// Used this video to help construct cone

public class VisionConePlayer : MonoBehaviour {
	public float viewRadius;
	public float meshResolution;
	[Range(0,360)]
	public float viewAngle;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	public List<Transform> visibleTargets = new List<Transform> ();
	public MeshFilter viewMeshFilter;
	Mesh viewMesh;

	IEnumerator FindTargetsWithDelay(float delay){
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	
	}

	void DrawFieldOfView(){
		int stepCount = Mathf.RoundToInt(viewAngle*meshResolution);
		float stepAngleSize=viewAngle/stepCount;
		List<Vector2> viewPoint = new List<Vector2> ();
		for (int i = 0; i <= stepCount; i++) {
			float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
			ViewCastInfo newViewCast = ViewCast (angle);
			viewPoint.Add (newViewCast.point);
		}
		int vertexCount = viewPoint.Count+ 1;
		Vector2[] vertices = new Vector2[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];
		vertices [0] = Vector2.zero;
		for (int i = 0; i < vertexCount - 1; i++) {
			vertices [i + 1] = viewPoint [i];
			if (i<vertexCount-2){
				triangles[i*3]=0;
				triangles [i * 3 + 1] = i + 1;
				triangles [i * 3 + 2] = i + 2;
			}
		}
	}		

	void FindVisibleTargets(){
		Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll (transform.position, viewRadius, targetMask);
		for (int i=0; i< targetsInViewRadius.Length; i++){
			Transform target = targetsInViewRadius [i].transform;
			Vector2 dirToTarget = (target.position - transform.position).normalized;
			if (Vector2.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector2.Distance (transform.position, target.position);
				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					visibleTargets.Add (target);
				}
			}
		}
	}
	ViewCastInfo ViewCast(float globalAngle) {
		Vector2 dir = Angle (globalAngle, true);
		RaycastHit hit;
		if (Physics2D.Raycast(transform.position, dir,  hit, viewRadius, obstacleMask)){
			return new ViewCastInfo (true, hit.point, hit.distance, globalAngle);
		}
		else{
			return new ViewCastInfo (false, (Vector2) (transform.position + dir * viewRadius), viewRadius, globalAngle);
		}
	}

	public Vector2 Angle(float angleInDegrees, bool angleIsGlobal){
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
			
		return new Vector2 (Mathf.Sin (angleInDegrees * Mathf.Deg2Rad), Mathf.Cos (angleInDegrees * Mathf.Deg2Rad));
	}
	public struct ViewCastInfo {
		public bool hit;
		public Vector2 point;
		public float dst;
		public float angle;
	public ViewCastInfo (bool _hit, Vector2 _point, float _dst, float _angle){
		hit = _hit;
		point = _point;
		dst= _dst;
		angle=_angle;
	}
	}

	// Use this for initialization
	void Start () {
		viewMesh = new Mesh();
		viewMesh.name = "View Mesh";
		viewMeshFilter.mesh = viewMesh;
		StartCoroutine ("FindTargetsWithDelay", .2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
