namespace NRK.Dotnetskolen

module Dto =

    type TransmissionDto = {
        Title: string
        StartTime: string
        EndTime: string
    }

    type EpgDto = {
        Nrk1: TransmissionDto list
        Nrk2: TransmissionDto list
    }

    let fromDomain (domain: Domain.Epg) : EpgDto =
        let toTransmissionDto (transmission: Domain.Transmission) =
            {
                Title = transmission.Title
                StartTime = transmission.StartTime.ToString("o")
                EndTime = transmission.EndTime.ToString("o")
            }
        
        let nrk1Transmissions = 
            domain 
            |> List.filter (fun t -> t.Channel = "NRK1")
            |> List.map toTransmissionDto

        let nrk2Transmissions = 
            domain 
            |> List.filter (fun t -> t.Channel = "NRK2")
            |> List.map toTransmissionDto
            
        {
            Nrk1 = nrk1Transmissions
            Nrk2 = nrk2Transmissions
        }
        