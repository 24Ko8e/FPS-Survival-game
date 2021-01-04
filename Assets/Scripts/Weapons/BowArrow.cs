using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowArrow : MonoBehaviour
{
    Rigidbody body;
    public float speed = 30f;
    public float deactivateTimer = 3f;
    public float damage = 35f;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameobject", deactivateTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch( Camera camera)
    {
        body.velocity = camera.transform.forward * speed;
        transform.LookAt(transform.position + body.velocity);
    }

    void DeactivateGameobject()
    {
        if(gameObject.activeInHierarchy)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Enemy")
        {
            target.GetComponent<HealthScript>().applyDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
