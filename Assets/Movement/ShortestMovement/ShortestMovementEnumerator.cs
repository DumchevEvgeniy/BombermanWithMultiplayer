using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShortestMovementEnumerator : IEnumerable<PonderableNode<Int32>> {
    private ShortestMovementIterator iterator;

    public ShortestMovementEnumerator() {
        iterator = new ShortestMovementIterator();
    }

    public void Add(PonderableNode<Int32> item) {
        iterator.AvailableNodes.Add(item);
    }
    public Boolean WasVisited(Cell node) {
        return iterator.VisitedNodes.Exists(el => el.Equals(node));
    }
    public PonderableNode<Int32> Find(Cell node) {
        return iterator.AvailableNodes.FirstOrDefault(el => el.Equals(node));
    }

    public IEnumerator<PonderableNode<Int32>> GetEnumerator() {
        while(iterator.MoveNext())
            yield return iterator.Current;
    }
    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}