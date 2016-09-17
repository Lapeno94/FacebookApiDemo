namespace FacebookApiDemo

open System
open Nancy
open Nancy.Hosting.Self

module Rest =
    type FacebookApiModule() as this =
        inherit NancyModule()

        do
            this.Get.["/"] <- fun parameters -> "Hello World" :> obj;

module MainModule =

    [<EntryPoint>]
    let main argv =
        let url = "http://localhost:1234"

        printfn "Starting Facebook API consumer service..."

        use host = new NancyHost(Uri(url))
        host.Start()
        printfn "Running on %A" url

        Console.ReadLine() |> ignore
        0
