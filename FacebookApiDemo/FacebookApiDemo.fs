namespace FacebookApiDemo

open System
open Nancy
open Nancy.Hosting.Self
open Nancy.Extensions

module Rest =
    type FacebookApiModule() as this =
        inherit NancyModule()

        do
            this.Get.["/"] <- fun parameters ->
                let query = this.Request.Query :?> Nancy.DynamicDictionary
                let mode = query.["hub.mode"]
                let challenge = query.["hub.challenge"]
                let token = query.["hub.verify_token"]
                
                let arguments = parameters :?> Nancy.DynamicDictionary
                printfn "GET Request arrived: Mode:%A Challenge:%A Token:%A" mode challenge token
                challenge

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
