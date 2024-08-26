namespace Ecs.Services
{
    public class PoolNode<T>
    {
        public T Value;
        public bool IsActive;

        public PoolNode(T value, bool isActive)
        {
            Value = value;
            IsActive = isActive;
        }
    }
}