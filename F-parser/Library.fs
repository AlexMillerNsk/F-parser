module Library

open System
open System.IO
open Types
open FSharp.Data

type Names = JsonProvider<"../result.json">

let names = Names.GetSample()

let Names() = 
    names.Messages
    |>Array.map (fun x -> x.Text|>string)
    |>Array.filter (fun x -> x.Contains "UserName=" )
    |>Array.toList|>List.distinct|>List.iter (fun x -> printfn $"{x}")


let CountUniqueUserNames (filePath: string) =
    let mutable names = []
    use reader = new StreamReader(filePath)
    while not reader.EndOfStream do
        let line = reader.ReadLine()
        if line.Contains("UserName=") && line.Contains("MachineName") then
            let startIndex = line.IndexOf("UserName=") + 9
            let endIndex = line.IndexOf("nMachineName") - 1
            let username = line.Substring(startIndex, endIndex - startIndex)
            names <- username :: names
    names
    |> List.distinct 

let Create id time typeof text = {Id = id; Time = time; Type = typeof; Text = text}

let ParseLogs dirLogs  =
    let mutable errormessages = []
    let files = Directory.GetFiles(dirLogs)
    for file in files do
        use streamReader = new StreamReader(file)
        while not streamReader.EndOfStream do
        let line = streamReader.ReadLine()
        let (<->) (s: string) (n: string) = s.Contains n
        match line with
        | s when s <-> "INFO"  -> errormessages  <- Create 0 (s.Substring (11, 8)) Info (s.Substring 25):: errormessages
        | s when s <-> "DEBUG" -> errormessages <-  Create 0 (s.Substring (11, 8)) Debug (s.Substring 25):: errormessages
        | s when s <-> "ERROR" -> errormessages <-  Create 0 (s.Substring (11, 8)) Error (s.Substring 25):: errormessages
        | _ -> ()
    errormessages

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