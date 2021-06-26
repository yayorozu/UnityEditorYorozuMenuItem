using UnityEditor;

namespace Yorozu.EditorTool
{
	internal static class ForceDirty
	{
		[MenuItem(YorozuMenuItem.ToolPath + "Force Dirty")]
		private static void ForceDirtyMene()
		{
			var guids = Selection.assetGUIDs;

			if (guids.Length <= 0)
				return;

			foreach (var guid in guids)
			{
				var path = AssetDatabase.GUIDToAssetPath(guid);
				var obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
				EditorUtility.SetDirty(obj);
			}

			AssetDatabase.SaveAssets();
		}

	}
}
