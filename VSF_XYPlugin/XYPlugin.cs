using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace VSF_XYPlugin
{
    [BepInPlugin(GUID, PluginName, VERSION)]
    public class XYPlugin : BaseUnityPlugin
    {
        public const string GUID = "me.xiaoye97.plugin.VSeeFace.VSF_XYPlugin";
        public const string PluginName = "VSF_XYPlugin";
        public const string VERSION = "1.0.0";
        public static AssetBundle DropItemAB;
        public static GameObject[] DropItemPrefabs;
        public static Transform DropItemRoot;
        public static int DropItemCount;

        public void Start()
        {
            Debug.Log("XYPlugin启动");
            Harmony.CreateAndPatchAll(typeof(MiscPatch));
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                TestAB();
            }
            if (DropItemCount > 0)
            {
                DropItemCount--;
                foreach (var prefab in DropItemPrefabs)
                {
                    Debug.Log($"实例化{prefab.name}");
                    var go = GameObject.Instantiate(prefab, DropItemRoot);
                    go.transform.position += MiscHelper.RandomV3(0.3f);
                    go.AddComponent<DropItemBehaviour>();
                    go.GetComponent<Rigidbody>().velocity = MiscHelper.RandomV3(0.5f);
                }
            }
        }

        public void TestAB()
        {
            if (DropItemAB == null)
            {
                DropItemAB = AssetBundle.LoadFromFile($"{Paths.PluginPath}/VSF_XYPlugin/Assets/dropitem.ab");
                DropItemPrefabs = DropItemAB.LoadAllAssets<GameObject>();
                DropItemRoot = new GameObject("DropItemRoot").transform;
                DropItemRoot.transform.position = new Vector3(0, 5, 0);
                Debug.Log("加载了掉落物AB包，再按一次开始测试掉落");
                return;
            }
            if (DropItemAB != null)
            {
                DropItemCount = 100;
            }
            else
            {
                Debug.LogError("没有加载到AB包");
            }
        }
    }
}
