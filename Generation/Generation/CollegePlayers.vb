
Imports System.Text.RegularExpressions
Public Class CollegePlayers
    Inherits Players
    Dim MyPos As String
    Dim PosType As String
    Dim DraftRound As String

    Public Sub GenDraftPlayers(ByVal PlayerNum As Integer, ByVal XCollegePlayer As CollegePlayers, ByVal DraftDT As DataTable, ByVal DraftClass As ArrayList)

        XCollegePlayer = New CollegePlayers

        Try
            DraftDT.Rows.Add(PlayerNum)
            MyPos = GetCollegePos(PlayerNum, DraftDT)
            DraftDT.Rows(PlayerNum).Item("FortyYardTime") = Get40Time(MyPos, PlayerNum, DraftDT)
            PosType = GetPosType(MyPos, PlayerNum, DraftDT)
            DraftDT.Rows(PlayerNum).Item("CollegePOS") = String.Format("'{0}'", MyPos)
            DraftDT.Rows(PlayerNum).Item("PosType") = String.Format("'{0}'", PosType)
            GenNames(DraftDT, PlayerNum, "CollegePlayer", MyPos)
            GetPersonalityStats(DraftDT, PlayerNum, XCollegePlayer)
            DraftRound = GetDraftRound(MyPos, DraftClass)
            DraftDT.Rows(PlayerNum).Item("TwentyYardTime") = Get20Time(MyPos)
            DraftDT.Rows(PlayerNum).Item("TenYardTime") = Get10Time(MyPos)
            DraftDT.Rows(PlayerNum).Item("ShortShuttle") = GetShortShuttle(MyPos)
            DraftDT.Rows(PlayerNum).Item("BroadJump") = GetBroadJump(MyPos)
            DraftDT.Rows(PlayerNum).Item("VertJump") = GetVertJump(MyPos)
            DraftDT.Rows(PlayerNum).Item("ThreeConeDrill") = Get3Cone(MyPos)
            DraftDT.Rows(PlayerNum).Item("BenchPress") = GetBenchPress(MyPos)
            DraftDT.Rows(PlayerNum).Item("InterviewSkills") = CInt(MT.GetGaussian(49.5, 16.5))
            DraftDT.Rows(PlayerNum).Item("WonderlicTest") = GetWonderlic(MyPos)
            DraftDT.Rows(PlayerNum).Item("SkillsTranslateToNFL") = GetSkillsTranslate(MyPos)
            DraftDT.Rows(PlayerNum).Item("ProjNFLPos") = GetNFLPos(String.Format("'{0}'", MyPos), PlayerNum)
            DraftDT.Rows(PlayerNum).Item("DLPrimaryTech") = "'NONE'"
            DraftDT.Rows(PlayerNum).Item("DLSecondaryTech") = "'NONE'"
            DraftDT.Rows(PlayerNum).Item("DLPassRushTech") = "'NONE'"
            DraftDT.Rows(PlayerNum).Item("RETKickReturn") = GetKickRetAbility(MyPos, PlayerNum)
            DraftDT.Rows(PlayerNum).Item("RETPuntReturn") = GetPuntRetAbility(MyPos, PlayerNum, DraftDT)
            GetSTAbility(MyPos, PlayerNum, DraftDT)
            GetLSAbility(MyPos, PlayerNum, DraftDT)
            GetKeyRatings(DraftDT, PlayerNum, MyPos, DraftRound) 'Assigns base ratings based on Round drafted using an Exponential Decay Function 

            For x As Integer = 0 To DraftDT.Columns.Count - 1 'cycles through the columns and assigns a rating to any of them that are still NULL values
                If DraftDT.Rows(PlayerNum).Item(x) Is DBNull.Value Then
                    DraftDT.Rows(PlayerNum).Item(x) = MT.GetGaussian(49.5, 16.5)
                End If
            Next x
            GetPosRatings(MyPos, PosType, PlayerNum, DraftDT) 'Will get positional skills for players based on their position type

        Catch ex As System.InvalidCastException
            Console.WriteLine(ex.Data)
            Console.WriteLine(ex.Message)
        End Try
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
                   DraftDT.Rows(PlayerNum).Item("COD") > 7.0 And DraftDT.Rows(PlayerNum).Item("ShortAcc") < 6.0 And DraftDT.Rows(PlayerNum).Item("QBDecMaking") < 6.0 Then
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
        Dim result As Integer
        Select Case Pos
            Case "QB" 'QB 53% Bust 33% ProBowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 53 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 54 To 66 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "RB" 'RB Skills 49% Bust 36% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 49 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 50 To 63 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "FB" 'FB Skills usually translate pretty well to the NFL as well..
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 30 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 31 To 75 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "WR" 'WR 45% Bust 31% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 45 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 46 To 68 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "TE" 'TE Skills are fairly translatable
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 35 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 36 To 67 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "OT", "C", "OG" '31% Bust 26% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 31 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 32 To 73 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "DE" '31% Bust 33% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 31 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 32 To 66 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "DT" '33% Bust 40% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 33 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 34 To 59 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "OLB", "ILB" '16% Bust 26% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 16 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 17 To 73 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "CB" '29% Bust 23% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 29 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 30 To 77 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "FS", "SS" '11% Bust 53% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 11 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 12 To 46 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "K"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 45 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 46 To 76 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
            Case "P"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 40 : result = CInt(MT.GetGaussian(16.5, 5.5))
                    Case 41 To 76 : result = CInt(MT.GetGaussian(50, 6.667))
                    Case Else : result = CInt(MT.GetGaussian(85, 5))
                End Select
        End Select
        Return result
    End Function
    Private Function GetWonderlic(ByVal Pos As String) As Integer
        Dim result As Integer
        Select Case Pos
            Case "QB" : result = CInt(MT.GetGaussian(26.678, 6.521))
            Case "RB" : result = CInt(MT.GetGaussian(18.68, 6.1561))
            Case "FB" : result = CInt(MT.GetGaussian(19.5313, 6.13384))
            Case "WR" : result = CInt(MT.GetGaussian(19.6814, 6.14898))
            Case "TE" : result = CInt(MT.GetGaussian(24.0909, 8.18485))
            Case "OT" : result = CInt(MT.GetGaussian(24.881, 7.15376))
            Case "C" : result = CInt(MT.GetGaussian(26.9697, 6.28844))
            Case "OG" : result = CInt(MT.GetGaussian(24.0339, 6.09241))
            Case "DE" : result = CInt(MT.GetGaussian(20.1348, 7.59389))
            Case "DT" : result = CInt(MT.GetGaussian(19.4054, 7.94052))
            Case "OLB" : result = CInt(MT.GetGaussian(20.2623, 5.85872))
            Case "ILB" : result = CInt(MT.GetGaussian(22.4286, 6.27976))
            Case "CB" : result = CInt(MT.GetGaussian(18.4881, 5.74351))
            Case "FS" : result = CInt(MT.GetGaussian(21.0213, 5.71854))
            Case "SS" : result = CInt(MT.GetGaussian(19.2553, 5.38109))
            Case "K" : result = CInt(MT.GetGaussian(24.32, 6.21431))
            Case "P" : result = CInt(MT.GetGaussian(25.875, 5.38952))
        End Select
        Return result
    End Function
    Private Function GetBenchPress(ByVal Pos As String) As Integer
        Dim result As Integer
        Select Case Pos
            Case "QB" : result = CInt(MT.GetGaussian(19.8, 2.85657))
            Case "RB" : result = CInt(MT.GetGaussian(20.3148, 4.51673))
            Case "FB" : result = CInt(MT.GetGaussian(23.4848, 4.28614))
            Case "WR" : result = CInt(MT.GetGaussian(16.7586, 3.38001))
            Case "TE" : result = CInt(MT.GetGaussian(21.3333, 4.45034))
            Case "OT" : result = CInt(MT.GetGaussian(24.9524, 4.60651))
            Case "C" : result = CInt(MT.GetGaussian(26.5313, 4.24253))
            Case "OG" : result = CInt(MT.GetGaussian(26.4419, 4.79487))
            Case "DE" : result = CInt(MT.GetGaussian(24.6825, 5.9089))
            Case "DT" : result = CInt(MT.GetGaussian(28.3519, 5.78362))
            Case "OLB" : result = CInt(MT.GetGaussian(22.5873, 4.95899))
            Case "ILB" : result = CInt(MT.GetGaussian(22.8947, 3.85793))
            Case "CB" : result = CInt(MT.GetGaussian(15.4, 3.32265))
            Case "FS" : result = CInt(MT.GetGaussian(16.5, 3.59048))
            Case "SS" : result = CInt(MT.GetGaussian(17.2333, 4.63093))
        End Select
        Return result
    End Function
    Private Function Get3Cone(ByVal Pos As String) As Double
        Dim result As Double
        Select Case Pos
            Case "QB" : result = Math.Round(MT.GetGaussian(7.16143, 0.27591), 2)
            Case "RB" : result = Math.Round(MT.GetGaussian(7.00848, 0.16455), 2)
            Case "FB" : result = Math.Round(MT.GetGaussian(7.30794, 0.26069), 2)
            Case "WR" : result = Math.Round(MT.GetGaussian(6.95564, 0.16844), 2)
            Case "TE" : result = Math.Round(MT.GetGaussian(7.15089, 0.20702), 2)
            Case "OT" : result = Math.Round(MT.GetGaussian(7.80527, 0.30271), 2)
            Case "C" : result = Math.Round(MT.GetGaussian(7.71718, 0.25131), 2)
            Case "OG" : result = Math.Round(MT.GetGaussian(7.87723, 0.32694), 2)
            Case "DE" : result = Math.Round(MT.GetGaussian(7.33298, 0.27823), 2)
            Case "DT" : result = Math.Round(MT.GetGaussian(7.70881, 0.22825), 2)
            Case "OLB" : result = Math.Round(MT.GetGaussian(7.12055, 0.23154), 2)
            Case "ILB" : result = Math.Round(MT.GetGaussian(7.222, 0.21437), 2)
            Case "CB" : result = Math.Round(MT.GetGaussian(6.984, 0.22688), 2)
            Case "FS" : result = Math.Round(MT.GetGaussian(7.02656, 0.19514), 2)
            Case "SS" : result = Math.Round(MT.GetGaussian(7.06844, 0.24738), 2)
        End Select
        Return result
    End Function
    Private Function GetVertJump(ByVal Pos As String) As Double
        Dim result As Double
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
        If NumString = NumStr Then : result = NumStr
        Else

            Num = Regex.Match(NumString, "(?<=\d+\.)\d").Value
            If Num < 4 Then : result = NumStr
            ElseIf Num = 4 Then : result = CDbl(NumString) + 0.1
            ElseIf Num = 5 Then : result = CDbl(NumString)
            ElseIf Num = 6 Then : result = CDbl(NumString) - 0.1
            ElseIf Num = 7 Then : result = CDbl(NumString) - 0.2
            ElseIf Num = 8 Then : result = CDbl(NumString) + 0.2
            ElseIf Num = 9 Then : result = CDbl(NumString) + 0.1
            End If
        End If
        Return result
    End Function
    Private Function GetBroadJump(ByVal Pos As String) As Integer
        Dim result As Integer
        Select Case Pos
            Case "QB" : result = CInt(MT.GetGaussian(109.792, 5.32666))
            Case "RB" : result = CInt(MT.GetGaussian(117.897, 6.53304))
            Case "FB" : result = CInt(MT.GetGaussian(112.414, 6.87583))
            Case "WR" : result = CInt(MT.GetGaussian(120.213, 4.79013))
            Case "TE" : result = CInt(MT.GetGaussian(113.587, 5.61665))
            Case "OT" : result = CInt(MT.GetGaussian(102.7, 5.56567))
            Case "C" : result = CInt(MT.GetGaussian(102.432, 4.95133))
            Case "OG" : result = CInt(MT.GetGaussian(100.839, 5.82041))
            Case "DE" : result = CInt(MT.GetGaussian(113.898, 5.58356))
            Case "DT" : result = CInt(MT.GetGaussian(106.217, 4.38829))
            Case "OLB" : result = CInt(MT.GetGaussian(116.323, 5.55841))
            Case "ILB" : result = CInt(MT.GetGaussian(113.263, 5.00886))
            Case "CB" : result = CInt(MT.GetGaussian(121.865, 5.82475))
            Case "FS" : result = CInt(MT.GetGaussian(119.432, 6.13162))
            Case "SS" : result = CInt(MT.GetGaussian(119.075, 6.38117))
        End Select
        Return result
    End Function
    Private Function GetShortShuttle(ByVal Pos As String) As Double
        Dim result As Double
        Select Case Pos
            Case "QB" : result = Math.Round(MT.GetGaussian(4.37, 0.18174), 2)
            Case "RB" : result = Math.Round(MT.GetGaussian(4.31, 0.13471), 2)
            Case "FB" : result = Math.Round(MT.GetGaussian(4.4, 0.16438), 2)
            Case "WR" : result = Math.Round(MT.GetGaussian(4.25, 0.12744), 2)
            Case "TE" : result = Math.Round(MT.GetGaussian(4.39, 0.16592), 2)
            Case "OT" : result = Math.Round(MT.GetGaussian(4.76, 0.19038), 2)
            Case "C" : result = Math.Round(MT.GetGaussian(4.62, 0.17958), 2)
            Case "OG" : result = Math.Round(MT.GetGaussian(4.81, 0.18228), 2)
            Case "DE" : result = Math.Round(MT.GetGaussian(4.45, 0.18935), 2)
            Case "DT" : result = Math.Round(MT.GetGaussian(4.64, 0.18484), 2)
            Case "OLB" : result = Math.Round(MT.GetGaussian(4.31, 0.15413), 2)
            Case "ILB" : result = Math.Round(MT.GetGaussian(4.32, 0.12603), 2)
            Case "CB" : result = Math.Round(MT.GetGaussian(4.24, 0.13143), 2)
            Case "FS" : result = Math.Round(MT.GetGaussian(4.28, 0.16186), 2)
            Case "SS" : result = Math.Round(MT.GetGaussian(4.23, 0.15594), 2)
        End Select
        Return result
    End Function

    Private Function Get20Time(ByVal Pos As String) As Double
        Dim result As Double
        Select Case Pos
            Case "QB" : result = Math.Round(MT.GetGaussian(2.8, 0.094), 2)
            Case "RB" : result = Math.Round(MT.GetGaussian(2.63, 0.082), 2)
            Case "FB" : result = Math.Round(MT.GetGaussian(2.74, 0.0776), 2)
            Case "WR" : result = Math.Round(MT.GetGaussian(2.62, 0.0715), 2)
            Case "TE" : result = Math.Round(MT.GetGaussian(2.76, 0.0889), 2)
            Case "OT" : result = Math.Round(MT.GetGaussian(3.04, 0.1187), 2)
            Case "OG" : result = Math.Round(MT.GetGaussian(3.01, 0.0839), 2)
            Case "C" : result = Math.Round(MT.GetGaussian(3.07, 0.1067), 2)
            Case "DE" : result = Math.Round(MT.GetGaussian(2.8, 0.086), 2)
            Case "DT" : result = Math.Round(MT.GetGaussian(2.96, 0.0906), 2)
            Case "OLB" : result = Math.Round(MT.GetGaussian(2.7, 0.0809), 2)
            Case "ILB" : result = Math.Round(MT.GetGaussian(2.75, 0.0885), 2)
            Case "CB" : result = Math.Round(MT.GetGaussian(2.595, 0.0704), 2)
            Case "FS" : result = Math.Round(MT.GetGaussian(2.644, 0.0675), 2)
            Case "SS" : result = Math.Round(MT.GetGaussian(2.649, 0.08177), 2)
        End Select
        Return result
    End Function
    Private Function Get10Time(ByVal Pos As String) As Double
        Dim result As Double
        Select Case Pos
            Case "QB" : result = Math.Round(MT.GetGaussian(1.84, 0.064), 2)
            Case "RB" : result = Math.Round(MT.GetGaussian(1.6, 0.0519), 2)
            Case "FB" : result = Math.Round(MT.GetGaussian(1.67, 0.056), 2)
            Case "WR" : result = Math.Round(MT.GetGaussian(1.59, 0.0535), 2)
            Case "TE" : result = Math.Round(MT.GetGaussian(1.69, 0.0573), 2)
            Case "OT" : result = Math.Round(MT.GetGaussian(1.84, 0.064), 2)
            Case "OG" : result = Math.Round(MT.GetGaussian(1.84, 0.0695), 2)
            Case "C" : result = Math.Round(MT.GetGaussian(1.8, 0.0632), 2)
            Case "DE" : result = Math.Round(MT.GetGaussian(1.68, 0.0605), 2)
            Case "DT" : result = Math.Round(MT.GetGaussian(1.77, 0.0636), 2)
            Case "OLB" : result = Math.Round(MT.GetGaussian(1.63, 0.0548), 2)
            Case "ILB" : result = Math.Round(MT.GetGaussian(1.66, 0.0638), 2)
            Case "CB" : result = Math.Round(MT.GetGaussian(1.49, 0.0465), 2)
            Case "SS" : result = Math.Round(MT.GetGaussian(1.51, 0.0403), 2)
            Case "FS" : result = Math.Round(MT.GetGaussian(1.51, 0.0386), 2)
        End Select
        Return result
    End Function

    Private Function GetExplosiveNumber(ByVal Pos As String, i As Integer, DT As DataTable) As Double
        Dim result As Integer
        result = DT.Rows(i).Item("VerticalJump") + DT.Rows(i).Item("BenchPressReps") + (DT.Rows(i).Item("BroadJump") / 12)
        Return result
    End Function
    ''' <summary>
    ''' Types of drafts---Poor--few good players and lack of depth, Shallow---lack of both depth and good players, Normal, Top Heavy(high quality players at the top not good depth), Deep Draft(Not as many high quality top players but more good players than normal in later
    ''' rounds, Stacked Draft(High Quality and Deep---very rare), need to be position specific
    ''' Returns an ArrayList(QBPercentageTopEnd, QBPercentageMidRound, RBPercentageTopEnd, RBPercentageMidround, WRPercentageTopend, WRPercentageMidround, TEPercentageTopEnd, TEPercentageMidRound, OTPercentageTopEnd, OTPercentageMidRound,
    ''' CPercentageTopeend, CPercentageMidRound, OGPercentageopEnd, OGPercentageMidRound, DEPercentageTopEnd, DEPercentageMidRound, DTPercentageTopEnd, DTPercentageMidRound, OLBPercentageTopEnd, OLBPercentageMidRound, ILBPercentageTopEnd, ILBPercentageMidRound,
    ''' CBPrecentageTopEnd, CBPercentageMidRound,SFPercentageTopEnd, SFPercentageidRound)
    ''' </summary>

    Public Function GenDraftClass(ByVal DraftClassType As ArrayList) As ArrayList
        Dim result As Integer
        Dim TopEnd As Double
        Dim MidRound As Double

        'Generates a draft class
        '-------------------------------------------------
        'Gens strength of draft class for each position
        '---------------------------------------------------
        'Poor-3%/25-35% less 1st/2nd round talent|25-35% less mid round talent
        'Shallow-10%/3-13% less 1st/2nd round talent|25-35% less mid round talent
        'LackingTopEndButDeep-10%/ 15-25% less 1st/2nd round talent|15-25% more mid round talent
        'Normal-54%/between -5 to 5% on both
        'TopHeavy-10%/ 15-25% more 1st/2nd round talent|15-25% less mid round talent
        'Deep- 10%/3-13% more 1st/2nd round talent/25-35% more mid round talent
        'Stacked-3%

        For i As Integer = 1 To 17
            result = MT.GenerateInt32(1, 100) 'selects the type of class for each of the 16 positions

            Select Case result
                Case 1 To 2 'poor
                    TopEnd = MT.GenerateDouble(0.65, 0.75)
                    MidRound = MT.GenerateDouble(0.65, 0.75)
                Case 3 To 8 'shallow
                    TopEnd = MT.GenerateDouble(0.87, 0.97)
                    MidRound = MT.GenerateDouble(0.65, 0.75)
                Case 9 To 15 'LackingButDeep
                    TopEnd = MT.GenerateDouble(0.75, 0.85)
                    MidRound = MT.GenerateDouble(1.15, 1.25)
                Case 16 To 86 'Normal
                    TopEnd = MT.GenerateDouble(0.95, 1.05)
                    MidRound = MT.GenerateDouble(0.95, 1.05)
                Case 87 To 92 'TopHeavy
                    TopEnd = MT.GenerateDouble(1.15, 1.25)
                    MidRound = MT.GenerateDouble(0.75, 0.85)
                Case 93 To 98 'Deep
                    TopEnd = MT.GenerateDouble(1.03, 1.13)
                    MidRound = MT.GenerateDouble(1.25, 1.35)
                Case 99 To 100 'Stacked
                    TopEnd = MT.GenerateDouble(1.25, 1.35)
                    MidRound = MT.GenerateDouble(1.25, 1.35)
            End Select

            DraftClassType.Add(TopEnd)
            DraftClassType.Add(MidRound)
        Next i

        Return DraftClassType
    End Function
    ''' <summary>
    ''' Position numbers for PlayerDict: QB1,HB2,FB3,WR4,TE5,OT6,C7,OG8,DE9,DT10,OLB11,ILB12,CB13,FS13,SS14,K15,P16
    ''' </summary>
    Private Function GetDraftRound(ByVal Pos As String, ByVal DraftClassType As ArrayList) As String
        Dim TopEnd As Double
        Dim MidRound As Double
        Dim result As String = ""
        Dim CheckPos As Integer
        Dim DraftPosEnd(14) As Double
        Dim Remaining As Integer
        Dim RemainingPercentage(6) As Single

        Select Case Pos
            Case "QB" : CheckPos = 0
            Case "RB" : CheckPos = 2
            Case "FB" : CheckPos = 4
            Case "WR" : CheckPos = 6
            Case "TE" : CheckPos = 8
            Case "OT" : CheckPos = 10
            Case "C" : CheckPos = 12
            Case "OG" : CheckPos = 14
            Case "DE" : CheckPos = 16
            Case "DT" : CheckPos = 18
            Case "OLB" : CheckPos = 20
            Case "ILB" : CheckPos = 22
            Case "CB" : CheckPos = 24
            Case "FS" : CheckPos = 26
            Case "SS" : CheckPos = 28
            Case "K" : CheckPos = 30
            Case "P" : CheckPos = 32
        End Select

        'These are the breakdowns of how players get created by rounds---the top 4 are all 1st round---top 5 talent, top 10 talent, mid first round talent, late first round talent
        DraftPosEnd(1) = 6
        DraftPosEnd(2) = 14
        DraftPosEnd(3) = 24
        DraftPosEnd(4) = 38
        DraftPosEnd(5) = 98 '2nd
        DraftPosEnd(6) = 188 '3rd
        DraftPosEnd(7) = 308 '4th
        DraftPosEnd(8) = 458 '5th
        DraftPosEnd(9) = 638 '6th
        DraftPosEnd(10) = 838 '7th
        DraftPosEnd(11) = 1138 'PFA
        DraftPosEnd(12) = 1538 'LFA
        DraftPosEnd(13) = 2038 'PracSquad
        DraftPosEnd(14) = 3800 'Reject
        Try
            Remaining = DraftPosEnd(14) - DraftPosEnd(8)
            RemainingPercentage(1) = (DraftPosEnd(9) - DraftPosEnd(8)) / Remaining
            RemainingPercentage(2) = (DraftPosEnd(10) - DraftPosEnd(9)) / Remaining
            RemainingPercentage(3) = (DraftPosEnd(11) - DraftPosEnd(10)) / Remaining
            RemainingPercentage(4) = (DraftPosEnd(12) - DraftPosEnd(11)) / Remaining
            RemainingPercentage(5) = (DraftPosEnd(13) - DraftPosEnd(12)) / Remaining
            RemainingPercentage(6) = (DraftPosEnd(14) - DraftPosEnd(13)) / Remaining

            TopEnd = DraftClassType.Item(CheckPos)
            MidRound = DraftClassType.Item(CheckPos + 1)
        Catch ex As System.ArgumentOutOfRangeException
            Console.WriteLine(ex.Data)
            Console.WriteLine(ex.Message)
        End Try
        '##### TOP ROUND MODIFIERS #####
        DraftPosEnd(1) *= TopEnd
        DraftPosEnd(2) = CInt((DraftPosEnd(2) - DraftPosEnd(1) + 1) * TopEnd)
        DraftPosEnd(3) = CInt((DraftPosEnd(3) - DraftPosEnd(2) + 1) * TopEnd)
        DraftPosEnd(4) = CInt((DraftPosEnd(4) - DraftPosEnd(3) + 1) * TopEnd)
        DraftPosEnd(5) = CInt((DraftPosEnd(5) - DraftPosEnd(4) + 1) * TopEnd)
        '##### MID ROUND MODIFIERS #####
        DraftPosEnd(6) = CInt((DraftPosEnd(6) - DraftPosEnd(5) + 1) * MidRound)
        DraftPosEnd(7) = CInt((DraftPosEnd(7) - DraftPosEnd(6) + 1) * MidRound)
        DraftPosEnd(8) = CInt((DraftPosEnd(8) - DraftPosEnd(7) + 1) * MidRound)
        '##### LATE ROUND/UFA MODIFIERS ##### Basically we are taking the remaining number of players and breaking them down according to the same percentage as they are currently
        Remaining = DraftPosEnd(14) - DraftPosEnd(8)

        DraftPosEnd(9) = CInt(RemainingPercentage(1) * Remaining) + DraftPosEnd(8)
        DraftPosEnd(10) = CInt(RemainingPercentage(2) * Remaining) + DraftPosEnd(9)
        DraftPosEnd(11) = CInt(RemainingPercentage(3) * Remaining) + DraftPosEnd(10)
        DraftPosEnd(12) = CInt(RemainingPercentage(4) * Remaining) + DraftPosEnd(11)
        DraftPosEnd(13) = CInt(RemainingPercentage(5) * Remaining) + DraftPosEnd(12)

        Select Case MT.GenerateInt32(1, 3800) 'normal draft at 0% changes
            Case 1 To DraftPosEnd(1) 'Elite 1st---Top 5
                result = "R1Top5"
            Case DraftPosEnd(1) + 1 To DraftPosEnd(2) 'Elite 1st---Top 10
                result = "R1Top10"
            Case DraftPosEnd(2) + 1 To DraftPosEnd(3) 'Mid First
                result = "R1MidFirst"
            Case DraftPosEnd(3) + 1 To DraftPosEnd(4) 'LowFirst
                result = "R1LateFirst"
            Case DraftPosEnd(4) + 1 To DraftPosEnd(5) '2nd
                result = "R2"
            Case DraftPosEnd(5) + 1 To DraftPosEnd(6) '3rd
                result = "R3"
            Case DraftPosEnd(6) + 1 To DraftPosEnd(7) '4th
                result = "R4"
            Case DraftPosEnd(7) + 1 To DraftPosEnd(8) '5th
                result = "R5"
            Case DraftPosEnd(8) + 1 To DraftPosEnd(9) '6th
                result = "R6"
            Case DraftPosEnd(9) + 1 To DraftPosEnd(10) '7th
                result = "R7"
            Case DraftPosEnd(10) + 1 To DraftPosEnd(11) 'PSA
                result = "PUFA"
            Case DraftPosEnd(11) + 1 To DraftPosEnd(12) 'PFA
                result = "LUFA"
            Case DraftPosEnd(12) + 1 To DraftPosEnd(13) 'LFA
                result = "PracSquad"
            Case DraftPosEnd(13) + 1 To DraftPosEnd(14) 'Reject
                result = "Reject"
        End Select
        Return result
    End Function

End Class
