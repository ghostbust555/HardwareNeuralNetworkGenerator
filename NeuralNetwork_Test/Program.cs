using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NeuralNetwork_Test.Neural_Networks;
using System.Collections;

namespace NeuralNetwork_Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //VHDL();
            var f = new NetworkDesigner();
            Application.Run(f);            
        }

        const string STD_VECTOR = "STD_LOGIC_VECTOR(31 DOWNTO 0)";
        const string OUT = "OUT";
        const string IN = "IN";
        const string SIGNAL = "SIGNAL";
        const string SIG_VECTOR = ":  STD_LOGIC_VECTOR(31 DOWNTO 0)";

        public static void VHDL(NeuralNetwork nn)
        {
            string header = 
@"
library IEEE; 
use IEEE.std_logic_1164.all;
";

            string entityStart = 
@"
ENTITY NeuralNetwork IS 
	PORT
	(
        Clk : IN STD_LOGIC;
";

            string entityBody = "";
            string signals = "";

            for(int layerIdx = 0; layerIdx < nn.Layers.Count; layerIdx++)
            {
                var layer = nn.Layers[layerIdx];
                var inputLayer = layerIdx == 0;
                var outputLayer = layerIdx == nn.Layers.Count-1;
                for (int neuronIdx = 0; neuronIdx < layer.Count; neuronIdx++)
                {
                    if (inputLayer) {
                        entityBody += String.Format("I_{0} : {1} {2};\n", neuronIdx, IN, STD_VECTOR);                        
                    }
                    else if (outputLayer)
                    {
                        entityBody += String.Format("O_N{0} : {1} {2}", neuronIdx, OUT, STD_VECTOR);
                        if(neuronIdx < layer.Count - 1)
                        {
                            entityBody += ";\n";
                        }
                    }
                    else
                    {                        
                        string signalName = String.Format("L{0}_N{1}_OUT", layerIdx, neuronIdx);
                        signals += String.Format("{0} {1} {2};\n", SIGNAL, signalName, SIG_VECTOR);                        
                    }
                }
            }

            string entityStop = 
@"
    );
END NeuralNetwork;
";

            string architectureStart =
@"
ARCHITECTURE bdf_type OF NeuralNetwork IS 

COMPONENT \3Neuron\
	PORT(CLK : IN STD_LOGIC;
		 Input1 : IN STD_LOGIC_VECTOR(31 DOWNTO 0);
		 Input2 : IN STD_LOGIC_VECTOR(31 DOWNTO 0);
		 Input3 : IN STD_LOGIC_VECTOR(31 DOWNTO 0);
		 Weight1 : IN STD_LOGIC_VECTOR(31 DOWNTO 0);
		 Weight2 : IN STD_LOGIC_VECTOR(31 DOWNTO 0);
		 Weight3 : IN STD_LOGIC_VECTOR(31 DOWNTO 0);
		 output : OUT STD_LOGIC_VECTOR(31 DOWNTO 0)
	);
END COMPONENT;

";

            string architectureBody = "BEGIN \n";

            for (int layerIdx = 1; layerIdx < nn.Layers.Count; layerIdx++)
            {
                var layer = nn.Layers[layerIdx];
                var fromInputLayer = layerIdx == 1;
                var outputLayer = layerIdx == nn.Layers.Count - 1;
                for (int neuronIdx = 0; neuronIdx < layer.Count; neuronIdx++)
                {
                    string neuronStartString = String.Format(
@"
neuron_inst{0}_{1} : \3Neuron\
PORT MAP(
CLK => Clk,
", layerIdx, neuronIdx);
                    neuronStartString += "Input1 => \"00111111100000000000000000000000\",\n";
                    var outputString = String.Format("Output => L{0}_N{1}_OUT", layerIdx, neuronIdx);
                    
                    if (fromInputLayer)
                    {
                        for (int inputIdx = 1; inputIdx < layer[neuronIdx].N; inputIdx++)
                        {
                            neuronStartString += String.Format("Input{0} => I_{1},\n", inputIdx + 1, inputIdx - 1);
                        }                        
                    }
                    else 
                    {
                        if (outputLayer) outputString = String.Format("Output => O_N{1}", layerIdx, neuronIdx);
                        for (int inputIdx = 1; inputIdx < layer[neuronIdx].N; inputIdx++)
                        {
                            neuronStartString += String.Format("Input{0} => L{1}_N{2}_OUT,\n", inputIdx+1, layerIdx - 1, inputIdx-1);
                        }
                    }

                    for (int inputIdx = 0; inputIdx < layer[neuronIdx].N; inputIdx++)
                    {
                        neuronStartString += String.Format("Weight{0} => \"{1}\",\n", inputIdx + 1, GetFloatString(layer[neuronIdx].Weights[inputIdx]));
                    }

                    architectureBody += neuronStartString + outputString + ");\n\n";
                }
            }


            string output = header + entityStart + entityBody + entityStop + architectureStart + signals + architectureBody + "END bdf_type;";

            File.WriteAllText(@"C:\Users\Kyle\Documents\CompIntell\test.vhdl", output);
        }

        public static string GetFloatString(float f)
        {
            var b = new BitArray(BitConverter.GetBytes(f));
            var r = "";
            foreach(bool bit in b)
            {
                r += (bit?"1":"0");                
            }

            return r;
        }

        //static void NN()
        //{
        //    var r = new Random();

        //    var outputNode = new Neuron(2, r);

        //    int i = 0;

        //    for (i = 0; i < 100; i++)
        //    {
        //        outputNode.Train(0, 0, 0);
        //        outputNode.Train(0, 1, 0);
        //        outputNode.Train(0, 0, 1);
        //        outputNode.Train(1, 1, 1);
        //    }

        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Form1(i, outputNode));
        //}
    }
}
