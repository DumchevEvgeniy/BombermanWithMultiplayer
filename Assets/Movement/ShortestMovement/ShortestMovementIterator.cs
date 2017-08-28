using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShortestMovementIterator : IEnumerator<PonderableNode<Int32>> {
    private List<PonderableNode<Int32>> availableNodes;
    private List<PonderableNode<Int32>> visitedNodes;
    private PonderableNode<Int32> current;

    public ShortestMovementIterator() {
        Reset();
    }

    public Boolean MoveNext() {
        if(availableNodes.IsEmpty())
            return false;
        current = availableNodes.First(c => c.Weight == availableNodes.Min(el => el.Weight));
        availableNodes.Remove(current);
        visitedNodes.Add(current);
        return true;
    }
    public void Dispose() { }
    public void Reset() {
        availableNodes = new List<PonderableNode<Int32>>();
        visitedNodes = new List<PonderableNode<Int32>>();
        current = null;
    }

    public PonderableNode<Int32> Current { get { return current; } }
    Object IEnumerator.Current { get { return Current; } }
    public List<PonderableNode<Int32>> AvailableNodes {
        get { return availableNodes; }
    }
    public List<PonderableNode<Int32>> VisitedNodes {
        get { return visitedNodes; }
    }
}