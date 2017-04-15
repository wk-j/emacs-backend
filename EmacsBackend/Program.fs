module EmacsBackend.Program

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful

let app =
    choose
      [ GET >=> choose
          [ path "/hello" >=> OK "Hello GET"
            path "/goodbye" >=> OK "Good bye GET"]]

[<EntryPoint>]
let main argv =
    printfn "%A" argv
    startWebServer defaultConfig app
    0 // return an integer exit code
