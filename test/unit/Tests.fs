module Tests

open System
open Xunit
open NRK.Dotnetskolen.Domain

[<Theory>]
[<InlineData("abc12")>]
[<InlineData(".,-:!")>]
[<InlineData("ABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJ")>]
let ``isTitleValid valid title returns true`` (title: string) =
    let isTitledValid = isTitleValid title
    Assert.True isTitledValid


[<Theory>]
[<InlineData("abcd")>]
[<InlineData("@$%&/")>]
[<InlineData("abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghija")>]
let ``isTitleValid invalid title returns false`` (title: string) =
    let isTitledValid = isTitleValid title
    Assert.False isTitledValid


[<Theory>]
[<InlineData("NRK1")>]
[<InlineData("NRK2")>]
let ``isChannelValid valid channel returns true`` (channel: string) =
    let isChannelValid = isChannelValid channel
    Assert.True isChannelValid

[<Theory>]
[<InlineData("nrk1")>]
[<InlineData("NRK3")>]
let ``isChannelValid invalid channel returns false`` (channel: string) =
    let isChannelValid = isChannelValid channel
    Assert.False isChannelValid


[<Fact>]
let ``areStartTimeAndEndTimeValid start before end returns true`` () =
    let startTime = DateTimeOffset.Now
    let endTime = startTime.AddMinutes 30.

    let areStartTimeAndEndTimeValid = areStartTimeAndEndTimeValid startTime endTime
    Assert.True areStartTimeAndEndTimeValid

[<Fact>]
let ``areStartTimeAndEndTimeValid start after end returns false`` () =
    let startTime = DateTimeOffset.Now
    let endTime = startTime.AddMinutes -30.

    let areStartTimeAndEndTimeValid = areStartTimeAndEndTimeValid startTime endTime
    Assert.False areStartTimeAndEndTimeValid


[<Fact>]
let ``areStartTimeAndEndTimeValid start equals end returns false`` () =
    let startTime = DateTimeOffset.Now
    let endTime = startTime

    let areStartTimeAndEndTimeValid = areStartTimeAndEndTimeValid startTime endTime
    Assert.False areStartTimeAndEndTimeValid

[<Fact>]
let ``isTransmissionValid valid transmission returns true`` () =
    let transmission = {
        Title = "Jounal:Morning"
        Channel = "NRK1"
        StartTime = DateTimeOffset.Parse("2021-04-16T19:00:00+02:00")
        EndTime = DateTimeOffset.Parse("2021-04-16T19:30:00+02:00")
    }

    let isTransmissionValid = isTransmissionValid transmission
    Assert.True isTransmissionValid


[<Fact>]
let ``isTransmissionValid invalid transmission returns false`` () =
    let transmission = {
        Title = "InvalidChannel"
        Channel = "NRK3"
        StartTime = DateTimeOffset.Parse("2021-04-16T19:00:00+02:00")
        EndTime = DateTimeOffset.Parse("2021-04-16T19:30:00+02:00")
    }

    let isTransmissionValid = isTransmissionValid transmission
    Assert.False isTransmissionValid