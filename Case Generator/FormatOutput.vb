Imports System.Text
Imports System.Drawing

Public Class FormatOutput
    Public Sub FormatJson(controlName As RichTextBox)
        Dim originalText As String = controlName.Text
        Dim formattedText As New StringBuilder()

        For Each line As String In originalText.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
            Dim trimmedLine As String = line.Trim()

            If Not String.IsNullOrWhiteSpace(trimmedLine) Then
                If trimmedLine.StartsWith("{") OrElse trimmedLine.StartsWith("}") Then
                    formattedText.AppendLine(line)
                ElseIf trimmedLine.Contains(":") Then
                    Dim parts As String() = trimmedLine.Split(New Char() {":"c}, 2)

                    formattedText.Append(parts(0).Trim() & ": ")
                    formattedText.Append(parts(1).Trim() & Environment.NewLine)
                Else
                    formattedText.AppendLine(line)
                End If
            Else
                formattedText.AppendLine(line)
            End If
        Next

        controlName.Clear()
        controlName.AppendText(formattedText.ToString())

        ApplyColors(controlName)
    End Sub
    Public Sub ApplyColors(controlName As RichTextBox)
        controlName.SelectAll()
        controlName.SelectionColor = Color.Black

        Dim lines As String() = controlName.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

        For Each line As String In lines
            Dim currentIndex As Integer = 0

            While currentIndex < line.Length
                Dim currentChar As Char = line(currentIndex)

                If currentChar = "{"c OrElse currentChar = "}"c Then
                    controlName.SelectionStart = controlName.GetFirstCharIndexFromLine(Array.IndexOf(lines, line)) + currentIndex
                    controlName.SelectionLength = 1
                    controlName.SelectionColor = Color.Blue
                ElseIf currentChar = """"c Then
                    Dim endQuoteIndex As Integer = currentIndex + 1
                    While endQuoteIndex < line.Length AndAlso line(endQuoteIndex) <> """"c
                        endQuoteIndex += 1
                    End While

                    If endQuoteIndex < line.Length Then
                        Dim isKey As Boolean = (endQuoteIndex + 1 < line.Length AndAlso line(endQuoteIndex + 1) = ":"c)
                        controlName.SelectionStart = controlName.GetFirstCharIndexFromLine(Array.IndexOf(lines, line)) + currentIndex
                        controlName.SelectionLength = endQuoteIndex - currentIndex + 1
                        controlName.SelectionColor = If(isKey, Color.Purple, Color.Red)
                        currentIndex = endQuoteIndex
                    End If
                ElseIf Char.IsDigit(currentChar) Then
                    Dim numberStart As Integer = currentIndex
                    While currentIndex < line.Length AndAlso Char.IsDigit(line(currentIndex))
                        currentIndex += 1
                    End While
                    controlName.SelectionStart = controlName.GetFirstCharIndexFromLine(Array.IndexOf(lines, line)) + numberStart
                    controlName.SelectionLength = currentIndex - numberStart
                    controlName.SelectionColor = Color.DarkOrange
                    currentIndex -= 1
                ElseIf String.Compare(line.Substring(currentIndex, Math.Min(4, line.Length - currentIndex)), "true", StringComparison.OrdinalIgnoreCase) = 0 OrElse
                       String.Compare(line.Substring(currentIndex, Math.Min(5, line.Length - currentIndex)), "false", StringComparison.OrdinalIgnoreCase) = 0 OrElse
                       String.Compare(line.Substring(currentIndex, Math.Min(4, line.Length - currentIndex)), "null", StringComparison.OrdinalIgnoreCase) = 0 Then
                    Dim boolStart As Integer = currentIndex
                    While currentIndex < line.Length AndAlso Not Char.IsWhiteSpace(line(currentIndex)) AndAlso line(currentIndex) <> ","c AndAlso line(currentIndex) <> "}"c
                        currentIndex += 1
                    End While
                    controlName.SelectionStart = controlName.GetFirstCharIndexFromLine(Array.IndexOf(lines, line)) + boolStart
                    controlName.SelectionLength = currentIndex - boolStart
                    controlName.SelectionColor = Color.Green
                    currentIndex -= 1
                End If

                currentIndex += 1
            End While
        Next

        controlName.SelectionStart = 0
        controlName.SelectionLength = 0
        controlName.SelectionColor = Color.Black
    End Sub
End Class
