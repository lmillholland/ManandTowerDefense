using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    [Header("Level Settings")]
    public string levelName = "demo";
    public float fastForwardSpeed;

    // pause settings
    bool isPaused;
    public bool IsPaused => isPaused;

    // fast forward managers
    public float FastForwardSpeed {
        get {
            if (isFastForwarding)
                return fastForwardSpeed;
            else
                return 1f;
        }
    }
    [HideInInspector] public bool isFastForwarding = false;

    void Awake()
    {
        // manage singleton instance
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        /*
        Parse XML file containing all wave information and feed it to WaveManager.
        */
        var path = Path.Combine(Application.dataPath, "Resources/LevelMaps", levelName + ".xml");
        var levelWaves = WavesContainer.Load(path);
        WaveManager.instance.AssignWaves(levelWaves.waves);
    }

    public void GameOver(bool isWon)
    {
        /*
        Game Over is triggered when the Player's Castle is destroyed or all enemy waves are beaten.
        :param isWon: indicates whether the level was won or lost.
        
        TODO: 
            - proper Game Over screen(s)
            - send user to reward screen
        */
        isPaused = true;
        Debug.Log(("won", isWon));
    }
}
