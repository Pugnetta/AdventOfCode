using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Collections;

public class MaxQueue<T> : IEnumerable<T>
    where T : IComparable<T>
{
    private Stack<(T item, T max)> EnqueueStack { get; } =
      new Stack<(T item, T max)>();

    private Stack<(T item, T max)> DequeueStack { get; } =
      new Stack<(T item, T max)>();

    public int Count =>
      EnqueueStack.Count +
      DequeueStack.Count;

    public void Enqueue(T item) =>
      Push(item, EnqueueStack);

    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException();

        EnsurePopAvailable();
        return DequeueStack.Pop().item;
    }

    public T Max =>
      EnqueueStack.Count > 0 &&
      EnqueueStack.Peek().max is T leftMax
        ? GetMax(leftMax)
        : GetRightMax();

    private T GetMax(T left) =>
      DequeueStack.Count > 0 &&
      DequeueStack.Peek().max is T right &&
      right.CompareTo(left) > 0
        ? right
        : left;

    private T GetRightMax() =>
      DequeueStack.Count > 0
        ? DequeueStack.Peek().max
        : throw new InvalidOperationException();

    private void Push(T item, Stack<(T item, T max)> stack)
    {
        T max =
          stack.Count > 0 &&
          stack.Peek() is (T _, T prevMax) &&
          prevMax.CompareTo(item) > 0
            ? prevMax
            : item;

        stack.Push((item, max));
    }

    private void EnsurePopAvailable()
    {
        if (DequeueStack.Count == 0)
        {
            PourIntoDequeueStack();
        }
    }

    private void PourIntoDequeueStack()
    {
        while (EnqueueStack.TryPop(out (T item, T _) tuple))
        {
            Push(tuple.item, DequeueStack);
        }
    }

    public IEnumerator<T> GetEnumerator() =>
      DequeueStack
        .Concat(EnqueueStack.Reverse())
        .Select(tuple => tuple.item)
        .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
      GetEnumerator();

    public override string ToString() =>
      "[" + string.Join(", ", StringItems) + "]";

    private IEnumerable<string> StringItems =>
      this.Select(item => item.ToString());
}