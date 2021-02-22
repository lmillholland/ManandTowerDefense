using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType {
    Cannon,
    Mushroom,
    Eyeball
}

public class TowerManager : MonoBehaviour
{
    [Header("Tower Prefabs")]
    public GameObject cannonTowerPrefab;
    public GameObject eyeballTowerPrefab;
    public GameObject mushroomTowerPrefab;

    [Header("Tower Cost")]
    public int baseTowerCost = 100;
    public int upgradeTowerCost = 20;
    
    // tower trackers
    Dictionary<TowerType, GameObject> towerPrefabs;
    Dictionary<TowerType, int> towerCount;

    public static TowerManager instance = null;

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
        // Store prefabs in dict for easy access
        towerPrefabs = new Dictionary<TowerType, GameObject>() {
            {TowerType.Cannon, cannonTowerPrefab},
            {TowerType.Eyeball, eyeballTowerPrefab},
            {TowerType.Mushroom, mushroomTowerPrefab},
        };
        
        // Instantiate tracker for all tower counts
        towerCount = new Dictionary<TowerType, int>() {
            {TowerType.Cannon, 0},
            {TowerType.Eyeball, 0},
            {TowerType.Mushroom, 0}
        };
    }
    
    public Tower BuildTower(TowerType towerType, Transform targetTransform)
    {
        /*
        Create tower at given spot, and record that we've create a tower of this type (to
        bump up the cost of creating another).
        Returns the Tower component of the created object.
        */
        var towerPrefab = towerPrefabs[towerType];
        var tower = Instantiate(towerPrefab, targetTransform.position, Quaternion.identity);
        tower.transform.parent = targetTransform;
        towerCount[towerType]++;
        return tower.GetComponent<Tower>();
    }

    public int GetTowerCost(TowerType towerType)
    {
        /*
        Get cost considering how many towers of this type we've created.
        */
        var upgradeCost = upgradeTowerCost * towerCount[towerType];
        return baseTowerCost + upgradeCost;
    }
}
