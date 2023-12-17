using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestructableController))]
public class AsteroidController : MonoBehaviour
{
   
    [field: SerializeField]
    public AsteroidController OnDestroyedTemplate { get; private set; }

  
    [field: SerializeField]
    public float RotationSpeed { get; private set; }

   
    [field: SerializeField]
    public Vector2 Speed { get; private set; }

   
    public GameController GameController { get; private set; }

    // Static method to spawn a new asteroid based on a template, rotation speed, speed, and game controller.
    public static AsteroidController Spawn(AsteroidController template, float rotationSpeed, Vector2 speed, GameController gameController)
    {
        
        AsteroidController newAsteroid = Instantiate(template);

     
        newAsteroid.GameController = gameController;

       
        newAsteroid.GetComponent<DestructableController>().GameController = gameController;

       
        newAsteroid.RotationSpeed = rotationSpeed;
        newAsteroid.Speed = speed;

       
        return newAsteroid;
    }

   
    void Update()
    {
       
        RotateMeteor();

        
        MoveMeteor();
    }

    // Method called when the asteroid is hit by a laser.
    public void OnLaserHit(LaserController laser, DestructableController destructable)
    {
        // If there is a template for the destroyed version, spawn a new asteroid based on that template.
        if (OnDestroyedTemplate != null)
        {
            AsteroidController newObj = Spawn(OnDestroyedTemplate, RotationSpeed, Speed, GameController);

            // Set the position of the new asteroid to the position of the current asteroid.
            newObj.transform.position = this.transform.position;
        }

        // Call the DefaultDestroy method on the destructable component attached to the asteroid.
        destructable.DefaultDestroy(laser);
    }

    // Rotate the asteroid based on its rotation speed.
    private void RotateMeteor()
    {
        float newZ = transform.rotation.eulerAngles.z + (RotationSpeed * Time.deltaTime);
        Vector3 newR = new Vector3(0, 0, newZ);
        transform.rotation = Quaternion.Euler(newR);
    }

    // Move the asteroid based on its speed.
    public void MoveMeteor()
    {
        float newX = transform.position.x + (Speed.x * Time.deltaTime);
        float newY = transform.position.y + (Speed.y * Time.deltaTime);
        transform.position = new Vector2(newX, newY);
    }
}
