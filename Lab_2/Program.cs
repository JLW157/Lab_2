using System.Security.Cryptography;

Heap heap = new Heap(new int[] { 16,14,10,8,7,9,3,2,4,1});

heap.HeapSort();
heap.PrintHeap();

Console.ReadLine();


PriorityQueue<int> queue = new PriorityQueue<int>();
Random rnd = new Random();
//enqueue
for (int i = 0; i < 10; i++)
{
    int x = rnd.Next(3);
    int y = i;
    queue.Enqueue(x, y);
}
queue.Print();
Console.WriteLine();

//dequeue
while (queue.Count > 0)
{
    Console.WriteLine(queue.Dequeue());
}

queue.Print();

Console.ReadLine();


public class Heap
{
    public int[] A;
    public int heapSize; 
    
    //constructor
    public Heap(int[] array)
    {
        A = array;
        heapSize = array.Length - 1;
    }

    public void PrintHeap()
    {
        foreach (int ele in this.A)
            Console.WriteLine(ele);
    }
    //returns left child's index
    public int ChildL(int parent)
    {
        return parent * 2 + 1;
    }
    //returns right child's index
    public int ChildR(int parent)
    {
        return (parent * 2 + 2);
    }

    //swaps two index values in an array
    public void Swap(int i, int j)
    {
        if (i <= heapSize && j <= heapSize && i != j)
        {
            int temp = A[i];
            A[i] = A[j];
            A[j] = temp;
        }
    }

    public void MaxHeapify(int i)
    {
        int l = ChildL(i);
        int r = ChildR(i);
        int largest = i;

        if (l <= heapSize && A[l] > A[i])
            largest = l;

        if (r <= heapSize && A[r] > A[largest])
            largest = r;

        if (largest <= heapSize && largest != i)
        {
            Swap(largest, i);
            MaxHeapify(largest);
        }
    }
    public void BuildHeap()
    {
        for (int i = (A.Length / 2); i >= 0; i--)
        {
            MaxHeapify(i);
        }
    }
    public int[] HeapSort()
    {
        BuildHeap();
        for (int i = heapSize; i > 0; i--)
        {
            Swap(i, 0);
            heapSize--;
            MaxHeapify(0);
        }

        return A;
    }

}

public class PriorityQueue<T>
{
    class Node
    {
        public int Priority { get; set; }
        public T Object { get; set; }
        public override string ToString()
        {
            return $"{Object} - {Priority}";
        }
    }

    //object array
    List<Node> queue = new List<Node>();
    int heapSize = -1;
    bool _isMinPriorityQueue;
    public int Count { get { return queue.Count; } }

    /// <summary>
    /// If min queue or max queue
    /// </summary>
    /// <param name="isMinPriorityQueue"></param>
    public PriorityQueue(bool isMinPriorityQueue = false)
    {
        _isMinPriorityQueue = isMinPriorityQueue;
    }

    public void Print()
    {
        foreach (var item in this.queue)
        {
            Console.WriteLine($"{item}");
        }
    }

    public void Enqueue(int priority, T obj)
    {
        Node node = new Node() { Priority = priority, Object = obj };
        queue.Add(node);
        heapSize++;
        //Maintaining heap
        if (_isMinPriorityQueue)
            BuildHeapMin(heapSize);
        else
            BuildHeapMax(heapSize);
    }

    public T Dequeue()
    {
        if (heapSize > -1)
        {
            var returnVal = queue[0].Object;
            queue[0] = queue[heapSize];
            queue.RemoveAt(heapSize);
            heapSize--;

            //Maintaining lowest or highest at root based on min or max queue
            if (_isMinPriorityQueue)
                MinHeapify(0);
            else
                MaxHeapify(0);
            return returnVal;
        }
        else
            throw new Exception("Queue is empty");
    }

    public void UpdatePriority(T obj, int priority)
    {
        int i = 0;
        for (; i <= heapSize; i++)
        {
            Node node = queue[i];
            if (object.ReferenceEquals(node.Object, obj))
            {
                node.Priority = priority;
                if (_isMinPriorityQueue)
                {
                    BuildHeapMin(i);
                    MinHeapify(i);
                }
                else
                {
                    BuildHeapMax(i);
                    MaxHeapify(i);
                }
            }
        }
    }
    public bool IsInQueue(T obj)
    {
        foreach (Node node in queue)
            if (object.ReferenceEquals(node.Object, obj))
                return true;
        return false;
    }

    private void BuildHeapMax(int i)
    {
        while (i >= 0 && queue[(i - 1) / 2].Priority < queue[i].Priority)
        {
            Swap(i, (i - 1) / 2);
            i = (i - 1) / 2;
        }
    }
    private void BuildHeapMin(int i)
    {
        while (i >= 0 && queue[(i - 1) / 2].Priority > queue[i].Priority)
        {
            Swap(i, (i - 1) / 2);
            i = (i - 1) / 2;
        }
    }
    private void MaxHeapify(int i)
    {
        int left = ChildL(i);
        int right = ChildR(i);

        int heighst = i;

        if (left <= heapSize && queue[heighst].Priority < queue[left].Priority)
            heighst = left;
        if (right <= heapSize && queue[heighst].Priority < queue[right].Priority)
            heighst = right;

        if (heighst != i)
        {
            Swap(heighst, i);
            MaxHeapify(heighst);
        }
    }
    private void MinHeapify(int i)
    {
        int left = ChildL(i);
        int right = ChildR(i);

        int lowest = i;

        if (left <= heapSize && queue[lowest].Priority > queue[left].Priority)
            lowest = left;
        if (right <= heapSize && queue[lowest].Priority > queue[right].Priority)
            lowest = right;

        if (lowest != i)
        {
            Swap(lowest, i);
            MinHeapify(lowest);
        }
    }

    private void Swap(int i, int j)
    {
        var temp = queue[i];
        queue[i] = queue[j];
        queue[j] = temp;
    }
    private int ChildL(int i)
    {
        return i * 2 + 1;
    }
    private int ChildR(int i)
    {
        return i * 2 + 2;
    }
}

