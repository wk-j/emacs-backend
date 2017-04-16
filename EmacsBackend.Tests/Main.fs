module EmacsBackend.Tests

open EmacsBackend.Program

open Expecto

[<EntryPoint>]
let main argv =
    Tests.runTestsInAssembly defaultConfig argv
