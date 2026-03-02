using System.Numerics;
using Smash.EntityComponentSystem;

namespace Smash;

public interface IGrid<out T> where T : Entity
{
    public IEnumerable<T> GridEntities { get; }
    public T? GetElementAtPosition(Vector2 position);

    public float CellWidth { get; }
    public float CellHeight { get; }
}