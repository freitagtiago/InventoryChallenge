public interface IDragSource<T> where T : class
{
    public T GetItem();
    public int GetNumber();
    public void RemoveItems(int number);
}