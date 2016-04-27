Imports BusinessLogic
Imports Domain

Public Class DraftPlayersTable
    ' This call is required by the designer.

    Dim DraftPlayers As IQueryable(Of DraftPlayer)

    Public Sub New()
        InitializeComponent()

        DraftPlayers = New DraftPlayersApi().GetAllDraftPlayers()

        DraftPlayersDataGrid.ItemsSource = DraftPlayers.ToList()

    End Sub
End Class
