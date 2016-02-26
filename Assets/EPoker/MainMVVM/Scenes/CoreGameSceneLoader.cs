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
    
    
    public class CoreGameSceneLoader : CoreGameSceneLoaderBase {
        
        protected override IEnumerator LoadScene(CoreGameScene scene, Action<float, string> progressDelegate) {
            yield break;
        }
        
        protected override IEnumerator UnloadScene(CoreGameScene scene, Action<float, string> progressDelegate) {
            yield break;
        }
    }
}
