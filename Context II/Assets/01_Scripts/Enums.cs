
[System.Serializable]
public enum TileType {
    None = 0,
    BaseTile = 1,
    EmptyTile = 2,
    FarmTile = 3,
    HouseTile = 4,
    EnergyTile = 5
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