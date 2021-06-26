using System.IO;
using UnityEditor;

namespace Yorozu.EditorTool
{
	public class AddDefine
	{
		[MenuItem(YorozuMenuItem.ToolPath + "Add if UNITY_Editor")]
		private static void AddEditorDefine()
		{
			var guids = Selection.assetGUIDs;
			foreach (var guid in guids)
			{
				var path = AssetDatabase.GUIDToAssetPath(guid);
				var ext = Path.GetExtension(path);
				if (ext != ".cs")
					continue;

				var lines = File.ReadAllLines(path);
				if (HasDefine(lines))
					return;

				ArrayUtility.Insert(ref lines, 0, "#if UNITY_EDITOR");
				ArrayUtility.Insert(ref lines, lines.Length, "#endif");
				File.WriteAllLines(path, lines);
				AssetDatabase.ImportAsset(path);
			}
		}

		private static bool HasDefine(string[] lines)
		{
			foreach (var line in lines)
			{
				var trim = line.Trim();
				if (trim.StartsWith("#if UNITY_EDITOR"))
				{
					return true;
				}
				if (trim.StartsWith("namespace") || trim.StartsWith("public"))
				{
					return false;
				}
			}
			return false;
		}
	}
}
