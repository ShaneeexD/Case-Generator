Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Window

Public Class ListBoxChanged
    Public WithEvents listBoxMonitorTimer As New Timer()
    Private listBoxItemCounts As New Dictionary(Of ListBox, Integer)
    Private thisParent As Control
    Private thisButton As Button
    Public Sub ListBoxChanged(ByVal parent As Control, genButton As Button)
        thisParent = parent
        thisButton = genButton
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is ListBox Then
                Dim listBox As ListBox = DirectCast(ctrl, ListBox)

                If Not listBoxItemCounts.ContainsKey(listBox) Then
                    listBoxItemCounts.Add(listBox, listBox.Items.Count)
                End If
            ElseIf TypeOf ctrl Is TabControl Then
                Dim tabControl As TabControl = DirectCast(ctrl, TabControl)


                For Each tabPage As TabPage In tabControl.TabPages
                    ListBoxChanged(tabPage, genButton)
                Next
            End If

            If ctrl.HasChildren Then
                ListBoxChanged(ctrl, genButton)
            End If
        Next

        listBoxMonitorTimer.Interval = 1000
        listBoxMonitorTimer.Start()
    End Sub
    Private Sub listBoxMonitorTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles listBoxMonitorTimer.Tick
        ListBoxChanged(thisParent, thisButton)
        For Each listBox In listBoxItemCounts.Keys
            CheckListBoxItemCount(listBox)
        Next
    End Sub
    Public Sub CheckListBoxItemCount(ByVal listBox As ListBox)
        Dim previousCount As Integer = listBoxItemCounts(listBox)
        Dim currentCount As Integer = listBox.Items.Count

        If currentCount <> previousCount Then
            thisButton.PerformClick()
            listBoxItemCounts(listBox) = currentCount
        End If
    End Sub
    Public Sub ClearListBoxCounts()
        listBoxItemCounts.Clear()
    End Sub
End Class
