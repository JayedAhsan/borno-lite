'
'
'   This program Is free software; you can redistribute it And/Or modify
'   it under the terms Of the GNU General Public License As published by
'   the Free Software Foundation; either version 3 Of the License, Or
'   (at your option) any later version.
'
'   Software distributed under the License Is distributed On an "AS IS"
'   basis, WITHOUT WARRANTY Of ANY KIND, either express Or implied. See the
'   License for the specific language governing rights And limitations
'   under the License.
'
'   The Initial Developer of this Code is Jayed Ahsan Saad
'   Copyright© Jayed Ahsan Saad. All Rights Reserved
'
'


Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices


Public Class MainUI



    Public Const WM_NCLBUTTONDOWN As Integer = &HA1
    Public Const HT_CAPTION As Integer = &H2

    Public Shared isPhonetic As Boolean = False
    Public Shared isPhoneticSelected As Boolean = False

    <DllImportAttribute("user32.dll")>
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    <DllImportAttribute("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean
    End Function


    Public Shared Parser As Object




    Private Sub writeBornoLayout(ByVal layout As String)

        Dim filePath = Path.Combine(Application.StartupPath & "\data\keyboard layouts\", layout & ".kbl")

        If Not File.Exists(filePath) Then
            Directory.CreateDirectory(Path.Combine(Application.StartupPath & "\data\keyboard layouts\"))
            File.WriteAllBytes(filePath, My.Resources.Borno) 'match with layout variable
        End If


    End Sub


    Private Sub writeBornoEncodingLayout(ByVal layout As String)

        Dim filePath = Path.Combine(Application.StartupPath & "\data\keyboard layouts\", layout & ".kbl")

        If Not File.Exists(filePath) Then
            Directory.CreateDirectory(Path.Combine(Application.StartupPath & "\data\keyboard layouts\"))
            File.WriteAllBytes(filePath, My.Resources.Borno_Encoding) 'match with layout variable
        End If


    End Sub

    Private Sub writeNationalLayout(ByVal layout As String)

        Dim filePath = Path.Combine(Application.StartupPath & "\data\keyboard layouts\", layout & ".kbl")

        If Not File.Exists(filePath) Then
            Directory.CreateDirectory(Path.Combine(Application.StartupPath & "\data\keyboard layouts\"))
            File.WriteAllBytes(filePath, My.Resources.National) 'match with layout variable
        End If


    End Sub


    Private Sub MainUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        writeBornoLayout("Borno")
        writeBornoEncodingLayout("Borno Encoding")
        writeNationalLayout("National")
        Dim filePath = Path.Combine(Application.StartupPath & "\data\lib\", "libcpphonetic.dll")
        If File.Exists(filePath) Then
            Dim oType As Type
            Dim oAssembly As Assembly
            oAssembly = Assembly.LoadFrom(filePath)
            oType = oAssembly.GetType("libPhoneticParser.Parser")
            Parser = Activator.CreateInstance(oType)
            isPhonetic = True
            Dim dynItem As New ToolStripMenuItem() With {.Text = " Borno Phonetic", .Name = "bp", .Tag = 9999}
            AddHandler dynItem.Click, AddressOf mnuItem_Clicked
            LayoutList.Items.Add(dynItem)
        Else
            isPhonetic = False
        End If


        'Tray Icon
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)
        NotifyIcon1.Icon = Icon
        NotifyIcon1.Visible = False


        'Topbar UI
        Width = 277
        Height = 27
        Top = 0
        Left = (Screen.PrimaryScreen.Bounds.Width / 2) - (Width / 2) 'Center Topbar
        TopMost = True
        HookKeyboard()
        LayoutParser.SearchForLayouts()



    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)

        MyBase.OnPaintBackground(e)
        'Topbar Border
        BackColor = Color.FromArgb(242, 242, 244)
        Dim rect As New Rectangle(0, 0, Me.ClientSize.Width - 1, Me.ClientSize.Height - 1)
        e.Graphics.DrawRectangle(New Pen(Color.FromArgb(230, 230, 230)), rect)
    End Sub



    Private Sub buttonClose_Click(sender As Object, e As EventArgs) Handles buttonClose.Click
        Application.Exit()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles buttonInfo.Click
        MessageBox.Show("Instructions:" & Chr(13) & Chr(10) & "Press F10 to toggle Bangla mode." & Chr(13) & Chr(10) & "Press Ctrl + Backspace to delete word." & Chr(13) & Chr(10) & "visit: https://codepotro.com for the keymap." _
                        & Chr(13) & Chr(10) & Chr(13) & Chr(10) &
                        "Warning:" & Chr(13) & Chr(10) &
                        "You are not allowed to modify, adapt, translate, reverse engineer, decompile, disassemble or attempt to discover the source code of the library (libcpphonetic.dll)." _
                        & Chr(13) & Chr(10) & Chr(13) & Chr(10) &
                        "If you want to use the library (libcpphonetic.dll) in your application please contact with Codepotro" &
                        Chr(13) & Chr(10) & Chr(13) & Chr(10) & "For more information visit: https://codepotro.com" _
                        & Chr(13) & Chr(10) & Chr(13) & Chr(10) &
                        "About:" & Chr(13) & Chr(10) &
                        "Made for the experts by Codepotro in Bangladesh" &
                          Chr(13) & Chr(10) &
                         "Source code: https://codepotro.com/borno-lite/",
    "About Borno Phonteic Lite", MessageBoxButtons.OK, MessageBoxIcon.Information, 0)
    End Sub

    Private Sub iconPlacer_MouseDown(sender As Object, e As MouseEventArgs) Handles iconPlacer.MouseDown
        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0)
        End If
    End Sub

    Private Sub buttonMinimize_Click(sender As Object, e As EventArgs) Handles buttonMinimize.Click
        WindowState = FormWindowState.Minimized
    End Sub


    Public Sub mnuItem_Clicked(sender As Object, e As EventArgs)
        LayoutList.Hide()
        Dim item As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If item IsNot Nothing Then
            ResetInternalVals()
            crlay = item.Tag

            If item.Tag = 9999 Then
                isPhoneticSelected = True
            Else
                isPhoneticSelected = False
            End If



            layoutChooser.Text = item.Text
        End If
    End Sub

    Private Sub MainUI_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            NotifyIcon1.Visible = True
            Me.Hide()
            NotifyIcon1.BalloonTipText = "Borno Lite is running on Tray"
            NotifyIcon1.ShowBalloonTip(10)
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub

    Private Sub MainUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        NotifyIcon1.Visible = False
    End Sub




    Private Sub layoutChooser_Click(sender As Object, e As EventArgs) Handles layoutChooser.Click
        LayoutList.Show(CType(sender, Control), New Point(0, layoutChooser.Height))
    End Sub

    Private Sub LayoutList_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles LayoutList.Opening
        LayoutList.Renderer = New LayoutList_PopUp
    End Sub
End Class
