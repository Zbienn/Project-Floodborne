using UnityEngine;

public class OrbitBoat : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform holder;
    void Start()
    {
        
    }

    void Update()
    {
        holder.rotation = Quaternion.Euler(0F, 0F, holder.rotation.eulerAngles.z + (rotateSpeed*Time.deltaTime));
    }
}
