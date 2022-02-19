
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{

	//void OnSceneGUI() Circle
	//{
	//	FieldOfView fow = (FieldOfView)target;
	//	Handles.color = Color.white;
	//	Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
	//	Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
	//	Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

	//	Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
	//	Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

	//	Handles.color = Color.red;
	//	foreach (Transform visibleTarget in fow.visibleTargets)
	//	{
	//		Handles.DrawLine(fow.transform.position, visibleTarget.position);
	//	}
	//}
    void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

        Handles.DrawWireArc(fov.transform.position, Vector3.up, viewAngleA, fov.viewAngle, fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);
		
		Handles.color = Color.red;
		foreach (Transform visibleTarget in fov.visibleTargets)
		{
			Handles.DrawLine(fov.transform.position, visibleTarget.position);
		}
	}
}