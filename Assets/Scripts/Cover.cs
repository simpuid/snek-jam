using System;

using Scriptable;

using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Cover : MonoBehaviour {
	public Level level;
	public float thickness;

	private void Awake() {
		GetComponent<MeshFilter>().mesh = Build(level.bound, thickness);
	}

	private static Mesh Build(RectInt rect, float thickness) {
		var mesh = new Mesh();
		var offset = new Vector3(-0.5f, -0.5f);
		var vertices = new Vector3[] {
			new Vector3(rect.xMin, rect.yMin) + offset,
			new Vector3(rect.xMin, rect.yMax) + offset,
			new Vector3(rect.xMax, rect.yMax) + offset,
			new Vector3(rect.xMax, rect.yMin) + offset,

			new Vector3(rect.xMin, rect.yMin) + offset + new Vector3(-thickness, -thickness),
			new Vector3(rect.xMin, rect.yMax) + offset + new Vector3(-thickness, thickness),
			new Vector3(rect.xMax, rect.yMax) + offset + new Vector3(thickness, thickness),
			new Vector3(rect.xMax, rect.yMin) + offset + new Vector3(thickness, -thickness),
		};

		var triangles = new int[] {
			0, 4, 1, 1, 4, 5,
			1, 5, 2, 5, 6, 2,
			7, 2, 6, 7, 3, 2,
			7, 4, 3, 3, 4, 0
		};
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return mesh;
	}
}
