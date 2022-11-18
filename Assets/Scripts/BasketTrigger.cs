using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)            //Trigger to detect ball has entered the basket 
    {
        if(other.gameObject.tag=="Ball")
        {
            GameManager.instance.isBallInBasket = true;         
        }
            
    }
}
