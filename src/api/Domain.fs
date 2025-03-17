namespace NRK.Dotnetskolen

open System.Text.RegularExpressions

module Domain =

    open System

    type Transmission = {
        Title: string
        Channel: string
        StartTime: DateTimeOffset
        EndTime: DateTimeOffset
    }

    type Epg = Transmission list

    let isTitleValid (title: string) : bool =
        let titleRegex = Regex(@"^[\p{L}0-9\.,-:!]{5,100}$")
        titleRegex.IsMatch(title)

    let isChannelValid (channel: string) : bool =
        let channelRegex = Regex(@"^NRK[1-2]$")
        channelRegex.IsMatch(channel)

    let areStartTimeAndEndTimeValid (startTime: DateTimeOffset) (endTime: DateTimeOffset) =
        startTime < endTime

    let isTransmissionValid (transmission: Transmission) : bool =
        isTitleValid transmission.Title
        && isChannelValid transmission.Channel
        && areStartTimeAndEndTimeValid transmission.StartTime transmission.EndTime