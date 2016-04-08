// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by the Game Data Editor.
//
//      Changes to this file will be lost if the code is regenerated.
//
//      This file was generated from this data file:
//      /Users/EthanW/Documents/UnityProjects/EPoker/Assets/GameDataEditor/Resources/gde_data.txt
//  </autogenerated>
// ------------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections.Generic;

using GameDataEditor;

namespace GameDataEditor
{
    public class GDESConfigData : IGDEData
    {
        static string GameModeKey = "GameMode";
		string _GameMode;
        public string GameMode
        {
            get { return _GameMode; }
            set {
                if (_GameMode != value)
                {
                    _GameMode = value;
					GDEDataManager.SetString(_key, GameModeKey, _GameMode);
                }
            }
        }

        public GDESConfigData(string key) : base(key)
        {
            GDEDataManager.RegisterItem(this.SchemaName(), key);
        }
        public override Dictionary<string, object> SaveToDict()
		{
			var dict = new Dictionary<string, object>();
			dict.Add(GDMConstants.SchemaKey, "SConfig");
			
            dict.Merge(true, GameMode.ToGDEDict(GameModeKey));
            return dict;
		}

        public override void UpdateCustomItems(bool rebuildKeyList)
        {
        }

        public override void LoadFromDict(string dataKey, Dictionary<string, object> dict)
        {
            _key = dataKey;

			if (dict == null)
				LoadFromSavedData(dataKey);
			else
			{
                dict.TryGetString(GameModeKey, out _GameMode);
                LoadFromSavedData(dataKey);
			}
		}

        public override void LoadFromSavedData(string dataKey)
		{
			_key = dataKey;
			
            _GameMode = GDEDataManager.GetString(_key, GameModeKey, _GameMode);
        }

        public GDESConfigData ShallowClone()
		{
			string newKey = Guid.NewGuid().ToString();
			GDESConfigData newClone = new GDESConfigData(newKey);

            newClone.GameMode = GameMode;

            return newClone;
		}

        public GDESConfigData DeepClone()
		{
			GDESConfigData newClone = ShallowClone();
            return newClone;
		}

        public void Reset_GameMode()
        {
            GDEDataManager.ResetToDefault(_key, GameModeKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetString(GameModeKey, out _GameMode);
        }

        public void ResetAll()
        {
            GDEDataManager.ResetToDefault(_key, GameModeKey);


            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            LoadFromDict(_key, dict);
        }
    }
}
