module EmacsBackend.Program

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful

module Api =
    let getDotCakeLocation() =
        ""

let app =
    choose
      [ GET >=> choose
          [ path "/getDotCakeLocation" >=> OK "Hello GET"
            path "/goodbye" >=> OK "Good bye GET"]]

[<EntryPoint>]
let main argv =
    printfn "%A" argv
    startWebServer defaultConfig app
    0 // return an integer exit code
