public interface IDragDestination<T> where T : class
{
    public int MaxAcceptable(T item);
    public void AddItems(T item, int number);
}