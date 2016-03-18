
Imports System.Text.RegularExpressions
Public Class CollegePlayers
    Inherits Players
    Dim MyPlayer As New Players
    Dim SQLFieldNames As String
    Dim MyPos As String

    Public Sub GenDraftPlayers(ByVal NumPlayers As Integer)
        SQLFieldNames = GetSQLFields("College")
        SQLiteTables.CreateTable(MyDB, DraftDT, "DraftPlayers", SQLFieldNames)
        'SQLiteTables.DeleteTable(MyDB, DraftDT, "DraftPlayers")
        SQLiteTables.LoadTable(MyDB, DraftDT, "DraftPlayers")
        DraftDT.Rows.Add(0)

        For i As Integer = 1 To NumPlayers
            Try
                DraftDT.Rows.Add(i)
                MyPos = GetCollegePos()
                DraftDT.Rows(i).Item("CollegePOS") = String.Format("'{0}'", MyPos)
                GenNames(DraftDT, i, "CollegePlayer", MyPos)
                GetDraftGrades(i, MyPos) '#### TODO---> Convert from a sub to a function since it's returning a value         
                DraftDT.Rows(i).Item("FortyYardTime") = Get40Time(MyPos)
                DraftDT.Rows(i).Item("TwentyYardTime") = Get20Time(MyPos)
                DraftDT.Rows(i).Item("TenYardTime") = Get10Time(MyPos)
                DraftDT.Rows(i).Item("ShortShuttle") = GetShortShuttle(MyPos)
                DraftDT.Rows(i).Item("BroadJump") = GetBroadJump(MyPos)
                DraftDT.Rows(i).Item("VertJump") = GetVertJump(MyPos)
                DraftDT.Rows(i).Item("ThreeConeDrill") = Get3Cone(MyPos)
                DraftDT.Rows(i).Item("BenchPress") = GetBenchPress(MyPos)
                DraftDT.Rows(i).Item("InterviewSkills") = CInt(MT.GetGaussian(49.5, 16.5))
                DraftDT.Rows(i).Item("WonderlicTest") = GetWonderlic(MyPos)
                DraftDT.Rows(i).Item("SkillsTranslateToNFL") = GetSkillsTranslate(MyPos)
                DraftDT.Rows(i).Item("ProjNFLPos") = GetNFLPos(String.Format("'{0}'", MyPos), i)
                DraftDT.Rows(i).Item("Flexibility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("Clutch") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("Production") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("Consistency") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("TeamPlayer") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("Instincts") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("Focus") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("PlayStrength") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("Explosion") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("DeliversBlow") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("Leadership") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("Character") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("Toughness") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("InjuryProne") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("Fearless") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("Aggressive") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("RiskTaker") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("FilmStudy") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("WorkEthic") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("BallSecurity") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("FieldAwareness") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("JumpingAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("Timing") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(i).Item("PlaybookKnowledge") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
            Catch ex As System.InvalidCastException
                Console.WriteLine(ex.Data)
                Console.WriteLine(ex.Message)
            End Try
        Next i
        For row As Integer = 0 To DraftDT.Rows.Count - 1
            For col As Integer = 0 To DraftDT.Columns.Count - 1
                If DraftDT.Rows(row).Item(col) Is DBNull.Value Then
                    DraftDT.Rows(row).Item(col) = 0
                End If
            Next col
        Next row

        SQLiteTables.BulkInsert(MyDB, DraftDT, "DraftPlayers") 'Inserts into DB
    End Sub

    ''' <summary>
    ''' Common Position switches include:
    ''' QB ---> WR ---Typically very athletic QB's that aren't good enough at QB for the NFL(Julian Edelman for example)
    ''' QB ---> RB ---Typically very athletic QB's that are option type QB's in college who do a lot of running(Michael Robinson for example)
    ''' DE ---> OLB ---Typically "smaller", athletic DE's in college are too small to play DE in the NFL(Jerry Hughes for example)
    ''' CB ---> SF ---Typically slower type CB's in college that have good ball and football instincts but lack the speed to cover WR's or hands to catch the ball in the NFL(Jairius Byrd, Aaron Williams, and Devin McCourty for example)
    ''' WR ---> SF ---Typically slower type WR's that are good playing the ball but lack hands needed at WR(George Wilson for example)
    ''' OT ---> OG ---Typically "smaller" OT's in college that are the size of guards in the NFL(one of the most common--numerous examples)
    ''' LB ---> SF ---Typically "smaller" LB's in college that are athletic and fast enough to play safety but don't have enough size to play LB(Adam Archuleta for example)
    ''' FB ---> TE ---Typically the more athletic FB's in college in a run heavy offensive scheme can make more use of their skills as a TE or H-Back(Charles Clay for example)
    ''' other examples and less common changes occur---
    ''' 
    ''' Need to figure out how often and under what circumstances a player would have a different position---currently it sets it to the same position as they are in college
    ''' 
    ''' on offense: QB > WR(RB) > RB > FB > TE > OT > OG > OC
    ''' QB--->WR: Athleticism, speed, quickness, catching ability/QB Traits: Arm strength, height, 
    ''' 
    ''' 
    ''' 
    ''' 
    ''' On Defense: CB > S > LB > DE > DT
    '''
    ''' players are able To move up Or down 1 slot - so starting from most athletic To least athletic defensively you'd have CB > S > LB > DE > DT. 
    ''' So all a corner could do Is move DOWN. Safeties can move UP Or DOWN to corner Or LB. Any player moving UP would need to have considerable athleticism.
    '''</summary>
    ''' <param name="Pos"></param>
    ''' <returns></returns>
    Private Function GetNFLPos(ByVal Pos As String, ByVal PlayerNum As Integer) As String '####TODO: Determine how often and what percentage of players would play a different position in the NFL than in college(I'm thinking maybe 5-7%, most common is OT to OG and CB to SF
        'Players who are too small/light/slow for their current college positions
        'can be projected to play a different position in the NFL

        Select Case Pos
            Case "QB"
                If DraftDT.Rows(PlayerNum).Item("Athleticism") > 7.0 And DraftDT.Rows(PlayerNum).Item("FortyYardTime") < 4.5 And DraftDT.Rows(PlayerNum).Item("QAB") > 7.0 And
                   DraftDT.Rows(PlayerNum).Item("COD") > 7.0 And DraftDT.Rows(PlayerNum).Item("ShortAcc") < 6.0 And DraftDT.Rows(PlayerNum).Item("QBDecisionMaking") < 6.0 Then
                    'change pos to WR
                    DraftDT.Rows(PlayerNum).Item("NFLPos") = "WR"
                Else : Return "'QB'"
                End If
            Case "RB" : Return "'RB'"
            Case "FB" : Return "'FB'"
            Case "TE" : Return "'TE'"
            Case "WR" : Return "'WR'"
            Case "OT" : Return "'OT'"
            Case "OG" : Return "'OG'"
            Case "C" : Return "'C'"
            Case "DE" : Return "'DE'"
            Case "DT" : Return "'DT'"
            Case "OLB" : Return "'OLB'"
            Case "ILB" : Return "'ILB'"
            Case "SF" : Return "'SF'"
            Case "CB" : Return "'CB'"
            Case "P" : Return "'P'"
            Case "K" : Return "'K'"
        End Select




        Return Pos
    End Function

    Private Function GetSkillsTranslate(ByVal Pos As String) As Integer
        Select Case Pos
            Case "QB" 'QB 53% Bust 33% ProBowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 53 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 54 To 66 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "RB" 'RB Skills 49% Bust 36% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 49 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 50 To 63 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "FB" 'FB Skills usually translate pretty well to the NFL as well..
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 30 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 31 To 75 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "WR" 'WR 45% Bust 31% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 45 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 46 To 68 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "TE" 'TE Skills are fairly translatable
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 35 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 36 To 67 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "OT", "C", "OG" '31% Bust 26% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 31 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 32 To 73 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "DE" '31% Bust 33% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 31 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 32 To 66 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "DT" '33% Bust 40% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 33 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 34 To 59 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "OLB", "ILB" '16% Bust 26% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 16 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 17 To 73 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "CB" '29% Bust 23% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 29 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 30 To 77 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "FS", "SS" '11% Bust 53% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 11 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 12 To 46 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "K"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 45 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 46 To 76 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "P"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 40 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 41 To 76 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
        End Select
    End Function
    Private Function GetWonderlic(ByVal Pos As String) As Integer
        Select Case Pos
            Case "QB" : Return CInt(MT.GetGaussian(26.678, 6.521))
            Case "RB" : Return CInt(MT.GetGaussian(18.68, 6.1561))
            Case "FB" : Return CInt(MT.GetGaussian(19.5313, 6.13384))
            Case "WR" : Return CInt(MT.GetGaussian(19.6814, 6.14898))
            Case "TE" : Return CInt(MT.GetGaussian(24.0909, 8.18485))
            Case "OT" : Return CInt(MT.GetGaussian(24.881, 7.15376))
            Case "C" : Return CInt(MT.GetGaussian(26.9697, 6.28844))
            Case "OG" : Return CInt(MT.GetGaussian(24.0339, 6.09241))
            Case "DE" : Return CInt(MT.GetGaussian(20.1348, 7.59389))
            Case "DT" : Return CInt(MT.GetGaussian(19.4054, 7.94052))
            Case "OLB" : Return CInt(MT.GetGaussian(20.2623, 5.85872))
            Case "ILB" : Return CInt(MT.GetGaussian(22.4286, 6.27976))
            Case "CB" : Return CInt(MT.GetGaussian(18.4881, 5.74351))
            Case "FS" : Return CInt(MT.GetGaussian(21.0213, 5.71854))
            Case "SS" : Return CInt(MT.GetGaussian(19.2553, 5.38109))
            Case "K" : Return CInt(MT.GetGaussian(24.32, 6.21431))
            Case "P" : Return CInt(MT.GetGaussian(25.875, 5.38952))

        End Select
    End Function
    Private Function GetBenchPress(ByVal Pos As String) As Integer
        Select Case Pos
            Case "QB" : Return CInt(MT.GetGaussian(19.8, 2.85657))
            Case "RB" : Return CInt(MT.GetGaussian(20.3148, 4.51673))
            Case "FB" : Return CInt(MT.GetGaussian(23.4848, 4.28614))
            Case "WR" : Return CInt(MT.GetGaussian(16.7586, 3.38001))
            Case "TE" : Return CInt(MT.GetGaussian(21.3333, 4.45034))
            Case "OT" : Return CInt(MT.GetGaussian(24.9524, 4.60651))
            Case "C" : Return CInt(MT.GetGaussian(26.5313, 4.24253))
            Case "OG" : Return CInt(MT.GetGaussian(26.4419, 4.79487))
            Case "DE" : Return CInt(MT.GetGaussian(24.6825, 5.9089))
            Case "DT" : Return CInt(MT.GetGaussian(28.3519, 5.78362))
            Case "OLB" : Return CInt(MT.GetGaussian(22.5873, 4.95899))
            Case "ILB" : Return CInt(MT.GetGaussian(22.8947, 3.85793))
            Case "CB" : Return CInt(MT.GetGaussian(15.4, 3.32265))
            Case "FS" : Return CInt(MT.GetGaussian(16.5, 3.59048))
            Case "SS" : Return CInt(MT.GetGaussian(17.2333, 4.63093))
        End Select
    End Function
    Private Function Get3Cone(ByVal Pos As String) As Double
        Select Case Pos
            Case "QB" : Return Math.Round(MT.GetGaussian(7.16143, 0.27591), 2)
            Case "RB" : Return Math.Round(MT.GetGaussian(7.00848, 0.16455), 2)
            Case "FB" : Return Math.Round(MT.GetGaussian(7.30794, 0.26069), 2)
            Case "WR" : Return Math.Round(MT.GetGaussian(6.95564, 0.16844), 2)
            Case "TE" : Return Math.Round(MT.GetGaussian(7.15089, 0.20702), 2)
            Case "OT" : Return Math.Round(MT.GetGaussian(7.80527, 0.30271), 2)
            Case "C" : Return Math.Round(MT.GetGaussian(7.71718, 0.25131), 2)
            Case "OG" : Return Math.Round(MT.GetGaussian(7.87723, 0.32694), 2)
            Case "DE" : Return Math.Round(MT.GetGaussian(7.33298, 0.27823), 2)
            Case "DT" : Return Math.Round(MT.GetGaussian(7.70881, 0.22825), 2)
            Case "OLB" : Return Math.Round(MT.GetGaussian(7.12055, 0.23154), 2)
            Case "ILB" : Return Math.Round(MT.GetGaussian(7.222, 0.21437), 2)
            Case "CB" : Return Math.Round(MT.GetGaussian(6.984, 0.22688), 2)
            Case "FS" : Return Math.Round(MT.GetGaussian(7.02656, 0.19514), 2)
            Case "SS" : Return Math.Round(MT.GetGaussian(7.06844, 0.24738), 2)
        End Select
    End Function
    Private Function GetVertJump(ByVal Pos As String) As Double
        Dim NumString As String = ""
        Dim Num As Integer
        Dim NumStr As Double
        Select Case Pos
            Case "QB" : NumString = CStr(Math.Round(MT.GetGaussian(30.551, 3.69258), 1))
            Case "RB" : NumString = CStr(Math.Round(MT.GetGaussian(33.3657, 2.86198), 1))
            Case "FB" : NumString = CStr(Math.Round(MT.GetGaussian(32.0172, 2.91395), 1))
            Case "WR" : NumString = CStr(Math.Round(MT.GetGaussian(34.3807, 3.20614), 1))
            Case "TE" : NumString = CStr(Math.Round(MT.GetGaussian(32.1915, 3.69956), 1))
            Case "OT" : NumString = CStr(Math.Round(MT.GetGaussian(26.8952, 3.42092), 1))
            Case "C" : NumString = CStr(Math.Round(MT.GetGaussian(28.2368, 3.332601), 1))
            Case "OG" : NumString = CStr(Math.Round(MT.GetGaussian(26.1935, 2.41976), 1))
            Case "DE" : NumString = CStr(Math.Round(MT.GetGaussian(32.4123, 3.98806), 1))
            Case "DT" : NumString = CStr(Math.Round(MT.GetGaussian(29, 3.07743), 1))
            Case "OLB" : NumString = CStr(Math.Round(MT.GetGaussian(33.5635, 4.15808), 1))
            Case "ILB" : NumString = CStr(Math.Round(MT.GetGaussian(32.9865, 3.51538), 1))
            Case "CB" : NumString = CStr(Math.Round(MT.GetGaussian(35.5467, 3.33027), 1))
            Case "FS" : NumString = CStr(Math.Round(MT.GetGaussian(35.0238, 3.37348), 1))
            Case "SS" : NumString = CStr(Math.Round(MT.GetGaussian(35.2439, 3.07433), 1))
            Case "K", "P" : NumString = CStr(0)
        End Select

        NumStr = CInt(NumString)
        If NumString = NumStr Then : Return NumStr

        Else
            Num = Regex.Match(NumString, "(?<=\d+\.)\d").Value
            If Num < 4 Then : Return NumStr
            ElseIf Num = 4 Then : Return CDbl(NumString) + 0.1
            ElseIf Num = 5 Then : Return CDbl(NumString)
            ElseIf Num = 6 Then : Return CDbl(NumString) - 0.1
            ElseIf Num = 7 Then : Return CDbl(NumString) - 0.2
            ElseIf Num = 8 Then : Return CDbl(NumString) + 0.2
            ElseIf Num = 9 Then : Return CDbl(NumString) + 0.1
            End If
        End If
    End Function
    Private Function GetBroadJump(ByVal Pos As String) As Integer
        Select Case Pos
            Case "QB" : Return CInt(MT.GetGaussian(109.792, 5.32666))
            Case "RB" : Return CInt(MT.GetGaussian(117.897, 6.53304))
            Case "FB" : Return CInt(MT.GetGaussian(112.414, 6.87583))
            Case "WR" : Return CInt(MT.GetGaussian(120.213, 4.79013))
            Case "TE" : Return CInt(MT.GetGaussian(113.587, 5.61665))
            Case "OT" : Return CInt(MT.GetGaussian(102.7, 5.56567))
            Case "C" : Return CInt(MT.GetGaussian(102.432, 4.95133))
            Case "OG" : Return CInt(MT.GetGaussian(100.839, 5.82041))
            Case "DE" : Return CInt(MT.GetGaussian(113.898, 5.58356))
            Case "DT" : Return CInt(MT.GetGaussian(106.217, 4.38829))
            Case "OLB" : Return CInt(MT.GetGaussian(116.323, 5.55841))
            Case "ILB" : Return CInt(MT.GetGaussian(113.263, 5.00886))
            Case "CB" : Return CInt(MT.GetGaussian(121.865, 5.82475))
            Case "FS" : Return CInt(MT.GetGaussian(119.432, 6.13162))
            Case "SS" : Return CInt(MT.GetGaussian(119.075, 6.38117))
        End Select
    End Function
    Private Function GetShortShuttle(ByVal Pos As String) As Double
        Select Case Pos
            Case "QB" : Return Math.Round(MT.GetGaussian(4.37, 0.18174), 2)
            Case "RB" : Return Math.Round(MT.GetGaussian(4.31, 0.13471), 2)
            Case "FB" : Return Math.Round(MT.GetGaussian(4.4, 0.16438), 2)
            Case "WR" : Return Math.Round(MT.GetGaussian(4.25, 0.12744), 2)
            Case "TE" : Return Math.Round(MT.GetGaussian(4.39, 0.16592), 2)
            Case "OT" : Return Math.Round(MT.GetGaussian(4.76, 0.19038), 2)
            Case "C" : Return Math.Round(MT.GetGaussian(4.62, 0.17958), 2)
            Case "OG" : Return Math.Round(MT.GetGaussian(4.81, 0.18228), 2)
            Case "DE" : Return Math.Round(MT.GetGaussian(4.45, 0.18935), 2)
            Case "DT" : Return Math.Round(MT.GetGaussian(4.64, 0.18484), 2)
            Case "OLB" : Return Math.Round(MT.GetGaussian(4.31, 0.15413), 2)
            Case "ILB" : Return Math.Round(MT.GetGaussian(4.32, 0.12603), 2)
            Case "CB" : Return Math.Round(MT.GetGaussian(4.24, 0.13143), 2)
            Case "FS" : Return Math.Round(MT.GetGaussian(4.28, 0.16186), 2)
            Case "SS" : Return Math.Round(MT.GetGaussian(4.23, 0.15594), 2)
        End Select

    End Function
    Private Function Get40Time(ByVal Pos As String) As Double
        Select Case Pos
            'Case "QB" : Return Math.Round(MT.GetGaussian(4.84335, 0.17588), 2)
            Case "QB"
                Select Case MT.GenerateInt32(1, 168)
                    Case 1 : Return Math.Round(MT.GenerateDouble(4.37, 4.39), 2)
                    Case 2 To 3 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 4 To 15 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 16 To 30 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 31 To 71 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                    Case 72 To 111 : Return Math.Round(MT.GenerateDouble(4.8, 4.89), 2)
                    Case 112 To 135 : Return Math.Round(MT.GenerateDouble(4.9, 4.99), 2)
                    Case 136 To 153 : Return Math.Round(MT.GenerateDouble(5, 5.09), 2)
                    Case 154 To 163 : Return Math.Round(MT.GenerateDouble(5.1, 5.19), 2)
                    Case 164 To 167 : Return Math.Round(MT.GenerateDouble(5.2, 5.29), 2)
                    Case 168 : Return Math.Round(MT.GenerateDouble(5.4, 5.49), 2)
                End Select
                'Case "RB" : Return Math.Round(MT.GetGaussian(4.56, 0.097434), 2)
            Case "RB"
                Select Case MT.GenerateInt32(1, 144)
                    Case 1 : Return Math.Round(MT.GenerateDouble(4.25, 4.29), 2)
                    Case 2 To 5 : Return Math.Round(MT.GenerateDouble(4.3, 4.39), 2)
                    Case 6 To 37 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 38 To 102 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 103 To 135 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 136 To 143 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                    Case 144 : Return Math.Round(MT.GenerateDouble(4.8, 4.84), 2)
                End Select
            Case "FB" : Return Math.Round(MT.GetGaussian(4.76488, 0.1367), 2)

            Case "WR"
                Select Case MT.GenerateInt32(1, 305)
                    Case 1 : Return Math.Round(MT.GenerateDouble(4.25, 4.29), 2)
                    Case 2 To 16 : Return Math.Round(MT.GenerateDouble(4.3, 4.39), 2)
                    Case 17 To 75 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 76 To 203 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 204 To 293 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 294 To 305 : Return Math.Round(MT.GenerateDouble(4.7, 4.84), 2)
                End Select
                'Case "TE" : Return Math.Round(MT.GetGaussian(4.82, 0.132), 2)
            Case "TE"
                Select Case MT.GenerateInt32(1, 158)
                    Case 1 : Return Math.Round(MT.GenerateDouble(4.37, 4.49), 2)
                    Case 2 To 5 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 6 To 26 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 27 To 67 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                    Case 68 To 110 : Return Math.Round(MT.GenerateDouble(4.8, 4.89), 2)
                    Case 111 To 141 : Return Math.Round(MT.GenerateDouble(4.9, 4.99), 2)
                    Case 142 To 157 : Return Math.Round(MT.GenerateDouble(5, 5.09), 2)
                    Case 158 : Return Math.Round(MT.GenerateDouble(5.1, 5.15), 2)
                End Select
            Case "OT" : Return Math.Round(MT.GetGaussian(5.30864, 0.180568), 2)
            Case "OG" : Return Math.Round(MT.GetGaussian(5.32628, 0.192915), 2)
            Case "C" : Return Math.Round(MT.GetGaussian(5.25084, 0.18133), 2)
            Case "DE" : Return Math.Round(MT.GetGaussian(4.84454, 0.132509), 2)
            Case "DT" : Return Math.Round(MT.GetGaussian(5.10542, 0.147088), 2)
                'Case "OLB" : Return Math.Round(MT.GetGaussian(4.67983, 0.12102), 2)
            Case "OLB"
                Select Case MT.GenerateInt32(1, 177)
                    Case 1 To 8 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 9 To 42 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 43 To 107 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 108 To 153 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                    Case 154 To 165 : Return Math.Round(MT.GenerateDouble(4.8, 4.89), 2)
                    Case 166 To 174 : Return Math.Round(MT.GenerateDouble(4.9, 4.99), 2)
                    Case 175 To 177 : Return Math.Round(MT.GenerateDouble(5.0, 5.09), 2)
                End Select
                'Case "ILB" : Return Math.Round(MT.GetGaussian(4.77248, 0.12737), 2)

            Case "ILB"
                Select Case MT.GenerateInt32(1, 126)
                    Case 1 : Return Math.Round(MT.GenerateDouble(4.42, 4.49), 2)
                    Case 2 To 11 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 12 To 37 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 38 To 73 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                    Case 74 To 106 : Return Math.Round(MT.GenerateDouble(4.8, 4.89), 2)
                    Case 107 To 121 : Return Math.Round(MT.GenerateDouble(4.9, 4.99), 2)
                    Case 122 To 124 : Return Math.Round(MT.GenerateDouble(5, 5.09), 2)
                    Case 125 : Return Math.Round(MT.GenerateDouble(5.1, 5.19), 2)
                    Case 126 : Return Math.Round(MT.GenerateDouble(5.2, 5.25), 2)
                End Select
                'Case "CB" : Return Math.Round(MT.GetGaussian(4.51607, 0.086921), 2)
            Case "CB"
                Select Case MT.GenerateInt32(1, 201)
                    Case 1 To 4 : Return Math.Round(MT.GenerateDouble(4.25, 4.29), 2)
                    Case 5 To 21 : Return Math.Round(MT.GenerateDouble(4.3, 4.39), 2)
                    Case 22 To 80 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 81 To 174 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 175 To 201 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                End Select
                'Case "SS" : Return Math.Round(MT.GetGaussian(4.57, 0.08), 2)
            Case "SS"
                Select Case MT.GenerateInt32(1, 94)
                    Case 1 : Return Math.Round(MT.GenerateDouble(4.35, 4.39), 2)
                    Case 2 To 7 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 8 To 36 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 37 To 90 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 91 To 93 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                    Case 94 : Return Math.Round(MT.GenerateDouble(4.8, 4.85), 2)
                End Select
                'Case "FS" : Return Math.Round(MT.GetGaussian(4.56, 0.08), 2)
            Case "FS"
                Select Case MT.GenerateInt32(1, 102)
                    Case 1 To 2 : Return Math.Round(MT.GenerateDouble(4.3, 4.39), 2)
                    Case 3 To 13 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 14 To 63 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 64 To 100 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 101 To 102 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                End Select
            Case "K" : Return Math.Round(MT.GetGaussian(4.94, 0.114), 2)
            Case "P" : Return Math.Round(MT.GetGaussian(4.93, 0.127), 2)
        End Select
    End Function
    Private Function Get20Time(ByVal Pos As String) As Double
        Select Case Pos
            Case "QB" : Return Math.Round(MT.GetGaussian(2.8, 0.094), 2)
            Case "RB" : Return Math.Round(MT.GetGaussian(2.63, 0.082), 2)
            Case "FB" : Return Math.Round(MT.GetGaussian(2.74, 0.0776), 2)
            Case "WR" : Return Math.Round(MT.GetGaussian(2.62, 0.0715), 2)
            Case "TE" : Return Math.Round(MT.GetGaussian(2.76, 0.0889), 2)
            Case "OT" : Return Math.Round(MT.GetGaussian(3.04, 0.1187), 2)
            Case "OG" : Return Math.Round(MT.GetGaussian(3.01, 0.0839), 2)
            Case "C" : Return Math.Round(MT.GetGaussian(3.07, 0.1067), 2)
            Case "DE" : Return Math.Round(MT.GetGaussian(2.8, 0.086), 2)
            Case "DT" : Return Math.Round(MT.GetGaussian(2.96, 0.0906), 2)
            Case "OLB" : Return Math.Round(MT.GetGaussian(2.7, 0.0809), 2)
            Case "ILB" : Return Math.Round(MT.GetGaussian(2.75, 0.0885), 2)
            Case "CB" : Return Math.Round(MT.GetGaussian(2.595, 0.0704), 2)
            Case "FS" : Return Math.Round(MT.GetGaussian(2.644, 0.0675), 2)
            Case "SS" : Return Math.Round(MT.GetGaussian(2.649, 0.08177), 2)
        End Select
    End Function
    Private Function Get10Time(ByVal Pos As String) As Double
        Select Case Pos
            Case "QB" : Return Math.Round(MT.GetGaussian(1.84, 0.064), 2)
            Case "RB" : Return Math.Round(MT.GetGaussian(1.6, 0.0519), 2)
            Case "FB" : Return Math.Round(MT.GetGaussian(1.67, 0.056), 2)
            Case "WR" : Return Math.Round(MT.GetGaussian(1.59, 0.0535), 2)
            Case "TE" : Return Math.Round(MT.GetGaussian(1.69, 0.0573), 2)
            Case "OT" : Return Math.Round(MT.GetGaussian(1.84, 0.064), 2)
            Case "OG" : Return Math.Round(MT.GetGaussian(1.84, 0.0695), 2)
            Case "C" : Return Math.Round(MT.GetGaussian(1.8, 0.0632), 2)
            Case "DE" : Return Math.Round(MT.GetGaussian(1.68, 0.0605), 2)
            Case "DT" : Return Math.Round(MT.GetGaussian(1.77, 0.0636), 2)
            Case "OLB" : Return Math.Round(MT.GetGaussian(1.63, 0.0548), 2)
            Case "ILB" : Return Math.Round(MT.GetGaussian(1.66, 0.0638), 2)
            Case "CB" : Return Math.Round(MT.GetGaussian(1.49, 0.0465), 2)
            Case "SS" : Return Math.Round(MT.GetGaussian(1.51, 0.0403), 2)
            Case "FS" : Return Math.Round(MT.GetGaussian(1.51, 0.0386), 2)
        End Select
    End Function

    ''' <summary>
    ''' Types of drafts---Normal, Top Heavy(high quality players at the top not good depth), Deep Draft(Not as many high quality top players but more good players than normal in later
    ''' rounds, Stacked Draft(High Quality and Deep---very rare), need to be position specific
    ''' </summary>
    ''' <param name="NumPlayers"></param>
    Public Sub GenDraftClass(ByVal NumPlayers As Integer)

        'Generates a draft class
        '-------------------------------------------------
        'Gens strength of draft class for each position
        'Mean Number of players drafted at that position
        'Std Dev to get min and max numbers
        '---------------------------------------------------
    End Sub
    Private Sub GetDraftGrades(ByVal Num As Integer, ByVal Pos As String)
        Dim OverallGrade As Single
        Select Case Pos
            Case "QB"
                Select Case MT.GenerateInt32(1, 1982)
                    Case 1 To 6 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 7 To 14 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 15 To 26 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 27 To 59 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 60 To 85 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 86 To 125 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 126 To 155 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 156 To 206 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 207 To 260 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 261 To 335 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 336 To 435 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 436 To 560 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 561 To 710 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 711 To 1982 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "RB"
                Select Case MT.GenerateInt32(1, 3708)
                    Case 1 To 9 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 10 To 20 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 21 To 35 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 36 To 65 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 66 To 121 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 122 To 203 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 204 To 257 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 258 To 313 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 314 To 392 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 393 To 485 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 486 To 735 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 736 To 1135 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 1136 To 1735 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 1736 To 3708 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "FB"
                Select Case MT.GenerateInt32(1, 1092)
                    Case 1 To 3 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 4 To 7 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 8 To 12 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 13 To 29 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 30 To 57 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 58 To 102 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 103 To 142 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 143 To 235 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 236 To 365 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 366 To 495 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 496 To 1092 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "WR"
                Select Case MT.GenerateInt32(1, 6510)
                    Case 1 To 21 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 22 To 46 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 47 To 75 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 76 To 103 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 104 To 201 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 202 To 301 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 302 To 383 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 384 To 486 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 487 To 603 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 604 To 731 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 732 To 1231 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 1232 To 1931 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 1932 To 2931 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 2932 To 6510 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "TE"
                Select Case MT.GenerateInt32(1, 2020)
                    Case 1 To 5 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 6 To 10 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 11 To 18 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 19 To 30 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 31 To 74 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 75 To 109 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 110 To 153 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 154 To 207 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 208 To 272 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 273 To 361 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 362 To 485 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 486 To 682 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 683 To 982 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 983 To 2020 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "OT"
                Select Case MT.GenerateInt32(1, 3608)
                    Case 1 To 8 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 9 To 22 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 23 To 40 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 41 To 84 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 85 To 138 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 139 To 213 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 214 To 267 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 268 To 335 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 336 To 414 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 415 To 512 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 513 To 613 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 614 To 763 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 764 To 1050 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 1051 To 3608 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "OG"
                Select Case MT.GenerateInt32(1, 3290)
                    Case 1 To 5 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 6 To 10 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 11 To 18 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 19 To 30 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 31 To 74 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 75 To 109 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 110 To 153 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 154 To 207 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 208 To 272 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 273 To 361 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 362 To 485 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 486 To 682 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 683 To 982 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 983 To 3290 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "C"
                Select Case MT.GenerateInt32(1, 1675)
                    Case 1 To 2 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 3 To 5 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 6 To 9 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 10 To 14 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 15 To 33 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 34 To 61 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 62 To 87 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 88 To 115 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 116 To 162 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 163 To 227 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 228 To 327 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 328 To 477 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 478 To 677 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 678 To 1675 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "DE"
                Select Case MT.GenerateInt32(1, 3786)
                    Case 1 To 10 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 11 To 29 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 30 To 55 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 56 To 96 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 97 To 171 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 172 To 241 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 242 To 302 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 303 To 379 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 380 To 470 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 471 To 563 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 564 To 763 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 764 To 1063 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 1064 To 1663 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 1664 To 3786 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "DT"
                Select Case MT.GenerateInt32(1, 3112)
                    Case 1 To 9 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 10 To 25 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 26 To 45 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 46 To 75 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 76 To 133 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 134 To 189 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 190 To 252 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 253 To 317 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 318 To 401 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 402 To 497 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 498 To 673 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 674 To 998 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 999 To 1398 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 1399 To 3112 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "OLB"
                Select Case MT.GenerateInt32(1, 4801)
                    Case 1 To 7 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 8 To 19 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 20 To 35 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 36 To 65 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 66 To 116 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 117 To 179 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 180 To 235 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 236 To 296 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 297 To 380 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 381 To 485 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 486 To 885 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 886 To 1485 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 1486 To 2285 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 2286 To 4801 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "ILB"
                Select Case MT.GenerateInt32(1, 2186)
                    Case 1 To 4 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 5 To 9 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 10 To 15 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 16 To 24 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 25 To 44 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 45 To 93 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 94 To 144 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 145 To 193 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 194 To 244 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 245 To 314 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 315 To 454 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 455 To 654 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 655 To 954 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 955 To 2186 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "CB"
                Select Case MT.GenerateInt32(1, 4768)
                    Case 1 To 12 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 13 To 30 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 31 To 55 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 56 To 100 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 101 To 191 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 192 To 287 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 288 To 364 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 365 To 448 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 449 To 532 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 533 To 639 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 640 To 939 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 940 To 1439 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 1440 To 2439 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 2440 To 4768 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "FS"
                Select Case MT.GenerateInt32(1, 2815)
                    Case 1 To 3 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 4 To 7 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 8 To 12 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 13 To 19 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 20 To 49 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 50 To 93 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 94 To 140 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 141 To 196 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 197 To 259 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 260 To 348 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 349 To 448 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 449 To 598 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 599 To 848 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 849 To 2815 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "SS"
                Select Case MT.GenerateInt32(1, 2589)
                    Case 1 To 4 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 5 To 7 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 8 To 12 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 13 To 23 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 24 To 46 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 47 To 95 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 96 To 128 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 129 To 170 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 171 To 226 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 227 To 289 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 290 To 369 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 370 To 494 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 495 To 794 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 795 To 2589 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "K"
                Select Case MT.GenerateInt32(1, 1045)
                    Case 1 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 2 To 6 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 7 To 11 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 12 To 18 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 19 To 30 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 31 To 44 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 45 To 88 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 89 To 138 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 139 To 203 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 204 To 305 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 306 To 1045 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "P"
                Select Case MT.GenerateInt32(1, 1086)
                    Case 1 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 2 To 3 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 4 To 16 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 17 To 30 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 31 To 46 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 47 To 90 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 91 To 145 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 146 To 215 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 216 To 315 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 316 To 1086 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
        End Select

        DraftDT.Rows(Num).Item("ActualGrade") = Math.Round(OverallGrade, 2)
        'Console.WriteLine(Math.Round(OverallGrade, 2))
        GetIndGrades(Num, Pos, OverallGrade)


    End Sub
    Private Sub GetIndGrades(ByVal Num As Integer, ByVal Pos As String, ByVal Grade As Single)
        Select Case Pos
            Case "QB"
                Select Case Grade
                    Case Is > 7.49 '1st round talent
                        DraftDT.Rows(Num).Item("QBDropQuickness") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBSetUpQuickness") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBReleaseQuickness") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBShortAcc") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBMedAcc") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBLongAcc") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBDecMaking") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBFieldVision") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBPoise") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBBallHandling") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBTiming") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBDelivery") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBFollowThrough") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBAvoidRush") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBEscape") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBScrambling") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutRight") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutLeft") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBArmStrength") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBZip") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchScreenPass") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchSwingPass") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveShortOut") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveDeepOut") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveGoRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectivePostRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveCornerRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)

                    Case 7.0 To 7.49
                        DraftDT.Rows(Num).Item("QBDropQuickness") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBSetUpQuickness") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBReleaseQuickness") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBShortAcc") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBMedAcc") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBLongAcc") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDecMaking") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFieldVision") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBPoise") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBBallHandling") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTiming") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDelivery") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFollowThrough") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBAvoidRush") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEscape") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBScrambling") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutRight") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutLeft") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBArmStrength") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBZip") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchScreenPass") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTouchSwingPass") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveShortOut") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveDeepOut") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveGoRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectivePostRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveCornerRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)

                    Case 6.5 To 6.99 '3rd round
                        DraftDT.Rows(Num).Item("QBDropQuickness") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBSetUpQuickness") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBReleaseQuickness") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBShortAcc") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBMedAcc") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBLongAcc") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDecMaking") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFieldVision") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBPoise") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBBallHandling") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTiming") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDelivery") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFollowThrough") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBAvoidRush") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEscape") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBScrambling") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutRight") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutLeft") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBArmStrength") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBZip") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchScreenPass") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTouchSwingPass") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveShortOut") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveDeepOut") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveGoRoute") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectivePostRoute") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveCornerRoute") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)

                    Case 6.0 To 6.49
                        DraftDT.Rows(Num).Item("QBDropQuickness") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBSetUpQuickness") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBReleaseQuickness") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBShortAcc") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBMedAcc") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBLongAcc") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDecMaking") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFieldVision") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBPoise") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBBallHandling") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTiming") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDelivery") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFollowThrough") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBAvoidRush") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEscape") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBScrambling") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutRight") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutLeft") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBArmStrength") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBZip") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchScreenPass") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTouchSwingPass") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveShortOut") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveDeepOut") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveGoRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectivePostRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveCornerRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)

                    Case 5.0 To 5.99
                        DraftDT.Rows(Num).Item("QBDropQuickness") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBSetUpQuickness") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBReleaseQuickness") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBShortAcc") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBMedAcc") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBLongAcc") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDecMaking") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFieldVision") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBPoise") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBBallHandling") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTiming") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDelivery") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFollowThrough") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBAvoidRush") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEscape") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBScrambling") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutRight") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutLeft") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBArmStrength") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBZip") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchScreenPass") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTouchSwingPass") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveShortOut") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveDeepOut") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveGoRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectivePostRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveCornerRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)

                    Case Is < 5
                        DraftDT.Rows(Num).Item("QBDropQuickness") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBSetUpQuickness") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBReleaseQuickness") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBShortAcc") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBMedAcc") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBLongAcc") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDecMaking") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFieldVision") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBPoise") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBBallHandling") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTiming") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDelivery") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFollowThrough") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBAvoidRush") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEscape") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBScrambling") = Math.Round(MT.GetGaussian(5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutRight") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutLeft") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBArmStrength") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBZip") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchScreenPass") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTouchSwingPass") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveShortOut") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveDeepOut") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveGoRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectivePostRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveCornerRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "FB"
                DraftDT.Rows(Num).Item("RBEffortBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBDurability") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBPassBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBHands") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                Select Case Grade
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBPowerAbility") = Math.Round(MT.GetGaussian(6.5, 0.5), 1)
                        DraftDT.Rows(Num).Item("RBRunBlocking") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("RBRouteRunning") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("RBRunningStyle") = "'NONE'"
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.5), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.5), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.5), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBPowerAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("RBRunBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("RBRouteRunning") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunningStyle") = "'NONE'"
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBPowerAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunBlocking") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRouteRunning") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunningStyle") = "'NONE'"
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                    Case 5.0 To 5.99
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBPowerAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRouteRunning") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunBlocking") = Math.Round(MT.GetGaussian(5.5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunningStyle") = "'NONE'"
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.33333), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBPowerAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRouteRunning") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunBlocking") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunningStyle") = "'NONE'"
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.33333), 1)
                End Select
            Case "RB"
                DraftDT.Rows(Num).Item("RBHands") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBEffortBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBRunBlocking") = Math.Round(MT.GetGaussian(3, 0.583333), 1)
                DraftDT.Rows(Num).Item("RBDurability") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBPowerAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBRouteRunning") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBRunningStyle") = "'NONE'"
                DraftDT.Rows(Num).Item("RBPassBlocking") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.41666733), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(6.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(6.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(6.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(6.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(6.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.5), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.5), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.5), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                    Case 5.0 To 5.99
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.33333), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.33333), 1)
                End Select
            Case "WR"
                DraftDT.Rows(Num).Item("WRShortRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRCrowdReaction") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRCatchWhenHit") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRConcentration") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRFieldAwareness") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRBodyCatch") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRRelease") = Math.Round(MT.GetGaussian(6, 0.66667), 1)

                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("WRStart") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("WRPatterns") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("WRMedRoute") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("WRDeepRoute") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("WRBallAdjust") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("WRHandCatch") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("WRRAC") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7.0 To 7.49
                        DraftDT.Rows(Num).Item("WRStart") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRPatterns") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRMedRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRDeepRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRBallAdjust") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRHandCatch") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRRAC") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("WRStart") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRPatterns") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRMedRoute") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRDeepRoute") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRBallAdjust") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRHandCatch") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRRAC") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("WRStart") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRPatterns") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRMedRoute") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRDeepRoute") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRBallAdjust") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRHandCatch") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRRAC") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("WRStart") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRPatterns") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRMedRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRDeepRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRBallAdjust") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRHandCatch") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRRAC") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("WRStart") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRPatterns") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRMedRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRDeepRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRBallAdjust") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRHandCatch") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRRAC") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "TE"
                DraftDT.Rows(Num).Item("TEGetOffLineRunBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEOneOnOneBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEDoubleTeamBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEDownBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TETurnAndWallBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TESustainBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TECrowdReaction") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TECatchWhenHit") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEConcentration") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEBodyCatch") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEFieldAwareness") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEPassProtect") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("TEDriveIntoPassRoute") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TEPatterns") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TEShortRoute") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TEMedRoute") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TEDeepRoute") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TEBallAdjust") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TEHandCatch") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TERAC") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("TEDriveIntoPassRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEPatterns") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEShortRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEMedRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEDeepRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEBallAdjust") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEHandCatch") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TERAC") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("TEDriveIntoPassRoute") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TEPatterns") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TEShortRoute") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TEMedRoute") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TEDeepRoute") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TEBallAdjust") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TEHandCatch") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TERAC") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("TEDriveIntoPassRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEPatterns") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEShortRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEMedRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEDeepRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEBallAdjust") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEHandCatch") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TERAC") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("TEDriveIntoPassRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEPatterns") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEShortRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEMedRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEDeepRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEBallAdjust") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEHandCatch") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TERAC") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("TEDriveIntoPassRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEPatterns") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEShortRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEMedRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEDeepRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEBallAdjust") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEHandCatch") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TERAC") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "OT", "OG", "OC"
                DraftDT.Rows(Num).Item("OLGetOutside") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLReachBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLTurnDefender") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLPulling") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLTrapBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OL2ndLevelPull") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLAdjustToLB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLSlide") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLHandUse") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLHandPop") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLLongSnapPotential") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("OLGetOffLineRunBlock") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLOneOnOneBlocking") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLDriveBlocking") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLDownBlocking") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLSustainBlock") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLPassBlocking") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLPassDrops") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLFeetSetup") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLAnchorAbility") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLRecover") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLStrength") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        'DraftDT.Rows(Num).Item("OLSustain") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("OLGetOffLineRunBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLOneOnOneBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDriveBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDownBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLSustainBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassDrops") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLFeetSetup") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLAnchorAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLRecover") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLStrength") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        'DraftDT.Rows(Num).Item("OLSustain") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("OLGetOffLineRunBlock") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLOneOnOneBlocking") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLDriveBlocking") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("OLDownBlocking") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLSustainBlock") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLPassBlocking") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLPassDrops") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLFeetSetup") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLAnchorAbility") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLRecover") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLStrength") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        'DraftDT.Rows(Num).Item("OLSustain") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("OLGetOffLineRunBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLOneOnOneBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDriveBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDownBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLSustainBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassDrops") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLFeetSetup") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLAnchorAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLRecover") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLStrength") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        'DraftDT.Rows(Num).Item("OLSustain") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("OLGetOffLineRunBlock") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLOneOnOneBlocking") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDriveBlocking") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDownBlocking") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLSustainBlock") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassBlocking") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassDrops") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLFeetSetup") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLAnchorAbility") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLRecover") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLStrength") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        'DraftDT.Rows(Num).Item("OLSustain") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("OLGetOffLineRunBlock") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLOneOnOneBlocking") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDriveBlocking") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDownBlocking") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLSustainBlock") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassBlocking") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassDrops") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLFeetSetup") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLAnchorAbility") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLRecover") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLStrength") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        'DraftDT.Rows(Num).Item("OLSustain") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "DL", "DE"
                DraftDT.Rows(Num).Item("DLStyle") = "'NONE'"
                DraftDT.Rows(Num).Item("DLRunAtHim") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLTackling") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLAgainstTrapAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLSlideAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLRunPursuit") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLPassRushTechnique") = "'NONE'"
                DraftDT.Rows(Num).Item("DLHandUse") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLShedVsRunAway") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLTackleVsRunAway") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLChangeDirection") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLContain") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLStayAtHome") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("DLReleaseOffBall") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLOneOnOneAbility") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLDoubleTeamAbility") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLDefeatBlock") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLShedRunBlock") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLFirstStepPassRush") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLShedPassBlock") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLBurst") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLPressure") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLFinish") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("DLReleaseOffBall") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLOneOnOneAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDoubleTeamAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDefeatBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedRunBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFirstStepPassRush") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedPassBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLBurst") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLPressure") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFinish") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("DLReleaseOffBall") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLOneOnOneAbility") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLDoubleTeamAbility") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLDefeatBlock") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLShedRunBlock") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLFirstStepPassRush") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLShedPassBlock") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLBurst") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLPressure") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLFinish") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("DLReleaseOffBall") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLOneOnOneAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDoubleTeamAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDefeatBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedRunBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFirstStepPassRush") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedPassBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLBurst") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLPressure") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFinish") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("DLReleaseOffBall") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLOneOnOneAbility") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDoubleTeamAbility") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDefeatBlock") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedRunBlock") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFirstStepPassRush") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedPassBlock") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLBurst") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLPressure") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFinish") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("DLReleaseOffBall") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLOneOnOneAbility") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDoubleTeamAbility") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDefeatBlock") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedRunBlock") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFirstStepPassRush") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedPassBlock") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLBurst") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLPressure") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFinish") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "OLB", "ILB"
                DraftDT.Rows(Num).Item("LBDropDepth") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("LBCoverage") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("LBHands") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("LBBlitz") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("LBPassRushType") = "'NONE'"
                DraftDT.Rows(Num).Item("LBFillGaps") = Math.Round(MT.GetGaussian(6.0, 0.66667), 1)
                DraftDT.Rows(Num).Item("LBContain") = Math.Round(MT.GetGaussian(6.0, 0.66667), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("LBRead") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("LBInstincts") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("LBDefeatBlocks") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("LBShedBlocks") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("LBInsideTackle") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("LBOutsideTackle") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("LBRead") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInstincts") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBDefeatBlocks") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBShedBlocks") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInsideTackle") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBOutsideTackle") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("LBRead") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("LBInstincts") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("LBDefeatBlocks") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("LBShedBlocks") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("LBInsideTackle") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("LBOutsideTackle") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("LBRead") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInstincts") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBDefeatBlocks") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBShedBlocks") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInsideTackle") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBOutsideTackle") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("LBRead") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInstincts") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBDefeatBlocks") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBShedBlocks") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInsideTackle") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBOutsideTackle") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("LBRead") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInstincts") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBDefeatBlocks") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBShedBlocks") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInsideTackle") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBOutsideTackle") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "CB"
                DraftDT.Rows(Num).Item("DBPressBailCoverage") = Math.Round(MT.GetGaussian(6, 0.6667), 1)

                DraftDT.Rows(Num).Item("DBBallReaction") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBRunContain") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBWardOffBlockers") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBRunTackling") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBHands") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBBump") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBRunContain") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBBlitz") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCatchupSpeed") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCatchupSpeed") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 4.16667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 4.16667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBCatchupSpeed") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCatchupSpeed") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCatchupSpeed") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCatchupSpeed") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "FS", "SS"
                DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBPressBailCoverage") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBBump") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBRunContain") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBWardOffBlockers") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBBlitz") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBHands") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCatchUpSpeed") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBallreaction") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCatchUpSpeed") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBallreaction") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBCatchUpSpeed") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBBallreaction") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCatchUpSpeed") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBallreaction") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCatchUpSpeed") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBallreaction") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCatchUpSpeed") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBallreaction") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "K"
                DraftDT.Rows(Num).Item("KPlantRelationship") = "'NONE'"
                DraftDT.Rows(Num).Item("KApproachAngle") = "'NONE'"
                DraftDT.Rows(Num).Item("KBallFlight") = "'NONE'"
                DraftDT.Rows(Num).Item("KSteppingPattern") = "'NONE'"
                DraftDT.Rows(Num).Item("KKickingStyle") = "'NONE'"
                DraftDT.Rows(Num).Item("KHandlingWind") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("KTackling") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("KRunAndPassAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("KKOFootSpeed") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("KFGOperationTimes") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)

                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("KAccuracy") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KHandlingPressure") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KFootSpeed") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKickQuickness") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKickRise") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKOProduction") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKOMentalStability") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("KAccuracy") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KHandlingPressure") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KFootSpeed") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKickQuickness") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKickRise") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKOProduction") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKOMentalStability") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("KAccuracy") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("KHandlingPressure") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("KFootSpeed") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("KKickQuickness") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("KKickRise") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("KKOProduction") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("KKOMentalStability") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("KAccuracy") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KHandlingPressure") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KFootSpeed") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKickQuickness") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKickRise") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKOProduction") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKOMentalStability") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("KAccuracy") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KHandlingPressure") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KFootSpeed") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKickQuickness") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKickRise") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKOProduction") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKOMentalStability") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("KAccuracy") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KHandlingPressure") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KFootSpeed") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKickQuickness") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKickRise") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKOProduction") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKOMentalStability") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "P"
                DraftDT.Rows(Num).Item("PFootSpeed") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("PApproachLine") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("PHandlingTime") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("PSteppingPattern") = "'NONE'"
                DraftDT.Rows(Num).Item("PHands") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("PTackling") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("PRunAndPassAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("PDistance") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PHangTime") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PPressureKicking") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PBlockZone") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PHandToFootTime") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PTiming") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("PDistance") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PHangTime") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PPressureKicking") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PBlockZone") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PHandToFootTime") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PTiming") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("PDistance") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("PHangTime") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("PPressureKicking") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("PBlockZone") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("PHandToFootTime") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("PTiming") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("PDistance") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PHangTime") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PPressureKicking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PBlockZone") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PHandToFootTime") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PTiming") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("PDistance") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PHangTime") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PPressureKicking") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PBlockZone") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PHandToFootTime") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PTiming") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("PDistance") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PHangTime") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PPressureKicking") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PBlockZone") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PHandToFootTime") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PTiming") = Math.Round(MT.GetGaussian(5, 0.66667), 1)


                End Select
        End Select


    End Sub

End Class
