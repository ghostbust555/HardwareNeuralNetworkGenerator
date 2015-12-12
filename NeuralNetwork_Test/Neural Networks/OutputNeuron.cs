using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_Test
{
    public class OutputNeuron:Neuron
    {
        public OutputNeuron(Random r, ref List<Neuron> parents) {
            N = parents.Count()+ 1;
            R = r;
            Parents = parents;
            Weights = new float[N];

            for(int i = 0; i < N; i++)
            {
                Weights[i] = (float)r.NextDouble();
            }

            normalizeWeights();
        }

        /// <summary>
        /// </summary>
        /// <param name="Ok">output of K</param>
        /// <param name="Tk">target of K</param>
        public float GetError(float Ok, float Tk, float [] inputs)
        {            
            Dk = Ok * (1 - Ok) * (Ok - Tk);     
            return Dk;
        }

        public override void AdjustWeights()
        {
            var inputs = getInputs();

            for (int i = 0; i < N; i++)
            {
                Weights[i] -= A * Dk * inputs[i];
            }
        }
        
        public override float Train(float expected)
        {
            return GetError(getOutput(), expected, getInputs());
        }

        private float[] getInputs()
        {
            float[] withBias = new float[N];
            int j = 0;
            withBias[j] = 1;
            foreach (var parent in Parents)
            {
                j++;
                withBias[j] = parent.Output;
            }
            return withBias;
        }

        private float getOutput()
        {
            float[] withBias = getInputs();

            float sum = 0;
            for (int i = 0; i < N; i++)
            {
                sum += withBias[i] * Weights[i];
            }

            var rVal = sigmoid(sum);
            return Output = rVal;
        }
        
        public override void Fire()
        {
            float[] withBias = getInputs();

            float sum = 0;
            for (int i = 0; i < N; i++)
            {
                sum += withBias[i] * Weights[i];
            }

            var rVal = sigmoid(sum);
            Output = rVal;
        }             
    }
}
