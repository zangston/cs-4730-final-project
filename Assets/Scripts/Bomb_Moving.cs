using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Moving : MonoBehaviour
{
    public float speed = 5.0f;
    //public int plasmaDamage = 10;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }

}
