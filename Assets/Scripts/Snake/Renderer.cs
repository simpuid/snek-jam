using System;
using System.Collections.Generic;

using UnityEngine;

namespace Snake {
	public class Renderer {
		private struct Segment {
			public Vector2 Start;
			public Vector2 End;

			public Segment(Vector2 start, Vector2 end) {
				Start = start;
				End = end;
			}
		}

		private readonly float _width;
		private readonly Mesh _mesh;

		public Renderer(float width, Mesh mesh) {
			_mesh = mesh;
			_width = width;
		}

		public void Render(IEnumerable<Point> points, Vector2 direction, float tailT, float headT) {
			var segments = new List<Segment>();
			using (var enumerator = points.GetEnumerator()) {
				Point? tail = null;
				if (enumerator.MoveNext()) {
					tail = enumerator.Current;
				}

				Point? last = null;
				if (enumerator.MoveNext()) {
					last = enumerator.Current;
				}

				if (tail.HasValue && last.HasValue) {
					segments.Add(TailSegment(tail.Value, last.Value, tailT));
				} else if (tail.HasValue) {
					segments.Add(DotSegment(tail.Value, direction, tailT));
				}

				while (enumerator.MoveNext()) {
					if (last.HasValue) {
						segments.Add(MidSegment(last.Value, enumerator.Current));
					}

					last = enumerator.Current;
				}

				if (last.HasValue) {
					segments.Add(HeadSegment(last.Value, direction, headT));
				}
			}

			Generate(segments);
		}

		private Segment TailSegment(Point tail, Point next, float interpolation) {
			if (tail.Wrap == WrapStatus.WrapStart) {
				return new Segment(tail.Position, tail.Position);
			}

			var intermediate = Vector2.Lerp(tail.Position, next.Position, interpolation);
			return new Segment(intermediate, next.Position);
		}

		private Segment MidSegment(Point start, Point end) {
			return new Segment(start.Position, start.Wrap == WrapStatus.WrapStart ? start.Position : end.Position);
		}

		private Segment HeadSegment(Point head, Vector2 direction, float interpolation) {
			if (head.Wrap == WrapStatus.WrapStart) {
				return new Segment(head.Position, head.Position);
			}

			var intermediate = head.Position + direction * interpolation;
			return new Segment(head.Position, intermediate);
		}

		private Segment DotSegment(Point dot, Vector2 direction, float interpolation) {
			if (dot.Wrap == WrapStatus.WrapStart) {
				return new Segment(dot.Position, dot.Position);
			}

			var intermediate = dot.Position + direction * interpolation;
			return new Segment(intermediate, intermediate);
		}

		private void Generate(List<Segment> points) {
			_mesh.Clear();
			var count = points.Count;
			var vertices = new Vector3[count * 4];
			var triangles = new int[count * 6];
			var index = 0;
			var halfWidth = _width * 0.5f;
			foreach (var segment in points) {
				var min = Vector2.Min(segment.Start, segment.End) - Vector2.one * halfWidth;
				var max = Vector2.Max(segment.Start, segment.End) + Vector2.one * halfWidth;

				vertices[index * 4 + 0] = new Vector3(min.x, min.y);
				vertices[index * 4 + 1] = new Vector3(min.x, max.y);
				vertices[index * 4 + 2] = new Vector3(max.x, max.y);
				vertices[index * 4 + 3] = new Vector3(max.x, min.y);
				triangles[index * 6 + 0] = index * 4 + 0;
				triangles[index * 6 + 1] = index * 4 + 1;
				triangles[index * 6 + 2] = index * 4 + 2;
				triangles[index * 6 + 3] = index * 4 + 0;
				triangles[index * 6 + 4] = index * 4 + 2;
				triangles[index * 6 + 5] = index * 4 + 3;
				index++;
			}

			_mesh.vertices = vertices;
			_mesh.triangles = triangles;
			_mesh.RecalculateNormals();
			_mesh.RecalculateBounds();
		}
	}
}
