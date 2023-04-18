module Library

open System.IO

let CountUniqueUserNames (filePath: string) =
    let mutable names = []
    use reader = new StreamReader(filePath)
    while not reader.EndOfStream do
        let line = reader.ReadLine()
        if line.Contains("UserName=") && line.Contains("MachineName") then
            let indexStart = line.IndexOf("UserName=")    + 9
            let indexEnd   = line.IndexOf("nMachineName") - 1
            let username   = line.Substring(indexStart, indexEnd - indexStart)
            names <- username :: names
    List.distinct names


let ParseLogs dirLogs =
    let mutable infos  = []
    let mutable debugs = []
    let mutable errors = []
    let files = Directory.GetFiles dirLogs
    for file in files do
        use streamReader = new StreamReader(file)
        while not streamReader.EndOfStream do
        let line = streamReader.ReadLine()
        let (<->) (s: string) (n: string) = s.Contains n
        match line with
        | s when s <-> "INFO"  -> infos  <- s.Substring 25 :: infos
        | s when s <-> "DEBUG" -> debugs <- s.Substring 25 :: debugs
        | s when s <-> "ERROR" -> errors <- s.Substring 25 :: errors
        | _ -> ()
    infos, debugs, errors


let FindLinesCount directoryPath =
    let files = Directory.GetFiles directoryPath
    let mutable ShowSettingsManagerWindow = 0
    let mutable ShowCalculationManagerWindow = 0
    let mutable ShowPanelsManagerWindow = 0
    let mutable UpdateFamilies = 0
    let mutable ShowPlacementManagerWindow = 0
    
    for file in files do
        use streamReader = new StreamReader(file)
        while not streamReader.EndOfStream do
            let line = streamReader.ReadToEnd()
            if line.Contains("ShowSettingsManagerWindow")    then ShowSettingsManagerWindow    <- ShowSettingsManagerWindow + 1
            if line.Contains("ShowCalculationManagerWindow") then ShowCalculationManagerWindow <- ShowCalculationManagerWindow + 1
            if line.Contains("ShowPanelsManagerWindow")      then ShowPanelsManagerWindow      <- ShowPanelsManagerWindow + 1
            if line.Contains("UpdateFamilies")               then UpdateFamilies               <- UpdateFamilies + 1
            if line.Contains("ShowPlacementManagerWindow")   then ShowPlacementManagerWindow   <- ShowPlacementManagerWindow + 1
    
    ShowSettingsManagerWindow,
    ShowCalculationManagerWindow,
    ShowPanelsManagerWindow,
    UpdateFamilies,
    ShowPlacementManagerWindow