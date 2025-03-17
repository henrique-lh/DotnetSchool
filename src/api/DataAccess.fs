namespace NRK.Dotnetskolen.Api

module DataAccess =

    open System
    open NRK.Dotnetskolen.Domain

    type TransmissionEntity = {
        Title: string
        Channel: string
        StartTime: string
        EndTime: string
    }

    type EpgEntity = TransmissionEntity list

    let database =
        [
            {
                Title = "TestProgram1"
                Channel = "NRK1"
                StartTime = "2025-02-26T13:00:00Z"
                EndTime = "2025-02-26T13:30:00Z"
            }
            {
                Title = "TestProgram2"
                Channel = "NRK2"
                StartTime = "2025-02-26T14:00:00Z"
                EndTime = "2025-02-26T15:00:00Z"
            }
            {
                Title = "TestProgram3"
                Channel = "NRK2"
                StartTime = "2025-02-27T14:00:00Z"
                EndTime = "2025-02-28T15:00:00Z"
            }
            {
                Title = "TestProgram"
                Channel = "NRK3"
                StartTime = "2025-02-26T14:00:00Z"
                EndTime = "2025-02-26T16:30:00Z"
            }
        ]

    let validateTransmission (entity: TransmissionEntity) : Result<Transmission, string> =
        match isTitleValid entity.Title, 
              isChannelValid entity.Channel,
              areStartTimeAndEndTimeValid (DateTimeOffset.Parse(entity.StartTime)) (DateTimeOffset.Parse(entity.EndTime)) with
        | true, true, true ->
            Ok {
                Title = entity.Title
                Channel = entity.Channel
                StartTime = DateTimeOffset.Parse(entity.StartTime)
                EndTime = DateTimeOffset.Parse(entity.EndTime)
            }
        | false, _, _ -> Error "Invalid title"
        | _, false, _ -> Error "Invalid channel"
        | _, _, false -> Error "Invalid time range"

    let getAllTransmissions () : Epg =
        database
        |> List.choose (fun entity ->
            match validateTransmission entity with
            | Ok transmission -> Some transmission
            | Error _ -> None)
        |> function
            | [] -> []
            | validTransmissions -> validTransmissions
