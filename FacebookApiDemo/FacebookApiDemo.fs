namespace FacebookApiDemo

open System
open Nancy
open Nancy.Hosting.Self
open Nancy.Extensions
open System.Threading

module Rest =
    type FacebookApiModule() as this =
        inherit NancyModule()

        do
            this.Post.["/"] <- fun _ ->
                printfn "POST request arrived:"

                let headers = this.Request.Headers
                headers
                |> Seq.iter (fun keyValuePair -> printfn "%A: %A" keyValuePair.Key keyValuePair.Value)

                printfn ""

                let body = this.Request.Body.AsString()
                printfn "%A" body

                [] :> obj

module MainModule =

    [<EntryPoint>]
    let main argv =
        let url = "http://localhost:5004"

        printfn "Starting Facebook API consumer service..."

        use host = new NancyHost(Uri(url))
        host.Start()
        printfn "Running on %A" url

        while true do
            Thread.Sleep(10000)
        0
