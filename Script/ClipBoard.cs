using System;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Yorozu.EditorTool
{
	internal static class ClipBoard
	{
		[MenuItem(YorozuMenuItem.ToolPath + "Select Asset From Clipboard")]
		private static void SelectAsset()
		{
			var path = GUIUtility.systemCopyBuffer;
			if (!path.Contains("Assets/") && !path.Contains("Assets\\"))
				return;

			if (!path.StartsWith("Assets"))
			{
				var index = path.IndexOf("Assets", StringComparison.Ordinal);
				path = path.Substring(index);
			}
			path = path.Replace("\\", "/");
			var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
			if (asset == null)
				return;

			Selection.activeObject = asset;
			EditorGUIUtility.PingObject(asset);
		}

		[MenuItem("GameObject/CopyPath", false, int.MinValue)]
		private static void CopyPath()
		{
			var active = Selection.activeGameObject;
			if (active == null)
				return;

			var obj = active;
			var builder = new StringBuilder(obj.transform.name);
			var current = obj.transform.parent;

			while (current != null)
			{
				builder.Insert(0, current.name + "/");
				current = current.parent;
			}

			GUIUtility.systemCopyBuffer = builder.ToString();
		}
	}
}
