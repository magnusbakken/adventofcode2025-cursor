open System.IO

let input = File.ReadAllLines("day3_input.txt")

// Part 1: Find the maximum joltage for each bank
let maxJoltageForBank (bank: string) =
    let digits = bank |> Seq.map (fun c -> int c - int '0') |> Seq.toArray
    
    // Try all pairs of positions (i, j) where i < j
    let mutable maxJoltage = 0
    for i in 0 .. digits.Length - 2 do
        for j in i + 1 .. digits.Length - 1 do
            let joltage = digits.[i] * 10 + digits.[j]
            if joltage > maxJoltage then
                maxJoltage <- joltage
    maxJoltage

let part1 =
    input
    |> Array.map maxJoltageForBank
    |> Array.sum

printfn "Part 1: %d" part1
