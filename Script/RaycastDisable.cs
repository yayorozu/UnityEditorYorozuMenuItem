using UnityEditor;
using UnityEngine.UI;

namespace Yorozu.EditorTool
{
	internal static class RaycastDisable
	{
		[MenuItem(YorozuMenuItem.ToolPath + "Set Child Raycast Disable")]
		private static void SetRaycastDisable()
		{
			foreach (var obj in Selection.gameObjects)
			{
				var graphics = obj.GetComponentsInChildren<Graphic>();
				foreach (var graphic in graphics)
				{
					graphic.raycastTarget = false;
				}
			}
		}

		// ボタンがあれば Raycast を false にしない
		[MenuItem(YorozuMenuItem.ToolPath + "Set Child Raycast Disable No Button")]
		private static void SetRaycastDisableNoButton()
		{
			foreach (var obj in Selection.gameObjects)
			{
				var graphics = obj.GetComponentsInChildren<Graphic>();
				foreach (var graphic in graphics)
				{
					if (graphic.GetComponent<Button>() != null)
						continue;

					graphic.raycastTarget = false;
				}
			}
		}
	}
}
