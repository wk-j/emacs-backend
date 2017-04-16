module EmacsBackend.Program

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open System.IO
open System
open Newtonsoft.Json
open Newtonsoft.Json.Serialization

type 'a Result = {
    Success: bool
    Data : 'a }

module Util =
    let settings =
        let st  = JsonSerializerSettings()
        st.ContractResolver <- CamelCasePropertyNamesContractResolver()
        (st)

    let toJson (obj: 'a) =
        JsonConvert.SerializeObject(obj, settings)

module Api =
    let cakeName = "build.cake"

    let rec getDotLocation (currentPath: DirectoryInfo) =
        let cake = Path.Combine(currentPath.FullName, cakeName)
        if Object.ReferenceEquals(null, currentPath.Parent) then
            { Success = false
              Data = String.Empty }
        else
          if File.Exists cake then
              { Success = true
                Data = currentPath.FullName }
          else
              getDotLocation currentPath.Parent 

let app =
    let ok = Util.toJson >> OK
    let dotLocation =
        request (fun r ->
                 match r.queryParam "currentDir" with
                 | Choice1Of2 target ->
                   let dir = DirectoryInfo(target)
                   if dir.Exists then
                     Api.getDotLocation dir |> ok
                   else
                    { Success = false
                      Data = String.Empty } |> ok
                  | Choice2Of2 msg ->
                    { Success = false
                      Data = String.Empty } |> ok )

    choose
      [ GET >=> choose
          [ path "/getDotLocation" >=> dotLocation
            path "/" >=> OK "Hello, world!"
            path "/goodbye" >=> OK "Good bye GET"]]

[<EntryPoint>]
let main argv =
    // printfn "%A" argv
    startWebServer { defaultConfig with bindings= [Suave.Http.HttpBinding.createSimple HTTP "127.0.0.1" 9876] }  app
    0 // return an integer exit code
