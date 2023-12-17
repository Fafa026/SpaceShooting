using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [field: SerializeField]
    public float SpawnRate { get; private set; } = 2;
    [field: SerializeField]
    public float LastSpawn { get; private set; } = 0;
    [field: SerializeField]
    public float MinRotation { get; private set; } = -90;
    [field: SerializeField]
    public float MaxRotation { get; private set; } = 90;
    [field: SerializeField]
    public Vector2 MinSpeed { get; private set; }
    [field: SerializeField]
    public Vector2 MaxSpeed { get; private set; }
    [field: SerializeField]
    public Transform MinSpawnPoint { get; private set; }
    [field: SerializeField]
    public Transform MaxSpawnPoint;
    public Vector2 MinSpawnVector => MinSpawnPoint.position;
    public Vector2 MaxSpawnVector => MaxSpawnPoint.position;
    [field: SerializeField]
    public AsteroidController Template { get; private set; }
    [field: SerializeField]
    public GameController GameController { get; private set; }
    
    void Start()
    {
        LastSpawn = Time.time;
    }

    
    void Update()
    {
        if (ShouldSpawn())
        {
            SpawnAsteroid();
        }
    }

 
    private void SpawnAsteroid()
    {
        
        float rotationSpeed = Random.Range(MinRotation, MaxRotation);

       
        Vector2 speed = new Vector2(Random.Range(MinSpeed.x, MaxSpeed.x), Random.Range(MinSpeed.y, MaxSpeed.y));

        
        AsteroidController ac = AsteroidController.Spawn(Template, rotationSpeed, speed, GameController);

       
        Vector2 spawnPoint = new Vector2(Random.Range(MinSpawnVector.x, MaxSpawnVector.x), MinSpawnVector.y);

        
        ac.transform.position = spawnPoint;

       
        LastSpawn = Time.time;
    }

 // Method to check if a new asteroid should be spawned based on the spawn rate.
    private bool ShouldSpawn()
    {
        return Time.time > (LastSpawn + SpawnRate);
    }
}
