namespace FacebookApiDemo

open System
open Nancy
open Nancy.Hosting.Self
open Nancy.Extensions

module Rest =
    type GetRequestParams() =
        member val ``hub.mode`` = "" with get, set
        member val ``hub.challenge`` = "" with get, set
        member val ``hub.verify_token`` = "" with get, set

    type FacebookApiModule() as this =
        inherit NancyModule()

        do
            this.Get.["/"] <- fun parameters ->
                let arguments = parameters :?> GetRequestParams
                printfn "GET Request arrived: Mode:%A Challenge:%A Token:%A" 
                    arguments.``hub.mode`` arguments.``hub.challenge`` arguments.``hub.verify_token``
                arguments.``hub.challenge`` :> obj

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
