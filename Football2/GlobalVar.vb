''' <summary>
''' Holds any Globla Variables for use
''' </summary>
Module GlobalVar
    'Module contains Global Variables for various functions
    Public MT As New Mersenne.MersenneTwister
    Dim OwnerDT As New DataTable
    Dim DraftDT As New DataTable
    Dim AgentDT As New DataTable
    Dim CoachDT As New DataTable
    Dim TrainerDT As New DataTable
    Dim PersonnelDT As New DataTable
    Dim PlayerDT As New DataTable

    Dim NFLPlayer As New Generation.NFLPlayers
    Dim NFLAgent As New Generation.Agent
    Dim NFLPersonnel As New Generation.Personnel
    Dim NFLOwner As New Generation.Owner
    Dim CollegePlayer As New Generation.CollegePlayers
    Public People As New Generation.Person
    Dim NFLCoach As New Generation.Coaches
    Dim NFLTrainer As New Generation.Trainer
    Dim MyDraftClass As New ArrayList
    Dim MyDB As String = "Football"

    ''' <summary>
    ''' This is meant to run once at the onset of the game---creates all the initial people involved in the game
    ''' </summary>
    ''' <param name="PersonType"></param>
    ''' <param name="NumPeople"></param>
    Public Sub GeneratePeople(ByVal PersonType As String, ByVal NumPeople As Integer)
        Try
            Select Case PersonType

                Case "Coach"
                    NFLCoach.Initialize(MyDB, CoachDT, "Coaches", NFLCoach.GetSQLString("Coach"))
                    For i As Integer = 1 To NumPeople
                        NFLCoach.GenCoaches(i, NFLCoach, CoachDT)
                    Next i
                    NFLCoach.PutCoachesOnTeams(CoachDT)
                    CoachDT.Rows.RemoveAt(0)
                    NFLCoach.Update(MyDB, CoachDT, "Coaches")

                Case "CollegePlayer"
                    CollegePlayer.Initialize(MyDB, DraftDT, "DraftPlayers", CollegePlayer.GetSQLString("College"))
                    MyDraftClass = CollegePlayer.GenDraftClass(MyDraftClass)
                    For i As Integer = 1 To NumPeople
                        CollegePlayer.GenDraftPlayers(i, CollegePlayer, DraftDT, MyDraftClass)
                    Next i
                    DraftDT.Rows.RemoveAt(0)
                    CollegePlayer.Update(MyDB, DraftDT, "DraftPlayers")

                Case "NFLPlayer"
                    NFLPlayer.Initialize(MyDB, PlayerDT, "RosterPlayers", NFLPlayer.GetSQLString("NFL"))
                    For i As Integer = 1 To NumPeople
                        NFLPlayer.GetRosterPlayers(i, NFLPlayer, PlayerDT)
                    Next i
                    For i As Integer = 1 To 17
                        NFLPlayer.PutPlayerOnTeam(i, PlayerDT)
                    Next i

                Case "Personnel"
                    NFLPersonnel.Initialize(MyDB, PersonnelDT, "Personnel", NFLPersonnel.GetSQLString("Personnel"))
                    For i As Integer = 1 To NumPeople
                        NFLPersonnel.GenPersonnel(i, NFLPersonnel, PersonnelDT)
                    Next i
                    PersonnelDT.Rows.RemoveAt(0)
                    NFLPersonnel.Update(MyDB, PersonnelDT, "Personnel")

                Case "Owner"
                    NFLOwner.Initialize(MyDB, OwnerDT, "Owners", NFLOwner.OwnerSQLString)
                    For i As Integer = 1 To NumPeople
                        NFLOwner.GenOwners(i, NFLOwner, OwnerDT)
                    Next i
                    NFLOwner.GetTeamOwner(OwnerDT)
                    OwnerDT.Rows.RemoveAt(0)
                    NFLOwner.Update(MyDB, OwnerDT, "Owners")

                Case "Agent"
                    NFLAgent.Initialize(MyDB, AgentDT, "Agents", NFLAgent.AgentSQLString)
                    For i As Integer = 1 To NumPeople
                        NFLAgent.GenAgents(i, NFLAgent, AgentDT, PlayerDT)
                    Next i
                    AgentDT.Rows.RemoveAt(0)
                    PlayerDT.Rows.RemoveAt(0)
                    NFLAgent.Update(MyDB, AgentDT, "Agents")
                    NFLPlayer.Update(MyDB, PlayerDT, "RosterPlayers")

                Case "Trainer"
                    NFLTrainer.Initialize(MyDB, TrainerDT, "Trainers", NFLTrainer.TrainerSQLString)
                    For i As Integer = 1 To NumPeople
                        NFLTrainer.GenTrainers(i, NFLTrainer, TrainerDT)
                    Next i
                    NFLTrainer.PutTrainerOnTeam(TrainerDT)
                    TrainerDT.Rows.RemoveAt(0)
                    NFLTrainer.Update(MyDB, TrainerDT, "Trainers")
            End Select
        Catch ex As System.Invalidcastexception
            Console.WriteLine(Ex.Message)
            Console.WriteLine(ex.Data)
        End Try
    End Sub




End Module
