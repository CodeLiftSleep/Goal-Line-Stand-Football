Class GenerateHome
    Public GenDraft2Page As New GenerationDraft2
    Public GenDraftPage As New GenerationDraft
    Public GenOwnerPage As New GenerationOwner
    Public GenCoachPage As New GenerationCoach
    Public GenPersonnelPage As New GenerationPersonnel
    Public GenNFLPlayerPage As New GenerationNFLPlayer
    Public GenAgentPage As New GenerationAgents
    Sub New()

    End Sub

    Private Sub GenDraft_Click(ByVal Sender As Object, ByVal e As RoutedEventArgs) Handles GenDraftButton.Click
        'view Draft Generation Screen

        Me.NavigationService.Navigate(GenDraftPage)
    End Sub

    Private Sub GenOwner_Click(ByVal Sender As Object, ByVal e As RoutedEventArgs) Handles GenOwnerButton.Click

        Me.NavigationService.Navigate(GenOwnerPage)
    End Sub

    Private Sub GenCoach_Click(sender As Object, e As RoutedEventArgs) Handles GenCoachButton.Click

        Me.NavigationService.Navigate(GenCoachPage)
    End Sub

    Private Sub GenPersonnel_Click(sender As Object, e As RoutedEventArgs) Handles GenPersonnelButton.Click

        Me.NavigationService.Navigate(GenPersonnelPage)
    End Sub

    Private Sub GenNFLPlayer_Click(sender As Object, e As RoutedEventArgs) Handles GenNFLPlayers.Click

        Me.NavigationService.Navigate(GenNFLPlayerPage)
    End Sub

    Private Sub GenAgent_Click(sender As Object, e As RoutedEventArgs) Handles GenAgentsButton.Click

        Me.NavigationService.Navigate(GenAgentPage)
    End Sub
End Class