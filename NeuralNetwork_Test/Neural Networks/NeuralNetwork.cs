using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_Test.Neural_Networks
{
    public class NeuralNetwork
    {
        public List<List<Neuron>> Layers = new List<List<Neuron>>();

        public NeuralNetwork()
        {
        }

        public void AddLayer(List<Neuron> layer)
        {
            Layers.Add(layer);
        }

        public float[] Train(float[] inputs, float[] expectedOutput)
        {
            var inputLayer = Layers[0];
            int i = 0;
            foreach(var inputNeuron in inputLayer)
            {
                inputNeuron.Output = inputs[i];
                i++;
            }

            for(int k = 1; k < Layers.Count; k++)
            {
                var layer = Layers[k];

                foreach (Neuron neuron in layer)
                {
                    neuron.Fire();
                }                
            }

            var previous_dks = new List<float>();

            for (int k = Layers.Count - 1; k > 0; k--)
            {
                var layer = Layers[k];
                var j = 0;

                foreach (Neuron neuron in layer)
                {
                    if(k == Layers.Count - 1) previous_dks.Add(neuron.Train(expectedOutput[j]));
                    else previous_dks.Add(neuron.Train(j));
                    j++;
                }
            }

            for (int k = 1; k < Layers.Count; k++)
            {
                var layer = Layers[k];
                foreach (Neuron neuron in layer)
                {
                    neuron.AdjustWeights();
                }
            }

            return Layers.Last().Select(x => x.Output).ToArray();
        }

        public float[] Fire(float[] inputs)
        {
            var inputLayer = Layers[0];
            int i = 0;
            foreach (var inputNeuron in inputLayer)
            {
                inputNeuron.Output = inputs[i];
                i++;
            }

            for (int k = 1; k < Layers.Count; k++)
            {
                var layer = Layers[k];

                foreach (Neuron neuron in layer)
                {
                    neuron.Fire();
                }                
            }

            return Layers.Last().Select(x => x.Output).ToArray();
        }
    }
}
