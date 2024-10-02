Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class StringListForm
    Private file As String
    Dim folderNameNew As String = Form1.tempFilePath

    Private Sub StringListForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Icon
        SetupTable()

        If Form1.lastCreated = True Then
            Dim directoryPath As String = IO.Path.Combine(folderNameNew, "DDSContent")
            file = IO.Path.Combine(directoryPath, "strings", "english", "dds", "dds.blocks.csv")
            LoadGuidsAndTextsFromCsv(file)
        ElseIf Form1.lastOpened = True Then
            Dim directoryPath As String = System.IO.Path.GetDirectoryName(Form1.tempFilePath)
            file = IO.Path.Combine(directoryPath, "DDSContent", "strings", "english", "dds", "dds.blocks.csv")
            LoadGuidsAndTextsFromCsv(file)
        End If
    End Sub
    Private Sub LoadGuidsAndTextsFromCsv(filePath As String)
        Try
            Dim lines() As String = IO.File.ReadAllLines(filePath)

            tblStrings.Controls.Clear()
            tblStrings.RowCount = 0

            Dim tableSize As Size
            tableSize.Width = 642
            tableSize.Height = 1000

            tblStrings.MaximumSize = tableSize
            tblStrings.AutoSize = True
            tblStrings.AutoSizeMode = AutoSizeMode.GrowOnly

            For Each line As String In lines
                Dim parts() As String = line.Split(","c)
                If parts.Length >= 6 Then
                    Dim guid As String = parts(0).Trim()
                    Dim text As String = parts(2).Trim()

                    tblStrings.RowCount += 1

                    Dim lblGuid As New Label() With {
                    .Text = guid,
                    .AutoSize = True
                }
                    AddHandler lblGuid.MouseClick, Sub(s, e)
                                                       If e.Button = MouseButtons.Right Then
                                                           DeleteBlock(lblGuid.Text)
                                                       End If
                                                       If e.Button = MouseButtons.Left Then
                                                           CopyToClipboard(lblGuid.Text)
                                                       End If
                                                   End Sub

                    Dim lblText As New Label() With {
                    .Text = text,
                    .AutoSize = True
                }
                    AddHandler lblText.MouseClick, Sub(s, e)
                                                       If e.Button = MouseButtons.Left Then
                                                           EditString(lblText.Text)
                                                       End If
                                                   End Sub
                    tblStrings.Controls.Add(lblGuid, 0, tblStrings.RowCount - 1)
                    tblStrings.Controls.Add(lblText, 1, tblStrings.RowCount - 1)
                End If
            Next

            tblStrings.ColumnStyles.Clear()
            tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 300))
            tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message & vbCrLf & vbCrLf & "You most likely have not made any strings yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetupTable()
        Dim numRows As Integer = 20
        Dim numCols As Integer = 2

        tblStrings.ColumnStyles.Clear()
        tblStrings.RowStyles.Clear()

        For i As Integer = 0 To numCols - 1
            tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100 / numCols))
        Next

        For i As Integer = 0 To numRows - 1
            tblStrings.RowStyles.Add(New RowStyle(SizeType.Absolute, 18))
        Next

        tblStrings.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
    End Sub

    Private Sub CopyToClipboard(text As String)
        Clipboard.SetText(text)
        MessageBox.Show("Block copied to clipboard: " & text, "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub DeleteBlock(guid As String)
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this block?", "Delete Block", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.Yes Then
            Dim file As String
            Dim blockFile As String

            If Form1.lastCreated = True Then
                Dim directoryPath As String = IO.Path.Combine(folderNameNew, "DDSContent")
                blockFile = IO.Path.Combine(directoryPath, "DDS", "Blocks", guid & ".BLOCK")
                file = IO.Path.Combine(directoryPath, "strings", "english", "dds", "dds.blocks.csv")
            ElseIf Form1.lastOpened = True Then
                Dim directoryPath As String = System.IO.Path.GetDirectoryName(Form1.tempFilePath)
                blockFile = IO.Path.Combine(directoryPath, "DDSContent", "DDS", "Blocks", guid & ".BLOCK")
                file = IO.Path.Combine(directoryPath, "DDSContent", "strings", "english", "dds", "dds.blocks.csv")
            End If

            If IO.File.Exists(file) Then
                Dim lines As List(Of String) = IO.File.ReadAllLines(file).ToList()

                Dim lineToRemove As String = lines.FirstOrDefault(Function(line) line.StartsWith(guid))

                If lineToRemove IsNot Nothing Then
                    lines.Remove(lineToRemove)
                    IO.File.WriteAllLines(file, lines)

                    If IO.File.Exists(blockFile) Then
                        IO.File.Delete(blockFile)
                    Else
                        MessageBox.Show("Associated BLOCK file not found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                    Dim rowIndex As Integer = -1
                    For i As Integer = 0 To tblStrings.RowCount - 1
                        Dim lblGuid As Label = CType(tblStrings.GetControlFromPosition(0, i), Label)
                        If lblGuid IsNot Nothing AndAlso lblGuid.Text = guid Then
                            rowIndex = i
                            Exit For
                        End If
                    Next

                    If rowIndex >= 0 Then
                        tblStrings.Controls.Remove(tblStrings.GetControlFromPosition(0, rowIndex))
                        tblStrings.Controls.Remove(tblStrings.GetControlFromPosition(1, rowIndex))
                        tblStrings.RowCount -= 1
                    End If
                    LoadGuidsAndTextsFromCsv(file)
                Else
                    MessageBox.Show("Block not found in the CSV file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("CSV file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub EditString(originalText As String)
        Dim input As String = InputBox("Edit the string:", "Edit String", originalText)

        If Not String.IsNullOrEmpty(input) Then
            Dim file As String

            If Form1.lastCreated = True Then
                Dim directoryPath As String = IO.Path.Combine(folderNameNew, "DDSContent")
                file = IO.Path.Combine(directoryPath, "strings", "english", "dds", "dds.blocks.csv")
            ElseIf Form1.lastOpened = True Then
                Dim directoryPath As String = System.IO.Path.GetDirectoryName(Form1.tempFilePath)
                file = IO.Path.Combine(directoryPath, "DDSContent", "strings", "english", "dds", "dds.blocks.csv")
            End If

            If IO.File.Exists(file) Then
                Dim lines As List(Of String) = IO.File.ReadAllLines(file).ToList()

                Dim lineIndex As Integer = lines.FindIndex(Function(line) line.Contains(originalText))

                If lineIndex >= 0 Then
                    Dim columns As String() = lines(lineIndex).Split(","c)

                    If columns.Length > 2 Then
                        columns(2) = input
                        lines(lineIndex) = String.Join(",", columns)

                        IO.File.WriteAllLines(file, lines)

                        UpdateDisplayedStringInTable(originalText, input)

                    Else
                        MessageBox.Show("Invalid CSV format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("Original string not found in the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("CSV file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub


    Private Sub UpdateDisplayedStringInTable(originalText As String, newText As String)
        For Each control As Control In tblStrings.Controls
            If TypeOf control Is Label Then
                Dim lbl As Label = CType(control, Label)
                If lbl.Text = originalText Then
                    lbl.Text = newText
                    Exit For
                End If
            End If
        Next
    End Sub

End Class