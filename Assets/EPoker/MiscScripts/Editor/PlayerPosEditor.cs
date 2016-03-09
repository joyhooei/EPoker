using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(PlayerPos))]
public class PlayerPosEditor : Editor
{
	void OnSceneGUI ()
	{
		Handles.color = Color.yellow;
		PlayerPos playerPos = target as PlayerPos;
		Handles.RectangleCap (0, playerPos.transform.position, Quaternion.identity, .5f);
		GUIStyle style_s = new GUIStyle ();
		style_s.normal.textColor = Color.yellow;
		Handles.Label (playerPos.transform.position, playerPos.PositionId.ToString (), style_s);
	}

}
