
Public Class WPFUserControl
    Dim Mywin As New MainWindow
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub GenButton_Click(sender As Object, e As RoutedEventArgs) Handles GenButton.Click
        System.Windows.Forms.Integration.ElementHost.EnableModelessKeyboardInterop(Mywin)
        Mywin.Show()
        Mywin.Activate()
    End Sub
End Class
Public Class UIElementCollection
    Implements IList, ICollection, IEnumerable

    Public ReadOnly Property Count As Integer Implements ICollection.Count
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public ReadOnly Property IsFixedSize As Boolean Implements IList.IsFixedSize
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements IList.IsReadOnly
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public ReadOnly Property IsSynchronized As Boolean Implements ICollection.IsSynchronized
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Default Public Property Item(index As Integer) As Object Implements IList.Item
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Object)
            Throw New NotImplementedException()
        End Set
    End Property

    Public ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Sub Clear() Implements IList.Clear
        Throw New NotImplementedException()
    End Sub

    Public Sub CopyTo(array As Array, index As Integer) Implements ICollection.CopyTo
        Throw New NotImplementedException()
    End Sub

    Public Sub Insert(index As Integer, value As Object) Implements IList.Insert
        Throw New NotImplementedException()
    End Sub

    Public Sub Remove(value As Object) Implements IList.Remove
        Throw New NotImplementedException()
    End Sub

    Public Sub RemoveAt(index As Integer) Implements IList.RemoveAt
        Throw New NotImplementedException()
    End Sub

    Public Function Add(value As Object) As Integer Implements IList.Add
        Throw New NotImplementedException()
    End Function

    Public Function Contains(value As Object) As Boolean Implements IList.Contains
        Throw New NotImplementedException()
    End Function

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Throw New NotImplementedException()
    End Function

    Public Function IndexOf(value As Object) As Integer Implements IList.IndexOf
        Throw New NotImplementedException()
    End Function
End Class
