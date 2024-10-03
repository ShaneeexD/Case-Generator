Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Window

Public Class CustomEventHandlers
    Private controlEventHandlers As New Dictionary(Of Control, [Delegate])
    Private tabControlEventHandlers As New Dictionary(Of TabControl, ControlEventHandler)

    Private button As Button
    Public Sub AddValueChangedHandlers(ByVal parent As Control, genButton As Control)
        button = genButton
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is MenuStrip Then
                Continue For
            End If

            If TypeOf ctrl Is TextBox Then
                Dim handler As EventHandler = AddressOf ControlValueChanged
                AddHandler DirectCast(ctrl, TextBox).TextChanged, handler
                controlEventHandlers(ctrl) = handler
            ElseIf TypeOf ctrl Is CheckBox Then
                Dim handler As EventHandler = AddressOf ControlValueChanged
                AddHandler DirectCast(ctrl, CheckBox).CheckedChanged, handler
                controlEventHandlers(ctrl) = handler
            ElseIf TypeOf ctrl Is RadioButton Then
                Dim handler As EventHandler = AddressOf ControlValueChanged
                AddHandler DirectCast(ctrl, RadioButton).CheckedChanged, handler
                controlEventHandlers(ctrl) = handler
            ElseIf TypeOf ctrl Is ComboBox Then
                Dim handler As EventHandler = AddressOf ControlValueChanged
                AddHandler DirectCast(ctrl, ComboBox).SelectedIndexChanged, handler
                AddHandler DirectCast(ctrl, ComboBox).TextChanged, handler
                controlEventHandlers(ctrl) = handler
            ElseIf TypeOf ctrl Is NumericUpDown Then
                Dim handler As EventHandler = AddressOf ControlValueChanged
                AddHandler DirectCast(ctrl, NumericUpDown).ValueChanged, handler
                controlEventHandlers(ctrl) = handler
            ElseIf TypeOf ctrl Is TabControl Then
                Dim tabControl As TabControl = DirectCast(ctrl, TabControl)
                Dim tabControlHandler As ControlEventHandler = AddressOf TabPageAddedHandler
                AddHandler tabControl.ControlAdded, tabControlHandler
                tabControlEventHandlers(tabControl) = tabControlHandler

                Dim tabIndexChangedHandler As EventHandler = AddressOf ControlValueChanged
                AddHandler tabControl.SelectedIndexChanged, tabIndexChangedHandler
                controlEventHandlers(tabControl) = tabIndexChangedHandler


                For Each tabPage As TabPage In tabControl.TabPages
                    AddValueChangedHandlers(tabPage, button)
                Next
            ElseIf TypeOf ctrl Is DateTimePicker Then
                Dim handler As EventHandler = AddressOf ControlValueChanged
                AddHandler DirectCast(ctrl, DateTimePicker).ValueChanged, handler
                controlEventHandlers(ctrl) = handler
            End If

            If ctrl.HasChildren Then
                AddValueChangedHandlers(ctrl, button)
            End If
        Next
    End Sub
    Public Sub ControlValueChanged(sender As Object, e As EventArgs)
        button.PerformClick()
    End Sub
    Public Sub RemoveValueChangedHandlers(ByVal parent As Control)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is MenuStrip Then
                Continue For
            End If

            If controlEventHandlers.ContainsKey(ctrl) Then
                If TypeOf ctrl Is TextBox Then
                    RemoveHandler DirectCast(ctrl, TextBox).TextChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                ElseIf TypeOf ctrl Is CheckBox Then
                    RemoveHandler DirectCast(ctrl, CheckBox).CheckedChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                ElseIf TypeOf ctrl Is RadioButton Then
                    RemoveHandler DirectCast(ctrl, RadioButton).CheckedChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                ElseIf TypeOf ctrl Is ComboBox Then
                    RemoveHandler DirectCast(ctrl, ComboBox).SelectedIndexChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                    RemoveHandler DirectCast(ctrl, ComboBox).TextChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                ElseIf TypeOf ctrl Is NumericUpDown Then
                    RemoveHandler DirectCast(ctrl, NumericUpDown).ValueChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                ElseIf TypeOf ctrl Is DateTimePicker Then
                    RemoveHandler DirectCast(ctrl, DateTimePicker).ValueChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                ElseIf TypeOf ctrl Is TabControl Then
                    Dim tabControl As TabControl = DirectCast(ctrl, TabControl)
                    If tabControlEventHandlers.ContainsKey(tabControl) Then
                        RemoveHandler tabControl.ControlAdded, DirectCast(tabControlEventHandlers(tabControl), ControlEventHandler)
                        tabControlEventHandlers.Remove(tabControl)
                    End If
                    If controlEventHandlers.ContainsKey(tabControl) Then
                        RemoveHandler tabControl.TabIndexChanged, DirectCast(controlEventHandlers(tabControl), EventHandler)
                        tabControlEventHandlers.Remove(tabControl)
                    End If

                    For Each tabPage As TabPage In tabControl.TabPages
                        RemoveValueChangedHandlers(tabPage)
                    Next
                End If

                controlEventHandlers.Remove(ctrl)
            End If

            If ctrl.HasChildren Then
                RemoveValueChangedHandlers(ctrl)
            End If
        Next
    End Sub
    Private Sub TabPageAddedHandler(ByVal sender As Object, ByVal e As ControlEventArgs)
        If TypeOf e.Control Is TabPage Then
            Dim newTabPage As TabPage = DirectCast(e.Control, TabPage)

            AddValueChangedHandlers(newTabPage, button)
        End If
    End Sub
End Class
