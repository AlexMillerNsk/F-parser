
open System
open System.IO

let dirpath1 = "C:\Users\user\Downloads\Telegram Desktop\ChatExport_2023-04-17 (1)\Myfiles"
let filepath = @"C:\Users\user\Downloads\Telegram Desktop\ChatExport_2023-04-17 (1)\result.json"
let dirpath3 = "C:\Users\user\Downloads\Telegram Desktop\ChatExport_2023-04-17 (1)\Myfiles2"

let printValues (list: string list)  = 
    for value in list do
    printfn $"{value}"
    ()

let inline errorCounter x = 
    x
    |> Seq.groupBy id
    |> Seq.map (fun (elem, group) -> elem, Seq.length group)

let findNames =
    let mutable names = []
    use reader = new StreamReader(filepath)
    while not reader.EndOfStream do
        let line = reader.ReadLine()
        if line.Contains("UserName=") && line.Contains("MachineName") then
            let startIndex = line.IndexOf("UserName=") + 9
            let endIndex = line.IndexOf("nMachineName") - 1
            let username = line.Substring(startIndex, endIndex - startIndex)
            names <- username :: names
    names
    |> List.distinct

printfn "Количество оригинальных пользователей = %A" findNames.Length 

let printNames = findNames|>printValues

let findErrors  =
    let mutable errorlist = []
    let mutable debugList = []
    let mutable errorSys = []
    let files = Directory.GetFiles(dirpath1)
    for file in files do
        use streamReader = new StreamReader(file)
        while not streamReader.EndOfStream do
            let line = streamReader.ReadLine()
            if line.Contains("ERROR") then
                let dropLine = line.Substring 25
                errorlist <- dropLine :: errorlist
            if line.Contains("DEBUG Error") then
                let dropLine = line.Substring 25
                debugList <- dropLine :: debugList
            if line.Contains("ERROR System") then
                let dropLine = line.Substring 25
                errorSys <- dropLine :: errorSys   
    (errorlist, debugList, errorSys )

let errors = 
    match findErrors with
    | (x, _, _) -> x

let debug = 
    match findErrors with
    | (_, y, _) -> y

let errorSys = 
    match findErrors with
    | (_, _, z) -> z

let errorCount = errorCounter errors

let debugCount = errorCounter debug

let debugSysCount = errorCounter errorSys

printfn "КОЛИЧЕСТВО ОШИБОК В ЛОГАХ : %A" errorCount

printfn "КОЛИЧЕСТВО ДЕБАГ-ОШИБОК В ЛОГАХ : %A" debugCount

printfn "КОЛИЧЕСТВО SYSTEM-ОШИБОК В ЛОГАХ : %A" debugSysCount




let findLinesCount2 (directoryPath: string) =
    let files = Directory.GetFiles(directoryPath)
    let mutable ShowSettingsManagerWindow = 0
    let mutable ShowCalculationManagerWindow = 0
    let mutable ShowPanelsManagerWindow = 0
    let mutable UpdateFamilies = 0
    let mutable ShowPlacementManagerWindow = 0
    for file in files do
        use streamReader = new StreamReader(file)
        while not streamReader.EndOfStream do
            let line = streamReader.ReadToEnd()
            if line.Contains("ShowSettingsManagerWindow") then
                ShowSettingsManagerWindow <- ShowSettingsManagerWindow + 1
            if line.Contains("ShowCalculationManagerWindow") then
                ShowCalculationManagerWindow <- ShowCalculationManagerWindow + 1
            if line.Contains("ShowPanelsManagerWindow") then
                ShowPanelsManagerWindow <- ShowPanelsManagerWindow + 1
            if line.Contains("UpdateFamilies") then
                UpdateFamilies <- UpdateFamilies + 1
            if line.Contains("ShowPlacementManagerWindow") then
                ShowPlacementManagerWindow <- ShowPlacementManagerWindow + 1
    (ShowSettingsManagerWindow, ShowCalculationManagerWindow, ShowPanelsManagerWindow, UpdateFamilies, ShowPlacementManagerWindow)


let (ShowSettingsManagerWindow, ShowCalculationManagerWindow, ShowPanelsManagerWindow, UpdateFamilies, ShowPlacementManagerWindow) = findLinesCount2 dirpath1

printfn "Number of lines containing 'ShowSettingsManagerWindow': %d" ShowSettingsManagerWindow
printfn "Number of lines containing 'ShowCalculationManagerWindow': %d" ShowCalculationManagerWindow
printfn "Number of lines containing 'ShowPanelsManagerWindow': %d" ShowPanelsManagerWindow
printfn "Number of lines containing 'UpdateFamilies': %d" UpdateFamilies
printfn "Number of lines containing 'ShowPlacementManagerWindow': %d" ShowPlacementManagerWindow




