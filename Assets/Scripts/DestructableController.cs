using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Collider2D))]
public class DestructableController : MonoBehaviour
{
    public GameController GameController { get; set; }
    [field: SerializeField]
    public int Points { get; private set; }
    [field: SerializeField]
    public UnityEvent<LaserController, DestructableController>  OnHit { get; private set; }
    [field: SerializeField]
    public UnityEvent<DestructableController> OnDestroyed { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
       // Check if the colliding object has a LaserController component.
        LaserController asLaser = other.GetComponent<LaserController>();
        if (asLaser != null)
        {
            // Check if there are no persistent listeners for the OnHit event.
            if (OnHit.GetPersistentEventCount() == 0)
            {
                // If no listeners are present, perform the default destroy behavior.
                DefaultDestroy(asLaser);
            }
            else
            {
                // If listeners are present, invoke the OnHit event with the laser and this object.
                OnHit.Invoke(asLaser, this);
            }
        }
    }

    public void DefaultDestroy(LaserController laser)
    {
        GameController.IncrementScore(Points);
        Destroy(laser.gameObject);
        OnDestroyed.Invoke(this);
        Destroy(this.gameObject);
        
    }
}
