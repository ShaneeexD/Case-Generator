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
            ' Read all lines from the CSV file
            Dim lines() As String = IO.File.ReadAllLines(filePath)

            ' Clear existing rows in the TableLayoutPanel
            tblStrings.Controls.Clear()
            tblStrings.RowCount = 0

            Dim tableSize As Size
            tableSize.Width = 642
            tableSize.Height = 1000

            tblStrings.MaximumSize = tableSize
            tblStrings.AutoSize = True
            tblStrings.AutoSizeMode = AutoSizeMode.GrowOnly

            For Each line As String In lines
                ' Split the line by commas
                Dim parts() As String = line.Split(","c)
                If parts.Length >= 6 Then ' Ensure there are at least 6 parts
                    Dim guid As String = parts(0).Trim() ' The GUID is the first part
                    Dim text As String = parts(2).Trim() ' The text is the third part

                    ' Ensure we have space for the new row
                    tblStrings.RowCount += 1

                    ' Create new labels for the GUID and text
                    Dim lblGuid As New Label() With {
                    .Text = guid,
                    .AutoSize = True
                }
                    AddHandler lblGuid.Click, Sub(s, e) CopyToClipboard(lblGuid.Text)

                    Dim lblText As New Label() With {
                    .Text = text,
                    .AutoSize = True
                }

                    ' Add the labels to the TableLayoutPanel
                    tblStrings.Controls.Add(lblGuid, 0, tblStrings.RowCount - 1) ' Column 0 for GUID
                    tblStrings.Controls.Add(lblText, 1, tblStrings.RowCount - 1) ' Column 1 for text
                End If
            Next

            ' Set column styles to ensure consistent width and allow for vertical sizing
            tblStrings.ColumnStyles.Clear()
            tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 300)) ' Fixed width for GUID column
            tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize)) ' AutoSize for text column

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message & vbCrLf & vbCrLf & "You most likely have not made any strings yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetupTable()
        ' Example: Setting up the TableLayoutPanel
        Dim numRows As Integer = 20 ' Set to the number of rows you want
        Dim numCols As Integer = 2 ' Set to the number of columns you want

        ' Clear any existing styles
        tblStrings.ColumnStyles.Clear()
        tblStrings.RowStyles.Clear()

        ' Set equal column widths (e.g., 50% of total width for each column)
        For i As Integer = 0 To numCols - 1
            tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100 / numCols))
        Next

        ' Set equal row heights (e.g., 30 pixels for each row)
        For i As Integer = 0 To numRows - 1
            tblStrings.RowStyles.Add(New RowStyle(SizeType.Absolute, 18))
        Next

        tblStrings.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
    End Sub

    Private Sub CopyToClipboard(text As String)
        ' Copy the provided text to the clipboard
        Clipboard.SetText(text)
        MessageBox.Show("Block copied to clipboard: " & text, "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class