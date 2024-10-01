<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HelpForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        lblLink = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(9, 12)
        Label1.Name = "Label1"
        Label1.Size = New Size(337, 40)
        Label1.TabIndex = 0
        Label1.Text = "Make sure you have PiePieOnline's mod installed, " & vbCrLf & "which you can get by clicking"
        ' 
        ' lblLink
        ' 
        lblLink.AutoSize = True
        lblLink.Font = New Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point)
        lblLink.ForeColor = Color.MidnightBlue
        lblLink.Location = New Point(193, 32)
        lblLink.Name = "lblLink"
        lblLink.Size = New Size(41, 20)
        lblLink.TabIndex = 1
        lblLink.Text = "here."
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(9, 69)
        Label2.Name = "Label2"
        Label2.Size = New Size(262, 40)
        Label2.TabIndex = 2
        Label2.Text = "Hover over labels to see what they do." & vbCrLf & "(ToolTips enabled)" & vbCrLf
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(9, 139)
        Label3.Name = "Label3"
        Label3.Size = New Size(341, 60)
        Label3.TabIndex = 3
        Label3.Text = "For custom strings, go to Tools > New String, " & vbCrLf & "you can then open the string list from the Tools " & vbCrLf & "menu and copy the block into the desired textbox."
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point)
        Label4.Location = New Point(94, 115)
        Label4.Name = "Label4"
        Label4.Size = New Size(146, 20)
        Label4.TabIndex = 4
        Label4.Text = "Strings (V-Mails Etc)"
        ' 
        ' HelpForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(321, 203)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(lblLink)
        Controls.Add(Label1)
        Margin = New Padding(3, 4, 3, 4)
        MaximumSize = New Size(339, 250)
        MinimumSize = New Size(339, 250)
        Name = "HelpForm"
        Text = "Help"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents lblLink As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
End Class
