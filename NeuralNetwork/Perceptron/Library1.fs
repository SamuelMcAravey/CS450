namespace Perceptron

open System;
open System.Linq;
open MathNet.Numerics;

type Neuron<'T> = { evaluator: (float[] -> 'T); weights: float[] }

type Activators() =
    static member abstractNeuronEvaluator (activator: (float -> 'T)) (inputs: float[]) =
        inputs
        |> Seq.sum
        |> activator;
        
    static member numericConverter: (float -> float) = fun total -> 
        match total with
        | t when t > 0.0 -> 1.0
        | _ -> 0.0;
        
    static member eSigmoidActivator: (float -> float) = fun total -> (1.0+Constants.E**(-total))**(-1.0);
    static member tanhSigmoidActivator: (float -> float) = Trig.Tanh;

    static member numericNeuronEvaluator = Activators.abstractNeuronEvaluator Activators.numericConverter;
    static member eSigmoidNeuronEvaluator = Activators.abstractNeuronEvaluator Activators.eSigmoidActivator;
    static member tanhSigmoidNeuronEvaluator = Activators.abstractNeuronEvaluator Activators.tanhSigmoidActivator;

type neuronLayer(neuronCount: int, inputCount: int, evaluator: (float[] -> float)) =
    let rand = System.Random((int)DateTime.Now.Ticks);
    let neurons = [|for i in 0 .. neuronCount - 1 -> {evaluator = evaluator; weights=[|for j in 0 .. inputCount -> rand.NextDouble() - 0.5|]} |];

    member this.Neurons = neurons;
    member this.EvaluateLayer(inputs: float[]) =
        this.Neurons 
            |> Array.map (fun neuron -> 
                inputs 
                |> Array.zip neuron.weights
                |> Array.map (fun (input, weight) -> input * weight)
                |> neuron.evaluator);
    member this.SetWeight(neuronIndex: int, weightIndex: int, value: float) = this.Neurons.[neuronIndex].weights.[weightIndex] <- value;

type public MultiLayerPerceptron(layers: neuronLayer[]) =
    member this.Layers = layers;
    member private this.EvaluateInternal (index: int) (inputs:float[]) =
        match index with
        | i when i < (this.Layers.Length - 1) -> this.EvaluateInternal (index + 1) (this.Layers.[index].EvaluateLayer(Array.append inputs [|-1.0|]))
        | _ -> this.Layers.[index].EvaluateLayer(Array.append inputs [|-1.0|]);
    member public this.Evaluate = this.EvaluateInternal 0;
    member public this.SetWeight(layerIndex: int, neuronIndex: int, weightIndex: int, value: float) = 
        this.Layers.[layerIndex].SetWeight(neuronIndex, weightIndex, value);

    static member createLayers(inputCount: int, layerSizes: int[], evaluator: (float[] -> float), layers: neuronLayer[]) =
        match layerSizes.Length with
            | i when i > 0 ->
                let layer = neuronLayer(layerSizes.[0], inputCount, evaluator)
                MultiLayerPerceptron.createLayers(layer.Neurons.Length, Array.skip 1 layerSizes, evaluator, Array.concat [layers; [|neuronLayer(layerSizes.[0], inputCount, evaluator)|]])
            | _ -> layers

    static member public CreateMLP(inputCount: int, layerSizes: int[], evaluator: (float[] -> float)) =
        MultiLayerPerceptron(MultiLayerPerceptron.createLayers(inputCount, layerSizes, evaluator, Array.empty));