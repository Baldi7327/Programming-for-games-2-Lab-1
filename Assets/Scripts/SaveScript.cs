using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveScript : MonoBehaviour
{

    private Enemy[] enemies;
    private Player player;
    private string[] epos;
    private string[] ehp;
    private string ppos;
    private string camforward;
    private string[] enemyActive;
    private string currentScore;
    private PlayerCam cam;
    public List<string> eposOut = new List<string>();
    public List<string> ehpOut = new List<string>();
    public string pposOut = "";
    public string camforwardOut = "";
    public List<string> enemyActiveOut =  new List<string>();
    private MainHub Hub;
    public string scoreOut;


    private void Start()
    {
        enemies = FindObjectsOfType<Enemy>();
        player = FindObjectOfType<Player>();
        epos = new string[enemies.Length];
        ehp = new string[enemies.Length];
        enemyActive = new string[enemies.Length];
        cam = FindObjectOfType<PlayerCam>();
        Hub = FindObjectOfType<MainHub>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearJsonFile();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ClearJsonFile();
            for (int i = 0; i < enemies.Length; i++) {
                if (enemies[i] != null)
                {
                    epos[i] = "epos" + enemies[i].transform.position.x + "," + enemies[i].transform.position.y + "," + enemies[i].transform.position.z;
                    ehp[i] = "ehp" + enemies[i].hp;
                    enemyActive[i] = "eActive" + enemies[i].gameObject.activeInHierarchy;
                    WriteToJson(epos[i]);
                    WriteToJson(ehp[i]);
                    WriteToJson(enemyActive[i]);
                }
            }
            ppos = "ppos" + player.transform.position.x + "," + player.transform.position.y + "," + player.transform.position.z;
            camforward = "camF" + cam.transform.forward.x + "," + cam.transform.forward.y + "," + cam.transform.forward.z;
            currentScore = "Score" + Hub.Points;
            Debug.Log("EnemyPos: " + epos);
            Debug.Log("EnemyHp: " + ehp);
            Debug.Log("PlayerPos: " + ppos);
            Debug.Log("CamForward: " + camforward);
            WriteToJson(ppos);
            WriteToJson(camforward);
            WriteToJson(currentScore);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    private void WriteToJson(string jsonString)
    {
        string fileName = "output.json";
        string filePath = Path.Combine(Application.dataPath, fileName);

        // Check if the file exists, if not, create it
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
        }

        // Write the jsonString to the file using StreamWriter
        using (StreamWriter streamWriter = new StreamWriter(filePath, true))
        {
            streamWriter.WriteLine(jsonString);
        }
    }

    

    public void ReadFromFile()
    {
        string fileName = "output.json";
        string filePath = Path.Combine(Application.dataPath, fileName);

        if (File.Exists(filePath))
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.StartsWith("epos"))
                    {
                        eposOut.Add(line.Substring(4).Trim());
                    }
                    else if (line.StartsWith("ehp"))
                    {
                        ehpOut.Add(line.Substring(3).Trim());
                    }
                    else if (line.StartsWith("eActive"))
                    {
                        enemyActiveOut.Add(line.Substring(7).Trim());
                    }
                    else if (line.StartsWith("ppos"))
                    {
                        pposOut = line.Substring(4).Trim();
                    }
                    else if (line.StartsWith("camF"))
                    {
                        camforwardOut = line.Substring(4).Trim();
                    }
                    else if (line.StartsWith("Score"))
                    {
                        scoreOut = line.Substring(5).Trim();
                    }
                }
            }
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }
    }

    private void Load()
    {
        ReadFromFile();
        string[] loadedPlayerPos = pposOut.Split(',');
        player.transform.position = new Vector3(float.Parse(loadedPlayerPos[0]),float.Parse(loadedPlayerPos[1]),float.Parse(loadedPlayerPos[2]));

        string[] loadedCamF = camforwardOut.Split(',');
        cam.transform.forward = new Vector3(float.Parse(loadedCamF[0]), float.Parse(loadedCamF[1]), float.Parse(loadedCamF[2]));

        Hub.Points = int.Parse(scoreOut);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                string[] enemyPos = eposOut[i].Split(',');
                enemies[i].transform.position = new Vector3(float.Parse(enemyPos[0]), float.Parse(enemyPos[1]), float.Parse(enemyPos[2]));
                enemies[i].hp = float.Parse(ehpOut[i]);
                bool state = (enemyActiveOut[i].ToLower() == "true");
                enemies[i].gameObject.SetActive(state);

            }
        }
    }

    private void ClearJsonFile()
    {
        string fileName = "output.json";
        string filePath = Path.Combine(Application.dataPath, fileName);

        if (File.Exists(filePath))
        {
            //File.WriteAllText(filePath, "");
            File.Delete(filePath);
            camforwardOut = "";
            ehpOut = new List<string>();
            enemyActiveOut = new List<string>();
            eposOut = new List<string>();
            pposOut = "";
            scoreOut = "";
                
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }
    }
}
