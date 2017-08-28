using System;

public class NodeWeightCalculator<T> : IPonderable<T, Int32> where T : Cell {
    private IPonderableByMovement<T> movementSolver;

    public NodeWeightCalculator(IPonderableByMovement<T> movementSolver) {
        this.movementSolver = movementSolver;
    }

    public Int32 GetWeight(T source, T next, T destination) {
        return movementSolver.GetDistance(source, next) +
            movementSolver.GetDistanceToDestinition(next, destination) +
            movementSolver.GetTimeToRotate(source, next);
    }
}