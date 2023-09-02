using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    private GameObject gun;
    private GameObject laser;
    private GameObject fuelCan;

    void Awake()
    {
        gun = transform.Find("GunPrefab").gameObject;
        laser = transform.Find("Lazer").gameObject;
        fuelCan = transform.Find("FuelCan").gameObject;
    }

    public void Randomize()
    {
        var random = new System.Random();

        // randomize gun location
        RandomlyEnableAndLocateObject(gun, random, -15.0f, 7.5f);

        // randomize laser location
        RandomlyEnableAndLocateObject(laser, random, -17.5f, 7.3f);

        // randomize fuel can
        RandomlyEnableAndLocateObject(fuelCan, random, -15.7f, 9.1f);
    }

    private bool GetRandomBool(System.Random random)
    {
        return random.Next() > Int32.MaxValue / 2;
    }

    private void RandomlyEnableAndLocateObject(GameObject obj, System.Random random, float minX, float maxX)
    {
        obj.SetActive(GetRandomBool(random));
        obj.transform.localPosition = new Vector3(
            UnityEngine.Random.Range(minX, maxX),
            obj.transform.localPosition.y,
            0);
    }
}
