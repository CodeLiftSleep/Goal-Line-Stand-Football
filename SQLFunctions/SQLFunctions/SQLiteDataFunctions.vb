Imports System.Data.SQLite
Imports System.Collections
Imports System.Text
Public Class SQLiteDataFunctions
    Dim Conn As SQLite.SQLiteConnection = New SQLite.SQLiteConnection()
    Dim filepath As String = "Project Files/"
    ''' <summary>
    ''' Opens a connection to this DB--Must Explicitly pass the connection string before returning it, otherwise it will throw a Null exception as the connection string will be Nothing.
    ''' </summary>
    ''' <param name="DBName"></param>
    ''' <returns></returns>
    Public Function GetConnectionString(ByVal DBName As String) As String
        Conn.Close()
        If Conn.State <> ConnectionState.Open Then
            Conn.ConnectionString = "Data Source=" & filepath & DBName & ".sqlite;Version=3"
            '= "Data Source=|DataDirectory|\" & DBName & ".sqlite;Version=3"
            Conn.Open()
            Return Conn.ConnectionString
        Else
            Conn.ConnectionString = "Data Source=" & filepath & DBName & ".sqlite;Version=3"
        End If

    End Function
    ''' <summary>
    ''' check to see if the table exists, if not then create one
    ''' </summary>
    ''' <param name="DBName"></param>
    Public Sub CreateTable(ByVal DBName As String, ByVal DT As DataTable, ByVal TableName As String, ByVal SQLFieldNames As String)
        GetConnectionString(DBName)
        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If

        'Checks to see if the Table exists, if not create the table
        Try
            Dim SQL As String = ("CREATE TABLE If Not EXISTS " & TableName & "(" & SQLFieldNames & ")")
            Dim SQLCmd As SQLiteCommand = New SQLiteCommand(SQL, Conn)
            SQLCmd.ExecuteNonQuery()
            Conn.Close()
        Catch ex As System.Data.SQLite.SQLiteException
            Console.WriteLine(ex.Data)
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Loads a Table from the DB.  If the table does not exist, it creates one
    ''' </summary>
    ''' <param name="DBName"></param>
    ''' <param name="TableName"></param>
    Public Sub LoadTable(ByVal DBName As String, ByVal DT As DataTable, ByVal TableName As String)
        GetConnectionString(DBName)

        Dim SQLCmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand("Select * From " & TableName)
        Dim DA As SQLite.SQLiteDataAdapter = New SQLiteDataAdapter(SQLCmd)
        DA.SelectCommand.Connection = Conn
        DA.Fill(DT)
        Conn.Close()
    End Sub
    ''' <summary>
    ''' This will Keep the Actual Table, but simply Delete all the records in it
    ''' </summary>
    ''' <param name="DBName"></param>
    ''' <param name="DT"></param>
    ''' <param name="TableName"></param>
    Public Sub DeleteTable(ByVal DBName As String, ByVal DT As DataTable, ByVal TableName As String)
        GetConnectionString(DBName)
        Conn.Open()
        Dim SQL As String = "DELETE FROM " & TableName
        Using Conn
            Dim cmd As New SQLite.SQLiteCommand(SQL, Conn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    ''' <summary>
    ''' This completely removes the Table from the Database
    ''' </summary>
    ''' <param name="DBNAme"></param>
    ''' <param name="DT"></param>
    ''' <param name="TableName"></param>
    Public Sub DropTable(ByVal DBName As String, ByVal DT As DataTable, ByVal TableName As String)
        GetConnectionString(DBName)
        Conn.Open()
        Dim SQL As String = "DROP TABLE " & TableName
        Using Conn
            Dim cmd As New SQLite.SQLiteCommand(SQL, Conn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    ''' <summary>
    ''' Since there is no UpdateTable command like SQL Server, Bulk Insert Into will put all the rows from the DT into the DB
    ''' optimized to do it in a single transaction instead of every row
    ''' </summary>
    ''' <param name="DBName"></param>
    ''' <param name="DT"></param>
    ''' <param name="TableName"></param>
    Public Sub BulkInsert(ByVal DBName As String, ByVal DT As DataTable, ByVal TableName As String)

        Dim MyList As List(Of String) = New List(Of String)
        Using MyConn = New SQLite.SQLiteConnection(GetConnectionString(DBName))
            Using SQLCmd = New SQLiteCommand(MyConn)
                MyConn.Open() 'MUST declare Connection open at this stage or it returns an Invalid Operation Error due to connection being closed
                Using transaction = MyConn.BeginTransaction() 'This begins the bulk insert
                    Try
                        For row As Integer = 0 To DT.Rows.Count - 1 'cycle through each row
                            For Col As Integer = 0 To DT.Columns.Count - 1
                                MyList.Add(DT.Rows(row).Item(DT.Columns.Item(Col)))
                            Next Col
                            Dim sql = String.Format("INSERT INTO " & TableName & " VALUES ({0});", String.Join(", ", MyList))
                            SQLCmd.CommandText = sql
                            SQLCmd.ExecuteNonQuery()
                            MyList.Clear()
                        Next row
                        transaction.Commit() 'commits all changes to the DB
                    Catch ex As System.InvalidOperationException
                        Console.WriteLine(ex.ToString)
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            MyConn.Close()
        End Using
    End Sub


End Class


