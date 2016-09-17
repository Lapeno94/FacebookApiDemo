namespace FacebookApiDemo

open System
open Nancy
open Nancy.Hosting.Self
open Nancy.Extensions

module Rest =
    type FacebookApiModule() as this =
        inherit NancyModule()

        do
            this.Get.["/hub.mode={mode}&hub.challenge={challenge}&hub.verify_token={token}"] <- fun parameters ->
                let arguments = parameters :?> Nancy.DynamicDictionary
                printfn "GET Request arrived: Mode:%A Challenge:%A Token:%A" 
                    arguments.["mode"] arguments.["challenge"] arguments.["token"]
                arguments.["challenge"]

            this.Post.["/"] <- fun _ ->
                let body = this.Request.Body.AsString()
                printfn "POST Request arrived:\n%A" body
                [] :> obj

module MainModule =

    [<EntryPoint>]
    let main argv =
        let url = "http://localhost:5004"

        printfn "Starting Facebook API consumer service..."

        use host = new NancyHost(Uri(url))
        host.Start()
        printfn "Running on %A" url

        Console.ReadLine() |> ignore
        0
