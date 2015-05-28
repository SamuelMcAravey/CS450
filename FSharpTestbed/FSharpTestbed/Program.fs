// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System;
open System.Linq;
open MathNet.Numerics;

type Neuron<'T> = { evaluator: (float[] -> 'T); weights: float[] }
let abstractNeuronEvaluator (activator: (float -> 'T)) (inputs: float[]) =
        inputs
        |> Seq.sum
        |> activator;
        
let numericConverter: (float -> float) = fun total -> 
    match total with
    | t when t > 0.0 -> 1.0
    | _ -> 0.0;
        
let eSigmoidActivator: (float -> float) = fun total -> (1.0+Constants.E**(-total))**(-1.0);
let tanhSigmoidActivator: (float -> float) = Trig.Tanh;

let numericNeuronEvaluator = abstractNeuronEvaluator numericConverter;
let eSigmoidNeuronEvaluator = abstractNeuronEvaluator eSigmoidActivator;
let tanhSigmoidNeuronEvaluator = abstractNeuronEvaluator tanhSigmoidActivator;

type neuronLayer(neuronCount: int, inputCount: int, evaluator: (float[] -> float)) =
    let mutable currentNeuronCount = neuronCount;
    do currentNeuronCount <- neuronCount - 1;
    let mutable currentInputCount = inputCount;
    do currentInputCount <- inputCount - 1;
    let rand = System.Random();
    let neurons = [|for i in 0 .. currentNeuronCount -> {evaluator = evaluator; weights=[|for j in 0 .. currentInputCount -> rand.NextDouble() - 0.5|]} |];

    member this.Neurons = neurons;
    member this.EvaluateLayer(inputs: float[]) =
        this.Neurons 
            |> Array.map (fun neuron -> 
                inputs 
                |> Array.zip neuron.weights
                |> Array.map (fun (input, weight) -> input * weight)
                |> neuron.evaluator);
    member this.SetWeight(neuronIndex: int, weightIndex: int, value: float) = this.Neurons.[neuronIndex].weights.[weightIndex] <- value;

type mlp(layers: neuronLayer[]) =
    member this.Layers = layers;
    member private this.EvaluateInternal (index: int) (inputs:float[]) =
        match index with
        | i when i < (this.Layers.Length - 1) -> this.EvaluateInternal (index + 1) (this.Layers.[index].EvaluateLayer(inputs))
        | _ -> this.Layers.[index].EvaluateLayer(inputs);
    member this.Evaluate = this.EvaluateInternal 0;
    member this.SetWeight(layerIndex: int, neuronIndex: int, weightIndex: int, value: float) = 
        this.Layers.[layerIndex].SetWeight(neuronIndex, weightIndex, value);

let rec createLayers(inputCount: int, layerSizes: int[], evaluator: (float[] -> float), layers: neuronLayer[]) =
    match layerSizes.Length with
        | i when i > 0 ->
            let layer = neuronLayer(layerSizes.[0], inputCount, evaluator)
            createLayers(layer.Neurons.Length, Array.skip 1 layerSizes, evaluator, Array.concat [layers; [|neuronLayer(layerSizes.[0], inputCount, evaluator)|]])
        | _ -> layers

let createMLP(inputCount: int, layerSizes: int[], evaluator: (float[] -> float)) =
    mlp(createLayers(inputCount, layerSizes, evaluator, Array.empty));

[<EntryPoint>]
let main argv = 
    let r = System.Random();
    let l = createMLP(2,[|3|], eSigmoidNeuronEvaluator);
    for i in 0..10 do
        let res = l.Evaluate([|4.0;-1.0|]);
        for r in res do
            printfn "%f" r;
        l.SetWeight(0,0,0,r.NextDouble()-0.5);
        Console.WriteLine();
    printfn "%A" argv
    Console.ReadLine();
    0 // return an integer exit code
