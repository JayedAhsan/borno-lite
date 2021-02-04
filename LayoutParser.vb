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

Imports Microsoft.VisualBasic.CompilerServices
Imports System.Xml
Imports System.Collections.ObjectModel
Imports Microsoft.VisualBasic.FileIO


Public Class LayoutParser

    Public ID As Integer
    Public Name As String
    Public Path As String



    Public Key As Dictionary(Of Integer, ArrayList)

    Public KeySequences As Dictionary(Of String, String)



    Public Sub New()
        Me.Key = New Dictionary(Of Integer, ArrayList)()
        Me.KeySequences = New Dictionary(Of String, String)()
    End Sub



    Public Shared Function SearchForLayouts() As Boolean
        Dim readOnlyCollection As ReadOnlyCollection(Of String)
        Dim lcount As Integer



        Dim Lpath As String = String.Concat(Application.StartupPath & "\data\keyboard layouts\")

        If Not My.Computer.FileSystem.DirectoryExists(Lpath) Then
            Return False
        Else
            readOnlyCollection = My.Computer.FileSystem.GetFiles(Lpath, SearchOption.SearchAllSubDirectories,
           New String() {"*.kbl"})
            layoutCount = readOnlyCollection.Count - 1
            Layout = New LayoutParser(layoutCount) {}
            lcount = 0
            Do While lcount <= layoutCount
                Layout(lcount) = New LayoutParser
                Layout(lcount).Init(readOnlyCollection(lcount))
                Layout(lcount).[Path] = readOnlyCollection(lcount)

                Dim dynItem As New ToolStripMenuItem() With {.Text = Layout(lcount).Name, .Name = lcount, .Tag = lcount}
                MainUI.LayoutList.Items.Add(dynItem)
                AddHandler dynItem.Click, AddressOf MainUI.mnuItem_Clicked
                Layout(lcount).ID = lcount
                lcount += 1






            Loop
            Return True

        End If
    End Function



    'Got reading idea from: https://www.codeproject.com/Questions/696096/Load-A-Specific-XML-File

    Public Sub Init(ByVal Path As String)
        Dim xmlReaderSetting As XmlReaderSettings = New XmlReaderSettings() With
        {
            .CheckCharacters = True,
            .CloseInput = True
        }
        Using xmlReader As XmlReader = XmlReader.Create(Path, xmlReaderSetting)
            Try
                Try
                    xmlReader.ReadStartElement("Layout")
                    Me.Name = xmlReader.ReadElementString("Name")
                    xmlReader.ReadStartElement("Keys")
                    Do
                        Dim arrayLists As ArrayList = New ArrayList()
                        If (xmlReader.NodeType = XmlNodeType.Element Or xmlReader.NodeType = XmlNodeType.Attribute) Then
                            Dim [integer] As Integer = Conversions.ToInteger(xmlReader.GetAttribute("vkCode"))
                            arrayLists.Add(xmlReader.GetAttribute("N_O"))
                            arrayLists.Add(xmlReader.GetAttribute("S_O"))
                            xmlReader.Read()
                            arrayLists.Insert(0, xmlReader.ReadElementString("Normal"))
                            arrayLists.Insert(1, xmlReader.ReadElementString("Shift"))
                            Me.Key.Add([integer], arrayLists)
                        End If
                        xmlReader.Read()
                    Loop While Operators.CompareString(xmlReader.Name, "Keys", False) <> 0
                    xmlReader.ReadEndElement()
                    xmlReader.ReadStartElement("Combinations")
                    Do
                        If (xmlReader.NodeType = XmlNodeType.Element) Then
                            xmlReader.Read()
                            Dim sequence As String = xmlReader.ReadElementString("Input")
                            Dim output As String = xmlReader.ReadElementString("Output")
                            Me.KeySequences.Add(sequence, output)
                        End If
                        xmlReader.Read()
                    Loop While Operators.CompareString(xmlReader.Name, "Combinations", False) <> 0
                Catch exception As Exception

                End Try
            Finally
                xmlReader.Close()
            End Try
        End Using
    End Sub


End Class

