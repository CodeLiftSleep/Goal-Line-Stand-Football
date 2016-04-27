﻿Imports System.Net.Configuration
Imports System.Windows.Forms
Imports Generation

Class GenerationDraft
    Dim NumPlayers As Integer
    Shared MyDraft As New Generation.CollegePlayers
    Friend WithEvents ButtonPush As Controls.TextBox
    'Public Shared ReadOnly ButtonPush As RoutedEvent = MouseDownEvent.AddOwner(Button_MouseDown)
    Dim MyFilePath As String = ""
    Dim MyMsgBox As Windows.MessageBox '
    Dim ex As Exception
    Dim GenDraftPage2 As New GenerationDraft2
    Dim DraftPlayersTable As New DraftPlayersTable

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        '[AddHandler](MouseDownEvent, New MouseButtonEventHandler(AddressOf Button_MouseDown), True)
        ' Add any initialization after the InitializeComponent() call.
        CreateDraftTextBox.AddHandler(UIElement.PreviewMouseDownEvent, New MouseButtonEventHandler(AddressOf Button_MouseDown), True)

    End Sub
    Private Sub CreateMsgBox()
        MsgBox("Please Select A DataBase To Use Before Continuing", MsgBoxStyle.Exclamation, "Must Select DataBase!")
    End Sub

    Private Sub CreateDraft_Click(sender As Object, e As RoutedEventArgs) Handles CreateDraft.Click
        'Create the draft class based on the number of players specified

        If MyFilePath = "" Then
            CreateMsgBox()
            Exit Sub
        End If

        If IsNumeric(CreateDraftTextBox.Text) Then
            NumPlayers = CInt(CreateDraftTextBox.Text)
        Else
            NumPlayers = 1000
        End If

        MyDraft.Initialize("Football", DraftDT, "DraftPlayers", MyDraft.GetSQLString("College"), MyFilePath)

        MyDraft.GenDraftClass(DraftClass, DraftClassDesc)
        DepthPositions.Text = ""
        DepthPositions.Inlines.Add(String.Format("QB Depth: {0}", DraftClassDesc(0)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("RB Depth: {0}", DraftClassDesc(6)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("FB Depth: {0}", DraftClassDesc(2)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("WR Depth: {0}", DraftClassDesc(3)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("TE Depth: {0}", DraftClassDesc(4)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("OT Depth: {0}", DraftClassDesc(5)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("C Depth: {0}", DraftClassDesc(6)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("OG Depth: {0}", DraftClassDesc(7)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("DE Depth: {0}", DraftClassDesc(8)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("DT Depth: {0}", DraftClassDesc(9)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("OLB Depth: {0}", DraftClassDesc(10)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("ILB Depth: {0}", DraftClassDesc(16)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("CB Depth: {0}", DraftClassDesc(12)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("FS Depth: {0}", DraftClassDesc(13)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("SS Depth: {0}", DraftClassDesc(14)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("K Depth: {0}", DraftClassDesc(15)))
        DepthPositions.Inlines.Add(New LineBreak)
        DepthPositions.Inlines.Add(String.Format("P Depth: {0}", DraftClassDesc(16)))

        'GenDraftPanel.UpdateLayout()

        For i As Integer = 1 To NumPlayers
            MyDraft.GenDraftPlayers(i, MyDraft, DraftDT, DraftClass)
        Next i
        DraftDT.Rows.RemoveAt(0)
        GetStats()

        MyDraft.Update("Football", DraftDT, "DraftPlayers", MyFilePath)
        DraftGrid.DataContext = DraftDT.DefaultView

    End Sub

    Private Sub CreateDraftTextBox_TextInput(sender As Object, e As TextCompositionEventArgs)
        CreateDraftTextBoxInput()
    End Sub

    Private Sub CreateDraftTextBoxInput()
        NumPlayers = CreateDraftTextBox.Text
    End Sub
    Protected Sub Button_MouseDown(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles ButtonPush.PreviewMouseDown
        CreateDraftTextBox.Text = ""
    End Sub
    Private Sub OpenDB_Click(sender As Object, e As RoutedEventArgs) Handles OpenDB.Click 'Allows user to choose which DB to open
        Dim MyOFD As New OpenFileDialog
        MyOFD.Filter = "SQLite DataBase Files (*.sqlite)|*.sqlite"

        If MyOFD.ShowDialog = DialogResult.OK Then
            MyFilePath = MyOFD.FileName
            MyFilePath = (Replace(MyFilePath, "Football.sqlite", ""))
        End If

    End Sub

    Private Sub GetStats()
        Dim result As New List(Of Object)

        QBStats.Text = ""
        RBStats.Text = ""
        FindStats("'QB'", result)
        QBStats.Inlines.Add(String.Format("Total Number of QB's Created: {0}", result(0)))
        QBStats.Inlines.Add(New LineBreak)
        QBStats.Inlines.Add(String.Format("Average QB Height: {0} inches tall", result(1)))
        QBStats.Inlines.Add(New LineBreak)
        QBStats.Inlines.Add(String.Format("Average QB Weight: {0} pounds", result(2)))
        QBStats.Inlines.Add(New LineBreak)
        QBStats.Inlines.Add(String.Format("Average QB ArmLength: {0} inches long", result(3)))
        QBStats.Inlines.Add(New LineBreak)
        QBStats.Inlines.Add(String.Format("Average QB HandSize: {0} inches long", result(4)))
        QBStats.Inlines.Add(New LineBreak)
        QBStats.Inlines.Add(String.Format("Average QB Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'RB'", result)
        RBStats.Inlines.Add(String.Format("Total Number of RB's Created: {0}", result(0)))
        RBStats.Inlines.Add(New LineBreak)
        RBStats.Inlines.Add(New Run(String.Format("Average RB Height: {0} inches tall", result(1))))
        RBStats.Inlines.Add(New LineBreak)
        RBStats.Inlines.Add(String.Format("Average RB Weight: {0} pounds", result(2)))
        RBStats.Inlines.Add(New LineBreak)
        RBStats.Inlines.Add(String.Format("Average RB ArmLength: {0} inches long", result(3)))
        RBStats.Inlines.Add(New LineBreak)
        RBStats.Inlines.Add(String.Format("Average RB HandSize: {0} inches long", result(4)))
        RBStats.Inlines.Add(New LineBreak)
        RBStats.Inlines.Add(String.Format("Average RB Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'FB'", result)
        FBStats.Inlines.Add(String.Format("Total Number of FB's Created: {0}", result(0)))
        FBStats.Inlines.Add(New LineBreak)
        FBStats.Inlines.Add(String.Format("Average FB Height: {0} inches tall", result(1)))
        FBStats.Inlines.Add(New LineBreak)
        FBStats.Inlines.Add(String.Format("Average FB Weight: {0} pounds", result(2)))
        FBStats.Inlines.Add(New LineBreak)
        FBStats.Inlines.Add(String.Format("Average FB ArmLength: {0} inches long", result(3)))
        FBStats.Inlines.Add(New LineBreak)
        FBStats.Inlines.Add(String.Format("Average FB HandSize: {0} inches long", result(4)))
        FBStats.Inlines.Add(New LineBreak)
        FBStats.Inlines.Add(String.Format("Average FB Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'WR'", result)
        WRStats.Inlines.Add(String.Format("Total Number of WR's Created: {0}", result(0)))
        WRStats.Inlines.Add(New LineBreak)
        WRStats.Inlines.Add(String.Format("Average WR Height: {0} inches tall", result(1)))
        WRStats.Inlines.Add(New LineBreak)
        WRStats.Inlines.Add(String.Format("Average WR Weight: {0} pounds", result(2)))
        WRStats.Inlines.Add(New LineBreak)
        WRStats.Inlines.Add(String.Format("Average WR ArmLength: {0} inches long", result(3)))
        WRStats.Inlines.Add(New LineBreak)
        WRStats.Inlines.Add(String.Format("Average WR HandSize: {0} inches long", result(4)))
        WRStats.Inlines.Add(New LineBreak)
        WRStats.Inlines.Add(String.Format("Average WR Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'TE'", result)
        TEStats.Inlines.Add(String.Format("Total Number of TE's Created: {0}", result(0)))
        TEStats.Inlines.Add(New LineBreak)
        TEStats.Inlines.Add(String.Format("Average TE Height: {0} inches tall", result(1)))
        TEStats.Inlines.Add(New LineBreak)
        TEStats.Inlines.Add(String.Format("Average TE Weight: {0} pounds", result(2)))
        TEStats.Inlines.Add(New LineBreak)
        TEStats.Inlines.Add(String.Format("Average TE ArmLength: {0} inches long", result(3)))
        TEStats.Inlines.Add(New LineBreak)
        TEStats.Inlines.Add(String.Format("Average TE HandSize: {0} inches long", result(4)))
        TEStats.Inlines.Add(New LineBreak)
        TEStats.Inlines.Add(String.Format("Average TE Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'OT'", result)
        OTStats.Inlines.Add(String.Format("Total Number of OT's Created: {0}", result(0)))
        OTStats.Inlines.Add(New LineBreak)
        OTStats.Inlines.Add(String.Format("Average OT Height: {0} inches tall", result(1)))
        OTStats.Inlines.Add(New LineBreak)
        OTStats.Inlines.Add(String.Format("Average OT Weight: {0} pounds", result(2)))
        OTStats.Inlines.Add(New LineBreak)
        OTStats.Inlines.Add(String.Format("Average OT ArmLength: {0} inches long", result(3)))
        OTStats.Inlines.Add(New LineBreak)
        OTStats.Inlines.Add(String.Format("Average OT HandSize: {0} inches long", result(4)))
        OTStats.Inlines.Add(New LineBreak)
        OTStats.Inlines.Add(String.Format("Average OT Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'C'", result)
        CStats.Inlines.Add(String.Format("Total Number of C's Created: {0}", result(0)))
        CStats.Inlines.Add(New LineBreak)
        CStats.Inlines.Add(String.Format("Average C Height: {0} inches tall", result(1)))
        CStats.Inlines.Add(New LineBreak)
        CStats.Inlines.Add(String.Format("Average C Weight: {0} pounds", result(2)))
        CStats.Inlines.Add(New LineBreak)
        CStats.Inlines.Add(String.Format("Average C ArmLength: {0} inches long", result(3)))
        CStats.Inlines.Add(New LineBreak)
        CStats.Inlines.Add(String.Format("Average C HandSize: {0} inches long", result(4)))
        CStats.Inlines.Add(New LineBreak)
        CStats.Inlines.Add(String.Format("Average C Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'OG'", result)
        OGStats.Inlines.Add(String.Format("Total Number of OG's Created: {0}", result(0)))
        OGStats.Inlines.Add(New LineBreak)
        OGStats.Inlines.Add(String.Format("Average OG Height: {0} inches tall", result(1)))
        OGStats.Inlines.Add(New LineBreak)
        OGStats.Inlines.Add(String.Format("Average OG Weight: {0} pounds", result(2)))
        OGStats.Inlines.Add(New LineBreak)
        OGStats.Inlines.Add(String.Format("Average OG ArmLength: {0} inches long", result(3)))
        OGStats.Inlines.Add(New LineBreak)
        OGStats.Inlines.Add(String.Format("Average OG HandSize: {0} inches long", result(4)))
        OGStats.Inlines.Add(New LineBreak)
        OGStats.Inlines.Add(String.Format("Average OG Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'DE'", result)
        DEStats.Inlines.Add(String.Format("Total Number of DE's Created: {0}", result(0)))
        DEStats.Inlines.Add(New LineBreak)
        DEStats.Inlines.Add(String.Format("Average DE Height: {0} inches tall", result(1)))
        DEStats.Inlines.Add(New LineBreak)
        DEStats.Inlines.Add(String.Format("Average DE Weight: {0} pounds", result(2)))
        DEStats.Inlines.Add(New LineBreak)
        DEStats.Inlines.Add(String.Format("Average DE ArmLength: {0} inches long", result(3)))
        DEStats.Inlines.Add(New LineBreak)
        DEStats.Inlines.Add(String.Format("Average DE HandSize: {0} inches long", result(4)))
        DEStats.Inlines.Add(New LineBreak)
        DEStats.Inlines.Add(String.Format("Average DE Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'DT'", result)
        DTStats.Inlines.Add(String.Format("Total Number of DT's Created: {0}", result(0)))
        DTStats.Inlines.Add(New LineBreak)
        DTStats.Inlines.Add(String.Format("Average DT Height: {0} inches tall", result(1)))
        DTStats.Inlines.Add(New LineBreak)
        DTStats.Inlines.Add(String.Format("Average DT Weight: {0} pounds", result(2)))
        DTStats.Inlines.Add(New LineBreak)
        DTStats.Inlines.Add(String.Format("Average DT ArmLength: {0} inches long", result(3)))
        DTStats.Inlines.Add(New LineBreak)
        DTStats.Inlines.Add(String.Format("Average DT HandSize: {0} inches long", result(4)))
        DTStats.Inlines.Add(New LineBreak)
        DTStats.Inlines.Add(String.Format("Average DT Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'OLB'", result)
        OLBStats.Inlines.Add(String.Format("Total Number of OLB's Created: {0}", result(0)))
        OLBStats.Inlines.Add(New LineBreak)
        OLBStats.Inlines.Add(String.Format("Average OLB Height: {0} inches tall", result(1)))
        OLBStats.Inlines.Add(New LineBreak)
        OLBStats.Inlines.Add(String.Format("Average OLB Weight: {0} pounds", result(2)))
        OLBStats.Inlines.Add(New LineBreak)
        OLBStats.Inlines.Add(String.Format("Average OLB ArmLength: {0} inches long", result(3)))
        OLBStats.Inlines.Add(New LineBreak)
        OLBStats.Inlines.Add(String.Format("Average OLB HandSize: {0} inches long", result(4)))
        OLBStats.Inlines.Add(New LineBreak)
        OLBStats.Inlines.Add(String.Format("Average OLB Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'ILB'", result)
        ILBStats.Inlines.Add(String.Format("Total Number of ILB's Created: {0}", result(0)))
        ILBStats.Inlines.Add(New LineBreak)
        ILBStats.Inlines.Add(String.Format("Average ILB Height: {0} inches tall", result(1)))
        ILBStats.Inlines.Add(New LineBreak)
        ILBStats.Inlines.Add(String.Format("Average ILB Weight: {0} pounds", result(2)))
        ILBStats.Inlines.Add(New LineBreak)
        ILBStats.Inlines.Add(String.Format("Average ILB ArmLength: {0} inches long", result(3)))
        ILBStats.Inlines.Add(New LineBreak)
        ILBStats.Inlines.Add(String.Format("Average ILB HandSize: {0} inches long", result(4)))
        ILBStats.Inlines.Add(New LineBreak)
        ILBStats.Inlines.Add(String.Format("Average ILB Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'CB'", result)
        CBStats.Inlines.Add(String.Format("Total Number of CB's Created: {0}", result(0)))
        CBStats.Inlines.Add(New LineBreak)
        CBStats.Inlines.Add(String.Format("Average CB Height: {0} inches tall", result(1)))
        CBStats.Inlines.Add(New LineBreak)
        CBStats.Inlines.Add(String.Format("Average CB Weight: {0} pounds", result(2)))
        CBStats.Inlines.Add(New LineBreak)
        CBStats.Inlines.Add(String.Format("Average CB ArmLength: {0} inches long", result(3)))
        CBStats.Inlines.Add(New LineBreak)
        CBStats.Inlines.Add(String.Format("Average CB HandSize: {0} inches long", result(4)))
        CBStats.Inlines.Add(New LineBreak)
        CBStats.Inlines.Add(String.Format("Average CB Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'FS'", result)
        FSStats.Inlines.Add(String.Format("Total Number of FS's Created: {0}", result(0)))
        FSStats.Inlines.Add(New LineBreak)
        FSStats.Inlines.Add(String.Format("Average FS Height: {0} inches tall", result(1)))
        FSStats.Inlines.Add(New LineBreak)
        FSStats.Inlines.Add(String.Format("Average FS Weight: {0} pounds", result(2)))
        FSStats.Inlines.Add(New LineBreak)
        FSStats.Inlines.Add(String.Format("Average FS ArmLength: {0} inches long", result(3)))
        FSStats.Inlines.Add(New LineBreak)
        FSStats.Inlines.Add(String.Format("Average FS HandSize: {0} inches long", result(4)))
        FSStats.Inlines.Add(New LineBreak)
        FSStats.Inlines.Add(String.Format("Average FS Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'SS'", result)
        SSStats.Inlines.Add(String.Format("Total Number of SS's Created: {0}", result(0)))
        SSStats.Inlines.Add(New LineBreak)
        SSStats.Inlines.Add(String.Format("Average SS Height: {0} inches tall", result(1)))
        SSStats.Inlines.Add(New LineBreak)
        SSStats.Inlines.Add(String.Format("Average SS Weight: {0} pounds", result(2)))
        SSStats.Inlines.Add(New LineBreak)
        SSStats.Inlines.Add(String.Format("Average SS ArmLength: {0} inches long", result(3)))
        SSStats.Inlines.Add(New LineBreak)
        SSStats.Inlines.Add(String.Format("Average SS HandSize: {0} inches long", result(4)))
        SSStats.Inlines.Add(New LineBreak)
        SSStats.Inlines.Add(String.Format("Average SS Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()


        FindStats("'K'", result)
        KStats.Inlines.Add(String.Format("Total Number of K's Created: {0}", result(0)))
        KStats.Inlines.Add(New LineBreak)
        KStats.Inlines.Add(String.Format("Average K Height: {0} inches tall", result(1)))
        KStats.Inlines.Add(New LineBreak)
        KStats.Inlines.Add(String.Format("Average K Weight: {0} pounds", result(2)))
        KStats.Inlines.Add(New LineBreak)
        KStats.Inlines.Add(String.Format("Average K ArmLength: {0} inches long", result(3)))
        KStats.Inlines.Add(New LineBreak)
        KStats.Inlines.Add(String.Format("Average K HandSize: {0} inches long", result(4)))
        KStats.Inlines.Add(New LineBreak)
        KStats.Inlines.Add(String.Format("Average K Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()

        FindStats("'P'", result)
        PStats.Inlines.Add(String.Format("Total Number of P's Created: {0}", result(0)))
        PStats.Inlines.Add(New LineBreak)
        PStats.Inlines.Add(String.Format("Average P Height: {0} inches tall", result(1)))
        PStats.Inlines.Add(New LineBreak)
        PStats.Inlines.Add(String.Format("Average P Weight: {0} pounds", result(2)))
        PStats.Inlines.Add(New LineBreak)
        PStats.Inlines.Add(String.Format("Average P ArmLength: {0} inches long", result(3)))
        PStats.Inlines.Add(New LineBreak)
        PStats.Inlines.Add(String.Format("Average P HandSize: {0} inches long", result(4)))
        PStats.Inlines.Add(New LineBreak)
        PStats.Inlines.Add(String.Format("Average P Forty Yard Dash Time: {0} seconds", result(5)))
        result.Clear()
    End Sub
    ''' <summary>
    ''' Cycles through the DT and gets stats for each position
    ''' </summary>
    ''' <param name="Pos"></param>
    Private Function FindStats(ByVal Pos As String, ByVal MyList As List(Of Object)) As List(Of Object)

        Dim Height As Single
        Dim Weight As Single
        Dim ArmLength As Single
        Dim HandSize As Single
        Dim Count As Integer
        Dim FortyTime As Single
        For i As Integer = 1 To DraftDT.Rows.Count - 1
            If DraftDT.Rows(i).Item("CollegePOS") = Pos Then
                Count += 1
                Height += DraftDT.Rows(i).Item("Height")
                Weight += DraftDT.Rows(i).Item("Weight")
                ArmLength += DraftDT.Rows(i).Item("ArmLength")
                HandSize += DraftDT.Rows(i).Item("HandLength")
                FortyTime += DraftDT.Rows(i).Item("FortyYardTime")
            End If
        Next i
        Height = Math.Round((Height / Count), 2)
        Weight = Math.Round((Weight / Count), 2)
        ArmLength = Math.Round((ArmLength / Count), 2)
        HandSize = Math.Round((HandSize / Count), 2)
        FortyTime = Math.Round((FortyTime / Count), 2)
        MyList.Add(Count)
        MyList.Add(Height)
        MyList.Add(Weight)
        MyList.Add(ArmLength)
        MyList.Add(HandSize)
        MyList.Add(FortyTime)
        Return MyList
    End Function

    Private Sub NextPage_Click(sender As Object, e As RoutedEventArgs) Handles NextPage.Click
        GenDraftPage2.QBInfo1.Text = MyDraft.PosCount(1, 1)
        GenDraftPage2.QBInfo2.Text = MyDraft.PosCount(2, 1)
        GenDraftPage2.QBInfo3.Text = MyDraft.PosCount(3, 1)
        GenDraftPage2.QBInfo4.Text = MyDraft.PosCount(4, 1)
        GenDraftPage2.QBInfo5.Text = MyDraft.PosCount(5, 1)
        GenDraftPage2.QBInfo6.Text = MyDraft.PosCount(6, 1)
        GenDraftPage2.QBInfo7.Text = MyDraft.PosCount(7, 1)
        GenDraftPage2.QBInfo8.Text = MyDraft.PosCount(8, 1)
        GenDraftPage2.QBInfo9.Text = MyDraft.PosCount(9, 1)
        GenDraftPage2.QBInfo10.Text = MyDraft.PosCount(10, 1)
        GenDraftPage2.QBInfo11.Text = MyDraft.PosCount(11, 1)
        GenDraftPage2.QBInfo12.Text = MyDraft.PosCount(12, 1)
        GenDraftPage2.QBInfo13.Text = MyDraft.PosCount(13, 1)
        GenDraftPage2.QBInfo14.Text = MyDraft.PosCount(14, 1)
        GenDraftPage2.QBTotal.Text = MyDraft.PosCount(1, 1) + MyDraft.PosCount(2, 1) + MyDraft.PosCount(3, 1) + MyDraft.PosCount(4, 1) + MyDraft.PosCount(5, 1) + MyDraft.PosCount(6, 1) + MyDraft.PosCount(7, 1) + MyDraft.PosCount(8, 1) + MyDraft.PosCount(9, 1) +
            MyDraft.PosCount(10, 1) + MyDraft.PosCount(11, 1) + MyDraft.PosCount(12, 1) + MyDraft.PosCount(13, 1) + MyDraft.PosCount(14, 1)
        GenDraftPage2.RBInfo1.Text = MyDraft.PosCount(1, 2)
        GenDraftPage2.RBInfo2.Text = MyDraft.PosCount(2, 2)
        GenDraftPage2.RBInfo3.Text = MyDraft.PosCount(3, 2)
        GenDraftPage2.RBInfo4.Text = MyDraft.PosCount(4, 2)
        GenDraftPage2.RBInfo5.Text = MyDraft.PosCount(5, 2)
        GenDraftPage2.RBInfo6.Text = MyDraft.PosCount(6, 2)
        GenDraftPage2.RBInfo7.Text = MyDraft.PosCount(7, 2)
        GenDraftPage2.RBInfo8.Text = MyDraft.PosCount(8, 2)
        GenDraftPage2.RBInfo9.Text = MyDraft.PosCount(9, 2)
        GenDraftPage2.RBInfo10.Text = MyDraft.PosCount(10, 2)
        GenDraftPage2.RBInfo11.Text = MyDraft.PosCount(11, 2)
        GenDraftPage2.RBInfo12.Text = MyDraft.PosCount(12, 2)
        GenDraftPage2.RBInfo13.Text = MyDraft.PosCount(13, 2)
        GenDraftPage2.RBInfo14.Text = MyDraft.PosCount(14, 2)
        GenDraftPage2.RBTotal.Text = MyDraft.PosCount(1, 2) + MyDraft.PosCount(2, 2) + MyDraft.PosCount(3, 2) + MyDraft.PosCount(4, 2) + MyDraft.PosCount(5, 2) + MyDraft.PosCount(6, 2) + MyDraft.PosCount(7, 2) + MyDraft.PosCount(8, 2) + MyDraft.PosCount(9, 2) +
            MyDraft.PosCount(10, 2) + MyDraft.PosCount(11, 2) + MyDraft.PosCount(12, 2) + MyDraft.PosCount(13, 2) + MyDraft.PosCount(14, 2)
        GenDraftPage2.FBInfo1.Text = MyDraft.PosCount(1, 3)
        GenDraftPage2.FBInfo2.Text = MyDraft.PosCount(2, 3)
        GenDraftPage2.FBInfo3.Text = MyDraft.PosCount(3, 3)
        GenDraftPage2.FBInfo4.Text = MyDraft.PosCount(4, 3)
        GenDraftPage2.FBInfo5.Text = MyDraft.PosCount(5, 3)
        GenDraftPage2.FBInfo6.Text = MyDraft.PosCount(6, 3)
        GenDraftPage2.FBInfo7.Text = MyDraft.PosCount(7, 3)
        GenDraftPage2.FBInfo8.Text = MyDraft.PosCount(8, 3)
        GenDraftPage2.FBInfo9.Text = MyDraft.PosCount(9, 3)
        GenDraftPage2.FBInfo10.Text = MyDraft.PosCount(10, 3)
        GenDraftPage2.FBInfo11.Text = MyDraft.PosCount(11, 3)
        GenDraftPage2.FBInfo12.Text = MyDraft.PosCount(12, 3)
        GenDraftPage2.FBInfo13.Text = MyDraft.PosCount(13, 3)
        GenDraftPage2.FBInfo14.Text = MyDraft.PosCount(14, 3)
        GenDraftPage2.FBTotal.Text = MyDraft.PosCount(1, 3) + MyDraft.PosCount(2, 3) + MyDraft.PosCount(3, 3) + MyDraft.PosCount(4, 3) + MyDraft.PosCount(5, 3) + MyDraft.PosCount(6, 3) + MyDraft.PosCount(7, 3) + MyDraft.PosCount(8, 3) + MyDraft.PosCount(9, 3) +
            MyDraft.PosCount(10, 3) + MyDraft.PosCount(11, 3) + MyDraft.PosCount(12, 3) + MyDraft.PosCount(13, 3) + MyDraft.PosCount(14, 3)
        GenDraftPage2.WRInfo1.Text = MyDraft.PosCount(1, 4)
        GenDraftPage2.WRInfo2.Text = MyDraft.PosCount(2, 4)
        GenDraftPage2.WRInfo3.Text = MyDraft.PosCount(3, 4)
        GenDraftPage2.WRInfo4.Text = MyDraft.PosCount(4, 4)
        GenDraftPage2.WRInfo5.Text = MyDraft.PosCount(5, 4)
        GenDraftPage2.WRInfo6.Text = MyDraft.PosCount(6, 4)
        GenDraftPage2.WRInfo7.Text = MyDraft.PosCount(7, 4)
        GenDraftPage2.WRInfo8.Text = MyDraft.PosCount(8, 4)
        GenDraftPage2.WRInfo9.Text = MyDraft.PosCount(9, 4)
        GenDraftPage2.WRInfo10.Text = MyDraft.PosCount(10, 4)
        GenDraftPage2.WRInfo11.Text = MyDraft.PosCount(11, 4)
        GenDraftPage2.WRInfo12.Text = MyDraft.PosCount(12, 4)
        GenDraftPage2.WRInfo13.Text = MyDraft.PosCount(13, 4)
        GenDraftPage2.WRInfo14.Text = MyDraft.PosCount(14, 4)
        GenDraftPage2.WRTotal.Text = MyDraft.PosCount(1, 4) + MyDraft.PosCount(2, 4) + MyDraft.PosCount(3, 4) + MyDraft.PosCount(4, 4) + MyDraft.PosCount(5, 4) + MyDraft.PosCount(6, 4) + MyDraft.PosCount(7, 4) + MyDraft.PosCount(8, 4) + MyDraft.PosCount(9, 4) +
            MyDraft.PosCount(10, 4) + MyDraft.PosCount(11, 4) + MyDraft.PosCount(12, 4) + MyDraft.PosCount(13, 4) + MyDraft.PosCount(14, 4)
        GenDraftPage2.TEInfo1.Text = MyDraft.PosCount(1, 5)
        GenDraftPage2.TEInfo2.Text = MyDraft.PosCount(2, 5)
        GenDraftPage2.TEInfo3.Text = MyDraft.PosCount(3, 5)
        GenDraftPage2.TEInfo4.Text = MyDraft.PosCount(4, 5)
        GenDraftPage2.TEInfo5.Text = MyDraft.PosCount(5, 5)
        GenDraftPage2.TEInfo6.Text = MyDraft.PosCount(6, 5)
        GenDraftPage2.TEInfo7.Text = MyDraft.PosCount(7, 5)
        GenDraftPage2.TEInfo8.Text = MyDraft.PosCount(8, 5)
        GenDraftPage2.TEInfo9.Text = MyDraft.PosCount(9, 5)
        GenDraftPage2.TEInfo10.Text = MyDraft.PosCount(10, 5)
        GenDraftPage2.TEInfo11.Text = MyDraft.PosCount(11, 5)
        GenDraftPage2.TEInfo12.Text = MyDraft.PosCount(12, 5)
        GenDraftPage2.TEInfo13.Text = MyDraft.PosCount(13, 5)
        GenDraftPage2.TEInfo14.Text = MyDraft.PosCount(14, 5)
        GenDraftPage2.TETotal.Text = MyDraft.PosCount(1, 5) + MyDraft.PosCount(2, 5) + MyDraft.PosCount(3, 5) + MyDraft.PosCount(4, 5) + MyDraft.PosCount(5, 5) + MyDraft.PosCount(6, 5) + MyDraft.PosCount(7, 5) + MyDraft.PosCount(8, 5) + MyDraft.PosCount(9, 5) +
            MyDraft.PosCount(10, 5) + MyDraft.PosCount(11, 5) + MyDraft.PosCount(12, 5) + MyDraft.PosCount(13, 5) + MyDraft.PosCount(14, 5)
        GenDraftPage2.OTInfo1.Text = MyDraft.PosCount(1, 6)
        GenDraftPage2.OTInfo2.Text = MyDraft.PosCount(2, 6)
        GenDraftPage2.OTInfo3.Text = MyDraft.PosCount(3, 6)
        GenDraftPage2.OTInfo4.Text = MyDraft.PosCount(4, 6)
        GenDraftPage2.OTInfo5.Text = MyDraft.PosCount(5, 6)
        GenDraftPage2.OTInfo6.Text = MyDraft.PosCount(6, 6)
        GenDraftPage2.OTInfo7.Text = MyDraft.PosCount(7, 6)
        GenDraftPage2.OTInfo8.Text = MyDraft.PosCount(8, 6)
        GenDraftPage2.OTInfo9.Text = MyDraft.PosCount(9, 6)
        GenDraftPage2.OTInfo10.Text = MyDraft.PosCount(10, 6)
        GenDraftPage2.OTInfo11.Text = MyDraft.PosCount(11, 6)
        GenDraftPage2.OTInfo12.Text = MyDraft.PosCount(12, 6)
        GenDraftPage2.OTInfo13.Text = MyDraft.PosCount(13, 6)
        GenDraftPage2.OTInfo14.Text = MyDraft.PosCount(14, 6)
        GenDraftPage2.OTTotal.Text = MyDraft.PosCount(1, 6) + MyDraft.PosCount(2, 6) + MyDraft.PosCount(3, 6) + MyDraft.PosCount(4, 6) + MyDraft.PosCount(5, 6) + MyDraft.PosCount(6, 6) + MyDraft.PosCount(7, 6) + MyDraft.PosCount(8, 6) + MyDraft.PosCount(9, 6) +
            MyDraft.PosCount(10, 6) + MyDraft.PosCount(11, 6) + MyDraft.PosCount(12, 6) + MyDraft.PosCount(13, 6) + MyDraft.PosCount(14, 6)
        GenDraftPage2.CInfo1.Text = MyDraft.PosCount(1, 7)
        GenDraftPage2.CInfo2.Text = MyDraft.PosCount(2, 7)
        GenDraftPage2.CInfo3.Text = MyDraft.PosCount(3, 7)
        GenDraftPage2.CInfo4.Text = MyDraft.PosCount(4, 7)
        GenDraftPage2.CInfo5.Text = MyDraft.PosCount(5, 7)
        GenDraftPage2.CInfo6.Text = MyDraft.PosCount(6, 7)
        GenDraftPage2.CInfo7.Text = MyDraft.PosCount(7, 7)
        GenDraftPage2.CInfo8.Text = MyDraft.PosCount(8, 7)
        GenDraftPage2.CInfo9.Text = MyDraft.PosCount(9, 7)
        GenDraftPage2.CInfo10.Text = MyDraft.PosCount(10, 7)
        GenDraftPage2.CInfo11.Text = MyDraft.PosCount(11, 7)
        GenDraftPage2.CInfo12.Text = MyDraft.PosCount(12, 7)
        GenDraftPage2.CInfo13.Text = MyDraft.PosCount(13, 7)
        GenDraftPage2.CInfo14.Text = MyDraft.PosCount(14, 7)
        GenDraftPage2.CTotal.Text = MyDraft.PosCount(1, 7) + MyDraft.PosCount(2, 7) + MyDraft.PosCount(3, 7) + MyDraft.PosCount(4, 7) + MyDraft.PosCount(5, 7) + MyDraft.PosCount(6, 7) + MyDraft.PosCount(7, 7) + MyDraft.PosCount(8, 7) + MyDraft.PosCount(9, 7) +
            MyDraft.PosCount(10, 7) + MyDraft.PosCount(11, 7) + MyDraft.PosCount(12, 7) + MyDraft.PosCount(13, 7) + MyDraft.PosCount(14, 7)
        GenDraftPage2.OGInfo1.Text = MyDraft.PosCount(1, 8)
        GenDraftPage2.OGInfo2.Text = MyDraft.PosCount(2, 8)
        GenDraftPage2.OGInfo3.Text = MyDraft.PosCount(3, 8)
        GenDraftPage2.OGInfo4.Text = MyDraft.PosCount(4, 8)
        GenDraftPage2.OGInfo5.Text = MyDraft.PosCount(5, 8)
        GenDraftPage2.OGInfo6.Text = MyDraft.PosCount(6, 8)
        GenDraftPage2.OGInfo7.Text = MyDraft.PosCount(7, 8)
        GenDraftPage2.OGInfo8.Text = MyDraft.PosCount(8, 8)
        GenDraftPage2.OGInfo9.Text = MyDraft.PosCount(9, 8)
        GenDraftPage2.OGInfo10.Text = MyDraft.PosCount(10, 8)
        GenDraftPage2.OGInfo11.Text = MyDraft.PosCount(11, 8)
        GenDraftPage2.OGInfo12.Text = MyDraft.PosCount(12, 8)
        GenDraftPage2.OGInfo13.Text = MyDraft.PosCount(13, 8)
        GenDraftPage2.OGInfo14.Text = MyDraft.PosCount(14, 8)
        GenDraftPage2.OGTotal.Text = MyDraft.PosCount(1, 8) + MyDraft.PosCount(2, 8) + MyDraft.PosCount(3, 8) + MyDraft.PosCount(4, 8) + MyDraft.PosCount(5, 8) + MyDraft.PosCount(6, 8) + MyDraft.PosCount(7, 8) + MyDraft.PosCount(8, 8) + MyDraft.PosCount(9, 8) +
            MyDraft.PosCount(10, 8) + MyDraft.PosCount(11, 8) + MyDraft.PosCount(12, 8) + MyDraft.PosCount(13, 8) + MyDraft.PosCount(14, 8)
        GenDraftPage2.DEInfo1.Text = MyDraft.PosCount(1, 9)
        GenDraftPage2.DEInfo2.Text = MyDraft.PosCount(2, 9)
        GenDraftPage2.DEInfo3.Text = MyDraft.PosCount(3, 9)
        GenDraftPage2.DEInfo4.Text = MyDraft.PosCount(4, 9)
        GenDraftPage2.DEInfo5.Text = MyDraft.PosCount(5, 9)
        GenDraftPage2.DEInfo6.Text = MyDraft.PosCount(6, 9)
        GenDraftPage2.DEInfo7.Text = MyDraft.PosCount(7, 9)
        GenDraftPage2.DEInfo8.Text = MyDraft.PosCount(8, 9)
        GenDraftPage2.DEInfo9.Text = MyDraft.PosCount(9, 9)
        GenDraftPage2.DEInfo10.Text = MyDraft.PosCount(10, 9)
        GenDraftPage2.DEInfo11.Text = MyDraft.PosCount(11, 9)
        GenDraftPage2.DEInfo12.Text = MyDraft.PosCount(12, 9)
        GenDraftPage2.DEInfo13.Text = MyDraft.PosCount(13, 9)
        GenDraftPage2.DEInfo14.Text = MyDraft.PosCount(14, 9)
        GenDraftPage2.DETotal.Text = MyDraft.PosCount(1, 9) + MyDraft.PosCount(2, 9) + MyDraft.PosCount(3, 9) + MyDraft.PosCount(4, 9) + MyDraft.PosCount(5, 9) + MyDraft.PosCount(6, 9) + MyDraft.PosCount(7, 9) + MyDraft.PosCount(8, 9) + MyDraft.PosCount(9, 9) +
            MyDraft.PosCount(10, 9) + MyDraft.PosCount(11, 9) + MyDraft.PosCount(12, 9) + MyDraft.PosCount(13, 9) + MyDraft.PosCount(14, 9)
        GenDraftPage2.DTInfo1.Text = MyDraft.PosCount(1, 10)
        GenDraftPage2.DTInfo2.Text = MyDraft.PosCount(2, 10)
        GenDraftPage2.DTInfo3.Text = MyDraft.PosCount(3, 10)
        GenDraftPage2.DTInfo4.Text = MyDraft.PosCount(4, 10)
        GenDraftPage2.DTInfo5.Text = MyDraft.PosCount(5, 10)
        GenDraftPage2.DTInfo6.Text = MyDraft.PosCount(6, 10)
        GenDraftPage2.DTInfo7.Text = MyDraft.PosCount(7, 10)
        GenDraftPage2.DTInfo8.Text = MyDraft.PosCount(8, 10)
        GenDraftPage2.DTInfo9.Text = MyDraft.PosCount(9, 10)
        GenDraftPage2.DTInfo10.Text = MyDraft.PosCount(10, 10)
        GenDraftPage2.DTInfo11.Text = MyDraft.PosCount(11, 10)
        GenDraftPage2.DTInfo12.Text = MyDraft.PosCount(12, 10)
        GenDraftPage2.DTInfo13.Text = MyDraft.PosCount(13, 10)
        GenDraftPage2.DTInfo14.Text = MyDraft.PosCount(14, 10)
        GenDraftPage2.DTTotal.Text = MyDraft.PosCount(1, 10) + MyDraft.PosCount(2, 10) + MyDraft.PosCount(3, 10) + MyDraft.PosCount(4, 10) + MyDraft.PosCount(5, 10) + MyDraft.PosCount(6, 10) + MyDraft.PosCount(7, 10) + MyDraft.PosCount(8, 10) + MyDraft.PosCount(9, 10) +
            MyDraft.PosCount(10, 10) + MyDraft.PosCount(11, 10) + MyDraft.PosCount(12, 10) + MyDraft.PosCount(13, 10) + MyDraft.PosCount(14, 10)
        GenDraftPage2.OLBInfo1.Text = MyDraft.PosCount(1, 11)
        GenDraftPage2.OLBInfo2.Text = MyDraft.PosCount(2, 11)
        GenDraftPage2.OLBInfo3.Text = MyDraft.PosCount(3, 11)
        GenDraftPage2.OLBInfo4.Text = MyDraft.PosCount(4, 11)
        GenDraftPage2.OLBInfo5.Text = MyDraft.PosCount(5, 11)
        GenDraftPage2.OLBInfo6.Text = MyDraft.PosCount(6, 11)
        GenDraftPage2.OLBInfo7.Text = MyDraft.PosCount(7, 11)
        GenDraftPage2.OLBInfo8.Text = MyDraft.PosCount(8, 11)
        GenDraftPage2.OLBInfo9.Text = MyDraft.PosCount(9, 11)
        GenDraftPage2.OLBInfo10.Text = MyDraft.PosCount(10, 11)
        GenDraftPage2.OLBInfo11.Text = MyDraft.PosCount(11, 11)
        GenDraftPage2.OLBInfo12.Text = MyDraft.PosCount(12, 11)
        GenDraftPage2.OLBInfo13.Text = MyDraft.PosCount(13, 11)
        GenDraftPage2.OLBInfo14.Text = MyDraft.PosCount(14, 11)
        GenDraftPage2.OLBTotal.Text = MyDraft.PosCount(1, 11) + MyDraft.PosCount(2, 11) + MyDraft.PosCount(3, 11) + MyDraft.PosCount(4, 11) + MyDraft.PosCount(5, 11) + MyDraft.PosCount(6, 11) + MyDraft.PosCount(7, 11) + MyDraft.PosCount(8, 11) + MyDraft.PosCount(9, 11) +
            MyDraft.PosCount(10, 11) + MyDraft.PosCount(11, 11) + MyDraft.PosCount(12, 11) + MyDraft.PosCount(13, 11) + MyDraft.PosCount(14, 11)
        GenDraftPage2.ILBInfo1.Text = MyDraft.PosCount(1, 12)
        GenDraftPage2.ILBInfo2.Text = MyDraft.PosCount(2, 12)
        GenDraftPage2.ILBInfo3.Text = MyDraft.PosCount(3, 12)
        GenDraftPage2.ILBInfo4.Text = MyDraft.PosCount(4, 12)
        GenDraftPage2.ILBInfo5.Text = MyDraft.PosCount(5, 12)
        GenDraftPage2.ILBInfo6.Text = MyDraft.PosCount(6, 12)
        GenDraftPage2.ILBInfo7.Text = MyDraft.PosCount(7, 12)
        GenDraftPage2.ILBInfo8.Text = MyDraft.PosCount(8, 12)
        GenDraftPage2.ILBInfo9.Text = MyDraft.PosCount(9, 12)
        GenDraftPage2.ILBInfo10.Text = MyDraft.PosCount(10, 12)
        GenDraftPage2.ILBInfo11.Text = MyDraft.PosCount(11, 12)
        GenDraftPage2.ILBInfo12.Text = MyDraft.PosCount(12, 12)
        GenDraftPage2.ILBInfo13.Text = MyDraft.PosCount(13, 12)
        GenDraftPage2.ILBInfo14.Text = MyDraft.PosCount(14, 12)
        GenDraftPage2.ILBTotal.Text = MyDraft.PosCount(1, 12) + MyDraft.PosCount(2, 12) + MyDraft.PosCount(3, 12) + MyDraft.PosCount(4, 12) + MyDraft.PosCount(5, 12) + MyDraft.PosCount(6, 12) + MyDraft.PosCount(7, 12) + MyDraft.PosCount(8, 12) + MyDraft.PosCount(9, 12) +
            MyDraft.PosCount(10, 12) + MyDraft.PosCount(11, 12) + MyDraft.PosCount(12, 12) + MyDraft.PosCount(13, 12) + MyDraft.PosCount(14, 12)
        GenDraftPage2.CBInfo1.Text = MyDraft.PosCount(1, 13)
        GenDraftPage2.CBInfo2.Text = MyDraft.PosCount(2, 13)
        GenDraftPage2.CBInfo3.Text = MyDraft.PosCount(3, 13)
        GenDraftPage2.CBInfo4.Text = MyDraft.PosCount(4, 13)
        GenDraftPage2.CBInfo5.Text = MyDraft.PosCount(5, 13)
        GenDraftPage2.CBInfo6.Text = MyDraft.PosCount(6, 13)
        GenDraftPage2.CBInfo7.Text = MyDraft.PosCount(7, 13)
        GenDraftPage2.CBInfo8.Text = MyDraft.PosCount(8, 13)
        GenDraftPage2.CBInfo9.Text = MyDraft.PosCount(9, 13)
        GenDraftPage2.CBInfo10.Text = MyDraft.PosCount(10, 13)
        GenDraftPage2.CBInfo11.Text = MyDraft.PosCount(11, 13)
        GenDraftPage2.CBInfo12.Text = MyDraft.PosCount(12, 13)
        GenDraftPage2.CBInfo13.Text = MyDraft.PosCount(13, 13)
        GenDraftPage2.CBInfo14.Text = MyDraft.PosCount(14, 13)
        GenDraftPage2.CBTotal.Text = MyDraft.PosCount(1, 13) + MyDraft.PosCount(2, 13) + MyDraft.PosCount(3, 13) + MyDraft.PosCount(4, 13) + MyDraft.PosCount(5, 13) + MyDraft.PosCount(6, 13) + MyDraft.PosCount(7, 13) + MyDraft.PosCount(8, 13) + MyDraft.PosCount(9, 13) +
            MyDraft.PosCount(10, 13) + MyDraft.PosCount(11, 13) + MyDraft.PosCount(12, 13) + MyDraft.PosCount(13, 13) + MyDraft.PosCount(14, 13)
        GenDraftPage2.FSInfo1.Text = MyDraft.PosCount(1, 14)
        GenDraftPage2.FSInfo2.Text = MyDraft.PosCount(2, 14)
        GenDraftPage2.FSInfo3.Text = MyDraft.PosCount(3, 14)
        GenDraftPage2.FSInfo4.Text = MyDraft.PosCount(4, 14)
        GenDraftPage2.FSInfo5.Text = MyDraft.PosCount(5, 14)
        GenDraftPage2.FSInfo6.Text = MyDraft.PosCount(6, 14)
        GenDraftPage2.FSInfo7.Text = MyDraft.PosCount(7, 14)
        GenDraftPage2.FSInfo8.Text = MyDraft.PosCount(8, 14)
        GenDraftPage2.FSInfo9.Text = MyDraft.PosCount(9, 14)
        GenDraftPage2.FSInfo10.Text = MyDraft.PosCount(10, 14)
        GenDraftPage2.FSInfo11.Text = MyDraft.PosCount(11, 14)
        GenDraftPage2.FSInfo12.Text = MyDraft.PosCount(12, 14)
        GenDraftPage2.FSInfo13.Text = MyDraft.PosCount(13, 14)
        GenDraftPage2.FSInfo14.Text = MyDraft.PosCount(14, 14)
        GenDraftPage2.FSTotal.Text = MyDraft.PosCount(1, 14) + MyDraft.PosCount(2, 14) + MyDraft.PosCount(3, 14) + MyDraft.PosCount(4, 14) + MyDraft.PosCount(5, 14) + MyDraft.PosCount(6, 14) + MyDraft.PosCount(7, 14) + MyDraft.PosCount(8, 14) + MyDraft.PosCount(9, 14) +
            MyDraft.PosCount(10, 14) + MyDraft.PosCount(11, 14) + MyDraft.PosCount(12, 14) + MyDraft.PosCount(13, 14) + MyDraft.PosCount(14, 14)
        GenDraftPage2.SSInfo1.Text = MyDraft.PosCount(1, 15)
        GenDraftPage2.SSInfo2.Text = MyDraft.PosCount(2, 15)
        GenDraftPage2.SSInfo3.Text = MyDraft.PosCount(3, 15)
        GenDraftPage2.SSInfo4.Text = MyDraft.PosCount(4, 15)
        GenDraftPage2.SSInfo5.Text = MyDraft.PosCount(5, 15)
        GenDraftPage2.SSInfo6.Text = MyDraft.PosCount(6, 15)
        GenDraftPage2.SSInfo7.Text = MyDraft.PosCount(7, 15)
        GenDraftPage2.SSInfo8.Text = MyDraft.PosCount(8, 15)
        GenDraftPage2.SSInfo9.Text = MyDraft.PosCount(9, 15)
        GenDraftPage2.SSInfo10.Text = MyDraft.PosCount(10, 15)
        GenDraftPage2.SSInfo11.Text = MyDraft.PosCount(11, 15)
        GenDraftPage2.SSInfo12.Text = MyDraft.PosCount(12, 15)
        GenDraftPage2.SSInfo13.Text = MyDraft.PosCount(13, 15)
        GenDraftPage2.SSInfo14.Text = MyDraft.PosCount(14, 15)
        GenDraftPage2.SSTotal.Text = MyDraft.PosCount(1, 15) + MyDraft.PosCount(2, 15) + MyDraft.PosCount(3, 15) + MyDraft.PosCount(4, 15) + MyDraft.PosCount(5, 15) + MyDraft.PosCount(6, 15) + MyDraft.PosCount(7, 15) + MyDraft.PosCount(8, 15) + MyDraft.PosCount(9, 15) +
            MyDraft.PosCount(10, 15) + MyDraft.PosCount(11, 15) + MyDraft.PosCount(12, 15) + MyDraft.PosCount(13, 15) + MyDraft.PosCount(14, 15)
        GenDraftPage2.KInfo1.Text = MyDraft.PosCount(1, 16)
        GenDraftPage2.KInfo2.Text = MyDraft.PosCount(2, 16)
        GenDraftPage2.KInfo3.Text = MyDraft.PosCount(3, 16)
        GenDraftPage2.KInfo4.Text = MyDraft.PosCount(4, 16)
        GenDraftPage2.KInfo5.Text = MyDraft.PosCount(5, 16)
        GenDraftPage2.KInfo6.Text = MyDraft.PosCount(6, 16)
        GenDraftPage2.KInfo7.Text = MyDraft.PosCount(7, 16)
        GenDraftPage2.KInfo8.Text = MyDraft.PosCount(8, 16)
        GenDraftPage2.KInfo9.Text = MyDraft.PosCount(9, 16)
        GenDraftPage2.KInfo10.Text = MyDraft.PosCount(10, 16)
        GenDraftPage2.KInfo11.Text = MyDraft.PosCount(11, 16)
        GenDraftPage2.KInfo12.Text = MyDraft.PosCount(12, 16)
        GenDraftPage2.KInfo13.Text = MyDraft.PosCount(13, 16)
        GenDraftPage2.KInfo14.Text = MyDraft.PosCount(14, 16)
        GenDraftPage2.KTotal.Text = MyDraft.PosCount(1, 16) + MyDraft.PosCount(2, 16) + MyDraft.PosCount(3, 16) + MyDraft.PosCount(4, 16) + MyDraft.PosCount(5, 16) + MyDraft.PosCount(6, 16) + MyDraft.PosCount(7, 16) + MyDraft.PosCount(8, 16) + MyDraft.PosCount(9, 16) +
            MyDraft.PosCount(10, 16) + MyDraft.PosCount(11, 16) + MyDraft.PosCount(12, 16) + MyDraft.PosCount(13, 16) + MyDraft.PosCount(14, 16)
        GenDraftPage2.PInfo1.Text = MyDraft.PosCount(1, 17)
        GenDraftPage2.PInfo2.Text = MyDraft.PosCount(2, 17)
        GenDraftPage2.PInfo3.Text = MyDraft.PosCount(3, 17)
        GenDraftPage2.PInfo4.Text = MyDraft.PosCount(4, 17)
        GenDraftPage2.PInfo5.Text = MyDraft.PosCount(5, 17)
        GenDraftPage2.PInfo6.Text = MyDraft.PosCount(6, 17)
        GenDraftPage2.PInfo7.Text = MyDraft.PosCount(7, 17)
        GenDraftPage2.PInfo8.Text = MyDraft.PosCount(8, 17)
        GenDraftPage2.PInfo9.Text = MyDraft.PosCount(9, 17)
        GenDraftPage2.PInfo10.Text = MyDraft.PosCount(10, 17)
        GenDraftPage2.PInfo11.Text = MyDraft.PosCount(11, 17)
        GenDraftPage2.PInfo12.Text = MyDraft.PosCount(12, 17)
        GenDraftPage2.PInfo13.Text = MyDraft.PosCount(13, 17)
        GenDraftPage2.PInfo14.Text = MyDraft.PosCount(14, 17)
        GenDraftPage2.PTotal.Text = MyDraft.PosCount(1, 17) + MyDraft.PosCount(2, 17) + MyDraft.PosCount(3, 17) + MyDraft.PosCount(4, 17) + MyDraft.PosCount(5, 17) + MyDraft.PosCount(6, 17) + MyDraft.PosCount(7, 17) + MyDraft.PosCount(8, 17) + MyDraft.PosCount(9, 17) +
            MyDraft.PosCount(10, 17) + MyDraft.PosCount(11, 17) + MyDraft.PosCount(12, 17) + MyDraft.PosCount(13, 17) + MyDraft.PosCount(14, 17)

        GenDraftPage2.R1T5Totals.Text = MyDraft.PosCount(1, 1) + MyDraft.PosCount(1, 2) + MyDraft.PosCount(1, 3) + MyDraft.PosCount(1, 4) + MyDraft.PosCount(1, 5) + MyDraft.PosCount(1, 6) + MyDraft.PosCount(1, 7) + MyDraft.PosCount(1, 8) + MyDraft.PosCount(1, 9) + MyDraft.PosCount(1, 10) +
            MyDraft.PosCount(1, 11) + MyDraft.PosCount(1, 12) + MyDraft.PosCount(1, 13) + MyDraft.PosCount(1, 14) + MyDraft.PosCount(1, 15) + MyDraft.PosCount(1, 16) + MyDraft.PosCount(1, 17)
        GenDraftPage2.R1T10Totals.Text = MyDraft.PosCount(2, 1) + MyDraft.PosCount(2, 2) + MyDraft.PosCount(2, 3) + MyDraft.PosCount(2, 4) + MyDraft.PosCount(2, 5) + MyDraft.PosCount(2, 6) + MyDraft.PosCount(2, 7) + MyDraft.PosCount(2, 8) + MyDraft.PosCount(2, 9) + MyDraft.PosCount(2, 10) +
            MyDraft.PosCount(2, 11) + MyDraft.PosCount(2, 12) + MyDraft.PosCount(2, 13) + MyDraft.PosCount(2, 14) + MyDraft.PosCount(2, 15) + MyDraft.PosCount(2, 16) + MyDraft.PosCount(2, 17)
        GenDraftPage2.R1MidFirstTotals.Text = MyDraft.PosCount(3, 1) + MyDraft.PosCount(3, 2) + MyDraft.PosCount(3, 3) + MyDraft.PosCount(3, 4) + MyDraft.PosCount(3, 5) + MyDraft.PosCount(3, 6) + MyDraft.PosCount(3, 7) + MyDraft.PosCount(3, 8) + MyDraft.PosCount(3, 9) + MyDraft.PosCount(3, 10) +
            MyDraft.PosCount(3, 11) + MyDraft.PosCount(3, 12) + MyDraft.PosCount(3, 13) + MyDraft.PosCount(3, 14) + MyDraft.PosCount(3, 15) + MyDraft.PosCount(3, 16) + MyDraft.PosCount(3, 17)
        GenDraftPage2.R1LateFirstTotals.Text = MyDraft.PosCount(4, 1) + MyDraft.PosCount(4, 2) + MyDraft.PosCount(4, 3) + MyDraft.PosCount(4, 4) + MyDraft.PosCount(4, 5) + MyDraft.PosCount(4, 6) + MyDraft.PosCount(4, 7) + MyDraft.PosCount(4, 8) + MyDraft.PosCount(4, 9) + MyDraft.PosCount(4, 10) +
            MyDraft.PosCount(4, 11) + MyDraft.PosCount(4, 12) + MyDraft.PosCount(4, 13) + MyDraft.PosCount(4, 14) + MyDraft.PosCount(4, 15) + MyDraft.PosCount(4, 16) + MyDraft.PosCount(4, 17)
        GenDraftPage2.R2Totals.Text = MyDraft.PosCount(5, 1) + MyDraft.PosCount(5, 2) + MyDraft.PosCount(5, 3) + MyDraft.PosCount(5, 4) + MyDraft.PosCount(5, 5) + MyDraft.PosCount(5, 6) + MyDraft.PosCount(5, 7) + MyDraft.PosCount(5, 8) + MyDraft.PosCount(5, 9) + MyDraft.PosCount(5, 10) +
            MyDraft.PosCount(5, 11) + MyDraft.PosCount(5, 12) + MyDraft.PosCount(5, 13) + MyDraft.PosCount(5, 14) + MyDraft.PosCount(5, 15) + MyDraft.PosCount(5, 16) + MyDraft.PosCount(5, 17)
        GenDraftPage2.R3Totals.Text = MyDraft.PosCount(6, 1) + MyDraft.PosCount(6, 2) + MyDraft.PosCount(6, 3) + MyDraft.PosCount(6, 4) + MyDraft.PosCount(6, 5) + MyDraft.PosCount(6, 6) + MyDraft.PosCount(6, 7) + MyDraft.PosCount(6, 8) + MyDraft.PosCount(6, 9) + MyDraft.PosCount(6, 10) +
            MyDraft.PosCount(6, 11) + MyDraft.PosCount(6, 12) + MyDraft.PosCount(6, 13) + MyDraft.PosCount(6, 14) + MyDraft.PosCount(6, 15) + MyDraft.PosCount(6, 16) + MyDraft.PosCount(6, 17)
        GenDraftPage2.R4Totals.Text = MyDraft.PosCount(7, 1) + MyDraft.PosCount(7, 2) + MyDraft.PosCount(7, 3) + MyDraft.PosCount(7, 4) + MyDraft.PosCount(7, 5) + MyDraft.PosCount(7, 6) + MyDraft.PosCount(7, 7) + MyDraft.PosCount(7, 8) + MyDraft.PosCount(7, 9) + MyDraft.PosCount(7, 10) +
            MyDraft.PosCount(7, 11) + MyDraft.PosCount(7, 12) + MyDraft.PosCount(7, 13) + MyDraft.PosCount(7, 14) + MyDraft.PosCount(7, 15) + MyDraft.PosCount(7, 16) + MyDraft.PosCount(7, 17)
        GenDraftPage2.R5Totals.Text = MyDraft.PosCount(8, 1) + MyDraft.PosCount(8, 2) + MyDraft.PosCount(8, 3) + MyDraft.PosCount(8, 4) + MyDraft.PosCount(8, 5) + MyDraft.PosCount(8, 6) + MyDraft.PosCount(8, 7) + MyDraft.PosCount(8, 8) + MyDraft.PosCount(8, 9) + MyDraft.PosCount(8, 10) +
            MyDraft.PosCount(8, 11) + MyDraft.PosCount(8, 12) + MyDraft.PosCount(8, 13) + MyDraft.PosCount(8, 14) + MyDraft.PosCount(8, 15) + MyDraft.PosCount(8, 16) + MyDraft.PosCount(8, 17)
        GenDraftPage2.R6Totals.Text = MyDraft.PosCount(9, 1) + MyDraft.PosCount(9, 2) + MyDraft.PosCount(9, 3) + MyDraft.PosCount(9, 4) + MyDraft.PosCount(9, 5) + MyDraft.PosCount(9, 6) + MyDraft.PosCount(9, 7) + MyDraft.PosCount(9, 8) + MyDraft.PosCount(9, 9) + MyDraft.PosCount(9, 10) +
            MyDraft.PosCount(9, 11) + MyDraft.PosCount(9, 12) + MyDraft.PosCount(9, 13) + MyDraft.PosCount(9, 14) + MyDraft.PosCount(9, 15) + MyDraft.PosCount(9, 16) + MyDraft.PosCount(9, 17)
        GenDraftPage2.R7Totals.Text = MyDraft.PosCount(10, 1) + MyDraft.PosCount(10, 2) + MyDraft.PosCount(10, 3) + MyDraft.PosCount(10, 4) + MyDraft.PosCount(10, 5) + MyDraft.PosCount(10, 6) + MyDraft.PosCount(10, 7) + MyDraft.PosCount(10, 8) + MyDraft.PosCount(10, 9) + MyDraft.PosCount(10, 10) +
            MyDraft.PosCount(10, 11) + MyDraft.PosCount(10, 12) + MyDraft.PosCount(10, 13) + MyDraft.PosCount(10, 14) + MyDraft.PosCount(10, 15) + MyDraft.PosCount(10, 16) + MyDraft.PosCount(10, 17)
        GenDraftPage2.PUFATotals.Text = MyDraft.PosCount(11, 1) + MyDraft.PosCount(11, 2) + MyDraft.PosCount(11, 3) + MyDraft.PosCount(11, 4) + MyDraft.PosCount(11, 5) + MyDraft.PosCount(11, 6) + MyDraft.PosCount(11, 7) + MyDraft.PosCount(11, 8) + MyDraft.PosCount(11, 9) + MyDraft.PosCount(11, 10) +
            MyDraft.PosCount(11, 11) + MyDraft.PosCount(11, 12) + MyDraft.PosCount(11, 13) + MyDraft.PosCount(11, 14) + MyDraft.PosCount(11, 15) + MyDraft.PosCount(11, 16) + MyDraft.PosCount(11, 17)
        GenDraftPage2.LUFATotals.Text = MyDraft.PosCount(12, 1) + MyDraft.PosCount(12, 2) + MyDraft.PosCount(12, 3) + MyDraft.PosCount(12, 4) + MyDraft.PosCount(12, 5) + MyDraft.PosCount(12, 6) + MyDraft.PosCount(12, 7) + MyDraft.PosCount(12, 8) + MyDraft.PosCount(12, 9) + MyDraft.PosCount(12, 10) +
            MyDraft.PosCount(12, 11) + MyDraft.PosCount(12, 12) + MyDraft.PosCount(12, 13) + MyDraft.PosCount(12, 14) + MyDraft.PosCount(12, 15) + MyDraft.PosCount(12, 16) + MyDraft.PosCount(12, 17)
        GenDraftPage2.PracSquadTotals.Text = MyDraft.PosCount(13, 1) + MyDraft.PosCount(13, 2) + MyDraft.PosCount(13, 3) + MyDraft.PosCount(13, 4) + MyDraft.PosCount(13, 5) + MyDraft.PosCount(13, 6) + MyDraft.PosCount(13, 7) + MyDraft.PosCount(13, 8) + MyDraft.PosCount(13, 9) + MyDraft.PosCount(13, 10) +
            MyDraft.PosCount(13, 11) + MyDraft.PosCount(13, 12) + MyDraft.PosCount(13, 13) + MyDraft.PosCount(13, 14) + MyDraft.PosCount(13, 15) + MyDraft.PosCount(13, 16) + MyDraft.PosCount(13, 17)
        GenDraftPage2.RejectTotals.Text = MyDraft.PosCount(14, 1) + MyDraft.PosCount(14, 2) + MyDraft.PosCount(14, 3) + MyDraft.PosCount(14, 4) + MyDraft.PosCount(14, 5) + MyDraft.PosCount(14, 6) + MyDraft.PosCount(14, 7) + MyDraft.PosCount(14, 8) + MyDraft.PosCount(14, 9) + MyDraft.PosCount(14, 10) +
            MyDraft.PosCount(14, 11) + MyDraft.PosCount(14, 12) + MyDraft.PosCount(14, 13) + MyDraft.PosCount(14, 14) + MyDraft.PosCount(14, 15) + MyDraft.PosCount(14, 16) + MyDraft.PosCount(14, 17)
        GenDraftPage2.DraftTotals.Text = CInt(GenDraftPage2.R1T5Totals.Text) + CInt(GenDraftPage2.R1T10Totals.Text) + CInt(GenDraftPage2.R1MidFirstTotals.Text) + CInt(GenDraftPage2.R1LateFirstTotals.Text) + CInt(GenDraftPage2.R2Totals.Text) +
        CInt(GenDraftPage2.R3Totals.Text) + CInt(GenDraftPage2.R4Totals.Text) + CInt(GenDraftPage2.R5Totals.Text) + CInt(GenDraftPage2.R6Totals.Text) + CInt(GenDraftPage2.R7Totals.Text) + CInt(GenDraftPage2.PUFATotals.Text) + CInt(GenDraftPage2.LUFATotals.Text) +
        CInt(GenDraftPage2.PracSquadTotals.Text) + CInt(GenDraftPage2.RejectTotals.Text)
        Me.NavigationService.Navigate(GenDraftPage2)
    End Sub

    Private Sub Btn_PlayersTable(sender As Object, e As RoutedEventArgs) Handles PlayersTable.Click
        Me.NavigationService.Navigate(DraftPlayersTable)
    End Sub
End Class