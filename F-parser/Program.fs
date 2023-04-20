
open System.IO
open Library
open Types                           

let DirRoot         = "C:\Users\user\Downloads\Telegram Desktop"
let LogsDirName     = "Logs"
let MessagesDirName = "Messages"
let MessagesFileName = "Messages.json"
let Comb paths       = paths |> List.toArray |> Path.Combine
let LogsDir          = Comb [ DirRoot; LogsDirName ]
let MessagesDir      = Comb [ DirRoot; MessagesDirName ]
let MessagesFilePath = Comb [ MessagesDir; MessagesFileName ]


let PrintValues  = List.iter(fun x -> printfn $"{x}")
let printNames() = MessagesFilePath |> CountUniqueUserNames  |> PrintValues
let ermsgs() = (ParseLogs LogsDir)|> List.filter (fun x -> x.Type = Error)|>List.map (fun x -> x.Text)|>List.distinct|> List.iter(fun x -> printfn $"ERROR{x}")


printfn "-------------------------------------------\n"
ermsgs()
 
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




