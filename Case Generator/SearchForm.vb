Imports System.ComponentModel

Public Class SearchForm
    ' Variables to store the search term and occurrences
    Private searchTerm As String
    Private currentIndex As Integer = -1
    Private occurrences As List(Of Integer) = New List(Of Integer)
    Private txtOutput As RichTextBox
    Private originalBackColor As Color = SystemColors.HighlightText ' Original background color of the RichTextBox
    Private highlightColor As Color = Color.Yellow ' Highlight color for the search term

    ' Constructor to initialize the form with search term and occurrences
    Public Sub New(searchTerm As String, txtOutput As RichTextBox)
        Me.searchTerm = searchTerm
        Me.txtOutput = txtOutput
        InitializeComponent()

        ' Find all occurrences of the search term in the RichTextBox
        FindAllOccurrences()
    End Sub

    ' Method to find all occurrences of the search term in the RichTextBox
    Private Sub FindAllOccurrences()
        Dim startIndex As Integer = 0
        occurrences.Clear()

        While startIndex < txtOutput.TextLength
            startIndex = txtOutput.Find(searchTerm, startIndex, RichTextBoxFinds.None)
            If startIndex <> -1 Then
                occurrences.Add(startIndex)
                startIndex += searchTerm.Length
            Else
                Exit While
            End If
        End While

        ' Update the form's label to show how many occurrences were found
        UpdateLabel()
    End Sub

    ' Method to update the label text
    Private Sub UpdateLabel()
        lblStatus.Text = $"Found {occurrences.Count} occurrence(s) of '{searchTerm}'"
    End Sub

    ' Navigate to the next occurrence
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If occurrences.Count = 0 Then Return
        currentIndex += 1
        If currentIndex >= occurrences.Count Then currentIndex = 0 ' Loop to the beginning

        HighlightCurrentOccurrence()
    End Sub

    ' Navigate to the previous occurrence
    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If occurrences.Count = 0 Then Return
        currentIndex -= 1
        If currentIndex < 0 Then currentIndex = occurrences.Count - 1 ' Loop to the end

        HighlightCurrentOccurrence()
    End Sub

    ' Method to highlight the current occurrence with custom background color
    Private Sub HighlightCurrentOccurrence()
        ' Remove previous highlights before applying a new one
        RemoveAllHighlights()

        If currentIndex <> -1 AndAlso occurrences.Count > 0 Then
            ' Apply custom highlight (background color) to the found text
            txtOutput.SelectionStart = occurrences(currentIndex)
            txtOutput.SelectionLength = searchTerm.Length

            txtOutput.SelectionBackColor = highlightColor
            txtOutput.ScrollToCaret()
            txtOutput.Focus()
        End If
        Me.Focus()
    End Sub

    ' Remove all custom highlights by resetting the background color
    Private Sub RemoveAllHighlights()
        ' Clear any previous highlights by resetting the background color
        txtOutput.SelectAll()
        txtOutput.SelectionBackColor = originalBackColor
        txtOutput.DeselectAll() ' Ensure the selection is cleared
    End Sub

    ' Close the search form
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        ' Before closing, remove all highlights and restore original formatting
        RemoveAllHighlights()
        Me.Close()
    End Sub

    Private Sub SearchForm_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
        RemoveAllHighlights()
    End Sub

    Private Sub SearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Icon
    End Sub
End Class
