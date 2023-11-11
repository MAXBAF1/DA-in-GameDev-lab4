using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Perceptron : MonoBehaviour
{
    public TrainingSet[] trainingSets;
    
    private readonly double[] _weights = { 0, 0 };
    private double _bias;
    private double _totalError;
    private int ErrorsNumber { get; set; }

    private double DotProductBias(IReadOnlyCollection<double> v1, IReadOnlyList<double> v2)
    {
        if (v1 == null || v2 == null || v1.Count != v2.Count) return -1;
        
        return v1.Select((t, x) => t * v2[x]).Sum() + _bias;
    }

    private double CalcOutput(int i) => DotProductBias(_weights, trainingSets[i].input) > 0 ? 1 : 0;

    private void InitialiseWeights()
    {
        for (var i = 0; i < _weights.Length; i++)
            _weights[i] = Random.Range(-1.0f, 1.0f);

        _bias = Random.Range(-1.0f, 1.0f);
    }

    private void UpdateWeights(int j)
    {
        var error = trainingSets[j].output - CalcOutput(j);
        _totalError += Mathf.Abs((float)error);

        for (var i = 0; i < _weights.Length; i++)
            _weights[i] += error * trainingSets[j].input[i];

        _bias += error;
    }

    private double CalcOutput(double i1, double i2)
    {
        var inp = new[] { i1, i2 };
        var dp = DotProductBias(_weights, inp);
        
        return dp > 0 ? 1 : 0;
    }

    private void Train(int epochsCnt)
    {
        InitialiseWeights();

        for (var e = 0; e < epochsCnt; e++)
        {
            _totalError = 0;
            for (var t = 0; t < trainingSets.Length; t++)
            {
                UpdateWeights(t);
                Debug.Log($"W1: {_weights[0]} W2: {_weights[1]} B: {_bias}");
            }
            Debug.Log($"Epoch: {e + 1} Errors: {_totalError}");
            
            if (_totalError > 0) ErrorsNumber++;
            else break;
        }
    }

    private void Start()
    {
        Train(20);
        Debug.Log("------------------------------------------------------------------");
        Debug.Log($"Result {trainingSets[0].input[0]} {trainingSets[0].input[1]}: " + CalcOutput(trainingSets[0].input[0], trainingSets[0].input[1]));
        Debug.Log($"Result {trainingSets[1].input[0]} {trainingSets[1].input[1]}: " + CalcOutput(trainingSets[1].input[0], trainingSets[1].input[1]));
        Debug.Log($"Result {trainingSets[2].input[0]} {trainingSets[2].input[1]}: " + CalcOutput(trainingSets[2].input[0], trainingSets[2].input[1]));
        Debug.Log($"Result {trainingSets[3].input[0]} {trainingSets[3].input[1]}: " + CalcOutput(trainingSets[3].input[0], trainingSets[3].input[1]));
    }
}