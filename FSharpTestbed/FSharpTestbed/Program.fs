// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System;
open System.Linq;

type Neuron<'T> = { evaluator: (float[] -> 'T); weights: float[] }
let abstractNeuronEvaluator (neuronConverter: (float -> 'T)) = fun(inputs: float[]) -> 
        inputs
        |> Seq.sum
        |> neuronConverter;
        
let numericConverter: (float -> float) = fun total -> 
    match total with
    | t when t > 0.0 -> 1.0
    | _ -> 0.0;

let numericNeuronEvaluator() = fun(inputs: float[]) -> abstractNeuronEvaluator numericConverter inputs;

type neuronLayer(neuronCount: int, inputCount: int) =
    let mutable currentNeuronCount = neuronCount;
    do currentNeuronCount <- neuronCount - 1;
    let mutable currentInputCount = inputCount;
    do currentInputCount <- inputCount - 1;

    member this.Neurons = [|for i in 0 .. currentNeuronCount -> {evaluator = numericNeuronEvaluator(); weights=[|for j in 0 .. currentInputCount -> 0.1|]} |];
    member this.EvaluateLayer(inputs: float[]) =
        this.Neurons 
            |> Array.map (fun neuron -> 
                inputs 
                |> Array.zip neuron.weights
                |> Array.map (fun (input, weight) -> input * weight)
                |> neuron.evaluator);
    member this.SetWeight(neuronIndex: int, weightIndex: int, value: float) = this.Neurons.[neuronIndex].weights.[weightIndex] <- value;

//let layer (neuronCount: int) (inputCount: int) =
//    let inputs = Array.zeroCreate inputCount
//    //let mutable neurons: (unit -> float) list = [];
//    let neurons = ResizeArray<unit -> float>();
//    for i = 0 to neuronCount do 
//        let weights = [|for j in 0 .. inputCount -> 0.1|];
//        let mappedWeights = (Seq.mapi (fun index -> fun weight -> fun() -> inputs.[index]*weight) weights).ToArray();
//        let currentNeuron = neuron mappedWeights;
//        neurons.Add currentNeuron;

[<EntryPoint>]
let main argv = 
    let l = new neuronLayer(5,2);
    let res = l.EvaluateLayer([|4.0;-1.0|]);
    for r in res do
        printfn "%f" r;
    printfn "%A" argv
    Console.ReadLine();
    0 // return an integer exit code
