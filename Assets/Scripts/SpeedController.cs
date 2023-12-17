using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [field: SerializeField]
    public Vector2 Speed { get; set; }

    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        transform.position = (Vector2)transform.position + (Speed * Time.deltaTime);
    }
    
}
