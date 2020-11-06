using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileManager : MonoBehaviour
{
    public int id;
    public GameObject dmgVisualFeedback;
    public Material blood;
    public string collisionTag;

    public void Initialize(int _id)
    {
        id = _id;
    }

    public void DamageVisualFeedback(Vector3 _position, Vector3 _direction)
    {
        transform.position = _position;
        Instantiate(dmgVisualFeedback, dmgVisualFeedback.transform.position + transform.position, Quaternion.Euler(dmgVisualFeedback.transform.rotation.eulerAngles + new Vector3(0,0, ToAngle(-_direction.normalized))));
        // se for tag de inimigo, mudar material para sangue

        GameManager.projectiles.Remove(id);
        Destroy(gameObject);
    }

    private static float ToAngle(Vector3 _vector)
    {
        if (_vector.x < 0)
            return 360 - (Mathf.Atan2(_vector.x, _vector.y) * Mathf.Rad2Deg * -1);
        

        return Mathf.Atan2(_vector.x, _vector.y) * Mathf.Rad2Deg;
        
    }

    public void SetCollisionTag(string _tag)
    {
        tag = _tag;
    }
}
