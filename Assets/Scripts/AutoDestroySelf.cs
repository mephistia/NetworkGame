using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroySelf : MonoBehaviour
{
    public float seconds;

    private void FixedUpdate()
    {
        if ((seconds -= Time.fixedDeltaTime) <= 0f)
            Destroy(gameObject);
    }
}
