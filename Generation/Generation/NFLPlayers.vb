

''' <summary>
''' Class for Generating NFL Players at game startup, ie, players that are already on a team
''' </summary>
Public Class NFLPlayers
    Inherits Players
    Dim SQLFieldNames As String
    Dim SQLCmd As New SQLFunctions.SQLiteDataFunctions
    Private Sub PutPlayerOnTeam(ByVal Pos As Integer)
        'determines what team player is on via generic position limits

        Dim NumAllowed As Integer
        Dim MinAllowed As Integer
        'Dim SelectTeam As Integer
        Dim Count As Integer
        Dim i As Integer
        Dim PosString As String
        Dim RowArray As New ArrayList

        i = 1
        Count = 0
        NumAllowed = 0

        Select Case Pos
            Case 1, 2, 5, 7 : NumAllowed = 4 : MinAllowed = 3 '1 QB, 2 RB, 5 TE
            Case 6, 12 : NumAllowed = 3 : MinAllowed = 2 '6 C, 12 ILB
            Case 8, 9, 10, 11 : NumAllowed = 5 : MinAllowed = 3 '7 OG, 8 OT, 9 DE, 10 DT, 11 OLB
            Case 14, 15 : NumAllowed = 4 : MinAllowed = 2 '14 FS, 15 SS
            Case 3 : NumAllowed = 2 : MinAllowed = 1 '16 K, 17 P, 3 FB
            Case 16, 17 : NumAllowed = 1 : MinAllowed = 1
            Case 4, 13 : NumAllowed = 6 : MinAllowed = 4 '4 WR, 13 CB
        End Select

        Select Case Pos
            Case 1 : PosString = "QB"
            Case 2 : PosString = "RB"
            Case 3 : PosString = "FB"
            Case 4 : PosString = "WR"
            Case 5 : PosString = "TE"
            Case 6 : PosString = "C"
            Case 7 : PosString = "OG"
            Case 8 : PosString = "OT"
            Case 9 : PosString = "DE"
            Case 10 : PosString = "DE"
            Case 11 : PosString = "OLB"
            Case 12 : PosString = "ILB"
            Case 13 : PosString = "CB"
            Case 14 : PosString = "FS"
            Case 15 : PosString = "SS"
            Case 16 : PosString = "K"
            Case 17 : PosString = "P"
        End Select

        For x As Integer = 1 To PlayerDT.Rows.Count - 1
            If PlayerDT.Rows(x).Item("POS") = PosString Then
                RowArray.Add(x) 'adds all rows to an arraylist
            End If
        Next x

        For i = 1 To 32 'numteams
            Dim getnumpos As Integer = MT.GenerateInt32(MinAllowed, NumAllowed)

            For n As Integer = 1 To getnumpos
                If RowArray.Count > 0 Then
                    Dim ChooseArray As Integer = MT.GenerateInt32(0, RowArray.Count - 1)
                    Dim GetRow As Integer = RowArray.Item(ChooseArray)
                    RowArray.RemoveAt(ChooseArray)
                    PlayerDT.Rows(GetRow).Item("TeamID") = i
                End If
            Next n

        Next i

    End Sub
    Public Sub GetRosterPlayers(ByVal numplayers As Integer)
        SQLFieldNames = GetSQLFields("NFL")
        SQLiteTables.CreateTable(MyDB, PlayerDT, "Players", SQLFieldNames)
        SQLiteTables.DeleteTable(MyDB, PlayerDT, "Players")
        SQLiteTables.LoadTable(MyDB, PlayerDT, "Players")
        Dim MyPos As String

        PlayerDT.Rows.Add()

        For i As Integer = 1 To numplayers
            PlayerDT.Rows.Add(i)
            MyPos = GetCollegePos() 'returns the "normal" version without the  ' '
            PlayerDT.Rows(i).Item("POS") = String.Format("'{0}'", MyPos)
            GenNames(PlayerDT, i, "NFLPlayer", MyPos)
            GetPosSkills(MyPos, i)
            PlayerDT.Rows(i).Item("Explosion") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Athleticism") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("JumpingAbility") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Character") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Instincts") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Focus") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("TeamPlayer") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Consistency") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Leadership") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("FieldAwareness") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Clutch") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Fearless") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Aggressive") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("RiskTaker") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("FilmStudy") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("WorkEthic") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("BallSecurity") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("QAB") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("COD") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("RETKickReturn") = GetKickRetAbility(MyPos, i)
            PlayerDT.Rows(i).Item("RETPuntReturn") = GetPuntRetAbility(MyPos, i)
            PlayerDT.Rows(i).Item("PlaybookKnowledge") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Toughness") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("InjuryProne") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Timing") = MT.GetGaussian(49.5, 16.5)
            GetSTAbility(MyPos, i)
            GetLSAbility(MyPos, i)
        Next i


        For i As Integer = 1 To 17
            PutPlayerOnTeam(i)
        Next i

        For i As Integer = 0 To PlayerDT.Rows.Count - 1

            For col As Integer = 0 To PlayerDT.Columns.Count - 1
                If PlayerDT.Rows(i).Item(col) Is DBNull.Value Then
                    PlayerDT.Rows(i).Item(col) = 0
                End If

            Next col
        Next i

        SQLiteTables.BulkInsert("Football", PlayerDT, "Players")


    End Sub
    Public Sub GetPosSkills(ByVal Pos As String, ByVal i As Integer)
        Select Case Pos
            Case "QB"
                PlayerDT.Rows(i).Item("QBDropQuickness") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBSetUpQuickness") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBReleaseQuickness") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBShortAcc") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBMedAcc") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBLongAcc") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBDecMaking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBFieldVision") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBPoise") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBBallHandling") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBTiming") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBDelivery") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBFollowThrough") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBAvoidRush") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBEscape") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBScrambling") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBRolloutRight") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBRolloutLeft") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBArmStrength") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBZip") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBTouchScreenPass") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBTouchSwingPass") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBEffectiveShortOut") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBEffectiveDeepOut") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBEffectiveGoRoute") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBEffectivePostRoute") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("QBEffectiveCornerRoute") = MT.GetGaussian(49.5, 16.5)

            Case "RB", "FB"
                PlayerDT.Rows(i).Item("RBHands") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("RBEffortBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("RBRunBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("RBDurability") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("RBPowerAbility") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("RBRouteRunning") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("RBRunningStyle") = "'NONE'"
                PlayerDT.Rows(i).Item("RBPassBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("RBStart") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("RBRunVision") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("RBInsideAbility") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("RBOutsideAbility") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("RBElusiveAbility") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("RBBallAdjust") = MT.GetGaussian(49.5, 16.5)


            Case "WR"
                PlayerDT.Rows(i).Item("WRShortRoute") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRCrowdReaction") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRCatchWhenHit") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRConcentration") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRFieldAwareness") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRBodyCatch") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRRelease") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRRunDBOff") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRAcrobaticCatches") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRStart") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRPatterns") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRMedRoute") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRDeepRoute") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRBallAdjust") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRHandCatch") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("WRRAC") = MT.GetGaussian(49.5, 16.5)


            Case "TE"
                PlayerDT.Rows(i).Item("TEGetOffLineRunBlock") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEOneOnOneBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEDoubleTeamBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEDownBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TETurnAndWallBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TESustainBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TECrowdReaction") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TECatchWhenHit") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEConcentration") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEBodyCatch") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEFieldAwareness") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEPassProtect") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEDriveIntoPassRoute") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEPatterns") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEShortRoute") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEMedRoute") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEDeepRoute") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEBallAdjust") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TEHandCatch") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("TERAC") = MT.GetGaussian(49.5, 16.5)


            Case "OT", "OG", "C"
                PlayerDT.Rows(i).Item("OLGetOutside") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLReachBlock") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLTurnDefender") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLPulling") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLTrapBlock") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OL2ndLevelPull") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLAdjustToLB") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLSlide") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLHandUse") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLHandPop") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLLongSnapPotential") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLGetOffLineRunBlock") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLOneOnOneBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLDriveBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLDownBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLSustainBlock") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLPassBlocking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLPassDrops") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLFeetSetup") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLAnchorAbility") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLRecover") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("OLStrength") = MT.GetGaussian(49.5, 16.5)
            Case "DE", "DL"
                PlayerDT.Rows(i).Item("DLStyle") = "'NONE'"
                PlayerDT.Rows(i).Item("DLRunAtHim") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLTackling") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLAgainstTrapAbility") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLSlideAbility") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLRunPursuit") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLPassRushTechnique") = "'NONE'"
                PlayerDT.Rows(i).Item("DLHandUse") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLShedVsRunAway") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLTackleVsRunAway") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLChangeDirection") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLReleaseOffBall") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLOneOnOneAbility") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLDoubleTeamAbility") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLDefeatBlock") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLShedRunBlock") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLFirstStepPassRush") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLShedPassBlock") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLBurst") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLPressure") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLFinish") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLContain") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DLStayAtHome") = MT.GetGaussian(49.5, 16.5)

            Case "OLB", "ILB"
                PlayerDT.Rows(i).Item("LBDropDepth") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("LBCoverage") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("LBHands") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("LBBlitz") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("LBPassRushType") = "'NONE'"
                PlayerDT.Rows(i).Item("LBRead") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("LBInstincts") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("LBDefeatBlocks") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("LBShedBlocks") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("LBInsideTackle") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("LBOutsideTackle") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("LBFillGaps") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("LBContain") = MT.GetGaussian(49.5, 16.5)

            Case "CB", "FS", "SS"
                PlayerDT.Rows(i).Item("DBPressBailCoverage") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBBallReaction") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBTackling") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBZoneCoverage") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBRunContain") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBWardOffBlockers") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBRunTackling") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBHands") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBBump") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBRunContain") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBBlitz") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBRead") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBInstincts") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBBackpedal") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBTurn") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBClose") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBRange") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBBurst") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBCatchupSpeed") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBManToManCoverage") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBCOBP") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("DBFeet") = MT.GetGaussian(49.5, 16.5)
            Case "K"
                PlayerDT.Rows(i).Item("KPlantRelationship") = "'NONE'"
                PlayerDT.Rows(i).Item("KApproachAngle") = "'NONE'"
                PlayerDT.Rows(i).Item("KBallFlight") = "'NONE'"
                PlayerDT.Rows(i).Item("KSteppingPattern") = "'NONE'"
                PlayerDT.Rows(i).Item("KKickingStyle") = "'NONE'"
                PlayerDT.Rows(i).Item("KHandlingWind") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("KTackling") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("KRunAndPassAbility") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("KKOFootSpeed") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("KFGOperationTimes") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("KAccuracy") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("KHandlingPressure") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("KFootSpeed") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("KKickQuickness") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("KKickRise") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("KKOProduction") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("KKOMentalStability") = MT.GetGaussian(49.5, 16.5)
            Case "P"
                PlayerDT.Rows(i).Item("PFootSpeed") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("PApproachLine") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("PHandlingTime") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("PSteppingPattern") = "'NONE'"
                PlayerDT.Rows(i).Item("PHands") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("PTackling") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("PRunAndPassAbility") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("PDistance") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("PHangTime") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("PPressureKicking") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("PBlockZone") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("PHandToFootTime") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("PTiming") = MT.GetGaussian(49.5, 16.5)
        End Select
    End Sub
End Class
