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
	using HedgehogTeam.EasyTouch;

	public class CardTouchVC : CardTouchVCBase
	{
		public CardSelectMode CurrentSelectMode;

		public void OnTouchStart (Gesture gesture)
		{
			PlayerViewModel player = Card.CoreGameRoot.GetPlayerByActorId (Card.OwnerActorId);
			if (player != null && player.Status is MatchDeal) {
				
				CardViewModel vm = GetCard (gesture);
				if (vm != null) {
					// 确定本次的选择模式: 选择 or 取消选择
					if (vm.IsSelected) {
						CurrentSelectMode = CardSelectMode.Deselect;
					} else {
						CurrentSelectMode = CardSelectMode.Select;
					}

					if (CurrentSelectMode == CardSelectMode.Select) {
						vm.ExecuteSelectCard ();
					} else if (CurrentSelectMode == CardSelectMode.Deselect) {
						vm.ExecuteDeselectCard ();
					}

//					Debug.Log ("OnTouchStart " + vm.CardInfoStr);
				}
			}
		}

		public void OnTouchDown (Gesture gesture)
		{
			CardViewModel vm = GetCard (gesture);
			if (vm != null) {

				if (CurrentSelectMode == CardSelectMode.Select) {
					vm.ExecuteSelectCard ();
				} else if (CurrentSelectMode == CardSelectMode.Deselect) {
					vm.ExecuteDeselectCard ();
				}

//				Debug.Log ("OnTouchDown " + vm.CardInfoStr);
			}
		}

		public void OnTouchUp (Gesture gesture)
		{
			CardViewModel vm = GetCard (gesture);
			if (vm != null) {
//				Debug.Log ("OnTouchUp " + vm.CardInfoStr);
			}

			CurrentSelectMode = CardSelectMode.None;
		}

		public void OnCancel ()
		{
			Debug.Log ("OnCancel");
		}

		public CardViewModel GetCard (Gesture gesture)
		{
			Ray ray = Camera.main.ScreenPointToRay ((Vector3)gesture.position);
			RaycastHit2D[] hit2D = Physics2D.GetRayIntersectionAll (ray);

			if (hit2D.Length > 0) {
				CardTouchVC cardTouchVC = hit2D [0].collider.GetComponent<CardTouchVC> ();
				if (cardTouchVC != null) {
					return cardTouchVC.Card;
				}
			}
			return null;
		}
	}
}
