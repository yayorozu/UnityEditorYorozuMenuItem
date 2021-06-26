using UnityEditor;
using UnityEngine;

namespace Yorozu.EditorTool
{
	internal static class MoveDirectory
	{
		[MenuItem(YorozuMenuItem.ToolPath + "Move Directory")]
		private static void MoveAssets()
		{
			var guids = Selection.assetGUIDs;
			if (guids.Length <= 0)
				return;

			var path = EditorUtility.OpenFolderPanel("Select Move Directory", "Assets", "");
			if (string.IsNullOrEmpty(path))
				return;

			var inProjectPath = path.Replace(Application.dataPath, "Assets");
			foreach (var guid in guids)
			{
				var assetPath = AssetDatabase.GUIDToAssetPath(guid);
				var fileName = System.IO.Path.GetFileName(assetPath);
				AssetDatabase.MoveAsset(assetPath, System.IO.Path.Combine(inProjectPath, fileName));
			}
		}

	}
}
