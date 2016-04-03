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
	using DG.Tweening;

	public class CardView : CardViewBase
	{
        
		public Transform MainBody;
		public Transform Shadow;

		public Vector3 SelectedOffset;

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

			if (arg1 == null)
				return;

			string front_sprite_name = arg1.GetCardFrontPrefabName ();
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

		public override void LocalPositionChanged (Vector3 arg1)
		{
			transform.localPosition = arg1;
		}

		#region selected status

		public override void SelectedStatusChanged (Invert.StateMachine.State arg1)
		{
			base.SelectedStatusChanged (arg1);
		}

		public override void OnCardInit ()
		{
			base.OnCardInit ();
		}

		public override void OnCardSelected ()
		{
			base.OnCardSelected ();
			DOTween.Kill (this);
			CardTouchVC.transform.DOLocalMove (SelectedOffset, .2f).SetId (this);
		}

		public override void OnCardUnselected ()
		{
			base.OnCardUnselected ();
			DOTween.Kill (this);
			CardTouchVC.transform.DOLocalMove (Vector3.zero, .2f).SetId (this);
		}

		#endregion
	}
}
