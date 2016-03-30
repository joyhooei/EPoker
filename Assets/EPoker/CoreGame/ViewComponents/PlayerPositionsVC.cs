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
	using Unity.Linq;

	public class PlayerPositionsVC : PlayerPositionsVCBase
	{

		public override void Bind (ViewBase view)
		{
			base.Bind (view);

			CoreGameRoot.PosIdPosition.Clear ();

			gameObject.Descendants ().OfComponent<PlayerPos> ().ToList ().ForEach (pp => {
				CoreGameRoot.PosIdPosition.Add (pp.gameObject.name, pp.transform.position);
			});

//			for (int i = 0; i < transform.childCount; i++) {
//				Transform t = transform.GetChild (i);
//				CoreGameRoot.PosIdPosition.Add (t.gameObject.name, t.position);
//			}
//
////			Debug.Log (JsonConvert.SerializeObject (CoreGameRoot.PosIdPosition));
		}
	}
}
