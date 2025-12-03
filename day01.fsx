open System.IO

// Parse a rotation instruction
let parseRotation (line: string) =
    let direction = line.[0]
    let distance = int (line.Substring(1))
    (direction, distance)

// Apply a rotation to the current position
let applyRotation position (direction, distance) =
    let newPosition = 
        match direction with
        | 'L' -> position - distance
        | 'R' -> position + distance
        | _ -> failwith "Invalid direction"
    
    // Normalize to 0-99 range (handle wrapping)
    let normalized = newPosition % 100
    if normalized < 0 then normalized + 100 else normalized

// Solve the puzzle
let solve inputFile =
    let rotations = 
        File.ReadAllLines(inputFile)
        |> Array.map parseRotation
    
    // Start at position 50
    let mutable position = 50
    let mutable zeroCount = 0
    
    for rotation in rotations do
        position <- applyRotation position rotation
        if position = 0 then
            zeroCount <- zeroCount + 1
    
    zeroCount

// Run the solution
let result = solve "day1_input.txt"
printfn "Part 1: %d" result
