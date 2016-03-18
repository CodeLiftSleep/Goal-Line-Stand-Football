Imports System.IO
''' <summary>
''' Holds any Public variables for use within the Generation project
''' </summary>
Public Module FilesAndDataTables
    Public MT As New Mersenne.MersenneTwister
    Public CoachDT As New DataTable
    Public DraftDT As New DataTable
    Public GMDT As New DataTable
    Public OwnerDT As New DataTable
    Public ScoutDT As New DataTable
    Public ScoutGradeDT As New DataTable
    Public PlayerDT As New DataTable
    Public FirstNames As New DataTable
    Public LastNames As New DataTable
    Public Colleges As New DataTable
    Public Eval As New Evaluation
    Public MyDB As String = "Football"
    Public SQLiteTables As New SQLFunctions.SQLiteDataFunctions
    Dim filepath As String = "Project Files/"
    Public ReadFName As StreamReader = New StreamReader(filepath + "FName.txt")
    Public ReadLName As StreamReader = New StreamReader(filepath + "LName.txt")
    Public ReadCollege As StreamReader = New StreamReader(filepath + "Colleges.txt")


    Public Sub LoadFile(ByVal FileName As String)

    End Sub

End Module
