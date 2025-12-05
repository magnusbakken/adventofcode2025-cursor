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

// Part 2: Find the maximum joltage by selecting 12 batteries from each bank
let maxJoltageForBankPart2 (bank: string) =
    let digits = bank |> Seq.map (fun c -> int c - int '0') |> Seq.toArray
    let n = digits.Length
    let numToSelect = 12
    
    // Greedy approach: for each position, choose the largest digit
    // while ensuring enough digits remain
    let selected = Array.zeroCreate<int> numToSelect
    let mutable sourcePos = 0
    
    for i in 0 .. numToSelect - 1 do
        // We need (numToSelect - i) digits total, including this one
        // So we can look up to position (n - (numToSelect - i))
        let maxPos = n - (numToSelect - i)
        
        // Find the maximum digit in the valid range
        let mutable maxDigit = -1
        let mutable maxDigitPos = sourcePos
        for j in sourcePos .. maxPos do
            if digits.[j] > maxDigit then
                maxDigit <- digits.[j]
                maxDigitPos <- j
        
        selected.[i] <- maxDigit
        sourcePos <- maxDigitPos + 1
    
    // Convert the selected digits to a number
    let mutable result = 0L
    for digit in selected do
        result <- result * 10L + int64 digit
    result

let part1 =
    input
    |> Array.map maxJoltageForBank
    |> Array.sum

let part2 =
    input
    |> Array.map maxJoltageForBankPart2
    |> Array.sum

printfn "Part 1: %d" part1
printfn "Part 2: %d" part2
