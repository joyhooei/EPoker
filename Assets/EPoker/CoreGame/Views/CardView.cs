namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.Kernel;
	using uFrame.MVVM;
	using uFrame.MVVM.Services;
	using uFrame.MVVM.Bindings;
	using uFrame.Serialization;
	using UniRx;
	using UnityEngine;
	using Unity.Linq;

	public class CardView : CardViewBase
	{
        
		public Transform MainBody;
		public Transform Shadow;

		protected override void InitializeViewModel (uFrame.MVVM.ViewModel model)
		{
			base.InitializeViewModel (model);
		}

		public override void Bind ()
		{
			base.Bind ();
		}

		public override void AfterBind ()
		{
			base.AfterBind ();
		}

		public override void InfoChanged (CardInfo arg1)
		{
			Debug.LogFormat ("InfoChanged: {0}", arg1);

			string front_sprite_name = arg1.GetCardFrontSpriteName ();
			GameObject prefab = Resources.Load<GameObject> (front_sprite_name);
			GameObject front_go = Instantiate (prefab);
			front_go.transform.SetParent (gameObject.Descendants ("front").Single ().transform);
			front_go.transform.localPosition = Vector3.zero;
		}

		public override void FaceChanged (CardFace arg1)
		{
			List<SpriteRenderer> sr_front = gameObject.Descendants ("front").OfComponent<SpriteRenderer> ().ToList ();
			List<SpriteRenderer> sr_back = gameObject.Descendants ("back").OfComponent<SpriteRenderer> ().ToList ();

			if (arg1 == CardFace.FaceUp) {
				sr_front.ForEach (sr => sr.enabled = true);
				sr_back.ForEach (sr => sr.enabled = false);
			} else {
				sr_front.ForEach (sr => sr.enabled = false);
				sr_back.ForEach (sr => sr.enabled = true);
			}
		}
	}
}
