using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSee;
using HarmonyLib;
using UnityEngine;

namespace VSF_XYPlugin
{
    /// <summary>
    /// 一些杂项的补丁
    /// </summary>
    public class MiscPatch
    {
		// 屏蔽摄像头的日志，太长了
        [HarmonyPrefix, HarmonyPatch(typeof(OpenSeeWebcamInfo), "ListCameraDetails")]
        public static bool OpenSeeWebcamInfo_ListCameraDetails_Patch(bool includeBlackMagic, ref List<OpenSeeWebcam> __result)
        {
			string str = null;
			string str2;
			if (Environment.Is64BitProcess)
			{
				str2 = OpenSeeWebcamInfo.ListCameraDetails_x64();
				if (includeBlackMagic)
				{
					str = OpenSeeWebcamInfo.ListBlackMagicDetails_x64();
				}
			}
			else
			{
				str2 = OpenSeeWebcamInfo.ListCameraDetails_x86();
				if (includeBlackMagic)
				{
					str = OpenSeeWebcamInfo.ListBlackMagicDetails_x86();
				}
			}
			if (OpenSeeWebcamInfo.dumpJsonStatic)
			{
				//Debug.Log("Camera JSON: " + str2);
				if (includeBlackMagic)
				{
					//Debug.Log("Blackmagic JSON: " + str);
				}
			}
			List<OpenSeeWebcam> list = JsonUtility.FromJson<OpenSeeWebcamInfo.OpenSeeWebcamList>("{\"list\":" + str2 + "}").list;
			foreach (OpenSeeWebcam openSeeWebcam in list)
			{
				openSeeWebcam.type = OpenSeeWebcamType.DirectShow;
			}
			if (includeBlackMagic)
			{
				List<OpenSeeWebcam> list2 = JsonUtility.FromJson<OpenSeeWebcamInfo.OpenSeeWebcamList>("{\"list\":" + str + "}").list;
				foreach (OpenSeeWebcam openSeeWebcam2 in list2)
				{
					openSeeWebcam2.type = OpenSeeWebcamType.Blackmagic;
				}
				list.AddRange(list2);
			}
			__result = list;
			return false;
        }
    }
}
