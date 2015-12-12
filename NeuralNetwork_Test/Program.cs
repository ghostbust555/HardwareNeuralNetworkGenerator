using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NeuralNetwork_Test.Neural_Networks;

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
            var f = new Form1();
            Application.Run(f);            
        }

        const string STD_VECTOR = "STD_LOGIC_VECTOR(31 DOWNTO 0)";
        const string OUT = "OUT";
        const string IN = "IN";
        const string SIGNAL = "SIGNAL";
        const string SIG_VECTOR = ":  STD_LOGIC_VECTOR(31 DOWNTO 0);";

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
                    if (!inputLayer) { 
                        for (int inputIdx = 0; inputIdx < layer[neuronIdx].N; inputIdx++)
                        {
                            entityBody += String.Format("W_L{0}_N{1}_{2} : {3} {4};\n", layerIdx, neuronIdx, inputIdx, IN, STD_VECTOR);
                        }
                    }

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
                        signals += String.Format("{0} {1} {2}", SIGNAL, signalName, SIG_VECTOR);                        
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

            string output = header + entityStart + entityBody + entityStop;

            File.WriteAllText(@"C:\Users\Kyle\Documents\CompIntell\test.vhdl", output);
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
