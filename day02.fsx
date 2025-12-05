// Day 2: Gift Shop - Find invalid product IDs

// Part 1: Check if number is a pattern repeated exactly twice
let isInvalidPart1 (n: int64) =
    let s = string n
    let len = s.Length
    // Check if the number has even length and first half equals second half
    if len % 2 = 0 then
        let halfLen = len / 2
        s.Substring(0, halfLen) = s.Substring(halfLen, halfLen)
    else
        false

// Part 2: Check if number is a pattern repeated at least twice
let isInvalidPart2 (n: int64) =
    let s = string n
    let len = s.Length
    // Try all possible pattern lengths from 1 to len/2
    [1 .. len/2]
    |> List.exists (fun patternLen ->
        // Check if string length is divisible by pattern length
        if len % patternLen = 0 then
            let pattern = s.Substring(0, patternLen)
            // Check if the entire string is this pattern repeated
            let repeated = String.replicate (len / patternLen) pattern
            s = repeated
        else
            false
    )

let parseRanges (input: string) =
    input.Split(',')
    |> Array.filter (fun s -> s.Trim() <> "")
    |> Array.map (fun range ->
        let parts = range.Trim().Split('-')
        int64 parts.[0], int64 parts.[1]
    )

let findInvalidIds (ranges: (int64 * int64)[]) (isInvalid: int64 -> bool) =
    ranges
    |> Array.collect (fun (start, end_) ->
        [| start .. end_ |]
        |> Array.filter isInvalid
    )

// Read input
let input = System.IO.File.ReadAllText("day2_input.txt")
let ranges = parseRanges input

// Part 1
let invalidIdsPart1 = findInvalidIds ranges isInvalidPart1
let part1 = Array.sum invalidIdsPart1
printfn "Part 1: %d" part1

// Part 2
let invalidIdsPart2 = findInvalidIds ranges isInvalidPart2
let part2 = Array.sum invalidIdsPart2
printfn "Part 2: %d" part2
