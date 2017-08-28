using System;
using UnityEngine.Networking;

public class Level : NetworkBehaviour {
    public Int32 length = 21;
    public Int32 width = 13;
    public Int32 breakCubesCount = 54;
    public Int32 easyEnemiesCount = 6;
    public Int32 hardEnemyCount = 0;
    public Int32 bonusBombsCount = 0;
    public Int32 bonusFlamesCount = 0;
    public Int32 bonusSpeedCount = 0;
    public Int32 bonusWallpassCount = 0;
    public Int32 bonusDetonatorCount = 0;
    private SmartMap gameMap;

    public override void OnStartServer() {
        base.OnStartServer();
        InitializeMap();
    }

    private void InitializeMap() {
        gameMap = new SmartMap(length, width, new MapFloor(), new ConcreteCube());
        gameMap.AddElements<RandomPlacementOnEmptyPosition>(new BreakCube(), breakCubesCount);
        gameMap.AddElements<BonusesRandomPlacement>(GameFactory.CreateBonusSpeed(), bonusSpeedCount);
        gameMap.AddElements<BonusesRandomPlacement>(GameFactory.CreateBonusBombs(), bonusBombsCount);
        gameMap.AddElements<BonusesRandomPlacement>(GameFactory.CreateBonusFlames(), bonusFlamesCount);
        gameMap.AddElements<BonusesRandomPlacement>(GameFactory.CreateBonusWallpass(), bonusWallpassCount);
        gameMap.AddElements<BonusesRandomPlacement>(GameFactory.CreateBonusDetonator(), bonusDetonatorCount);
        gameMap.AddElements<RandomPlacementOnEmptyPosition>(GameFactory.CreateEasyEnemy(), easyEnemiesCount);
        gameMap.AddElements<RandomPlacementOnEmptyPosition>(GameFactory.CreateHardEnemy(gameMap.Field), hardEnemyCount);
        gameMap.CreateAllElements();
    }

    public SmartMap Map {
        get { return gameMap; }
    }
}