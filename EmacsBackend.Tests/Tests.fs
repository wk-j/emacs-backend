module Tests

open Expecto
open EmacsBackend.Program
open System.IO

[<Tests>]
let tests =
  testList "samples" [
    testCase "universe exists" <| fun _ ->
      let subject = true
      Expect.isTrue subject "I compute, therefore I am."

    testCase "should not fail" <| fun _ ->
      let subject = true
      Expect.isTrue subject "I should fail because the subject is false."

    testCase "should get dot locatino" <| fun _ ->
      let dir = DirectoryInfo("./")
      let rs = Api.getDotLocation dir
      Expect.equal rs.Data dir.FullName "I'm here"
  ]
