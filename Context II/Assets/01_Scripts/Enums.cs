
[System.Serializable]
public enum TileType {
    None = 0,
    BaseTile = 1,
    PlainsTile = 2,
    ForestTile = 3,
    SandTile = 4,
    SiltTile = 5,
    WaterTile = 6,
    CityTile = 7,
    OfficeTile = 8,
    FarmTile = 9,
    PastureTile = 10,
    WindmillTile = 11,
    ClubTile = 12,
    BeachHutTile = 13,
    FarmHouseTile = 14
}

[System.Serializable]
public enum TileRotation {
    Zero = 0,
    OneSixth = 1,
    TwoSixth = 2,
    ThreeSixth = 3,
    FourSixth = 4,
    FiveSixth = 5
}

[System.Serializable]
public enum TileHeight {
    Zero = 0,
    OneQuarter = 1,
    Half = 2,
    ThreeQuarter = 3,
    One = 4,
    One_OneQuarter = 5,
    One_Half = 6,
    One_ThreeQuarter = 7,
    Two = 8
}

[System.Serializable]
public enum ApprovalType {
    None = 0,
    Power = 1,
    Citizen = 2
}

[System.Serializable]
public enum GameState {
    MainMenu = 0,
    PlayMode = 1,
    IntermediateEnding = 3,
    FinalEnding = 4
}