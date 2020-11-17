using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyManager : MonoBehaviour
{
    public int id;
    public float timeToDestroy; // mostrar
    public float animSpeed = 2.5f;
    public Vector3 basePos;
    public TextMeshPro text;

    public void Initialize(int _id, float _timeToDestroy)
    {
        id = _id;
        timeToDestroy = _timeToDestroy;
        basePos = transform.position;
    }

    private void Update()
    {
        // animação
        transform.position = basePos + new Vector3(0f, 0.25f * Mathf.Sin(Time.time * animSpeed), 0f);

        // mostrar o texto
        text.text = timeToDestroy.ToString("F0");

        // atualizar
        timeToDestroy -= Time.deltaTime;
    }

    public void Despawn()
    {
        // animação?
        // ...
        Debug.Log($"Energy {id} despawned.");

        if (GameManager.energies.ContainsKey(id))
            GameManager.energies.Remove(id);

        Destroy(gameObject);
    }
}
