using UnityEngine;

public class ThrownWeapon : MonoBehaviour
{
    [SerializeField] private float throwPower;
    [SerializeField] private Rigidbody2D rg;

    [SerializeField] private float rotateSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rg.linearVelocity = new Vector2(Random.Range(-throwPower, throwPower), throwPower);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed * 360f * Time.deltaTime * Mathf.Sign(rg.linearVelocity.x)));
    }
}
