
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
let PrintNames() = MessagesFilePath |> CountUniqueUserNames  |> PrintValues
let PrintGroupErrMsg() = 
    (ParseLogs LogsDir)
    |>List.filter (fun x -> x.Type = Error)
    |>List.map    (fun x -> x.Text)|>List.groupBy id
    |>List.map    (fun (x, g) -> x, g.Length)
    |>List.sortBy (fun (_, x) -> x)
    |>List.iter   (fun x -> printfn $"{x}")


printfn "-------------------------------------------\n"
PrintGroupErrMsg()
 
printfn "-------------------------------------------\n" 

PrintNames()

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




