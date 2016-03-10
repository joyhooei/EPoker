namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.Kernel;
	using uFrame.IOC;
	using uFrame.MVVM;
	using uFrame.Serialization;
	using UniRx;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	public class CoreGameRootController : CoreGameRootControllerBase
	{

		public override void InitializeCoreGameRoot (CoreGameRootViewModel viewModel)
		{
			base.InitializeCoreGameRoot (viewModel);
			// This is called when a CoreGameRootViewModel is created
		}

		public static string posid_arrange_player_count = @"{
			2: [0,3],
			3: [0,2,4],
			4: [0,1,3,5],
			5: [0,1,2,4,5],
			6: [0,1,2,3,4,5]
		}";

		public override void ResetPlayerCount (CoreGameRootViewModel viewModel, int arg)
		{
			base.ResetPlayerCount (viewModel, arg);

			JObject jArrange = JObject.Parse (posid_arrange_player_count);
			JArray jArrangeItem = jArrange [arg.ToString ()] as JArray;

			viewModel.PlayerCount = arg;

			viewModel.PlayerCollection.Clear ();

			for (int i = 0; i < arg; i++) {
				PlayerViewModel player = MVVMKernelExtensions.CreateViewModel<PlayerViewModel> ();
				player.PosId = jArrangeItem [i].Value<string> ();

				viewModel.PlayerCollection.Add (player);
			}
		}
	}
}
