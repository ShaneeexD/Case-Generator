Imports System.ComponentModel
Imports System.Security.Policy

Public Class HelpForm
    Private Sub HelpForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Icon
    End Sub

    Private Sub lblLink_Click(sender As Object, e As EventArgs) Handles lblLink.Click
        Dim url As String = "https://thunderstore.io/c/shadows-of-doubt/p/Piepieonline/CommunityCaseLoader/"
        Process.Start(New ProcessStartInfo With {
                    .FileName = url,
                    .UseShellExecute = True
                })
    End Sub

    Private Sub HelpForm_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
        Form1.openForm = Form1
        Form1.EnableOtherForms()
    End Sub
End Class