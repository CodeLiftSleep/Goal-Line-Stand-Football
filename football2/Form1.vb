Imports System.Data.SQLite
'Imports System.Text
Public Class Form1

    Dim SQL As SQLFunctions.SQLiteDataFunctions = New SQLFunctions.SQLiteDataFunctions

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        People.LoadData()
        Randomize()
        'Dim SQLFieldNames = "Number varchar(5), Flt varchar(5), Name varchar(5)"
        Try
            CollegePlayer.GenDraftPlayers(3000)
            NFLScout.GenScouts(800)
            NFLOwner.GenOwners(100)
            NFLGM.GenGMs(100)
            NFLCoach.GenCoaches(800)
            NFLPlayer.GetRosterPlayers(2600)
        Catch ex As System.ArgumentException
            'Console.WriteLine(ex.GetBaseException)
            'Console.WriteLine(ex.Data)
            Console.WriteLine(ex.ToString)
            Console.WriteLine(ex.Message)
        End Try

    End Sub

End Class
