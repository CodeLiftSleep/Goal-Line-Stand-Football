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
        Me.components = New System.ComponentModel.Container()
        Me.CreatTbl = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.FootballDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FootballDataSet = New Football2.FootballDataSet()
        Me.QBDepth = New System.Windows.Forms.Label()
        Me.RBDepth = New System.Windows.Forms.Label()
        Me.WRDepth = New System.Windows.Forms.Label()
        Me.TEDepth = New System.Windows.Forms.Label()
        Me.OTDepth = New System.Windows.Forms.Label()
        Me.CDepth = New System.Windows.Forms.Label()
        Me.OGDepth = New System.Windows.Forms.Label()
        Me.DEDepth = New System.Windows.Forms.Label()
        Me.DTDepth = New System.Windows.Forms.Label()
        Me.OLBDepth = New System.Windows.Forms.Label()
        Me.ILBDepth = New System.Windows.Forms.Label()
        Me.CBDepth = New System.Windows.Forms.Label()
        Me.SFDepth = New System.Windows.Forms.Label()
        Me.KDepth = New System.Windows.Forms.Label()
        Me.PDepth = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FootballDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FootballDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CreatTbl
        '
        Me.CreatTbl.Location = New System.Drawing.Point(12, 12)
        Me.CreatTbl.Name = "CreatTbl"
        Me.CreatTbl.Size = New System.Drawing.Size(108, 23)
        Me.CreatTbl.TabIndex = 0
        Me.CreatTbl.Text = "Create Draft Class"
        Me.CreatTbl.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.DataSource = Me.FootballDataSetBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 401)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1390, 406)
        Me.DataGridView1.TabIndex = 1
        '
        'FootballDataSetBindingSource
        '
        Me.FootballDataSetBindingSource.DataSource = Me.FootballDataSet
        Me.FootballDataSetBindingSource.Position = 0
        '
        'FootballDataSet
        '
        Me.FootballDataSet.DataSetName = "FootballDataSet"
        Me.FootballDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'QBDepth
        '
        Me.QBDepth.AutoSize = True
        Me.QBDepth.Location = New System.Drawing.Point(14, 53)
        Me.QBDepth.Name = "QBDepth"
        Me.QBDepth.Size = New System.Drawing.Size(57, 13)
        Me.QBDepth.TabIndex = 2
        Me.QBDepth.Text = "QB Depth:"
        '
        'RBDepth
        '
        Me.RBDepth.AutoSize = True
        Me.RBDepth.Location = New System.Drawing.Point(14, 77)
        Me.RBDepth.Name = "RBDepth"
        Me.RBDepth.Size = New System.Drawing.Size(57, 13)
        Me.RBDepth.TabIndex = 3
        Me.RBDepth.Text = "RB Depth:"
        '
        'WRDepth
        '
        Me.WRDepth.AutoSize = True
        Me.WRDepth.Location = New System.Drawing.Point(14, 100)
        Me.WRDepth.Name = "WRDepth"
        Me.WRDepth.Size = New System.Drawing.Size(61, 13)
        Me.WRDepth.TabIndex = 4
        Me.WRDepth.Text = "WR Depth:"
        '
        'TEDepth
        '
        Me.TEDepth.AutoSize = True
        Me.TEDepth.Location = New System.Drawing.Point(14, 124)
        Me.TEDepth.Name = "TEDepth"
        Me.TEDepth.Size = New System.Drawing.Size(56, 13)
        Me.TEDepth.TabIndex = 5
        Me.TEDepth.Text = "TE Depth:"
        '
        'OTDepth
        '
        Me.OTDepth.AutoSize = True
        Me.OTDepth.Location = New System.Drawing.Point(14, 147)
        Me.OTDepth.Name = "OTDepth"
        Me.OTDepth.Size = New System.Drawing.Size(57, 13)
        Me.OTDepth.TabIndex = 6
        Me.OTDepth.Text = "OT Depth:"
        '
        'CDepth
        '
        Me.CDepth.AutoSize = True
        Me.CDepth.Location = New System.Drawing.Point(14, 171)
        Me.CDepth.Name = "CDepth"
        Me.CDepth.Size = New System.Drawing.Size(49, 13)
        Me.CDepth.TabIndex = 7
        Me.CDepth.Text = "C Depth:"
        '
        'OGDepth
        '
        Me.OGDepth.AutoSize = True
        Me.OGDepth.Location = New System.Drawing.Point(14, 195)
        Me.OGDepth.Name = "OGDepth"
        Me.OGDepth.Size = New System.Drawing.Size(58, 13)
        Me.OGDepth.TabIndex = 8
        Me.OGDepth.Text = "OG Depth:"
        '
        'DEDepth
        '
        Me.DEDepth.AutoSize = True
        Me.DEDepth.Location = New System.Drawing.Point(14, 218)
        Me.DEDepth.Name = "DEDepth"
        Me.DEDepth.Size = New System.Drawing.Size(57, 13)
        Me.DEDepth.TabIndex = 9
        Me.DEDepth.Text = "DE Depth:"
        Me.DEDepth.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DTDepth
        '
        Me.DTDepth.AutoSize = True
        Me.DTDepth.Location = New System.Drawing.Point(14, 242)
        Me.DTDepth.Name = "DTDepth"
        Me.DTDepth.Size = New System.Drawing.Size(57, 13)
        Me.DTDepth.TabIndex = 10
        Me.DTDepth.Text = "DT Depth:"
        '
        'OLBDepth
        '
        Me.OLBDepth.AutoSize = True
        Me.OLBDepth.Location = New System.Drawing.Point(14, 264)
        Me.OLBDepth.Name = "OLBDepth"
        Me.OLBDepth.Size = New System.Drawing.Size(63, 13)
        Me.OLBDepth.TabIndex = 11
        Me.OLBDepth.Text = "OLB Depth:"
        '
        'ILBDepth
        '
        Me.ILBDepth.AutoSize = True
        Me.ILBDepth.Location = New System.Drawing.Point(14, 289)
        Me.ILBDepth.Name = "ILBDepth"
        Me.ILBDepth.Size = New System.Drawing.Size(58, 13)
        Me.ILBDepth.TabIndex = 12
        Me.ILBDepth.Text = "ILB Depth:"
        '
        'CBDepth
        '
        Me.CBDepth.AutoSize = True
        Me.CBDepth.Location = New System.Drawing.Point(14, 311)
        Me.CBDepth.Name = "CBDepth"
        Me.CBDepth.Size = New System.Drawing.Size(56, 13)
        Me.CBDepth.TabIndex = 13
        Me.CBDepth.Text = "CB Depth:"
        '
        'SFDepth
        '
        Me.SFDepth.AutoSize = True
        Me.SFDepth.Location = New System.Drawing.Point(14, 333)
        Me.SFDepth.Name = "SFDepth"
        Me.SFDepth.Size = New System.Drawing.Size(55, 13)
        Me.SFDepth.TabIndex = 14
        Me.SFDepth.Text = "SF Depth:"
        '
        'KDepth
        '
        Me.KDepth.AutoSize = True
        Me.KDepth.Location = New System.Drawing.Point(14, 355)
        Me.KDepth.Name = "KDepth"
        Me.KDepth.Size = New System.Drawing.Size(49, 13)
        Me.KDepth.TabIndex = 15
        Me.KDepth.Text = "K Depth:"
        '
        'PDepth
        '
        Me.PDepth.AutoSize = True
        Me.PDepth.Location = New System.Drawing.Point(14, 377)
        Me.PDepth.Name = "PDepth"
        Me.PDepth.Size = New System.Drawing.Size(49, 13)
        Me.PDepth.TabIndex = 16
        Me.PDepth.Text = "P Depth:"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1390, 807)
        Me.Controls.Add(Me.PDepth)
        Me.Controls.Add(Me.KDepth)
        Me.Controls.Add(Me.SFDepth)
        Me.Controls.Add(Me.CBDepth)
        Me.Controls.Add(Me.ILBDepth)
        Me.Controls.Add(Me.OLBDepth)
        Me.Controls.Add(Me.DTDepth)
        Me.Controls.Add(Me.DEDepth)
        Me.Controls.Add(Me.OGDepth)
        Me.Controls.Add(Me.CDepth)
        Me.Controls.Add(Me.OTDepth)
        Me.Controls.Add(Me.TEDepth)
        Me.Controls.Add(Me.WRDepth)
        Me.Controls.Add(Me.RBDepth)
        Me.Controls.Add(Me.QBDepth)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.CreatTbl)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FootballDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FootballDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CreatTbl As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents FootballDataSetBindingSource As BindingSource
    Friend WithEvents FootballDataSet As FootballDataSet
    Friend WithEvents QBDepth As Label
    Friend WithEvents RBDepth As Label
    Friend WithEvents WRDepth As Label
    Friend WithEvents TEDepth As Label
    Friend WithEvents OTDepth As Label
    Friend WithEvents CDepth As Label
    Friend WithEvents OGDepth As Label
    Friend WithEvents DEDepth As Label
    Friend WithEvents DTDepth As Label
    Friend WithEvents OLBDepth As Label
    Friend WithEvents ILBDepth As Label
    Friend WithEvents CBDepth As Label
    Friend WithEvents SFDepth As Label
    Friend WithEvents KDepth As Label
    Friend WithEvents PDepth As Label
    'Friend WithEvents PbpTableAdapter1 As Football2.FootballTableAdapters.PBPTableAdapter

End Class
