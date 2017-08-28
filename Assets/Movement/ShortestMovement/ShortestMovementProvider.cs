using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortestMovementProvider : ShortestMovement, IEnumerable<PonderableNode<Int32>> {
    private Vector3 direction;
    private Cell sourceCell;
    private Cell destinationCell;
    public IPonderable<PonderableNode<Int32>, Int32> WeightCalculator {
        get { return weightCalculator; }
        set { weightCalculator = value; }
    }
    public IRoute<Cell> RouteSeacher {
        get { return routeSeacher; }
        set { routeSeacher = value; }
    }

    public ShortestMovementProvider(Vector3 direction, Cell source, Cell destination, Field field) : base(field) {
        this.direction = direction;
        sourceCell = source;
        destinationCell = destination;
    }

    public override void BuildARoute() {
        weightCalculator = WeightCalculator ?? DefaultWeightCalculator;
        routeSeacher = RouteSeacher ?? DefaultRouteSeacher;
        source = new PonderableNode<Int32>(sourceCell, weightCalculator);
        source.Direction = direction;
        destination = new PonderableNode<Int32>(destinationCell, weightCalculator);
        base.BuildARoute();
    }

    public IEnumerator<PonderableNode<Int32>> GetEnumerator() {
        foreach(var element in Route)
            yield return element;
    }
    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    private IPonderable<PonderableNode<Int32>, Int32> DefaultWeightCalculator {
        get { return new NodeWeightCalculator<PonderableNode<Int32>>(
            new CellMovementCalculator<PonderableNode<Int32>>()); }
    }
    private IRoute<Cell> DefaultRouteSeacher {
        get {
            var barrierTypes = new List<Type> { typeof(ConcreteCube) };
            var routeSeacher = new RouteSeacher<Cell>(barrierTypes, field);
            return new RouteFromCell<Cell>(routeSeacher, field);
        }
    }
}