// Day 2: Gift Shop - Find invalid product IDs

let isInvalid (n: int64) =
    let s = string n
    let len = s.Length
    // Check if the number has even length and first half equals second half
    if len % 2 = 0 then
        let halfLen = len / 2
        s.Substring(0, halfLen) = s.Substring(halfLen, halfLen)
    else
        false

let parseRanges (input: string) =
    input.Split(',')
    |> Array.filter (fun s -> s.Trim() <> "")
    |> Array.map (fun range ->
        let parts = range.Trim().Split('-')
        int64 parts.[0], int64 parts.[1]
    )

let findInvalidIds (ranges: (int64 * int64)[]) =
    ranges
    |> Array.collect (fun (start, end_) ->
        [| start .. end_ |]
        |> Array.filter isInvalid
    )

// Part 1
let input = System.IO.File.ReadAllText("day2_input.txt")
let ranges = parseRanges input
let invalidIds = findInvalidIds ranges
let part1 = Array.sum invalidIds

printfn "Part 1: %d" part1
