Public Class ToolTips
    Public Sub SetTooltip(controlName As String, tooltipText As String)
        Dim control As Control = Form1.Controls.Find(controlName, True).FirstOrDefault()
        If control IsNot Nothing Then
            Form1.toolTip.SetToolTip(control, tooltipText)
        End If
    End Sub
    Public Sub SetTooltipMOLeads(controlName As String, tooltipText As String)
        Dim tabPage As TabPage = Form1.tabControlCase.TabPages.Cast(Of TabPage)().FirstOrDefault(Function(tp) tp.Text.Contains("MOlead Entry"))

        If tabPage IsNot Nothing Then
            Dim label As Label = tabPage.Controls.Find(controlName, True).FirstOrDefault()
            If label IsNot Nothing Then
                Form1.toolTip.SetToolTip(label, tooltipText)
            End If
        End If
    End Sub

    Public Sub SetAllTooltips()
        SetTooltip("lblCaseName", $"Your case name.")
        SetTooltip("lblDesc", $"The description of your case.")
        SetTooltip("lblCopyFromMain", $"Copy all values from another preset. You can override these.")
        SetTooltip("lblDisabled", $"Enable/Disable the case from being run.")
        SetTooltipMOLeads("lblName", $"Name of the MOLead.")
    End Sub
End Class