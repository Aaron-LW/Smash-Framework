namespace Smash.EntityComponentSystem;

public class Entity
{
    private static int _nextId = 0;
    public int Id;

    protected List<Component> _components = new List<Component>();

    public Entity(Component[]? components = null)
    {
        if (components != null) _components = components.ToList();

        Id = _nextId++;
    }

    public T GetComponent<T>() where T : Component
    {
        foreach (Component component in _components)
        {
            if (component.GetType() == typeof(T))
            {
                return (T)component;
            }
        }

        return null!;
    }

    public T? TryGetComponent<T>() where T : Component
    {
        foreach (Component component in _components)
        {
            if (component.GetType() == typeof(T))
            {
                return (T)component;
            }
        }

        return null;
    }

    public void SetComponent(Component component)
    {
        _components.Add(component);
    }

    public virtual Entity Clone()
    {
        return new Entity(_components.Select(c => c.Clone()).ToArray());
    }

    public void ListComponents()
    {
        foreach (Component component in _components)
        {
            Console.WriteLine(component.GetType());
        }
    }
}