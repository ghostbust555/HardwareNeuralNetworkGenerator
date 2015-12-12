using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_Test
{
    public class Neuron
    {
        public List<Neuron> Parents;
        public List<Neuron> Children;
        public float Output;
        public float LastOutput;
        public float Dk = 0;
        public float A = .5f;
        public float Err = 0;

        public Random R;
        public float[] Weights;
        public int N;

        public Neuron() { }

        //public void Punish(bool falsePositive)//true - false positive, false - false negative
        //{
        //    float[] copy = new float[N];
        //    int[] indexes = Enumerable.Range(0, copy.Length).ToArray();
        //    Weights.CopyTo(copy, 0);
        //    Array.Sort(copy, indexes);

        //    int start = N / 2;
        //    int stop = N;

        //    if (!falsePositive) { 
        //        start = 0;
        //        stop = N / 2;
        //    }

        //    for (int i = start; i < stop; i++)
        //    {
        //        int idx = indexes[i];
        //        if (falsePositive) Weights[idx] -= (float)R.NextDouble()/5.0f;
        //        else Weights[idx] += (float)R.NextDouble()/5.0f;
        //        if (Parents != null && Parents.Length > 0 && idx > 0) Parents[idx].Punish(falsePositive);
        //    }            

        //   // normalizeWeights();
        //}

        public virtual void AdjustWeights()
        {
        }

        public void normalizeWeights()
        {
            float sum = 0;
            float currentWeight = 0;

            for (int i = 0; i < N; i++)
            {
                currentWeight = Weights[i];
                sum += currentWeight * currentWeight;
            }

            float magnitude = (float)Math.Sqrt(sum);

            for (int i = 0; i < N; i++)
            {
                Weights[i] /= magnitude;
            }
        }
        public virtual float Train(float expected = 0)
        {
            return 0;
        }

        public float sigmoid(float x)
        {
            return (float)(1 / (1 + Math.Exp(-x)));
        }

        public virtual void Fire() {}
    }
}
