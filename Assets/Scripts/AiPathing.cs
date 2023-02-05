using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPathing : MonoBehaviour
{
    public GameObject target;
    public float speed;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;

        transform.position= Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
