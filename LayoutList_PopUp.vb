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

Public Class LayoutList_PopUp
    Inherits ToolStripRenderer

    Protected Overloads Overrides Sub OnRenderMenuItemBackground(e As ToolStripItemRenderEventArgs)
        Dim xa = New Point(0, 0)
        Dim rc As New Rectangle(xa, e.Item.Size)
        Dim c As Color
        If e.Item.Selected Then

            c = Color.FromArgb(201, 222, 245)
            e.Item.ForeColor = Color.FromArgb(20, 20, 20)
        Else
            c = Color.FromArgb(246, 246, 246)
            e.Item.ForeColor = Color.FromArgb(20, 20, 20)


        End If

        Using brush As New SolidBrush(c)
            e.Graphics.FillRectangle(brush, rc)


        End Using
    End Sub


    Protected Overloads Overrides Sub OnRenderItemCheck(e As ToolStripItemImageRenderEventArgs)

        Dim xa = New Point(0, 0)
        Dim rc As New Rectangle(xa, e.Item.Size)
        Dim c As Color

        c = Color.FromArgb(253, 253, 253)
        e.Item.ForeColor = Color.FromArgb(20, 20, 20)





        Using brush As New SolidBrush(c)
            e.Graphics.FillRectangle(brush, rc)


        End Using

    End Sub


    Protected Overloads Overrides Sub OnRenderToolStripBorder(e As ToolStripRenderEventArgs)

    End Sub
End Class
