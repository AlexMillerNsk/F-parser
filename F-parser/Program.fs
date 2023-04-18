module Program

open System.IO
open Library

let DirRoot         = "C:\Users\MSbook\Desktop\Data"
let LogsDirName     = "Logs"
let MessagesDirName = "Messages"
let MessagesFileName = "Messages.json"
let Comb paths       = paths |> List.toArray |> Path.Combine
let LogsDir          = Comb [ DirRoot; LogsDirName ]
let MessagesDir      = Comb [ DirRoot; MessagesDirName ]
let MessagesFilePath = Comb [ MessagesDir; MessagesFileName ]

let PrintValues  = List.iter(fun x -> printfn $"{x}")
let GroupBySelf  = List.groupBy id >> List.map (fun (n, g) -> n, List.length g)
let printNames() = MessagesFilePath |> CountUniqueUserNames  |> PrintValues

let infos, debuds, errors = ParseLogs LogsDir

let CountInfo  = GroupBySelf infos
let CountDebug = GroupBySelf debuds
let CountError = GroupBySelf errors

printfn "-------------------------------------------\n"
printfn $"Number of info:  {CountInfo}"
printfn $"Number of debug: {CountDebug}" 
printfn $"Number of error: {CountError}" 
printfn "-------------------------------------------\n"

printNames()

printfn "-------------------------------------------\n"

let ShowSettingsManagerWindow,
    ShowCalculationManagerWindow,
    ShowPanelsManagerWindow,
    UpdateFamilies,
    ShowPlacementManagerWindow =
        FindLinesCount LogsDir

printfn "-------------------------------------------\n"

printfn $"ShowSettingsManagerWindow:    {ShowSettingsManagerWindow}" 
printfn $"ShowCalculationManagerWindow: {ShowCalculationManagerWindow}" 
printfn $"ShowPanelsManagerWindow:      {ShowPanelsManagerWindow}" 
printfn $"UpdateFamilies:               {UpdateFamilies}" 
printfn $"ShowPlacementManagerWindow:   {ShowPlacementManagerWindow}" 




