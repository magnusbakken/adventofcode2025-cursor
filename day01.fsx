open System.IO

// Parse a rotation instruction
let parseRotation (line: string) =
    let direction = line.[0]
    let distance = int (line.Substring(1))
    (direction, distance)

// Apply a rotation to the current position and return (newPosition, timesPointingAtZero)
let applyRotation position (direction, distance) =
    let newPosition = 
        match direction with
        | 'L' -> position - distance
        | 'R' -> position + distance
        | _ -> failwith "Invalid direction"
    
    // Normalize to 0-99 range (handle wrapping)
    let normalized = newPosition % 100
    let finalPos = if normalized < 0 then normalized + 100 else normalized
    
    // Count how many times we point at 0 during this rotation
    let zeroCount = 
        match direction with
        | 'R' -> 
            // For right rotation, we cross 0 every 100 clicks
            (position + distance) / 100
        | 'L' -> 
            // For left rotation, we cross 0 when going backwards through it
            // We hit 0 after 'position' clicks, then every 100 clicks after that
            distance / 100 + (if distance % 100 > position then 1 else 0)
        | _ -> failwith "Invalid direction"
    
    (finalPos, zeroCount)

// Solve part 1: count only when ending at 0
let solvePart1 inputFile =
    let rotations = 
        File.ReadAllLines(inputFile)
        |> Array.map parseRotation
    
    // Start at position 50
    let mutable position = 50
    let mutable zeroCount = 0
    
    for rotation in rotations do
        let (newPos, _) = applyRotation position rotation
        position <- newPos
        if position = 0 then
            zeroCount <- zeroCount + 1
    
    zeroCount

// Solve part 2: count all times pointing at 0 (during and at end of rotations)
let solvePart2 inputFile =
    let rotations = 
        File.ReadAllLines(inputFile)
        |> Array.map parseRotation
    
    // Start at position 50
    let mutable position = 50
    let mutable totalZeroCount = 0
    
    for rotation in rotations do
        let (newPos, zerosDuringRotation) = applyRotation position rotation
        position <- newPos
        totalZeroCount <- totalZeroCount + zerosDuringRotation
    
    totalZeroCount

// Run the solutions
let result1 = solvePart1 "day1_input.txt"
let result2 = solvePart2 "day1_input.txt"
printfn "Part 1: %d" result1
printfn "Part 2: %d" result2
