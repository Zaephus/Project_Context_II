using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    public List<Tile> Generate(TextAsset _asset, LevelGenerator _gen) {

        if(_asset == null) {
            return null;
        }

        return _gen.Generate(LoadFile(_asset));

    }

    private TileData[] LoadFile(TextAsset _asset) {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(TileData[]));
        MemoryStream memoryStream = new MemoryStream(_asset.bytes);
        return xmlSerializer.Deserialize(memoryStream) as TileData[];
    }

}