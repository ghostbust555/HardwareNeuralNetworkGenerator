using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_Test
{
    public class HiddenNeuron:Neuron
    {
        public HiddenNeuron(Random r, ref List<Neuron> parents) {
            N = parents.Count + 1;
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
        /// <param name="Ok">target output of K</param>
        /// <param name="Tk">output of K</param>
        public float GetError(float Ok, float[] inputs, int index)
        {
            float sum = 0;
            
            foreach(Neuron n in Children)
            {
                sum += n.Dk * n.Weights[index+1];
            }

            Dk = Ok * (1 - Ok) * (sum);
            
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
        public override float Train(float index = 0)
        {
            return GetError(getOutput(), getInputs(), (int)index);
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
