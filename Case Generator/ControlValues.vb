Public Class ControlValues
    Private defaultValues As New Dictionary(Of String, Object)
    Private Sub CaptureControlDefaultValue(ctrl As Control)
        If TypeOf ctrl Is TextBox Then
            defaultValues(ctrl.Name) = CType(ctrl, TextBox).Text
        ElseIf TypeOf ctrl Is ComboBox Then
            defaultValues(ctrl.Name) = CType(ctrl, ComboBox).SelectedIndex
        ElseIf TypeOf ctrl Is CheckBox Then
            defaultValues(ctrl.Name) = CType(ctrl, CheckBox).Checked
        ElseIf TypeOf ctrl Is RadioButton Then
            defaultValues(ctrl.Name) = CType(ctrl, RadioButton).Checked
        ElseIf TypeOf ctrl Is ListBox Then
            defaultValues(ctrl.Name) = CType(ctrl, ListBox).SelectedIndex
        ElseIf TypeOf ctrl Is NumericUpDown Then
            defaultValues(ctrl.Name) = CType(ctrl, NumericUpDown).Value
        ElseIf TypeOf ctrl Is RichTextBox Then
            defaultValues(ctrl.Name) = CType(ctrl, RichTextBox).Text
        End If

        If TypeOf ctrl Is TabControl Then
            For Each tabPage As TabPage In CType(ctrl, TabControl).TabPages
                For Each childCtrl As Control In tabPage.Controls
                    CaptureControlDefaultValue(childCtrl)
                Next
            Next
        ElseIf TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is Panel Then
            For Each childCtrl As Control In ctrl.Controls
                CaptureControlDefaultValue(childCtrl)
            Next
        End If
    End Sub
    Public Sub CaptureDefaultValues(control)
        For Each ctrl As Control In control.Controls
            CaptureControlDefaultValue(ctrl)
        Next
    End Sub
    Public Sub ResetToDefaultValues(control)
        For Each ctrl As Control In control.Controls
            ResetControlToDefault(ctrl)
        Next
    End Sub
    Public Sub ResetControlToDefault(ctrl As Control)
        If defaultValues.ContainsKey(ctrl.Name) Then
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = defaultValues(ctrl.Name).ToString()
            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).SelectedIndex = CInt(defaultValues(ctrl.Name))
            ElseIf TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Checked = CBool(defaultValues(ctrl.Name))
            ElseIf TypeOf ctrl Is RadioButton Then
                CType(ctrl, RadioButton).Checked = CBool(defaultValues(ctrl.Name))
            ElseIf TypeOf ctrl Is ListBox Then
                CType(ctrl, ListBox).SelectedIndex = CInt(defaultValues(ctrl.Name))
            ElseIf TypeOf ctrl Is NumericUpDown Then
                CType(ctrl, NumericUpDown).Value = CDec(defaultValues(ctrl.Name))
            ElseIf TypeOf ctrl Is RichTextBox Then
                CType(ctrl, RichTextBox).Text = defaultValues(ctrl.Name).ToString()
            End If
        End If

        Dim targetTabControl As TabControl = CType(ctrl.Controls.Find("tabControlCase", True).FirstOrDefault(), TabControl)
        If targetTabControl IsNot Nothing AndAlso targetTabControl.TabPages.Count > 1 Then
            For i As Integer = targetTabControl.TabPages.Count - 1 To 1 Step -1
                targetTabControl.TabPages.RemoveAt(i)
            Next
        End If

        If TypeOf ctrl Is TabControl Then
            For Each tabPage As TabPage In CType(ctrl, TabControl).TabPages
                For Each childCtrl As Control In tabPage.Controls
                    ResetControlToDefault(childCtrl)
                Next
            Next
        ElseIf TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is Panel Then
            For Each childCtrl As Control In ctrl.Controls
                ResetControlToDefault(childCtrl)
            Next
        End If
    End Sub
End Class
