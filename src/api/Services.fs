namespace NRK.Dotnetskolen.Api

module Services =

    open System
    open NRK.Dotnetskolen.Domain

    let getEpgForDate (getAllTransmissions : unit -> Epg) (date: DateOnly) : Epg =
        let allTransmissions = getAllTransmissions()
        let result =
            allTransmissions 
            |> List.filter (fun t -> 
                DateOnly.FromDateTime(t.StartTime.DateTime) = date)
        result
        
