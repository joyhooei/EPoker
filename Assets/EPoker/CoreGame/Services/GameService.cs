namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.MVVM;
	using uFrame.IOC;
	using uFrame.Kernel;
	using UniRx;
	using UnityEngine;
	using Newtonsoft.Json;

    
	public class GameService : GameServiceBase
	{
		public List<CardInfo> GetDeck (bool disorder)
		{
			List<CardInfo> card_info_list = new List<CardInfo> ();
			for (int s = 0; s < 4; s++) {
				for (int n = 0; n < 13; n++) {
					card_info_list.Add (new CardInfo ((Suit)s, (NumericalValue)n));
				}
			}
			card_info_list.Add (new CardInfo (Suit.SMALL_JOKER, NumericalValue.NV_SMALL_JOKER));
			card_info_list.Add (new CardInfo (Suit.BIG_JOKER, NumericalValue.NV_BIG_JOKER));

//			card_info_list.ForEach (ci => {
//				string s = ci.ToString ();
//				CardInfo ci_p = CardInfo.Parse (s);
//				Debug.Log (JsonConvert.SerializeObject (ci_p));
//			});

			if (disorder) {
				card_info_list = card_info_list.OrderBy (x => Guid.NewGuid ()).ToList ();
			}

			return card_info_list;
		}

		public override void UploadInfoJsonHandler (UploadInfoJson data)
		{
			base.UploadInfoJsonHandler (data);
			// 将 CoreGameRoot InfoJson 数据上传房间属性
		}

		public override void OnInfoJsonUpdateHandler (OnInfoJsonUpdate data)
		{
			base.OnInfoJsonUpdateHandler (data);
		}

		public override void OpenCoreGameHandler (OpenCoreGame data)
		{
			base.OpenCoreGameHandler (data);
			Publish (new LoadSceneCommand () {
				SceneName = "CoreGameScene"
			});
		}

		public override void CloseCoreGameHandler (CloseCoreGame data)
		{
			base.CloseCoreGameHandler (data);
			Publish (new UnloadSceneCommand () {
				SceneName = "CoreGameScene"
			});
		}
	}
}
