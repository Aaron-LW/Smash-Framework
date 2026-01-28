using System.Numerics;
using Smash.EntityComponentSystem;

namespace Smash;

public abstract class Grid<GridEntity> : IGrid<GridEntity> where GridEntity : Entity
{
    protected Dictionary<Vector2, GridEntity> GridElements { get; private set; } = new Dictionary<Vector2, GridEntity>();
    public IEnumerable<GridEntity> GridEntities => GetGridElements();

    private float _cellWidth;
    private float _cellHeigth;

    public float CellWidth => _cellWidth * Scale;
    public float CellHeight => _cellHeigth * Scale;
    public Vector2 CellBounds => new Vector2(CellWidth, CellHeight);

    public float Scale { get; private set; }

    public Grid(float cellWidth, float cellHeight, float scale)
    {
        _cellWidth = cellWidth;
        _cellHeigth = cellHeight;
        Scale = scale;
    }

    public Vector2 AlignToGrid(Vector2 position)
    {
        return new Vector2((int)MathF.Floor(position.X / CellWidth) * CellWidth,
                           (int)MathF.Floor(position.Y / CellHeight) * CellHeight);
    }

    public abstract void SetElementAtPosition(Vector2 position, GridEntity? entity = null);

    public virtual GridEntity? GetElementAtPosition(Vector2 position)
    {
        position = AlignToGrid(position);
        GridElements.TryGetValue(position, out GridEntity? gridEntity);
        return gridEntity;

    }

    public GridEntity[] GetGridElements()
    {
        return GridElements.Values.ToArray();
    }
}