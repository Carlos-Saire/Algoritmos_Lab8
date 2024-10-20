using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyControl : MonoBehaviour
{
    private Vector2 positionToMove;
    public float speedMove;
    public float energy;
    private float fullenergy;
    bool confirms=true;
    private void Start()
    {
        fullenergy = energy;
    }
    private void Update()
    {
        if (confirms)
        {
            transform.position = Vector2.MoveTowards(transform.position, positionToMove, speedMove * Time.deltaTime);
        }   
    }
    public void SetNewPosition(Vector2 newPosition)
    {
        positionToMove = newPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Node"))
        {
            SetNewPosition(collision.GetComponent<NodeControl>().GetAdjacentNode().transform.position);
            energy-=collision.GetComponent<NodeControl>().energy;
            if(energy <= 0)
            {
                confirms = false;
                StartCoroutine(RestaurarVida());
            }
        }
    }
    private IEnumerator RestaurarVida()
    {
        while (energy < fullenergy)
        {
            energy += fullenergy * 0.1f; 
            yield return new WaitForSeconds(1f);
        }
        confirms = true; 
    }
}
