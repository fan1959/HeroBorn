using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
public class DataManager : MonoBehaviour, IManager
{
    // Start is called before the first frame update
    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
    private string _dataPath;
    private string _textFile;
    private string _streamingTextFile;
    private string _xmlLevelProgress;
    private string _xmlWeapons;
    private string _jsonWeapons;

    private List<Weapon> weaponInventory = new List<Weapon>
    {
        new Weapon("Sword of Doom",100),
        new Weapon("Butterfly knives",25),
        new Weapon("Brass Knuckles",25),
    };

    private void Awake()
    {
        _dataPath = Application.dataPath + "/Player_Data/";
        Debug.LogFormat($"Data path:{_dataPath}");
        _textFile = _dataPath + "Save_Data.txt";
        _streamingTextFile = _dataPath + "Streaming_Save_Data.txt";
        _xmlLevelProgress = _dataPath + "Progress_Data..xml";
        _xmlWeapons = _dataPath + "WeaponInventory.xml";
        _jsonWeapons = _dataPath + "WeaponJSON.json";
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Initialize()
    {
        _state = "Data manager is initialized";
        Debug.Log(_state);
        FilesystemInfo();
        NewDirectory();
        //NewTextFile();
        //UpdataTextFile();
        //ReadFromFile(_textFile);
        //WriteToStream(_streamingTextFile);
        //ReadFromStream(_streamingTextFile);
        //WriteToXML(_xmlLevelProgress);
        //ReadFromXML(_streamingTextFile);
        //SerializeXML();
        //DeserializeXML();  
        SerializeJSON();
        DeserializeJSON();
    }
    public void FilesystemInfo()
    {
        Debug.LogFormat($"Path separator character:{Path.PathSeparator}");
        Debug.LogFormat($"Directory separator character:{Path.DirectorySeparatorChar}");
        Debug.LogFormat($"Current directory:{Directory.GetCurrentDirectory()}");
        Debug.LogFormat($"Temporary directory:{Path.GetTempPath()}");
    }
    public void NewDirectory()
    {
        if (Directory.Exists(_dataPath))
        {
            Debug.LogFormat($"Directory already exists at path:{_dataPath}");
        }
        else
        {
            Directory.CreateDirectory(_dataPath);
            Debug.LogFormat($"New directory created at path:{_dataPath}");
        }
    }

    public void DeleteDirectory()
    {
        if (Directory.Exists(_dataPath))
        {
            Directory.Delete(_dataPath, true);
            Debug.Log("Directory successfully deleted!");
        }
        else
        {
            Debug.Log("Directory doesn't exist or has already been deleted...");
        }
    }
    public void NewTextFile()
    {
        if (File.Exists(_textFile))
        {
            Debug.Log("File already exists...");
        }
        else
        {
            File.WriteAllText(_textFile, "<SAVE DATA>\n");
        }
    }
    public void UpdataTextFile()
    {
        if (File.Exists(_textFile))
        {
            File.AppendAllText(_textFile, $"Game started:{DateTime.Now}\n");
            Debug.Log("File updated successfully!");
        }
        else
        {
            Debug.Log("File doesn't exist...");
        }
    }
    public void ReadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        Debug.Log(File.ReadAllText(filename));

    }
    public void DeleteFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        File.Delete(_textFile);
        Debug.Log("File successfully deleted!");
    }
    public void WriteToStream(string filename)
    {
        if (!File.Exists(_textFile))
        {
            using (StreamWriter newStream = File.CreateText(filename))
            {
                newStream.WriteLine("<Save Data>for HERO BORN \n");
            }
            Debug.Log("New file created with StreamWriter");
        }
        StreamWriter streamWriter = File.AppendText(filename);
        streamWriter.WriteLine("Game ended: " + DateTime.Now);
        streamWriter.Close();
        Debug.Log("File contents updated with StreamWriter!");
    }

    public void ReadFromStream(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        StreamReader streamReader = new StreamReader(filename);
        Debug.Log(streamReader.ReadToEnd());
    }
    public void WriteToXML(string filename)
    {
        if (!File.Exists(filename))
        {
            //先创建一个流，然后再创建一个写入器
            using (FileStream xmlStream = File.Create(filename))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(xmlStream))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("level_progress");
                    for (int i = 0; i < 5; i++)
                    {
                        xmlWriter.WriteElementString("level", "level-" + i);
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.Close();
                    xmlStream.Close();
                }
            }
        }
    }
    public void ReadFromXML(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        XmlReader xmlReader = XmlReader.Create(filename);
        Debug.Log(xmlReader.Read());
    }
    public void SerializeXML()
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Weapon>));
        using(FileStream stream = File.Create(_xmlWeapons))
        {
            xmlSerializer.Serialize(stream, weaponInventory);
        }
    }
    public void DeserializeXML()
    {
        if (File.Exists(_xmlWeapons))
        {
            var xmlSerializer=new XmlSerializer(typeof(List<Weapon>));
            using(FileStream stream = File.OpenRead(_xmlWeapons))
            {
                var weapons=(List<Weapon>)xmlSerializer.Deserialize(stream);
                foreach (var weapon in weapons)
                {
                    Debug.LogFormat($"Weapon:{weapon.name}-Damage:{weapon.damage}");
                }
            }
        }
    }
    public void SerializeJSON()
    {
        WeaponShop shop = new WeaponShop();
        shop.inventory=weaponInventory;
        string jsonString = JsonUtility.ToJson(shop,true);
        using (StreamWriter stream = File.CreateText(_jsonWeapons))
        {
            stream.WriteLine(jsonString);
        }
    }
    public void DeserializeJSON()
    {
        if (File.Exists(_jsonWeapons))
        {
            using(StreamReader stream=new StreamReader(_jsonWeapons))
            {
                var jsonString= stream.ReadToEnd();
                var weaponData= JsonUtility.FromJson<WeaponShop>(jsonString);
                foreach(var weapon in weaponData.inventory)
                {
                    Debug.LogFormat($"Weapon:{weapon.name}-Damage:{weapon.damage}");
                }
            }
        }
    }
}

