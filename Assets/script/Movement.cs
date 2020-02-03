using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Speed * Time.deltaTime;
    }
    public void setSpeed(float f){
        Speed = f;
    }
    public float getSpeed(){
        return Speed;
    }
    public float getX(){
        return transform.position.x;
    }
}
