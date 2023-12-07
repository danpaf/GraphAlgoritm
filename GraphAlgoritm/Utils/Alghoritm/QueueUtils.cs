using System;
using System.Collections.Generic;

namespace GraphAlgoritm.Utils.Alghoritm;

public class QueueUtils<T>
{
    private List<Tuple<T, int>> data;

    public QueueUtils()
    {
        this.data = new List<Tuple<T, int>>();
    }

    public void Enqueue(T item, int priority)
    {
        this.data.Add(Tuple.Create(item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;

        for (int i = 0; i < this.data.Count; i++)
        {
            if (this.data[i].Item2 < this.data[bestIndex].Item2)
            {
                bestIndex = i;
            }
        }

        T bestItem = this.data[bestIndex].Item1;
        this.data.RemoveAt(bestIndex);
        return bestItem;
    }
}