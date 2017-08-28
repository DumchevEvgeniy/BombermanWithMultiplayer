using System;
using System.Collections.Generic;

public class ShortestMovement {
    protected PonderableNode<Int32> source;
    protected PonderableNode<Int32> destination;
    protected Field field;
    protected IRoute<Cell> routeSeacher;
    protected IPonderable<PonderableNode<Int32>, Int32> weightCalculator;
    protected ShortestMovementEnumerator collection;
    public Boolean ExistRoute { get { return Route != null; } }
    public List<PonderableNode<Int32>> Route { get; private set; }

    public ShortestMovement(PonderableNode<Int32> source, PonderableNode<Int32> destination, Field field,
        IRoute<Cell> routeSeacher, IPonderable<PonderableNode<Int32>, Int32> weightCalculator) {
        this.source = source;
        this.destination = destination;
        this.field = field;
        this.routeSeacher = routeSeacher;
        this.weightCalculator = weightCalculator;
        collection = new ShortestMovementEnumerator();
    }
    protected ShortestMovement(Field field) : this(null, null, field, null, null) { }

    public virtual void BuildARoute() {
        collection.Add(source);
        foreach(var item in collection) {
            if(item.Equals(destination)) {
                RouteInitialize(item);
                return;
            }
            FindPossibleRoutes(item);
        }
    }

    private void FindPossibleRoutes(PonderableNode<Int32> sourceNode) {
        if(TryReachDestination(routeSeacher.GetAdditionalRoutes(sourceNode), sourceNode))
            return;
        TryReachDestination(routeSeacher.GetOptimalRoutes(sourceNode), sourceNode, optimalCell => {
            var node = collection.Find(optimalCell);
            if(node != null)
                RecalculateParameters(sourceNode, node);
            else
                collection.Add(CreatePonderableNode(optimalCell, sourceNode));
        });
    }
    private Boolean TryReachDestination(IEnumerable<Cell> possibleCells, PonderableNode<Int32> sourceNode, Action<Cell> action = null) {
        foreach(var cell in possibleCells) {
            if(collection.WasVisited(cell))
                continue;
            if(new Route(sourceNode, cell, field).Exist(destination)) {
                collection.Add(CreatePonderableNode(destination, sourceNode));
                return true;
            }
            if(action != null)
                action(cell);
        }
        return false;
    }
    private PonderableNode<Int32> CreatePonderableNode(Cell node, PonderableNode<Int32> previous = null) {
        var newNode = new PonderableNode<Int32>(node, weightCalculator);
        if(previous != null) {
            newNode.PreviousNode = previous;
            newNode.Direction = newNode.GetRelativeDirection(previous);
            newNode.Weight = previous.CalculateWeight(newNode, destination);
        }
        return newNode;
    }
    private void RecalculateParameters(PonderableNode<Int32> from, PonderableNode<Int32> to) {
        var weight = from.CalculateWeight(to, destination);
        if(to.CompareTo(weight) > 0) {
            to.Weight = weight;
            to.PreviousNode = from;
            to.Direction = to.GetRelativeDirection(from);
        }
    }
    private void RouteInitialize(PonderableNode<Int32> last) {
        Route = new List<PonderableNode<Int32>>();
        Route.Add(last);
        PonderableNode<Int32> current = last;
        while(current.PreviousNode != null) {
            current = current.PreviousNode as PonderableNode<Int32>;
            Route.Add(current);
        }
        Route.Reverse();
    }
}
