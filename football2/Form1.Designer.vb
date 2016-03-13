<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CreatTbl = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CreatTbl
        '
        Me.CreatTbl.Location = New System.Drawing.Point(96, 123)
        Me.CreatTbl.Name = "CreatTbl"
        Me.CreatTbl.Size = New System.Drawing.Size(75, 23)
        Me.CreatTbl.TabIndex = 0
        Me.CreatTbl.Text = "Create Table"
        Me.CreatTbl.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.CreatTbl)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CreatTbl As Button
    'Friend WithEvents PbpTableAdapter1 As Football2.FootballTableAdapters.PBPTableAdapter

End Class
