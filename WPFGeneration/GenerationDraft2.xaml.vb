Class GenerationDraft2
    Dim Rounds(14) As String
    Dim NumPlayers(14) As Integer
    Dim Pos(17) As Integer
    Dim MyList As New List(Of Object)

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub BackButton_Click(sender As Object, e As RoutedEventArgs) Handles BackButton.Click
        'Dim GenDraftPage As New GenerationDraft
        Me.NavigationService.GoBack()
    End Sub

End Class
