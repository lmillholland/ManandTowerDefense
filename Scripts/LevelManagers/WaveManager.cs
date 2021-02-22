using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance = null;

    [Header("References")]
    public Text waveText;
    public NextWaveButton nextWaveButton;

    [Header("Settings")]
    public float enemySpawnDelay = .9f; // TODO: random
    public float delayBetweenWaves = 10f;

    // wave trackers
    List<Wave> waves;
    int currWaveIdx = -1;
    bool canStartNextWave = true;
    public bool CanStartNextWave => canStartNextWave;

    void Awake()
    {
        // manage singelton instance
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    public void AssignWaves(List<Wave> waves)
    {
        // Receive waves data from XML.
        this.waves = waves;
        UpdateWaveText();
    }

    void UpdateWaveText()
    {
        /*
        Update wave text at top of screen in "12/40" format.
        */
        var displayWaveNumber = (currWaveIdx + 1).ToString(); // since the real world doesn't
                                                              // run on zero-based indexing
        waveText.text = "Wave " + displayWaveNumber + "/" + waves.Count.ToString();
    }

    public IEnumerator StartNextWave()
    {
        /*
        Spawn each enemy in next wave.
        */
        if (canStartNextWave)
        {
            // access next wave
            canStartNextWave = false;
            currWaveIdx++;
            UpdateWaveText();
            var wave = waves[currWaveIdx];

            // Loop over each battalion and instantiate each enemy
            foreach (var battalion in wave.battalions)
            {
                for (int i = 0; i < battalion.NumEnemies; i++)
                {
                    EnemyManager.instance.CreateEnemy(battalion.EnemyType, battalion.Level);
                    yield return new WaitForSeconds(enemySpawnDelay);
                }
            }
        }
    }

    public void FinishWave()
    {
        // check if we completed the last level
        if (currWaveIdx == waves.Count - 1)
        {
            LevelManager.instance.GameOver(true);
        }
        // otherwise allow us to start next wave
        else
        {
            ResourceManager.instance.GainTool(); // award player one tool
            canStartNextWave = true;
            nextWaveButton.Activate();

            // auto-start next wave
            StartCoroutine(AutoStartNextWave(currWaveIdx));
        }
    }

    IEnumerator AutoStartNextWave(int waveIdx)
    {
        /*
        Auto-start next wave if the user hasn't done so manually.
        */
        yield return new WaitForSeconds(delayBetweenWaves);
        bool userStartedNextWave = waveIdx != currWaveIdx;
        
        // start next wave
        if (!userStartedNextWave)
        {
            StartCoroutine(StartNextWave());
            nextWaveButton.Deactivate();
        }
    }
}
