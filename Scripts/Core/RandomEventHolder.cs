using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventHolder : MonoBehaviour
{
    public List<RandomEventBase> goodEventCommonSleep;
    public List<RandomEventBase> goodEventCitySleep;
    public List<RandomEventBase> goodEventTownSleep;
    public List<RandomEventBase> goodEventWildSleep;

    public List<RandomEventBase> goodEventCommonSupply;
    public List<RandomEventBase> goodEventCitySupply;
    public List<RandomEventBase> goodEventTownSupply;
    public List<RandomEventBase> goodEventWildSupply;

    public List<RandomEventBase> badEventCommonSleep;
    public List<RandomEventBase> badEventCitySleep;
    public List<RandomEventBase> badEventTownSleep;
    public List<RandomEventBase> badEventWildSleep;

    public List<RandomEventBase> badEventCommonSupply;
    public List<RandomEventBase> badEventCitySupply;
    public List<RandomEventBase> badEventTownSupply;
    public List<RandomEventBase> badEventWildSupply;
    public RandomEventBase GetGoodEventCity(bool isSleep)
    {
        if(!isSleep)
            return goodEventCitySupply[Random.Range(0, goodEventCitySupply.Count)];

        return goodEventCitySleep[Random.Range(0, goodEventCitySleep.Count)];
    }
    public RandomEventBase GetGoodEventTown(bool isSleep)
    {
        if(!isSleep)
            return goodEventTownSupply[Random.Range(0, goodEventTownSupply.Count)];

        return goodEventTownSleep[Random.Range(0, goodEventTownSleep.Count)];
    }
    public RandomEventBase GetGoodEventWild(bool isSleep)
    {
        if (!isSleep)
            return goodEventWildSupply[Random.Range(0, goodEventWildSupply.Count)];

        return goodEventWildSleep[Random.Range(0, goodEventWildSleep.Count)];
    }
    public RandomEventBase GetGoodEventCommon(bool isSleep)
    {
        if (!isSleep)
            return goodEventCommonSupply[Random.Range(0, goodEventCommonSupply.Count)];

        return goodEventCommonSleep[Random.Range(0, goodEventCommonSleep.Count)];
    }



    public RandomEventBase GetBadEventCity(bool isSleep)
    {
        if (!isSleep)
            return badEventCitySupply[Random.Range(0, badEventCitySupply.Count)];

        return badEventCitySleep[Random.Range(0, badEventCitySleep.Count)];

    }
    public RandomEventBase GetBadEventTown(bool isSleep)
    {
        if (!isSleep)
            return badEventTownSupply[Random.Range(0, badEventTownSupply.Count)];

        return badEventTownSleep[Random.Range(0, badEventTownSleep.Count)];

    }
    public RandomEventBase GetBadEventWild(bool isSleep)
    {
        if (!isSleep)
            return badEventWildSupply[Random.Range(0, badEventWildSupply.Count)];

        return badEventWildSleep[Random.Range(0, badEventWildSleep.Count)];

    }
    public RandomEventBase GetBadEventCommon(bool isSleep)
    {
        if (!isSleep)
            return badEventCommonSupply[Random.Range(0, badEventCommonSupply.Count)];

        return badEventCommonSleep[Random.Range(0, badEventCommonSleep.Count)];

    }

}