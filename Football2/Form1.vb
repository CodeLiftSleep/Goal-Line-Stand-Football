Imports System.Data.SQLite
Imports System.IO
Imports Troschuetz.Random.Generators.XorShift128Generator
Imports LeagueCalendar.LeagueState
Public Class Form1

    Dim SQL As SQLFunctions.SQLiteDataFunctions = New SQLFunctions.SQLiteDataFunctions
    Dim MyRand As New Troschuetz.Random.TRandom
    Dim Getnum As Double
    Dim GetGaus As Double
    Public GetDay As New LeagueCalendar.LeagueCalendar
    Public MyDate As Date

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        People.LoadData()
        Randomize()

        For i As Integer = 1 To 20 'properly initializes the random number generator
            Getnum = MyRand.NextUInt(0, 100)
            GetGaus = CInt(MyRand.Normal(49.5, 16.5))
        Next i
        For i As Integer = 1 To 365 'testing out the LeagueCalendar and RaiseEvent code
            MyDate = GetDay.AdvDay(GetDay.CurDate, 1).ToShortDateString()
            Console.WriteLine("Date: {0}", MyDate.ToShortDateString())
        Next i


        Try
            CollegePlayer.GenDraftPlayers(3000)
            'NFLScout.GenScouts(800)
            'NFLOwner.GenOwners(100)
            'NFLGM.GenPersonnel(850)
            'NFLCoach.GenCoaches(1300)
            'NFLPlayer.GetRosterPlayers(2600)
        Catch ex As System.ArgumentException
            Console.WriteLine(ex.ToString)
            Console.WriteLine(ex.Message)
        End Try

    End Sub


End Class
