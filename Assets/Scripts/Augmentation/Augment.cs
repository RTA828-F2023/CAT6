using System.Collections;
using UnityEngine;

public class Augment : MonoBehaviour
{
    [Header("Augment Stats")]
    [SerializeField] private new string name;
    [SerializeField] private float duration;

    public virtual void Apply(Player player)
    {

    }

    public virtual void Revert(Player player)
    {
        Destroy(gameObject);
    }

    public IEnumerator ApplyCoroutine(Player player)
    {
        Apply(player);
        yield return new WaitForSeconds(duration);
        // Revert(player);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ApplyCoroutine(other.GetComponent<Player>()));
            gameObject.SetActive(false);
        }
    }
}
