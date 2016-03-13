''' <summary>
''' Holds any Globla Variables for use
''' </summary>
Module GlobalVar
    'Module contains Global Variables for various functions
    Public GetSQLite As New SQLFunctions.SQLiteDataFunctions

    'Public GetTables As New SQLFunctions.DataFunctions
    'Public GetPBP As New PBP
    'Public PBPDT As New DataTable
    'Public OwnerDT As New DataTable
    'Public PlayerDT As New DataTable
    'Public DraftDT As New DataTable
    'Public GMDT As New DataTable
    'Public CoachDT As New DataTable
    'Public TrainerDT As New DataTable
    'Public ScoutDT As New DataTable
    'Public CityInfo As New DataTable
    Public HomeTeamDT As New DataTable
    Public AwayTeamDT As New DataTable
    'Public FBall As New DataSet
    'Public PlayStats As New DataTable
    'Public PlayData As New DataTable
    Public MT As New Mersenne.MersenneTwister
    Public GenData As New Generation.Person
    'Public ScoutGradeDT As New DataTable
    'Public TeamDraft As New Drafting
    'Public TeamDraftDT As New DataTable
    'Public WithEvents Game As New GameEngine
    'Public WithEvents GetDec As New AIDecisionsInGame
    'Public WithEvents GameClock As New Stopwatch
    'Public WithEvents PlayClock As New Stopwatch
    Public NFLPlayer As New Generation.NFLPlayers
    Public NFLCoach As New Generation.Coaches
    Public NFLGM As New Generation.GeneralManager
    Public NFLScout As New Generation.Scouts
    Public NFLOwner As New Generation.Owner
    Public CollegePlayer As New Generation.CollegePlayers
    Public People As New Generation.Person
    Public GenSchedule As New NFLScheduler.Scheduler

    'Public Sub GetDS() 'Adds tables to the dataset
    'FBall.Tables.Add(PBPDT)
    'FBall.Tables.Add(OwnerDT)
    'FBall.Tables.Add(PlayerDT)
    'FBall.Tables.Add(GMDT)
    'FBall.Tables.Add(DraftDT)
    'FBall.Tables.Add(TrainerDT)
    'FBall.Tables.Add(ScoutDT)
    'FBall.Tables.Add(CoachDT)
    'FBall.Tables.Add(PlayStats)
    'FBall.Tables.Add(PlayData)
    'FBall.Tables.Add(ScoutGradeDT)
    'FBall.Tables.Add(TeamDraftDT)
    'FBall.Tables.Add(CityInfo)
    'FBall.Tables.Add(HomeTeamDT)
    'FBall.Tables.Add(AwayTeamDT)


    Public Sub Instantiate()

        'Catch ex As Exception
        'Console.WriteLine(ex.Message)
        'End Try
        'GetDS()
        'People.LoadData()
        'Try
        'NFLPlayer.GetRosterPlayers(2555) ''###TODO Figure out why it is skipping rows at some places...
        'CollegePlayer.GenDraftPlayers(2555)
        'NFLCoach.GenCoaches(800)
        'NFLGM.GenGMs(96)
        'NFLScout.GenScouts(800)
        'NFLOwner.GenOwners(96)
        'Catch ex As System.InvalidCastException
        'System.Data.SqlClient.SqlException
        'Console.WriteLine(ex.Message)
        'End Try
        'GenSchedule.GetSchedule(16)
        'GenData.GetRosterPlayers(2600)
        'GenData.GenOwners(96)
        'GenData.GenGMs(96)
        'GenData.GenCoaches(800)
        'GenData.GenScouts(800)
        'GenData.GenDraftPlayers(2555)
        'GenData.GenDraftClass(2555)
        'Game.InGame()
        'Eval.ScoutPlayerEval()
        'GenData.GenScoutGrades(800, 2555)
        'GetTables.DeleteTable(PBPDT, "PBP")
        'GetTables.LoadTable(PBPDT, "PBP")
        'GetPBP.GenSQL()
        'GetPBP.GetPBP("pbptest.txt")
        'GetPBP.GetPBP("fix2002pbp.txt")
        'GetPBP.GetPBP("2003pbp.txt")
        'GetTables.UpdateTable(PBPDT, "PBP")
        'GetSQL.StoredProcs()
    End Sub

End Module
