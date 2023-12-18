class MyQue<T> where T : IComparable<T>
{
    private Stack<T> _stack;
    private Stack<T> _stack2;
    public MyQue()
    {
        _stack = new Stack<T>();
        _stack2 = new Stack<T>();
    }
    public void Enque(T item)
    {
        _stack.Push(item);
    }
    public T Deque() 
    {
        ReverseStack();
        if (_stack2.Count > 0)
        {
            return _stack2.Pop();
        }
        else throw new ArgumentException("que is empty");
    }
    public T GetMaxValue()
    {
        ReverseStack();
        if (_stack2.Count > 0)
        {
            return _stack2.Max();
        }
        else throw new ArgumentException("que is empty");
    }
    private void ReverseStack()
    {
        while (_stack.Count > 0)
        {
            _stack2.Push(_stack.Pop());
        }
    }
}