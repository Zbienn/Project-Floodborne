using UnityEngine;

public class XPPickup : MonoBehaviour
{
    [SerializeField] private float xpAmount = 5f;
    [SerializeField] private PlayerExperienceData playerExperience;
    private Transform target;
    private bool isBeingPulled = false;
    private float pullSpeed = 8f;

    private void Update()
    {
        if (isBeingPulled && target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, pullSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                if (playerExperience != null)
                {
                    bool leveledUp = playerExperience.AddXP(xpAmount);
                    if (leveledUp)
                        Debug.Log("Level Up!");
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isBeingPulled && collision.name == "PickUpRange")
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            isBeingPulled = true;
        }
    }
}
