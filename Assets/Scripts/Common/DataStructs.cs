using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GrowthRate
{
    private List<float> _rateList;
    
    public GrowthRate(Vector2 range, int battleCount)
    {
        _rateList = new List<float>();
        
        float factor = (range.y - range.x) / (battleCount - 1);

        for (int i = 0; i < battleCount; i++)
        {
            float rate = range.x + factor * i;
            
            _rateList.Add(rate);
        }
    }

    public float GetRate(int battleStageOrder)
    {
        if (battleStageOrder > _rateList.Count)
            throw new("Invalid Battle Stage Order");

        return _rateList[battleStageOrder - 1];
    }
}