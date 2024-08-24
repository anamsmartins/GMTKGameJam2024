using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatManager : MonoBehaviour
{
    public static RatManager Instance { get; private set; } = null;
    
    private Dictionary<string, List<float>> maxScaleValues = new Dictionary<string, List<float>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DefineMaxScaleFactors();
    }

    private void DefineMaxScaleFactors()
    {
        List<float> rat1MaxScaleValues = new List<float>
        {
            0, // left
            0, // right
            300, // up
            200, // down
        };
        maxScaleValues.Add("Rat1", rat1MaxScaleValues);

        List<float> rat2MaxScaleValues = new List<float>
        {
            0, // left
            0, // right
            100, // up
            0, // down
        };
        maxScaleValues.Add("Rat2", rat2MaxScaleValues);

        List<float> rat3MaxScaleValues = new List<float>
        {
            0, // left
            100, // right
            100, // up
            0, // down
        };
        maxScaleValues.Add("Rat3", rat3MaxScaleValues);
    }

    public List<float> GetRatMaxScaleValuesList(string name)
    {
        return maxScaleValues[name];
    }

    public float GetRatMaxScaleValue(string name, int index)
    {
        return maxScaleValues[name][index];
    }

    public void SetRatMaxScaleValues(string name, int index, float value)
    {
        maxScaleValues[name][index] = value;
    }

    public bool HasCompletedPuzzle()
    {
        foreach (KeyValuePair<string, List<float>> maxScaleValue in maxScaleValues)
        {
            foreach (float value in maxScaleValue.Value)
            {
                if (value != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
