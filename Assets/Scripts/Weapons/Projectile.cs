using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * MoveSpeed * Time.deltaTime;    
    }
}
