using System.Collections.Generic;

using Scriptable;

using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Ground : MonoBehaviour {
	public float cellSize;
	public Level level;

	private void Awake() {
		GetComponent<MeshFilter>().mesh = Build(level.bound, cellSize);
	}

	private static Mesh Build(RectInt rect, float size) {
		var mesh = new Mesh();
		var vertices = new List<Vector3>();
		var triangles = new List<int>();
		var halfSize = size * 0.5f;
		var offset = new Vector2[4] {
			new Vector2(-halfSize, -halfSize),
			new Vector2(-halfSize, halfSize),
			new Vector2(halfSize, halfSize),
			new Vector2(halfSize, -halfSize)
		};
		var triangleOffset = new int[6] {
			0, 1, 2, 0, 2, 3
		};
		foreach (var point in rect.allPositionsWithin) {
			if ((point.x + point.y) % 2 == 0) {
				continue;
			}

			var index = vertices.Count;
			foreach (var off in offset) {
				vertices.Add(point + off);
			}

			foreach (var off in triangleOffset) {
				triangles.Add(index + off);
			}
		}

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return mesh;
	}
}
