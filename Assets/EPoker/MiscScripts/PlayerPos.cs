using UnityEngine;
using System.Collections;

public class PlayerPos : MonoBehaviour
{
	public int PositionId;

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube (transform.position, Vector3.one * .5f);
	}
}
