Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.Strings
Imports System.String
Imports SQLFunctions.SQLiteDataFunctions
Public Class Person
    ''' <summary>
    ''' Calls the Sub PutDataInDT to load all data in the tables
    ''' </summary>
    Public Sub LoadData()
        PutDataInDT(FirstNames, ReadFName)
        PutDataInDT(LastNames, ReadLName)
        PutDataInDT(Colleges, ReadCollege)
    End Sub
    ''' <summary>
    ''' Takes a Data Table parameter and the file name parameter and then creates 3 seperate data tables for them.
    ''' Loads Files to DataTables and then uses the datatables while generating to prevent constant opening and closing of files
    ''' </summary>
    ''' <param name="DT"></param>
    ''' <param name="file"></param>
    Private Sub PutDataInDT(ByVal DT As DataTable, ByVal file As StreamReader)

        Dim StringArray As String() = file.ReadLine().Split(","c)
        Dim Row As DataRow
        Dim line As String

        For Each s As String In StringArray
            DT.Columns.Add(New DataColumn())
        Next

        Row = DT.NewRow() 'adds in the initial line, or this was getting skipped previously
        Row.ItemArray = StringArray
        DT.Rows.Add(Row)

        Using file
            While Not file.EndOfStream
                Line = file.ReadLine
                Row = DT.NewRow()
                Try
                    Row.ItemArray = Line.Split(","c)
                    DT.Rows.Add(Row)
                Catch ex As System.ArgumentException
                    Console.WriteLine(ex.Message)
                End Try

            End While
        End Using
    End Sub
    Private Function GetItem(ByVal MyItem As DataTable, ByVal OutputTo As DataTable) As String
        Dim count As Integer = MyItem.Rows.Count - 1

        Dim MinName As Double = 0
        Dim MaxName As Double = MyItem.Rows(count).Item(2) 'gets the last frequency value
        Dim MinCol As Integer = 0
        Dim MaxCol As Integer = MyItem.Rows(count).Item(2)
        Dim ResName As Double = Math.Round(MT.GenerateDouble(MinName, MaxName), 3)
        Dim ResCol As Integer = MT.GenerateInt32(MinCol, MaxCol)
        Dim SupName As String
        Dim RowMin As Double
        Dim RowMax As Double

        'Console.WriteLine(ResName & "   " & ResCol)
        For row As Integer = 0 To MyItem.Rows.Count - 1 'cycles through the rows until it finds the appropriate row
            RowMin = MyItem.Rows(row).Item(1)
            RowMax = MyItem.Rows(row).Item(2)
            Select Case MyItem.Rows.Count - 1 'for some reason DataTable info is all NULL
                Case < 300 'choose college format
                    If ResCol >= RowMin And ResCol <= RowMax Then
                        SupName = MyItem.Rows(row).Item(0).ToString
                        'Console.WriteLine("Min: {0} Max: {1} Result: {2} Supposed to be Name: {3}", RowMin, RowMax, ResCol, SupName)
                        Return MyItem.Rows(row).Item(0).ToString 'return colleges normally because they are capitalized many times(USC, UCLA, etc...)
                        Exit For
                    End If
                Case Else
                    If ResName >= RowMin And ResName <= RowMax Then 'return name in Proper Case instead of All Caps
                        SupName = StrConv(MyItem.Rows(row).Item(0).ToString, VbStrConv.ProperCase)
                        'Console.WriteLine("Min: {0} Max: {1} Result: {2} Supposed to be Name: {3}", RowMin, RowMax, ResName, SupName)
                        Return StrConv(MyItem.Rows(row).Item(0).ToString, VbStrConv.ProperCase)
                        Exit For
                    End If
            End Select
        Next row
        'Console.WriteLine("Min: {0} Max: {1} Result: {2} Supposed to be Name: {3}", RowMin, RowMax, ResName, SupName)
        ' Console.WriteLine("Min: {0} Max: {1} Result: {2} Supposed to be Name: {3}", RowMin, RowMax, ResCol, SupName)
    End Function
    ''' <summary>
    ''' Generates all the Data a "Person" would have
    ''' For some reason names and colleges will return nothing at times, trying to track down the source of this...have it regenerating a value when this
    ''' happens currently.
    ''' </summary>
    ''' <param name="DTOutputTo"></param>
    ''' <param name="Row"></param>
    ''' <param name="PersonType"></param>
    ''' <param name="Position"></param>
    Public Sub GenNames(ByVal DTOutputTo As DataTable, ByVal Row As Integer, ByVal PersonType As String, Optional ByVal Position As String = "")

        'While DTOutputTo.Rows(Row).Item("FName") = DBNull.Value Or DTOutputTo.Rows(Row).Item("FName") = "''" Or DTOutputTo.Rows(Row).Item("LName") = DBNull.Value Or
        'DTOutputTo.Rows(Row).Item("LName") = "''" Or DTOutputTo.Rows(Row).Item("Colleges") = DBNull.Value Or DTOutputTo.Rows(Row).Item("Colleges") = "''"
        Try
                DTOutputTo.Rows(Row).Item("FName") = String.Format("'{0}'", GetItem(FirstNames, DTOutputTo)) 'adds the necessary ' ' modifier to strings for SQLite
                DTOutputTo.Rows(Row).Item("LName") = String.Format("'{0}'", GetItem(LastNames, DTOutputTo))
                DTOutputTo.Rows(Row).Item("College") = String.Format("'{0}'", GetItem(Colleges, DTOutputTo))
                DTOutputTo.Rows(Row).Item("Age") = GenAge(PersonType, Position)
            DTOutputTo.Rows(Row).Item("DOB") = String.Format("'{0}", GetDOB(DTOutputTo.Rows(Row).Item("Age")))

        Catch ex As System.InvalidCastException
                Console.WriteLine(ex.Message)
                Console.WriteLine(ex.Data)
            End Try
        'End While

        If Position <> "" Then 'only generates this data if they are a player as its not relevant to the other people
            DTOutputTo.Rows(Row).Item("Height") = GetHeight(Position)
            DTOutputTo.Rows(Row).Item("Weight") = GetWeight(Position, DTOutputTo.Rows(Row).Item("Height"))
            DTOutputTo.Rows(Row).Item("HandLength") = GetHandLength(DTOutputTo.Rows(Row).Item("Height"), DTOutputTo.Rows(Row).Item("Age"))
            DTOutputTo.Rows(Row).Item("ArmLength") = GetArmLength(DTOutputTo.Rows(Row).Item("Height"))
            Console.WriteLine("Height: {0}  ArmLength: {1}  Hand Length {2}", DTOutputTo.Rows(Row).Item("Height"), DTOutputTo.Rows(Row).Item("ArmLength"), DTOutputTo.Rows(Row).Item("HandLength"))
        End If
    End Sub
    ''' <summary>
    ''' Returns the person's Date Of Birth
    ''' </summary>
    ''' <param name="Age"></param>
    ''' <returns></returns>
    Private Function GetDOB(ByVal Age As Integer) As String
        Dim Day As Integer
        Dim Month As Integer
        Dim Year As Integer

        Month = MT.GenerateInt32(1, 12)

        Select Case Month
            Case 1, 3, 5, 7, 8, 10, 12
                Day = MT.GenerateInt32(1, 31)
            Case 2
                Day = MT.GenerateInt32(1, 28)
            Case Else
                Day = MT.GenerateInt32(1, 30)
        End Select

        Year = Date.Today.Year - Age

        Return String.Format("{0}/{1}/{2}'", Month, Day, Year) 'creates the proper format for SQLite ' ' around string

    End Function
    ''' <summary>
    ''' Generates the player age based on Person Type and in the case of NFL Player, by Position
    ''' </summary>
    ''' <param name="PersonType"></param>
    ''' <param name="Position"></param>
    ''' <returns></returns>
    Private Function GenAge(ByVal PersonType As String, Optional ByVal Position As String = "") As Integer
        Select Case PersonType
            Case "Owner"
                Return MT.GenerateInt32(45, 89)
            Case "GM"
                Return MT.GenerateInt32(35, 70)
            Case "Coach", "Scout"
                Return MT.GenerateInt32(32, 70)
            Case "NFLPlayer"
                Return GetPlayerAge(Position)
            Case "CollegePlayer"
                Return GetDraftAge()
        End Select
    End Function

    Private Function GetPlayerAge(ByVal Pos As String) As Integer
        Select Case Pos
            Case "QB"
                Select Case MT.GenerateInt32(1, 97)
                    Case 1 To 2 : Return 21
                    Case 3 To 5 : Return 22
                    Case 6 To 11 : Return 23
                    Case 12 To 25 : Return 24
                    Case 26 To 35 : Return 25
                    Case 36 To 47 : Return 26
                    Case 48 To 54 : Return 27
                    Case 55 To 62 : Return 28
                    Case 63 To 71 : Return 29
                    Case 72 To 76 : Return 30
                    Case 77 To 78 : Return 31
                    Case 79 To 84 : Return 32
                    Case 85 To 87 : Return 33
                    Case 88 To 90 : Return 34
                    Case 91 : Return 35
                    Case 92 : Return 36
                    Case 93 To 94 : Return 37
                    Case 95 : Return 38
                    Case 96 To 97 : Return 39
                End Select
            Case "RB"
                Select Case MT.GenerateInt32(1, 128)
                    Case 1 To 2 : Return 21
                    Case 3 To 18 : Return 22
                    Case 19 To 31 : Return 23
                    Case 32 To 56 : Return 24
                    Case 57 To 71 : Return 25
                    Case 72 To 89 : Return 26
                    Case 90 To 99 : Return 27
                    Case 100 To 106 : Return 28
                    Case 107 To 112 : Return 29
                    Case 113 To 121 : Return 30
                    Case 122 To 124 : Return 31
                    Case 125 To 126 : Return 32
                    Case 127 To 128 : Return 33
                End Select
            Case "FB"
                Select Case MT.GenerateInt32(1, 39)
                    Case 1 To 5 : Return 23
                    Case 6 To 9 : Return 24
                    Case 10 To 11 : Return 25
                    Case 12 To 15 : Return 26
                    Case 16 To 19 : Return 27
                    Case 20 To 23 : Return 28
                    Case 24 To 31 : Return 29
                    Case 32 To 33 : Return 30
                    Case 34 To 35 : Return 31
                    Case 36 To 37 : Return 32
                    Case 38 To 39
                        Select Case MT.GenerateInt32(1, 5)
                            Case 1 : Return 33
                            Case 2 : Return 34
                            Case 3 : Return 35
                            Case 4 : Return 36
                            Case 5 : Return 37
                        End Select
                End Select
            Case "WR"
                Select Case MT.GenerateInt32(1, 195)
                    Case 1 To 4 : Return 21
                    Case 5 To 23 : Return 22
                    Case 24 To 48 : Return 23
                    Case 49 To 75 : Return 24
                    Case 76 To 98 : Return 25
                    Case 99 To 121 : Return 26
                    Case 122 To 135 : Return 27
                    Case 136 To 157 : Return 28
                    Case 158 To 162 : Return 29
                    Case 163 To 174 : Return 30
                    Case 175 To 179 : Return 31
                    Case 180 To 182 : Return 32
                    Case 183 To 188 : Return 33
                    Case 189 : Return 34
                    Case 190 To 191 : Return 35
                    Case 192 To 194 : Return 36
                    Case 195 : Return 37
                End Select
            Case "TE"
                Select Case MT.GenerateInt32(1, 115)
                    Case 1 To 9 : Return 22
                    Case 10 To 17 : Return 23
                    Case 18 To 33 : Return 24
                    Case 34 To 50 : Return 25
                    Case 51 To 63 : Return 26
                    Case 64 To 72 : Return 27
                    Case 73 To 79 : Return 28
                    Case 80 To 92 : Return 29
                    Case 93 To 100 : Return 30
                    Case 101 To 103 : Return 31
                    Case 104 To 110 : Return 32
                    Case 111 To 115 : Return 33
                End Select
            Case "C"
                Select Case MT.GenerateInt32(1, 90)
                    Case 1 : Return 22
                    Case 2 To 9 : Return 23
                    Case 10 To 17 : Return 24
                    Case 18 To 23 : Return 25
                    Case 24 To 34 : Return 26
                    Case 35 To 45 : Return 27
                    Case 46 To 53 : Return 28
                    Case 54 To 60 : Return 29
                    Case 61 To 67 : Return 30
                    Case 68 To 69 : Return 31
                    Case 70 To 81 : Return 32
                    Case 82 : Return 33
                    Case 83 To 85 : Return 34
                    Case 86 : Return 35
                    Case 87 : Return 36
                    Case 88 : Return 37
                    Case 89 To 90 : Return 38
                End Select
            Case "OG"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 4 : Return 22
                    Case 5 To 15 : Return 23
                    Case 16 To 26 : Return 24
                    Case 27 To 36 : Return 25
                    Case 37 To 50 : Return 26
                    Case 51 To 66 : Return 27
                    Case 67 To 72 : Return 28
                    Case 73 To 81 : Return 29
                    Case 82 To 88 : Return 30
                    Case 89 To 93 : Return 31
                    Case 94 To 97 : Return 32
                    Case 98 To 100 : Return 33
                End Select
            Case "OT"
                Select Case MT.GenerateInt32(1, 143)
                    Case 1 : Return 21
                    Case 2 To 5 : Return 22
                    Case 6 To 25 : Return 23
                    Case 26 To 45 : Return 24
                    Case 46 To 67 : Return 25
                    Case 68 To 88 : Return 26
                    Case 89 To 95 : Return 27
                    Case 96 To 104 : Return 28
                    Case 105 To 115 : Return 29
                    Case 116 To 126 : Return 30
                    Case 127 To 129 : Return 31
                    Case 130 To 132 : Return 32
                    Case 133 To 137 : Return 33
                    Case 138 To 141 : Return 34
                    Case 142 : Return 35
                End Select
            Case "DE"
                Select Case MT.GenerateInt32(1, 155)
                    Case 1 To 4 : Return 21
                    Case 5 To 13 : Return 22
                    Case 14 To 35 : Return 23
                    Case 36 To 53 : Return 24
                    Case 54 To 70 : Return 25
                    Case 71 To 87 : Return 26
                    Case 88 To 98 : Return 27
                    Case 99 To 109 : Return 28
                    Case 110 To 118 : Return 29
                    Case 119 To 128 : Return 30
                    Case 129 To 134 : Return 31
                    Case 135 To 142 : Return 32
                    Case 143 To 148 : Return 33
                    Case 149 To 151 : Return 34
                    Case 152 To 154 : Return 35
                    Case 155 : Return 36
                End Select
            Case "DT"
                Select Case MT.GenerateInt32(1, 150)
                    Case 1 To 9 : Return 22
                    Case 10 To 26 : Return 23
                    Case 27 To 47 : Return 24
                    Case 48 To 67 : Return 25
                    Case 68 To 87 : Return 26
                    Case 88 To 99 : Return 27
                    Case 100 To 109 : Return 28
                    Case 110 To 120 : Return 29
                    Case 121 To 131 : Return 30
                    Case 132 To 137 : Return 31
                    Case 138 To 142 : Return 32
                    Case 143 To 145 : Return 33
                    Case 146 To 147 : Return 34
                    Case 148 To 150
                        Select Case MT.GenerateInt32(1, 5)
                            Case 1 : Return 35
                            Case 2 : Return 36
                            Case 3 : Return 37
                            Case 4 : Return 38
                            Case 5 : Return 39
                        End Select
                End Select
            Case "OLB", "ILB"
                Select Case MT.GenerateInt32(1, 238)
                    Case 1 To 15 : Return 22
                    Case 16 To 45 : Return 23
                    Case 46 To 80 : Return 24
                    Case 81 To 114 : Return 25
                    Case 115 To 143 : Return 26
                    Case 144 To 166 : Return 27
                    Case 167 To 182 : Return 28
                    Case 183 To 197 : Return 29
                    Case 198 To 210 : Return 30
                    Case 211 To 217 : Return 31
                    Case 218 To 228 : Return 32
                    Case 229 To 232 : Return 33
                    Case 233 To 236 : Return 34
                    Case 237 To 238 : Return 35
                End Select
            Case "CB"
                Select Case MT.GenerateInt32(1, 195)
                    Case 1 To 5 : Return 21
                    Case 6 To 23 : Return 22
                    Case 24 To 54 : Return 23
                    Case 55 To 87 : Return 24
                    Case 88 To 109 : Return 25
                    Case 110 To 133 : Return 26
                    Case 134 To 147 : Return 27
                    Case 148 To 163 : Return 28
                    Case 164 To 167 : Return 29
                    Case 168 To 176 : Return 30
                    Case 177 To 181 : Return 31
                    Case 182 To 186 : Return 32
                    Case 187 To 189 : Return 33
                    Case 190 To 193 : Return 34
                    Case 194 To 195 : Return 35
                End Select
            Case "FS", "SS"
                Select Case MT.GenerateInt32(1, 138)
                    Case 1 To 10 : Return 22
                    Case 11 To 18 : Return 23
                    Case 19 To 42 : Return 24
                    Case 43 To 57 : Return 25
                    Case 58 To 86 : Return 26
                    Case 87 To 101 : Return 27
                    Case 102 To 110 : Return 28
                    Case 111 To 119 : Return 29
                    Case 120 To 125 : Return 30
                    Case 126 To 130 : Return 31
                    Case 132 To 133 : Return 32
                    Case 134 To 135 : Return 33
                    Case 136 : Return 34
                    Case 137 To 138 : Return 35
                End Select
            Case "K"
                Select Case MT.GenerateInt32(1, 37)
                    Case 1 To 2 : Return 22
                    Case 3 To 5 : Return 23
                    Case 6 To 7 : Return 24
                    Case 8 To 10 : Return 25
                    Case 11 To 12 : Return 26
                    Case 13 To 14 : Return 27
                    Case 15 To 16 : Return 28
                    Case 17 : Return 29
                    Case 18 : Return 30
                    Case 19 To 22 : Return 31
                    Case 23 To 24 : Return 32
                    Case 25 To 27 : Return 33
                    Case 28 To 29 : Return 34
                    Case 30 : Return 35
                    Case 31 To 33 : Return 36
                    Case 37 : Return 1
                    Case 38 : Return 1
                    Case 39 : Return 1
                    Case 40 : Return 1
                End Select
            Case "P"
                Select Case MT.GenerateInt32(1, 34)
                    Case 1 : Return 22
                    Case 2 To 3 : Return 23
                    Case 4 : Return 24
                    Case 5 To 7 : Return 25
                    Case 8 To 9 : Return 26
                    Case 10 To 13 : Return 27
                    Case 14 To 16 : Return 28
                    Case 17 To 19 : Return 29
                    Case 20 To 21 : Return 30
                    Case 22 To 23 : Return 31
                    Case 24 To 25 : Return 32
                    Case 26 To 27 : Return 33
                    Case 28 : Return 34
                    Case 29 : Return 35
                    Case 30 : Return 36
                    Case 31 : Return 37
                    Case 32 : Return 38
                    Case 33 : Return 39
                    Case 34 : Return 40
                End Select
        End Select

    End Function
    ''' <summary>
    ''' Generates the draft age for a college player
    ''' </summary>
    ''' <returns></returns>
    Private Function GetDraftAge() As Integer
        Dim i As Integer = MT.GenerateInt32(1, 100)
        Select Case i
            Case 1 To 82
                Return 22
            Case 83 To 92
                Return 21
            Case 93 To 96
                Return 23
            Case 97
                Return 20
            Case 98 To 99
                Return 24
            Case 100
                Return 25
        End Select
    End Function
    'Generates the Weight by position
    Private Function GetWeight(ByVal Pos As String, ByVal Height As Integer) As Integer
        Select Case Pos
            Case "QB" : Return CInt(MT.GetGaussian(3.0, 0.16) * Height)
            Case "RB" : Return CInt(MT.GetGaussian(3.07, 0.16) * Height)
            Case "FB" : Return CInt(MT.GetGaussian(3.39, 0.13) * Height)
            Case "WR" : Return CInt(MT.GetGaussian(2.74, 0.15) * Height)
            Case "TE" : Return CInt(MT.GetGaussian(3.37, 0.14) * Height)
            Case "OT" : Return CInt(MT.GetGaussian(4.07, 0.17) * Height)
            Case "OG" : Return CInt(MT.GetGaussian(4.13, 0.18) * Height)
            Case "C" : Return CInt(MT.GetGaussian(4.01, 0.16) * Height)
            Case "DE" : Return CInt(MT.GetGaussian(3.61, 0.22) * Height)
            Case "DT" : Return CInt(MT.GetGaussian(4.09, 0.22) * Height)
            Case "NT" : Return CInt(MT.GetGaussian(4.29, 0.27) * Height)
            Case "LB" : Return CInt(MT.GetGaussian(3.26, 0.14) * Height)
            Case "OLB" : Return CInt(MT.GetGaussian(3.29, 0.15) * Height)
            Case "ILB" : Return CInt(MT.GetGaussian(3.3, 0.11) * Height)
            Case "CB" : Return CInt(MT.GetGaussian(2.7, 0.11) * Height)
            Case "DB" : Return CInt(MT.GetGaussian(2.77, 0.15) * Height)
            Case "SS" : Return CInt(MT.GetGaussian(2.92, 0.06) * Height)
            Case "FS" : Return CInt(MT.GetGaussian(2.83, 0.11) * Height)
            Case "K" : Return CInt(MT.GetGaussian(2.81, 0.21) * Height)
            Case "P" : Return CInt(MT.GetGaussian(2.9, 0.21) * Height)
        End Select

    End Function
    'Generates the Height By Position
    Private Function GetHeight(ByVal Pos As String) As Integer
        Select Case Pos
            Case "QB"
                Select Case MT.GenerateInt32(1, 141)
                    Case 1 : Return 68
                    Case 2 : Return 69
                    Case 3 : Return 70
                    Case 4 : Return 71
                    Case 5 To 12 : Return 72
                    Case 13 To 29 : Return 73
                    Case 30 To 68 : Return 74
                    Case 69 To 93 : Return 75
                    Case 94 To 119 : Return 76
                    Case 120 To 138 : Return 77
                    Case 139 To 141 : Return 78
                End Select
            Case "RB"
                Select Case MT.GenerateInt32(1, 183)
                    Case 1 To 2 : Return 66
                    Case 3 To 6 : Return 67
                    Case 7 To 14 : Return 68
                    Case 15 To 41 : Return 69
                    Case 42 To 88 : Return 70
                    Case 89 To 125 : Return 71
                    Case 126 To 155 : Return 72
                    Case 156 To 176 : Return 73
                    Case 177 To 181 : Return 74
                    Case 182 : Return 75
                    Case 183 : Return 76
                End Select
            Case "FB"
                Select Case MT.GenerateInt32(1, 92)
                    Case 1 : Return 69
                    Case 2 To 6 : Return 70
                    Case 7 To 25 : Return 71
                    Case 26 To 54 : Return 72
                    Case 55 To 70 : Return 73
                    Case 71 To 84 : Return 74
                    Case 85 To 88 : Return 75
                    Case 89 To 92 : Return 76
                End Select
            Case "WR"
                Select Case MT.GenerateInt32(1, 375)
                    Case 1 : Return 67
                    Case 2 To 11 : Return 68
                    Case 12 To 27 : Return 69
                    Case 28 To 65 : Return 70
                    Case 66 To 112 : Return 71
                    Case 113 To 174 : Return 72
                    Case 175 To 233 : Return 73
                    Case 234 To 286 : Return 74
                    Case 287 To 329 : Return 75
                    Case 330 To 358 : Return 76
                    Case 359 To 370 : Return 77
                    Case 371 To 375 : Return 78
                End Select
            Case "TE"
                Select Case MT.GenerateInt32(1, 182)
                    Case 1 : Return 72
                    Case 2 To 5 : Return 73
                    Case 6 To 16 : Return 74
                    Case 17 To 52 : Return 75
                    Case 53 To 104 : Return 76
                    Case 105 To 147 : Return 77
                    Case 148 To 170 : Return 78
                    Case 171 To 177 : Return 79
                    Case 178 To 182 : Return 80
                End Select
            Case "OT"
                Select Case MT.GenerateInt32(1, 213)
                    Case 1 : Return 74
                    Case 2 To 14 : Return 75
                    Case 15 To 54 : Return 76
                    Case 55 To 106 : Return 77
                    Case 107 To 158 : Return 78
                    Case 159 To 195 : Return 79
                    Case 196 To 210 : Return 80
                    Case 211 To 213 : Return 81
                End Select
            Case "OG"
                Select Case MT.GenerateInt32(1, 183)
                    Case 1 : Return 73
                    Case 2 To 21 : Return 74
                    Case 22 To 69 : Return 75
                    Case 70 To 125 : Return 76
                    Case 126 To 162 : Return 77
                    Case 163 To 175 : Return 78
                    Case 176 To 182 : Return 79
                    Case 183 : Return 80
                End Select
            Case "C"
                Select Case MT.GenerateInt32(1, 82)
                    Case 1 : Return 72
                    Case 2 To 5 : Return 73
                    Case 6 To 21 : Return 74
                    Case 22 To 48 : Return 75
                    Case 49 To 70 : Return 76
                    Case 71 To 81 : Return 77
                    Case 82 : Return 78
                End Select
            Case "DE"
                Select Case MT.GenerateInt32(1, 231)
                    Case 1 : Return 71
                    Case 2 : Return 72
                    Case 3 To 10 : Return 73
                    Case 11 To 42 : Return 74
                    Case 43 To 98 : Return 75
                    Case 99 To 159 : Return 76
                    Case 160 To 202 : Return 77
                    Case 203 To 225 : Return 78
                    Case 226 To 231 : Return 79
                End Select
            Case "DT"
                Select Case MT.GenerateInt32(1, 194)
                    Case 1 To 2 : Return 71
                    Case 3 To 13 : Return 72
                    Case 14 To 34 : Return 73
                    Case 35 To 77 : Return 74
                    Case 78 To 132 : Return 75
                    Case 133 To 167 : Return 76
                    Case 168 To 183 : Return 77
                    Case 184 To 191 : Return 78
                    Case 192 To 194 : Return 79
                End Select
            Case "NT"
                Select Case MT.GenerateInt32(1, 26)
                    Case 1 To 3 : Return 72
                    Case 4 To 8 : Return 73
                    Case 9 To 16 : Return 74
                    Case 17 To 20 : Return 75
                    Case 21 To 24 : Return 76
                    Case 25 : Return 77
                    Case 26 : Return 78
                End Select
            Case "LB"
                Select Case MT.GenerateInt32(1, 227)
                    Case 1 : Return 69
                    Case 2 To 6 : Return 70
                    Case 7 To 14 : Return 71
                    Case 9 To 51 : Return 72
                    Case 52 To 111 : Return 73
                    Case 112 To 161 : Return 74
                    Case 162 To 209 : Return 75
                    Case 210 To 218 : Return 76
                    Case 219 To 226 : Return 77
                    Case 227 : Return 78
                End Select
            Case "OLB"
                Select Case MT.GenerateInt32(1, 83)
                    Case 1 To 3 : Return 71
                    Case 4 To 19 : Return 72
                    Case 20 To 30 : Return 73
                    Case 31 To 46 : Return 74
                    Case 47 To 66 : Return 75
                    Case 67 To 77 : Return 76
                    Case 78 To 82 : Return 77
                    Case 83 : Return 78
                End Select
            Case "ILB"
                Select Case MT.GenerateInt32(1, 48)
                    Case 1 : Return 70
                    Case 2 To 3 : Return 71
                    Case 4 To 8 : Return 72
                    Case 9 To 25 : Return 73
                    Case 26 To 42 : Return 74
                    Case 43 To 46 : Return 75
                    Case 47 To 48 : Return 76
                End Select
            Case "CB"
                Select Case MT.GenerateInt32(1, 173)
                    Case 1 To 5 : Return 68
                    Case 6 To 21 : Return 69
                    Case 22 To 66 : Return 70
                    Case 67 To 109 : Return 71
                    Case 110 To 140 : Return 72
                    Case 141 To 166 : Return 73
                    Case 167 To 171 : Return 74
                    Case 172 : Return 75
                    Case 173 : Return 76
                End Select
            Case "DB"
                Select Case MT.GenerateInt32(1, 222)
                    Case 1 To 4 : Return 68
                    Case 5 To 19 : Return 69
                    Case 20 To 58 : Return 70
                    Case 59 To 104 : Return 71
                    Case 105 To 139 : Return 72
                    Case 140 To 188 : Return 73
                    Case 189 To 210 : Return 74
                    Case 211 To 222 : Return 75
                End Select
            Case "SS"
                Select Case MT.GenerateInt32(1, 47)
                    Case 1 : Return 68
                    Case 2 : Return 69
                    Case 3 To 8 : Return 70
                    Case 9 To 13 : Return 71
                    Case 14 To 29 : Return 72
                    Case 30 To 40 : Return 73
                    Case 41 To 45 : Return 74
                    Case 46 To 47 : Return 75
                End Select
            Case "FS"
                Select Case MT.GenerateInt32(1, 49)
                    Case 1 : Return 68
                    Case 2 : Return 69
                    Case 3 To 7 : Return 70
                    Case 8 To 21 : Return 71
                    Case 22 To 30 : Return 72
                    Case 31 To 36 : Return 73
                    Case 37 To 46 : Return 74
                    Case 47 : Return 75
                    Case 48 : Return 76
                    Case 49 : Return 77
                End Select
            Case "K"
                Select Case MT.GenerateInt32(1, 53)
                    Case 1 : Return 68
                    Case 2 To 4 : Return 69
                    Case 5 To 10 : Return 70
                    Case 11 To 19 : Return 71
                    Case 20 To 33 : Return 72
                    Case 34 To 42 : Return 73
                    Case 43 To 49 : Return 74
                    Case 50 To 51 : Return 75
                    Case 52 : Return 76
                    Case 53 : Return 77
                End Select
            Case "P"
                Select Case MT.GenerateInt32(1, 63)
                    Case 1 : Return 68
                    Case 2 : Return 69
                    Case 3 To 5 : Return 70
                    Case 6 To 8 : Return 71
                    Case 9 To 16 : Return 72
                    Case 17 To 27 : Return 73
                    Case 28 To 40 : Return 74
                    Case 41 To 50 : Return 75
                    Case 51 To 56 : Return 76
                    Case 57 To 61 : Return 77
                    Case 62 : Return 78
                    Case 63 : Return 79
                End Select
        End Select
    End Function
    ''' <summary>
    ''' An equation exists to get predicted HandLength in CM when you know a male's height:
    ''' avg HL(cm)=(Height(cm)-80.4+(.195 x age in years) - 6.383)/5.122
    ''' STDev was 4.4cm of their height and since average height is roughly around 9.08 times your hand length, I took roughly 4.4/9, and reduced
    ''' it slightly because NFL players seemingly have larger hands for their bodies
    ''' </summary>
    ''' <param name="Height"></param>
    ''' <param name="Age"></param>
    ''' <returns></returns>
    Private Function GetHandLength(Height As Integer, Age As Integer) As Double

        Dim HeightinCM As Double = (Height * 2.54) 'converts to cm
        Dim HLinCM As Double = (HeightinCM - 80.4 + (0.195 * Age) - 6.383) / 5.122 'gets avg HL in cm for that height
        HLinCM /= 2.54
        Return (Math.Round(MT.GetGaussian(HLinCM, 0.454), 2) + 0.75)

    End Function
    ''' <summary>
    ''' Arm Length is on average A person's height * 0.45
    ''' </summary>
    ''' <param name="Height"></param>
    ''' <returns></returns>
    Private Function GetArmLength(Height As Integer) As Double
        Return Math.Round(MT.GetGaussian((Height * 0.435), 0.55), 2)
    End Function

End Class
