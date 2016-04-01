namespace yigame.epoker {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.MVVM;
    using uFrame.Serialization;
    using UnityEngine;
    
    
    public class PlayerTestSceneLoader : PlayerTestSceneLoaderBase {
        
        protected override IEnumerator LoadScene(PlayerTestScene scene, Action<float, string> progressDelegate) {
            yield break;
        }
        
        protected override IEnumerator UnloadScene(PlayerTestScene scene, Action<float, string> progressDelegate) {
            yield break;
        }
    }
}
