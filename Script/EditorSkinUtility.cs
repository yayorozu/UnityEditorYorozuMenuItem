using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;

namespace Yorozu.EditorTool
{
	public static class EditorSkinUtility
	{
		private static void ChangeSkin()
		{
			typeof(EditorGUIUtility).InvokeMember("Internal_SwitchSkin", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, null, null);
			// 適応するために static インスタンスの初期化が必要なのでコンパイルする
			CompilationPipeline.RequestScriptCompilation();
		}

		[MenuItem(YorozuMenuItem.ToolPath + "ChangeDefaultSkin")]
		private static void ChangeDefaultSkin()
		{
			if (!EditorGUIUtility.isProSkin)
				return;

			ChangeSkin();
		}

		[MenuItem(YorozuMenuItem.ToolPath + "ChangeProSkin")]
		private static void ChangeProSkin()
		{
			if (EditorGUIUtility.isProSkin)
				return;

			ChangeSkin();
		}
	}
}
