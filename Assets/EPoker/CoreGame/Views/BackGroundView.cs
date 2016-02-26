namespace yigame.epoker {
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
    
    
    public class BackGroundView : BackGroundViewBase {
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as BackGroundViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.BackGround to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }

		protected override void OnEnable ()
		{
			base.OnEnable ();
			AutoSetScale ();
		}

		void AutoSetScale ()
		{
			SpriteRenderer sr = GetComponent<SpriteRenderer> ();

			float camera_height = Camera.main.orthographicSize * 2f;
			float camera_width = camera_height * Camera.main.aspect;
			float sprite_pixel_per_unit = sr.sprite.pixelsPerUnit;
			float scale;
			if (sr.sprite.rect.width / sr.sprite.rect.height < Camera.main.aspect) {
				scale = camera_width / (sr.sprite.rect.width / sprite_pixel_per_unit);
			} else {
				scale = camera_height / (sr.sprite.rect.height / sprite_pixel_per_unit);
			}

			transform.localScale = Vector3.one * scale;
		}
    }
}
