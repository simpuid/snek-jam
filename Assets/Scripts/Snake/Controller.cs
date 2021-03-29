using System.Collections.Generic;

using Cells;

using Scriptable;

using UnityEngine;

namespace Snake {
	public class Controller {

		private readonly Queue<Point> _queue;
		private Vector2Int _head;
		private Direction _direction;
		private Map _map;
		private Level _level;
		private Game _game;

		public Controller(Map map, Level level, Game game, Vector2Int head, Direction direction, int length) {
			_queue = new Queue<Point>();
			_map = map;
			_level = level;
			_game = game;
			for (var i = length - 1; i > 0; i--) {
				Enqueue(new Point(head + Vector2Int.left * i));
			}

			Enqueue(new Point(head));
			_direction = direction;
		}

		public IEnumerable<Point> GetPoints() {
			return _queue;
		}

		public Direction GetDirection() {
			return _direction;
		}

		public Vector2Int GetHead() {
			return _head;
		}

		public Vector2Int GetNewHead() {
			return _head + _direction.ToVector2Int();
		}

		public void SetDirection(Direction direction) {
			_direction = direction;
		}

		public void Step(Vector2Int position) {
			Enqueue(new Point(position, WrapStatus.Normal));
		}

		public void Wrap(Vector2Int from, Vector2Int to) {
			Enqueue(new Point(from, WrapStatus.WrapStart));
			Enqueue(new Point(to, WrapStatus.WrapEnd));
		}

		public int Count() {
			return _queue.Count;
		}

		public bool Tick(Direction nextDirection, out bool pickDirection) {
			var dead = false;
			pickDirection = false;
			if (IsEating()) {
				_game.IncrementScore();
				var food = _map.GetFood(GetNewHead());
				if (!(food is null)) {
					Object.Destroy(food.gameObject);
				}
			} else {
				Dequeue();
			}

			if (Collide(GetNewHead())) {
				dead = true;
			}

			var cell = _map.GetCell(GetNewHead());
			if (!(cell is null) && cell.Penalty()) {
				_game.Penalty();
			}

			if (cell is WrapCell wrapCell) {
				if (Collide(wrapCell.wrapPosition)) {
					dead = true;
				}

				Wrap(wrapCell.position, wrapCell.wrapPosition);
			} else {
				Step(GetNewHead());
				var direction = (cell is DirectionCell directionCell) ? directionCell.direction : nextDirection;

				if (_direction.ToVector2Int() + direction.ToVector2Int() != Vector2Int.zero) {
					if (cell is DirectionCell) {
						Object.Destroy(cell.gameObject);
						pickDirection = true;
					}

					SetDirection(direction);
				}
			}

			return dead;
		}

		public bool IsEating() {
			return !(_map.GetFood(GetNewHead()) is null);
		}

		public void Clear() {
			while (_queue.Count > 0) {
				Dequeue();
			}
		}

		private bool Collide(Vector2Int position) {
			foreach (var point in _queue) {
				if (point.Position == position) {
					return true;
				}
			}

			return false;
		}

		private void Enqueue(Point point) {
			AddPoint(point);
			_head = point.Position;
			_game.SetHead(_head);
		}

		private void AddPoint(Point point) {
			_queue.Enqueue(point);
			_map.MarkCell(point.Position);
		}

		private void RemovePoint() {
			var point = _queue.Dequeue();
			_map.UnmarkCell(point.Position);
		}

		private void Dequeue() {
			RemovePoint();

			if (Count() > 0 && _queue.Peek().Wrap == WrapStatus.WrapStart) {
				RemovePoint();
			}
		}
	}
}
