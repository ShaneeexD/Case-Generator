<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchForm
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
        lblStatus = New Label()
        btnNext = New Button()
        btnPrevious = New Button()
        btnClose = New Button()
        SuspendLayout()
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(12, 9)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(41, 15)
        lblStatus.TabIndex = 0
        lblStatus.Text = "Label1"
        ' 
        ' btnNext
        ' 
        btnNext.Location = New Point(12, 45)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(75, 23)
        btnNext.TabIndex = 1
        btnNext.Text = "Next"
        btnNext.UseVisualStyleBackColor = True
        ' 
        ' btnPrevious
        ' 
        btnPrevious.Location = New Point(93, 45)
        btnPrevious.Name = "btnPrevious"
        btnPrevious.Size = New Size(75, 23)
        btnPrevious.TabIndex = 2
        btnPrevious.Text = "Previous"
        btnPrevious.UseVisualStyleBackColor = True
        ' 
        ' btnClose
        ' 
        btnClose.Location = New Point(174, 45)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(75, 23)
        btnClose.TabIndex = 3
        btnClose.Text = "Close"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' SearchForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(276, 77)
        Controls.Add(btnClose)
        Controls.Add(btnPrevious)
        Controls.Add(btnNext)
        Controls.Add(lblStatus)
        Name = "SearchForm"
        Text = "Search Results"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblStatus As Label
    Friend WithEvents btnNext As Button
    Friend WithEvents btnPrevious As Button
    Friend WithEvents btnClose As Button
End Class
