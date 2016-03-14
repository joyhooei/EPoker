namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.Kernel;
	using uFrame.MVVM;
	using uFrame.MVVM.Bindings;
	using uFrame.Serialization;
	using yigame.epoker;
	using UnityEngine;
	using Newtonsoft.Json;

	public class PlayerPositionsVC : PlayerPositionsVCBase
	{

		public override void Bind (ViewBase view)
		{
			base.Bind (view);

			for (int i = 0; i < transform.childCount; i++) {
				Transform t = transform.GetChild (i);
				CoreGameRoot.PosIdPosition.Add (t.gameObject.name, t.position);
			}

//			Debug.Log (JsonConvert.SerializeObject (CoreGameRoot.PosIdPosition));
		}
	}
}
