using System.Collections;
using System.Collections.Generic;

public static class QueueExtensions
{
    public static bool TryPeek<T>(this Queue<T> queue, out T result)
    {
        try
        {
            result = queue.Peek();
            return true;
        }
        catch
        {
            result = default(T);
            return false;
        }
    }

    public static bool TryDequeue<T>(this Queue<T> queue, out T result)
    {
        try
        {
            result = queue.Dequeue();
            return true;
        }
        catch
        {
            result = default(T);
            return false;
        }
    }
}
