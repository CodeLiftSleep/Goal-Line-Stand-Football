''' <summary>
''' These are the "Football" People on the team--GM's, Coaches and Scouts.  They all have football related evaluations they use contained in the functions below 
''' </summary>
Public Class Personnel
    Inherits Person

    Public Function GetSQLFieldNames(ByVal PersonnelType As String) As String
        Dim result As String = ""
        Select Case PersonnelType
            Case "Personnel"
                result = "PersonnelID int PRIMARY KEY NOT NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Age int NULL, DOB varchar(12) NULL, PersonnelType int NULL, Experience int NULL, Patience int NULL, 
Risktaker int NULL, ValuesDraftPicks int NULL, ValuesCombine int NULL, ValuesCharacter int NULL, ValuesProduction int NULL, ValuesIntangibles int NULL, FranchiseTag int NULL, TransitionTag int NULL, JudgingDraft int NULL, JudgingFA int NULL, JudgingOwn int NULL, 
JudgingPot int NULL, JudgingQB int NULL, JudgingRB int NULL, JudgingWR int NULL, JudgingTE int NULL, JudgingOL int NULL, JudgingDL int NULL, JudgingLB int NULL, JudgingCB int NULL, JudgingSF int NULL, Loyalty int NULL, Ego int NULL, OffPhil varchar(20) NULL, 
QBImp int NULL, RBImp int NULL, FBImp int NULL, WRImp int NULL, WR2Imp int NULL, WR3Imp int NULL, LTImp int NULL, LGImp int NULL, CImp int NULL, RGImp int NULL, RTImp int NULL, TEImp int NULL, DefPhil varchar(20) NULL, DEImp int NULL, DE2Imp int NULL, DTImp int NULL, 
DT2Imp int NULL, NTImp int NULL, MLBImp int NULL, WLBImp int NULL, SLBImp int NULL, ROLBImp int NULL, LOLBImp int NULL, CB1Imp int NULL, CB2Imp int NULL, CB3Imp int NULL, FSImp int NULL, SSImp int NULL, DraftStrategy varchar(20) NULL, 
TeamBuilding varchar(20) NULL, Ambition int NULL, Delegation int NULL, WorkEthic int NULL, DraftPreparation int NULL, Convincing int NULL, RespectedByPlayers int NULL, RespectedByCoaches int NULL, RespectedByScouts int NULL, OrganizationalPower int NULL, 
Dysfunctional int NULL, Leadership int NULL, Accountability int NULL, Adaptability int NULL, BackStabber int NULL"

            Case "Coach"
                result = "CoachID int PRIMARY KEY NOT NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Age int NULL, DOB varchar(12) NULL, CoachType int NULL, SideOfBall varchar(3) NULL, Experience int NULL, 
OffPhil varchar(50) NULL, DefPhil Varchar(50) NULL, LoyaltyPlayers int NULL, LoyaltyCoaches int NULL, Ego int NULL, OffAbility int NULL, DefAbility int NULL, CoachQB int NULL, CoachRB int NULL, CoachWR int NULL, CoachTE int NULL, CoachOL int NULL, 
Stability int NULL, DevPlayers int NULL, CoachDL int NULL, CoachLB int NULL, CoachDB int NULL, CoachST int NULL, JudgingAct int NULL, JudgingPot int NULL, JudgingQB int NULL, JudgingRB int NULL, JudgingWR int NULL, JudgingTE int NULL, JudgingOL int NULL, JudgingDL int NULL, 
JudgingLB int NULL, JudgingCB int NULL, JudgingSF int NULL, Leadership int NULL, Adaptability int NULL, Delegation int NULL, Accountability int NULL, Motivating int NULL, TimeMgmt int NULL, Clutch int NULL, Conservative int NULL, FBInt int NULL, 
Adjustments int NULL, ValuesST int NULL,  ValuesCharacter int NULL, Ambition int NULL, WorkEthic int NULL, Preparation int NULL, BackStabber int NULL, Focus int NULL, PlaycallingSkill int NULL, RespectedByPlayers int NULL, RespectedByCoaches int NULL, SeesBigPicture int NULL, 
DevQB int NULL, DevRB int NULL, DevWR int NULL, DevTE int NULL, DevOL int NULL, DevDL int NULL, DevLB int NULL, DevCB int NULL, DevSF int NULL, LowerBodyTrain int NULL, UpperBodyTrain int NULL, CoreTrain int NULL, PreventInjuryTrain int NULL, StaminaTrain int NULL"
        End Select
        Return result
    End Function
    Public Sub GenScoutGrades(ByVal NumScouts As Integer, ByVal NumPlayers As Integer)

        'SQLiteTables.LoadTable(ScoutGradeDT, "ScoutGrades")
        'If ScoutGradeDT.Rows.Count = 0 Then
        'Dim SQLFields As String = "(PID INTEGER PRIMARY KEY,"

        'For ScoutID As Integer = 0 To NumScouts
        'If ScoutID <> NumScouts Then
        'SQLFields += "Scout" & ScoutID & " Decimal(9,2),"
        'Else
        'SQLFields += "Scout" & ScoutID & " Decimal(9,2));"
        'End If
        'Next ScoutID

        SQLiteTables.DeleteTable(MyDB, ScoutGradeDT, "ScoutsGrade")
        SQLiteTables.LoadTable(MyDB, ScoutGradeDT, "ScoutsGrade")

        ScoutGradeDT.Rows.Add(0)

        For PlayerId As Integer = 1 To NumPlayers
            ScoutGradeDT.Rows.Add(PlayerId)
        Next PlayerId

        Eval.ScoutPlayerEval()
        'End If
    End Sub
    Public Function GetOffPhil() As String
        Dim result As String = ""
        Select Case MT.GenerateInt32(1, 90)
            Case 1 To 10 : result = "BalPass"
            Case 11 To 20 : result = "BalRun"
            Case 21 To 30 : result = "VertPass"
            Case 31 To 40 : result = "Smashmouth"
            Case 41 To 50 : result = "WCPass"
            Case 51 To 60 : result = "WCRun"
            Case 61 To 70 : result = "WCBal"
            Case 71 To 76 : result = "SpreadRun"
            Case 77 To 82 : result = "SpreadPass"
            Case 83 To 88 : result = "SpreadBal"
            Case 89 To 90 : result = "Run-N-Shoot"
                'Case 91 To 100 : Return "PassHeavy"
        End Select
        Return result
    End Function
    Public Function GetDefPhil() As String
        Dim result As String = ""
        Select Case MT.GenerateInt32(1, 93)
            Case 1 To 12 : result = "4-3Attack"
            Case 13 To 23 : result = "4-3Cover"
            Case 23 To 33 : result = "4-3Bal"
            Case 33 To 43 : result = "4-3StuffRun"
            Case 44 To 49 : result = "3-4Attack"
            Case 50 To 55 : result = "3-4Cover"
            Case 56 To 61 : result = "3-4Bal"
            Case 62 To 67 : result = "3-4StuffRun"
            Case 68 To 75 : result = "Cover2Attack"
            Case 76 To 83 : result = "Cover2Cover"
            Case 84 To 91 : result = "Cover2Bal"
            Case 92 To 93 : result = "46"
                'Case 94 To 100 : Return "Hybrid"
        End Select
        Return result
    End Function
End Class
