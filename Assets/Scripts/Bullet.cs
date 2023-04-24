using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1000f);
        Destroy(this.gameObject, 3f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        var contact = collision.GetContact(0);
        var obj = Instantiate(effect, contact.point, Quaternion.LookRotation(-contact.normal));
        Destroy(obj, 2f);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
