
''' <summary>
''' Creates the GM's as requested by the number supplied.  32 GM's will randomly be placed on a team
''' the other GM's will be unemployed and will have a chance to get hired when a GM gets fired
''' </summary>
Public Class GeneralManager
    Inherits Personnel
    Public Sub GenPersonnel(ByVal NumPersonnel As Integer)
        Dim GetPersonnel As New Personnel
        Dim MyOPhil As String
        Dim MyDPhil As String
        Dim PersonnelDict(33) As Dictionary(Of Integer, Integer)
        Dim PersonnelType As Integer

        For i As Integer = 1 To 32 'initializes the array of dictionaries that keeps track of how many of each peronnel type are on each team
            PersonnelDict(i) = New Dictionary(Of Integer, Integer)
            For x As Integer = 0 To 13
                PersonnelDict(i).Add(x, 0)
            Next x
        Next i

        SQLiteTables.CreateTable(MyDB, PersonnelDT, "Personnel", GetSQLFieldNames("Personnel"))
        'SQLiteTables.DeleteTable(MyDB, PersonnelDT, "GMs")
        SQLiteTables.LoadTable(MyDB, PersonnelDT, "Personnel")
        PersonnelDT.Rows.Add(0)
        Try
            For i As Integer = 1 To NumPersonnel
                PersonnelDT.Rows.Add(i)
                MyOPhil = GetOffPhil()
                MyDPhil = GetDefPhil()
                GenNames(PersonnelDT, i, "Personnel")
                PersonnelDT.Rows(i).Item("PersonnelType") = GetPersonnelType(PersonnelDT.Rows(i).Item("Age"))
                PersonnelDT.Rows(i).Item("OrganizationalPower") = GetOrgPower(PersonnelDT.Rows(i).Item("PersonnelType"))
                PersonnelDT.Rows(i).Item("Experience") = (PersonnelDT.Rows(i).Item("Age") - 24)
                PersonnelDT.Rows(i).Item("Patience") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingDraft") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingFA") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingOwn") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingPot") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingQB") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingRB") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingWR") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingTE") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingOL") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingDL") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingLB") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingCB") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("JudgingSF") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("Loyalty") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("Ego") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("OffPhil") = String.Format("'{0}'", MyOPhil)
                PersonnelDT.Rows(i).Item("DefPhil") = String.Format("'{0}'", MyDPhil)
                PersonnelDT.Rows(i).Item("DraftStrategy") = String.Format("'{0}'", DraftStrategy())
                PersonnelDT.Rows(i).Item("TeamBuilding") = String.Format("'{0}'", TeamBuilding())
                PositionalImp(i, MyOPhil, MyDPhil)
                PersonnelDT.Rows(i).Item("Risktaker") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("ValuesDraftPicks") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("FranchiseTag") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("ValuesCombine") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("ValuesCharacter") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("ValuesProduction") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("ValuesIntangibles") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("Ambition") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("Delegation") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("TransitionTag") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("WorkEthic") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("Convincing") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("RespectedByPlayers") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("RespectedByCoaches") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("DraftPreparation") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("RespectedByScouts") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("Dysfunctional") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("Leadership") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("Accountability") = MT.GetGaussian(49.5, 16.5)
                PersonnelDT.Rows(i).Item("Adaptability") = MT.GetGaussian(49.5, 16.5) 'How good can he change his schemes/methods to fit players?
                PersonnelDT.Rows(i).Item("Backstabber") = MT.GetGaussian(49.5, 16.5) 'Does he throw other people under the bus to save himself?
                PersonnelType = PersonnelDT.Rows(i).Item("PersonnelType")

                For x = 1 To 32

                    Select Case PersonnelType
                        Case 1 To 8, 11, 12 'only 1 per team
                            If PersonnelDict(x).Item(PersonnelType) = 0 Then
                                PersonnelDict(x).Item(PersonnelType) = 1
                                PersonnelDT.Rows(i).Item("TeamID") = x
                                PersonnelType = 0
                            Else : PersonnelDT.Rows(i).Item("TeamID") = 0
                            End If

                        Case 9, 13 'National Scouts/Scouting Assistants 2 per team
                            If PersonnelDict(x).Item(PersonnelType) < 2 Then
                                PersonnelDict(x).Item(PersonnelType) += 1
                                PersonnelDT.Rows(i).Item("TeamID") = x
                                PersonnelType = 0
                            Else : PersonnelDT.Rows(i).Item("TeamID") = 0
                            End If

                        Case 10 'Area Scouts---6 Per Team
                            If PersonnelDict(x).Item(PersonnelType) < 6 Then
                                PersonnelDict(x).Item(PersonnelType) += 1
                                PersonnelDT.Rows(i).Item("TeamID") = x
                                PersonnelType = 0
                            Else : PersonnelDT.Rows(i).Item("TeamID") = 0
                            End If

                    End Select
                Next x

            Next i
        Catch ex As System.InvalidCastException
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.Data)
        End Try
        Try
            SQLiteTables.BulkInsert(MyDB, PersonnelDT, "Personnel")
        Catch ex As System.InvalidCastException
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.Data)
        End Try
    End Sub
    Private Function GetOrgPower(ByVal PersonnelType As Integer) As Integer
        Dim result As Integer
        Select Case PersonnelType
            Case 1 : result = MT.GetGaussian(75, 8.33)
            Case 2 : result = MT.GetGaussian(65, 8.33)
            Case 3 : result = MT.GetGaussian(60, 8.33)
            Case 4 : result = MT.GetGaussian(50, 8.33)
            Case 5 : result = MT.GetGaussian(55, 8.33)
            Case 6 : result = MT.GetGaussian(45, 8.33)
            Case 7 : result = MT.GetGaussian(50, 8.33)
            Case 8 : result = MT.GetGaussian(40, 8.33)
            Case 9 : result = MT.GetGaussian(35, 8.33)
            Case 10 : result = MT.GetGaussian(25, 8.33)
            Case 11 : result = MT.GetGaussian(30, 8.33)
            Case 12 : result = MT.GetGaussian(15, 5.0)
            Case 13 : result = MT.GetGaussian(10, 3.33)
        End Select
        Return result
    End Function
    ''' <summary>
    ''' Gets the type of personnel this person is based on age---younger people have  lower chance to be higher up in the organization than older people
    ''' </summary>
    ''' <param name="age"></param>
    ''' <returns></returns>
    Private Function GetPersonnelType(ByVal age As Integer) As Integer
        Dim result As Integer
        Select Case age
            Case < 26
                result = MT.GenerateInt32(1, 10)
                Select Case result
                    Case 1 To 4 : result = 13
                    Case 5 To 10 : result = 12 'NationalScoutingOrgScout
                End Select
            Case < 30
                result = 10 'Area Scout
            Case < 33
                result = MT.GenerateInt32(1, 6)
                Select Case result
                    Case 1 : result = 4
                    Case 2 : result = 6
                    Case 3 : result = 8
                    Case 4 : result = 9
                    Case 5 : result = 10
                    Case 6 : result = 11
                End Select
            Case < 39
                result = MT.GenerateInt32(1, 100)
                Select Case result
                    Case 1 '1% chance of being Assistant GM
                        result = 2
                    Case 2  '1% chance of being Director of Player Personnel
                        result = 3
                    Case 3 To 7 '5% chance of being AsstDir of Player Personnel
                        result = 4
                    Case 8 To 10 '3% chance of being Director of Pro Personnel
                        result = 5
                    Case 11 To 17 '7% chance of being Asst Director of Pro Personnel
                        result = 6
                    Case 18 To 26 '9% chance of being Director of College Scouting
                        result = 7
                    Case 27 To 41 '15% chance of being Asst Director of College Scouting
                        result = 8
                    Case 42 To 61 '20% chance of being National Scout
                        result = 9
                    Case 62 To 91 '30% chance of being Area Scout
                        result = 10
                    Case 92 To 100 '9% chance of being Advance Scout
                        result = 11
                End Select
            Case < 45
                result = MT.GenerateInt32(1, 100)
                Select Case result
                    Case 1 To 2 '2% chance of being selected as GM
                        result = 1
                    Case 3 To 6 '4% chance of being Assistant GM
                        result = 2
                    Case 7 To 10 '4% chance of being Dir Player Personnel
                        result = 3
                    Case 11 To 18 '8% chance of being Asst Director of Player Personnel
                        result = 4
                    Case 19 To 24 '6% Dir Pro Personnel
                        result = 5
                    Case 25 To 33 '9% chance Asst Dir Pro Personnel
                        result = 6
                    Case 34 To 44 '11% chance of Dir College Scouting
                        result = 7
                    Case 45 To 57 '13% chance of Asst Dir College Scouting
                        result = 8
                    Case 58 To 72 '15% chance National College Scout
                        result = 9
                    Case 73 To 92 '20% chance of Area Scout
                        result = 10
                    Case 92 To 100 '7% chance of Advance Scout
                        result = 11
                End Select
            Case Else
                result = MT.GenerateInt32(1, 100)
                Select Case result
                    Case 1 To 3 '3% GM
                        result = 1
                    Case 4 To 9 '6% Asst GM
                        result = 2
                    Case 10 To 19 '10% Player Personnel
                        result = 3
                    Case 20 To 27 '8% Asst Play Personnel
                        result = 4
                    Case 28 To 37 '10% Pro Personnel
                        result = 5
                    Case 38 To 45 '8% Asst Pro Personnel
                        result = 6
                    Case 46 To 55 '10% Dir College Scouting
                        result = 7
                    Case 56 To 63 '8% Asst Dir College Scouting
                        result = 8
                    Case 64 To 78 '15% National Scout
                        result = 9
                    Case 79 To 95 '17% Area Scout
                        result = 10
                    Case 96 To 100 '5% Advance Scout
                        result = 11
                End Select
        End Select
        Return result
    End Function
    Private Function DraftStrategy() As String
        Dim result As String = ""
        Select Case MT.GenerateInt32(1, 100)
            Case 1 To 15 : result = "DraftTradeDown"
            Case 16 To 30 : result = "DraftBestAvail"
            Case 31 To 45 : result = "DraftTheirGuy"
            Case 46 To 60 : result = "DraftBiggestNeed"
            Case 61 To 75 : result = "DraftTradeUp"
            Case 76 To 85 : result = "TradePicksForPlayers"
            Case 86 To 100 : result = "TradePlayersForPicks"
        End Select
        Return result
    End Function
    Private Function TeamBuilding() As String
        Dim result As String = ""
        Select Case MT.GenerateInt32(1, 90)
            Case 1 To 15 : result = "DraftKeepCore"
            Case 16 To 30 : result = "DraftLetVetsGo"
            Case 31 To 45 : result = "DraftGetVets"
            Case 46 To 60 : result = "FAKeepCore"
            Case 61 To 75 : result = "FALetVetsGo"
            Case 76 To 90 : result = "FAGetVets"
        End Select
        Return result
    End Function
    Private Sub PositionalImp(ByVal i As Integer, ByVal OffPhil As String, ByVal DefPhil As String)
        Select Case OffPhil
            'Sets the Importance of Various Positions for the offenses--over 100 is above normal
            'Under 100 is below normal---100 is average importance
            '1170 Points Allotted
            Case "BalPass"
                PersonnelDT.Rows(i).Item("QBImp") = 110
                PersonnelDT.Rows(i).Item("RBImp") = 90
                PersonnelDT.Rows(i).Item("FBImp") = 74
                PersonnelDT.Rows(i).Item("WRImp") = 110
                PersonnelDT.Rows(i).Item("WR2Imp") = 95
                PersonnelDT.Rows(i).Item("WR3Imp") = 85
                PersonnelDT.Rows(i).Item("LTImp") = 110
                PersonnelDT.Rows(i).Item("LGImp") = 95
                PersonnelDT.Rows(i).Item("CImp") = 105
                PersonnelDT.Rows(i).Item("RGImp") = 95
                PersonnelDT.Rows(i).Item("RTImp") = 95
                PersonnelDT.Rows(i).Item("TEImp") = 106

            Case "BalRun"
                PersonnelDT.Rows(i).Item("QBImp") = 100
                PersonnelDT.Rows(i).Item("RBImp") = 110
                PersonnelDT.Rows(i).Item("FBImp") = 103
                PersonnelDT.Rows(i).Item("WRImp") = 90
                PersonnelDT.Rows(i).Item("WR2Imp") = 80
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 105
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 110
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 95
                PersonnelDT.Rows(i).Item("TEImp") = 108

            Case "VertPass"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100

            Case "Smashmouth"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100

            Case "WCPass"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100
            Case "WCRun"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100
            Case "WCBal"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100
            Case "SpreadRun"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100
            Case "SpreadPass"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100
            Case "SpreadBal"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100
            Case "Run-N-Shoot"
                PersonnelDT.Rows(i).Item("QBImp") = 120
                PersonnelDT.Rows(i).Item("RBImp") = 85
                PersonnelDT.Rows(i).Item("FBImp") = 65
                PersonnelDT.Rows(i).Item("WRImp") = 120
                PersonnelDT.Rows(i).Item("WR2Imp") = 110
                PersonnelDT.Rows(i).Item("WR3Imp") = 70
                PersonnelDT.Rows(i).Item("LTImp") = 120
                PersonnelDT.Rows(i).Item("LGImp") = 105
                PersonnelDT.Rows(i).Item("CImp") = 115
                PersonnelDT.Rows(i).Item("RGImp") = 105
                PersonnelDT.Rows(i).Item("RTImp") = 105
                PersonnelDT.Rows(i).Item("TEImp") = 100

        End Select

        Select Case DefPhil
            Case "4-3Attack"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "4-3Cover"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "4-3Bal"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "4-3StuffRun"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "3-4Attack"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "3-4Cover"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "3-4Bal"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "3-4StuffRun"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "Cover2Attack"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "Cover2Cover"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "Cover2Bal"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
            Case "46"
                PersonnelDT.Rows(i).Item("DEImp") = 120
                PersonnelDT.Rows(i).Item("DE2Imp") = 85
                PersonnelDT.Rows(i).Item("DTImp") = 65
                PersonnelDT.Rows(i).Item("DT2Imp") = 120
                PersonnelDT.Rows(i).Item("NTImp") = 110
                PersonnelDT.Rows(i).Item("MLBImp") = 70
                PersonnelDT.Rows(i).Item("WLBImp") = 120
                PersonnelDT.Rows(i).Item("SLBImp") = 105
                PersonnelDT.Rows(i).Item("ROLBImp") = 115
                PersonnelDT.Rows(i).Item("LOLBImp") = 105
                PersonnelDT.Rows(i).Item("CB1Imp") = 105
                PersonnelDT.Rows(i).Item("CB2Imp") = 100
                PersonnelDT.Rows(i).Item("CB3Imp") = 100
                PersonnelDT.Rows(i).Item("FSImp") = 100
                PersonnelDT.Rows(i).Item("SSImp") = 100
        End Select


    End Sub
    ''' <summary>
    ''' Determines what scouting organization the scout is a part of. 12 teams use BLESTO, 15 use NFS, and the others aren't part of an organziation.
    ''' </summary>
    ''' <param name="TeamID"></param>
    ''' <returns></returns>
    Private Function GetScoutOrg(ByVal TeamID As Integer) As Integer 'determines membership in BLESTO/NFS
        Dim result As Integer
        Select Case TeamID
            Case 1, 3, 4, 5, 6, 8, 9, 11 To 14, 16, 18 To 32
                result = 12 'BLESTO Scout/NFS Scout
            Case Else : result = 13 'Scouting Assistant
        End Select
        Return result
    End Function

End Class
