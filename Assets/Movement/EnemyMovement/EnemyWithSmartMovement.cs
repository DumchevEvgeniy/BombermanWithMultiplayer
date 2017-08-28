using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWithSmartMovement : EnemyWithSmoothMovement {
    protected ShortestMovementProvider provider;
    protected Field gameField;
    private GameObject targerPlayer;

    protected override void OnStart() {
        base.OnStart();
        var enemySettings = GetComponent<EnemySettings>();
        if(enemySettings != null)
            gameField = enemySettings.gameField;
    }

    protected override Boolean CanMove() {
        if(CanSmartMove())
            return GetRotationAngle() == 0 ? true : false;
        provider = null;
        return base.CanMove();
    }
    protected override Single GetRotationAngle() {
        if(provider == null || !provider.ExistRoute || provider.Route.IsEmpty())
            return base.GetRotationAngle();
        var first = provider.First();
        var second = provider.Route.Count == 1 ? first : provider.ElementAt(1);
        return GetAngle(first, second);
    }
    protected override Boolean NeededRotateAfterMoving(GameObject gameObject) {
        return (provider == null || !provider.ExistRoute) ? base.NeededRotateAfterMoving(gameObject) : false;
    }

    protected virtual IRoute<Cell> RouteSeacher {
        get {
            var barrierTypes = new List<Type>();
            barrierTypes.Add(typeof(ConcreteCube));
            barrierTypes.Add(typeof(Bomb));
            var enemySettings = GetComponent<EnemySettings>();
            if(enemySettings != null && !enemySettings.wallpass)
                barrierTypes.Add(typeof(BreakCube));
            var routeSeacher = new RouteSeacher<Cell>(barrierTypes, gameField);
            return new RouteFromCell<Cell>(routeSeacher, gameField);
        }
    }

    private Single GetAngle(DirectedNode first, DirectedNode second) {
        var mainDirection = first.Direction;
        var neededDirection = second.GetRelativeDirection(first);
        if(mainDirection == neededDirection)
            return 0;
        if(mainDirection == -neededDirection)
            return random.Next(0, 2) == 0 ? -180 : 180;
        if(mainDirection == Vector3.right)
            return neededDirection == Vector3.forward ? -90 : 90;
        if(mainDirection == Vector3.left)
            return neededDirection == Vector3.back ? -90 : 90;
        if(mainDirection == Vector3.back)
            return neededDirection == Vector3.right ? -90 : 90;
        return neededDirection == Vector3.left ? -90 : 90;
    }
    private Cell GetCellOnField(Vector3 position) {
        var cell = position.ToCell();
        return gameField.GetCell(cell.IndexRow, cell.IndexColumn);
    }
    private Boolean CanSmartMove() {
        if(GetComponent<EnemySettings>() == null || gameField == null)
            return false;
        if(targerPlayer != null && ExistRoute(targerPlayer))
            return true;
        var allPlayers = gameObject.scene.FindPlayers().ToList();
        while(!allPlayers.IsEmpty()) {
            var indexPlayer = random.Next(0, allPlayers.Count);
            var currentPlayer = allPlayers[indexPlayer];
            if(ExistRoute(currentPlayer)) {
                targerPlayer = currentPlayer;
                return true;
            }
            allPlayers.RemoveAt(indexPlayer);
        }
        return false;
    }
    private Boolean ExistRoute(GameObject player) {
        var enemyPosition = GetCellOnField(gameObject.GetIntegerPosition());
        var playerPositon = GetCellOnField(player.GetIntegerPosition());
        provider = new ShortestMovementProvider(gameObject.transform.forward, enemyPosition, playerPositon, gameField);
        provider.RouteSeacher = RouteSeacher;
        provider.BuildARoute();
        return provider.ExistRoute;
    }
}