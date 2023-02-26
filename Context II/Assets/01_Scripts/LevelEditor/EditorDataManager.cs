using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SFB;

public static class EditorDataManager {

    private static ExtensionFilter[] extensionFilters = new [] {
        new ExtensionFilter("Binary", "bin"),
        new ExtensionFilter("XML", "xml")
    };

    private static XmlSerializer xmlSerializer = new XmlSerializer(typeof(TileData[]));
    private static BinaryFormatter binaryFormatter = new BinaryFormatter();

    private static FileStream fileStream;

    public static void SaveLevel(TileData[] _tileDatas) {

        string path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "Level", extensionFilters);

        if(File.Exists(path)) {
            fileStream = new FileStream(path, FileMode.Open);
        }
        else {
            fileStream = new FileStream(path, FileMode.Create);
        }

        try {
            if(path.Contains(".bin")) {
                binaryFormatter.Serialize(fileStream, _tileDatas);
            }
            else if(path.Contains(".xml")) {
                xmlSerializer.Serialize(fileStream, _tileDatas);
            }
        }
        catch(SerializationException _e) {
            throw new SerializationException("Failed to serialize.", _e);
        }
        finally {
            fileStream.Close();
        }

    }

    public static TileData[] LoadLevel() {

        TileData[] tileDatas = null;

        string[] paths = StandaloneFileBrowser.OpenFilePanel("Load File", "", extensionFilters, false);
        
        if(paths.Length < 1 || !File.Exists(paths[0])) {
            return null;
        }
        else {
            fileStream = new FileStream(paths[0], FileMode.Open);
        }

        try {
            if(paths[0].Contains(".bin")) {
                tileDatas = binaryFormatter.Deserialize(fileStream) as TileData[];
            }
            else if(paths[0].Contains(".xml")) {
                tileDatas = xmlSerializer.Deserialize(fileStream) as TileData[];
            }
        }
        catch(SerializationException _e) {
            throw new SerializationException("Failed to serialize.", _e);
        }
        finally {
            fileStream.Close();
        }

        return tileDatas;

    }

}