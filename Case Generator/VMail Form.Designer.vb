<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VMail_Form
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
        rtbString = New RichTextBox()
        btnConfirm = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(68, 15)
        Label1.TabIndex = 0
        Label1.Text = "New String:"
        ' 
        ' rtbString
        ' 
        rtbString.Location = New Point(12, 27)
        rtbString.Name = "rtbString"
        rtbString.Size = New Size(409, 122)
        rtbString.TabIndex = 1
        rtbString.Text = ""
        ' 
        ' btnConfirm
        ' 
        btnConfirm.Location = New Point(346, 155)
        btnConfirm.Name = "btnConfirm"
        btnConfirm.Size = New Size(75, 23)
        btnConfirm.TabIndex = 2
        btnConfirm.Text = "Confirm"
        btnConfirm.UseVisualStyleBackColor = True
        ' 
        ' VMail_Form
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(433, 189)
        Controls.Add(btnConfirm)
        Controls.Add(rtbString)
        Controls.Add(Label1)
        MaximumSize = New Size(449, 228)
        MinimumSize = New Size(449, 228)
        Name = "VMail_Form"
        Text = "New String"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents rtbString As RichTextBox
    Friend WithEvents btnConfirm As Button
End Class
