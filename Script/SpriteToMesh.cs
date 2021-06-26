using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Yorozu.EditorTool
{
	public class SpriteToMesh
	{
		[MenuItem(YorozuMenuItem.ToolPath + "SpriteToMesh")]
		private static void Do()
		{
			var guids = Selection.assetGUIDs;
			foreach (var guid in guids)
			{
				var path = AssetDatabase.GUIDToAssetPath(guid);
				GenerateMesh(path);
			}
		}

		private static void GenerateMesh(string path)
		{
			var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
			if (sprite == null)
			{
				Debug.Log("Select Asset is not Sprite. " + path);
				return;
			}

			var mesh = new Mesh
			{
				vertices = sprite.vertices.Select(v => (Vector3) v).ToArray(),
				uv = sprite.uv,
				triangles = sprite.triangles.Select(t => (int) t).ToArray()
			};

			mesh.RecalculateBounds();
			mesh.RecalculateNormals();
			mesh.RecalculateTangents();

			var savePath = EditorUtility.SaveFilePanelInProject(
				"Select Save Path",
				sprite.name + "_mesh",
				"asset",
				"Select Save Path",
				System.IO.Path.GetDirectoryName(path)
			);

			if (string.IsNullOrEmpty(savePath))
				return;

			AssetDatabase.CreateAsset(mesh, savePath);
			AssetDatabase.Refresh();
		}
	}
}
