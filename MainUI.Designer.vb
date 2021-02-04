<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainUI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainUI))
        Me.HookSwitch = New System.Windows.Forms.Button()
        Me.buttonInfo = New System.Windows.Forms.Button()
        Me.buttonMinimize = New System.Windows.Forms.Button()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.iconPlacer = New System.Windows.Forms.Button()
        Me.buttonClose = New System.Windows.Forms.Button()
        Me.layoutChooser = New System.Windows.Forms.Button()
        Me.LayoutList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SuspendLayout()
        '
        'HookSwitch
        '
        Me.HookSwitch.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.HookSwitch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.HookSwitch.FlatAppearance.BorderSize = 0
        Me.HookSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.HookSwitch.Location = New System.Drawing.Point(166, 1)
        Me.HookSwitch.Name = "HookSwitch"
        Me.HookSwitch.Size = New System.Drawing.Size(4, 25)
        Me.HookSwitch.TabIndex = 0
        Me.HookSwitch.UseVisualStyleBackColor = False
        '
        'buttonInfo
        '
        Me.buttonInfo.BackColor = System.Drawing.Color.Transparent
        Me.buttonInfo.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.buttonInfo.FlatAppearance.BorderSize = 0
        Me.buttonInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonInfo.ForeColor = System.Drawing.SystemColors.Desktop
        Me.buttonInfo.Location = New System.Drawing.Point(172, 1)
        Me.buttonInfo.Name = "buttonInfo"
        Me.buttonInfo.Size = New System.Drawing.Size(36, 25)
        Me.buttonInfo.TabIndex = 3
        Me.buttonInfo.Text = "i"
        Me.buttonInfo.UseVisualStyleBackColor = False
        '
        'buttonMinimize
        '
        Me.buttonMinimize.BackColor = System.Drawing.Color.Transparent
        Me.buttonMinimize.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.buttonMinimize.FlatAppearance.BorderSize = 0
        Me.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonMinimize.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonMinimize.ForeColor = System.Drawing.SystemColors.Desktop
        Me.buttonMinimize.Location = New System.Drawing.Point(208, 1)
        Me.buttonMinimize.Name = "buttonMinimize"
        Me.buttonMinimize.Size = New System.Drawing.Size(36, 25)
        Me.buttonMinimize.TabIndex = 6
        Me.buttonMinimize.Text = "-"
        Me.buttonMinimize.UseVisualStyleBackColor = False
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Text = "NotifyIcon1"
        '
        'iconPlacer
        '
        Me.iconPlacer.BackColor = System.Drawing.Color.Transparent
        Me.iconPlacer.BackgroundImage = CType(resources.GetObject("iconPlacer.BackgroundImage"), System.Drawing.Image)
        Me.iconPlacer.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.iconPlacer.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.iconPlacer.FlatAppearance.BorderSize = 0
        Me.iconPlacer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.iconPlacer.Location = New System.Drawing.Point(2, 1)
        Me.iconPlacer.Name = "iconPlacer"
        Me.iconPlacer.Size = New System.Drawing.Size(28, 25)
        Me.iconPlacer.TabIndex = 4
        Me.iconPlacer.UseVisualStyleBackColor = False
        '
        'buttonClose
        '
        Me.buttonClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.buttonClose.BackgroundImage = Global.CodepotroBornoLite.My.Resources.Resources.close_icon
        Me.buttonClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.buttonClose.FlatAppearance.BorderSize = 0
        Me.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonClose.Location = New System.Drawing.Point(246, 1)
        Me.buttonClose.Name = "buttonClose"
        Me.buttonClose.Size = New System.Drawing.Size(30, 25)
        Me.buttonClose.TabIndex = 2
        Me.buttonClose.UseVisualStyleBackColor = False
        '
        'layoutChooser
        '
        Me.layoutChooser.BackColor = System.Drawing.Color.Transparent
        Me.layoutChooser.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.layoutChooser.FlatAppearance.BorderSize = 0
        Me.layoutChooser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.layoutChooser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.layoutChooser.ForeColor = System.Drawing.SystemColors.Desktop
        Me.layoutChooser.Location = New System.Drawing.Point(32, 1)
        Me.layoutChooser.Name = "layoutChooser"
        Me.layoutChooser.Size = New System.Drawing.Size(134, 25)
        Me.layoutChooser.TabIndex = 9
        Me.layoutChooser.Text = "Click to Select Layout"
        Me.layoutChooser.UseVisualStyleBackColor = False
        '
        'LayoutList
        '
        Me.LayoutList.Name = "LayoutList"
        Me.LayoutList.Size = New System.Drawing.Size(61, 4)
        '
        'MainUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(277, 25)
        Me.Controls.Add(Me.layoutChooser)
        Me.Controls.Add(Me.buttonMinimize)
        Me.Controls.Add(Me.iconPlacer)
        Me.Controls.Add(Me.buttonClose)
        Me.Controls.Add(Me.buttonInfo)
        Me.Controls.Add(Me.HookSwitch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "MainUI"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Borno Lite"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents HookSwitch As Button
    Friend WithEvents buttonClose As Button
    Friend WithEvents buttonInfo As Button
    Friend WithEvents iconPlacer As Button
    Friend WithEvents buttonMinimize As Button
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents layoutChooser As Button
    Friend WithEvents LayoutList As ContextMenuStrip
End Class
