<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StringListForm
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
        tblStrings = New TableLayoutPanel()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        SuspendLayout()
        ' 
        ' tblStrings
        ' 
        tblStrings.AutoScroll = True
        tblStrings.BackColor = SystemColors.ActiveCaption
        tblStrings.ColumnCount = 2
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 23F))
        tblStrings.Location = New Point(14, 49)
        tblStrings.Margin = New Padding(3, 4, 3, 4)
        tblStrings.Name = "tblStrings"
        tblStrings.RowCount = 10
        tblStrings.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tblStrings.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tblStrings.RowStyles.Add(New RowStyle(SizeType.Absolute, 27F))
        tblStrings.RowStyles.Add(New RowStyle(SizeType.Absolute, 27F))
        tblStrings.RowStyles.Add(New RowStyle(SizeType.Absolute, 27F))
        tblStrings.RowStyles.Add(New RowStyle(SizeType.Absolute, 27F))
        tblStrings.RowStyles.Add(New RowStyle(SizeType.Absolute, 27F))
        tblStrings.RowStyles.Add(New RowStyle(SizeType.Absolute, 27F))
        tblStrings.RowStyles.Add(New RowStyle(SizeType.Absolute, 27F))
        tblStrings.RowStyles.Add(New RowStyle(SizeType.Absolute, 27F))
        tblStrings.Size = New Size(734, 269)
        tblStrings.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(160, 4)
        Label1.Name = "Label1"
        Label1.Size = New Size(49, 20)
        Label1.TabIndex = 1
        Label1.Text = "Block "
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(538, 4)
        Label2.Name = "Label2"
        Label2.Size = New Size(48, 20)
        Label2.TabIndex = 2
        Label2.Text = "String"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(98, 25)
        Label3.Name = "Label3"
        Label3.Size = New Size(192, 20)
        Label3.TabIndex = 3
        Label3.Text = "(LC To Copy - RC To Delete)"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(521, 24)
        Label4.Name = "Label4"
        Label4.Size = New Size(84, 20)
        Label4.TabIndex = 4
        Label4.Text = "(LC To Edit)"
        ' 
        ' StringListForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(759, 324)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(tblStrings)
        Margin = New Padding(3, 4, 3, 4)
        MaximumSize = New Size(777, 371)
        MinimumSize = New Size(777, 371)
        Name = "StringListForm"
        Text = "String List"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tblStrings As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
End Class
