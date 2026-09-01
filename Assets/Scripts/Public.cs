using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Public : MonoBehaviour
{
    public GameObject[] spectators;

    private void Start()
    {
        AllDissapear();
    }
    public void AllDissapear()
    {
        foreach (var spec in spectators)
        {
            spec.SetActive(false);
        }
        Shuffle(spectators);
    }
    public void Shuffle(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
    internal void SetPublics(int currentHealth, int maxHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;

        int spectatorsToActivate = Mathf.FloorToInt(
            (1f - healthPercentage) * spectators.Length
        );

        for (int i = 0; i < spectators.Length; i++)
        {
            spectators[i].SetActive(i < spectatorsToActivate);
        }
    }
}
