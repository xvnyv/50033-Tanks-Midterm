using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public int minPowerups = 2;
    public int maxPowerups = 10;
    public int totalCactus = 10;
    public GameObject cactus;
    public GameObject powerup;

    private System.Random rd = new System.Random();
    private GameObject[] cactusGameObjects;
    private GameObject[] powerupGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (minPowerups > totalCactus)
        {
            // Set to default 2 if invalid value
            minPowerups = 2;
        }

        if (maxPowerups > totalCactus)
        {
            // Set to default totalCactus if invalid value
            maxPowerups = totalCactus;
        }

        cactusGameObjects = new GameObject[totalCactus];
        powerupGameObjects = new GameObject[maxPowerups];

        for (int i = 0; i < totalCactus; i++)
        {
            GameObject obj = Instantiate(cactus, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            cactusGameObjects[i] = obj;
        }

        for (int i = 0; i < maxPowerups; i++)
        {
            GameObject obj = Instantiate(powerup, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            powerupGameObjects[i] = obj;
        }
    }

    public void SpawnPowerups(int winScore, int playerScore)
    {
        int roundPowerups = playerScore == 0 ? maxPowerups : (maxPowerups - minPowerups) * (winScore - 1 - playerScore) / winScore + minPowerups;

        for (int i = 0; i < cactusGameObjects.Length; i++)
        {
            // Reset game objects
            cactusGameObjects[i].SetActive(false);
            powerupGameObjects[i].SetActive(false);

            // Activate cactus and assign powerups
            cactusGameObjects[i].transform.position = GetRandomPosition();
            BreakableCactus breakableCactus = cactusGameObjects[i].GetComponent<BreakableCactus>();
            breakableCactus.hasPowerup = i < roundPowerups;
            if (i < roundPowerups)
            {
                breakableCactus.powerup = powerupGameObjects[i];
            }
            cactusGameObjects[i].SetActive(true);
        }
    }

    private Vector3 GetRandomPosition()
    {
        bool foundPosition = false;
        Vector3 newPosition = new Vector3(rd.Next(-38, 38), 0, rd.Next(-38, 38));
        while (!foundPosition)
        {
            foundPosition = true;
            Collider[] colliders = Physics.OverlapSphere(newPosition, 1);
            for (int j = 0; j < colliders.Length; j++)
            {
                if (!colliders[j].CompareTag("Ground"))
                {
                    newPosition = new Vector3(rd.Next(-38, 38), 0, rd.Next(-38, 38));
                    foundPosition = false;
                    break;
                }
            }
        }
        return newPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
