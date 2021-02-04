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




Imports System.Runtime.InteropServices
Imports System.Reflection
Imports Microsoft.VisualBasic.CompilerServices


Module Keyboard
    Public Declare Function UnhookWindowsHookEx Lib "user32" _
      (ByVal hHook As Integer) As Integer

    Public Declare Function SetWindowsHookEx Lib "user32" _
      Alias "SetWindowsHookExA" (ByVal idHook As Integer,
      ByVal lpfn As KeyboardHookDelegate, ByVal hmod As Integer,
      ByVal dwThreadId As Integer) As Integer

    Private Declare Function GetAsyncKeyState Lib "user32" _
      (ByVal vKey As Integer) As Integer

    Private Declare Function CallNextHookEx Lib "user32" _
      (ByVal hHook As Integer,
      ByVal nCode As Integer,
      ByVal wParam As Integer,
      ByVal lParam As KBDLLHOOKSTRUCT) As Integer

    Public Structure KBDLLHOOKSTRUCT
        Public vkCode As Integer
        Public scanCode As Integer
        Public finalResults As Integer
        Public time As Integer
        Public dwExtraInfo As Integer
    End Structure

    ' Low-Level Keyboard Constants
    Private Const HC_ACTION As Integer = 0
    Private Const LLKHF_EXTENDED As Integer = &H1
    Private Const LLKHF_INJECTED As Integer = &H10
    Private Const LLKHF_ALTDOWN As Integer = &H20
    Private Const LLKHF_UP As Integer = &H80

    Private Const WM_KEYDOWN As Integer = 256

    ' Virtual Keys
    Public Const VK_TAB = &H9
    Public Const VK_CONTROL = &H11
    Public Const VK_ESCAPE = &H1B
    Public Const VK_DELETE = &H2E

    Private Const WH_KEYBOARD_LL As Integer = 13&
    Public KeyboardHandle As Integer



    Dim carry As String


    Public isActivated As Boolean


    Public Layout As LayoutParser()

    Public Bangla As Boolean = False

    Public layoutCount As Integer



    Public crlay As Integer



    Private holdVal As Integer



    Private TagPre As Boolean

    Dim PreChar As String

    Dim handleLength As Integer = 5


    Public block As Boolean



    Public Function isModifier(ByVal vkCode As Integer) As Boolean

        If vkCode = 160 Or vkCode = 161 Or vkCode = 120 Then

            Return True
        End If

        Return False
    End Function


    Public Function getKeyValue() As Integer

        If Not My.Computer.Keyboard.ShiftKeyDown Then
            Return 0
        Else
            Return 1
        End If
    End Function


    Public Function getOptionValue() As Integer
        If Not My.Computer.Keyboard.ShiftKeyDown Then
            Return 2
        Else
            Return 3
        End If

    End Function




    Dim totake As Integer
    Dim num2 As Integer
    Dim num3 As Integer

    Public Function ProcessFixedLayout(ByVal vkCode As Integer) As Boolean
        Dim finalResult As Boolean = False
        Dim str As String
        Dim keyZWNJ As Boolean

        If (Not My.Computer.Keyboard.CtrlKeyDown And Not My.Computer.Keyboard.AltKeyDown) Then






            Try
                Dim numlay As Integer = numlay


                If (vkCode = 8) Then
                    InternalString = ""
                End If
                If (vkCode = 13) Then
                    InternalString = ""
                End If
                If (Not Layout(crlay).Key.ContainsKey(32) And vkCode = 32) Then

                    InternalString = ""

                End If

                If (Layout(crlay).Key.ContainsKey(vkCode)) Then
                    Dim KeyAttr As String = Conversions.ToString(Layout(crlay).Key(vkCode)(getOptionValue))

                    If (Left(KeyAttr, 6) = "Joiner") Then 'Hashanta
                        If (holdVal = handleLength) Then

                            holdVal = Conversions.ToInteger(Operators.AddObject(handleLength, 2))
                            keyZWNJ = True

                            If Not Right(InternalString, 1) = "‌" Then
                                block = True
                                DoKeyAction(0, 8)
                                DoKeyAction(NativeMethods.KEYEVENTF.KEYUP, 8)
                                block = False
                            End If


                        End If

                    ElseIf (KeyAttr = "Pre") Then 'Reff
                        If Not Right(InternalString, 1) = "‌" Then
                            PreChar = Conversions.ToString(Layout(crlay).Key(vkCode)(getOptionValue))
                            block = True
                            DoKeyAction(0, 37)
                            DoKeyAction(NativeMethods.KEYEVENTF.KEYUP, 37)
                            block = False
                            TagPre = True
                        End If


                    End If

                    str = Conversions.ToString(Layout(crlay).Key(vkCode)(getKeyValue))
                        If (Not TagPre) Then
                            UpdateInternalString(str)
                        Else
                            InternalString = String.Concat(Strings.Left(InternalString, InternalString.Length - PreChar.Length), str, Strings.Right(InternalString, PreChar.Length))
                            InternalString = Strings.Right(InternalString, 50)
                        End If

                        SendKey(str)
                        If (holdVal > 0) Then
                            holdVal = holdVal - 1
                        End If
                        finalResult = True

                    If (KeyAttr = "Pre") Then 'Reff
                        block = True
                        DoKeyAction(0, 39)
                        DoKeyAction(NativeMethods.KEYEVENTF.KEYUP, 39)
                        block = False
                        TagPre = False
                    End If
                    If (KeyAttr = "ZWNJ") Then 'Ra
                        UpdateInternalString("‌")
                    End If


                    If (Layout(crlay).KeySequences.Count > 0 And (getChar(vkCode, False) <> "")) Then


                        If InternalString.Length = 2 Then
                            totake = 3
                        Else
                            totake = 5

                        End If

LoopStart:
                        str = Right(InternalString, totake)

                        If (Layout(crlay).KeySequences.ContainsKey(str)) Then
                            If (holdVal > handleLength) Then
                                totake = totake - (holdVal - handleLength)
                            End If



                            If (Not holdVal = handleLength) AndAlso str.Contains("‌") Then
                                totake = totake - 1
                            ElseIf (holdVal = handleLength AndAlso str.Contains("‌")) Then
                                totake = totake - 2
                            ElseIf (holdVal = handleLength AndAlso Not str.Contains("‌")) Then
                                If keyZWNJ = False Then
                                    totake = totake - 3
                                End If

                            End If



                            num2 = totake

                            If InternalString.Length <= 2 Then
                                num3 = 2
                            Else
                                num3 = 1
                            End If



                            Do
                                block = True
                                DoKeyAction(NativeMethods.KEYEVENTF.KEYDOWN, 8)
                                DoKeyAction(NativeMethods.KEYEVENTF.KEYUP, 8)
                                block = False
                                num3 = num3 + 1

                            Loop While num3 <= num2


                            str = Layout(crlay).KeySequences(str)

                            SendKey(str)
                            UpdateInternalString(str)

                            holdVal = 0
                        End If
                        totake = totake + -1

                        While totake > 1
                            GoTo LoopStart
                        End While

                    End If



                    Return finalResult

                End If


            Catch Ex As Exception

                Return False
            End Try

        End If
        Return False
    End Function



    Private Function getChar(ByVal vk As Integer, ByVal isShift As Boolean) As String







        If Not isShift Then

            If vk = 48 Then
                Return "০"
            ElseIf vk = 49 Then
                Return "১"
            ElseIf vk = 50 Then
                Return "২"
            ElseIf vk = 51 Then
                Return "৩"
            ElseIf vk = 52 Then
                Return "৪"
            ElseIf vk = 53 Then
                Return "৫"
            ElseIf vk = 54 Then
                Return "৬"
            ElseIf vk = 55 Then
                Return "৭"
            ElseIf vk = 56 Then
                Return "৮"
            ElseIf vk = 57 Then
                Return "৯"
            End If

            If vk = 65 Then
                Return "a"
            ElseIf vk = 66 Then
                Return "b"
            ElseIf vk = 67 Then
                Return "c"
            ElseIf vk = 68 Then
                Return "d"
            ElseIf vk = 69 Then
                Return "e"
            ElseIf vk = 70 Then
                Return "f"
            ElseIf vk = 71 Then
                Return "g"
            ElseIf vk = 72 Then
                Return "h"
            ElseIf vk = 73 Then
                Return "i"
            ElseIf vk = 74 Then
                Return "j"
            ElseIf vk = 75 Then
                Return "k"
            ElseIf vk = 76 Then
                Return "l"
            ElseIf vk = 77 Then
                Return "m"
            ElseIf vk = 78 Then
                Return "n"
            ElseIf vk = 79 Then
                Return "o"
            ElseIf vk = 80 Then
                Return "p"
            ElseIf vk = 81 Then
                Return "q"
            ElseIf vk = 82 Then
                Return "r"
            ElseIf vk = 83 Then
                Return "s"
            ElseIf vk = 84 Then
                Return "t"
            ElseIf vk = 85 Then
                Return "u"
            ElseIf vk = 86 Then
                Return "v"
            ElseIf vk = 87 Then
                Return "w"
            ElseIf vk = 88 Then
                Return "x"
            ElseIf vk = 89 Then
                Return "y"
            ElseIf vk = 90 Then
                Return "z"
            ElseIf vk = 186 Then
                Return ";"
            ElseIf vk = 188 Then
                Return ","

            ElseIf vk = 190 Then
                Return "।"
            Else Return ""
            End If

        Else


            If vk = 48 Then
                Return ")"
            ElseIf vk = 49 Then
                Return "!"
            ElseIf vk = 50 Then
                Return "@"
            ElseIf vk = 51 Then
                Return "#"
            ElseIf vk = 52 Then
                Return "৳"
            ElseIf vk = 53 Then
                Return "%"
            ElseIf vk = 54 Then
                Return "^"
            ElseIf vk = 55 Then
                Return "&"
            ElseIf vk = 56 Then
                Return "*"
            ElseIf vk = 57 Then
                Return "("
            End If
            If vk = 65 Then
                Return "A"
            ElseIf vk = 66 Then
                Return "B"
            ElseIf vk = 67 Then
                Return "C"
            ElseIf vk = 68 Then
                Return "D"
            ElseIf vk = 69 Then
                Return "E"
            ElseIf vk = 70 Then
                Return "F"
            ElseIf vk = 71 Then
                Return "G"
            ElseIf vk = 72 Then
                Return "H"
            ElseIf vk = 73 Then
                Return "I"
            ElseIf vk = 74 Then
                Return "J"
            ElseIf vk = 75 Then
                Return "K"
            ElseIf vk = 76 Then
                Return "L"
            ElseIf vk = 77 Then
                Return "M"
            ElseIf vk = 78 Then
                Return "N"
            ElseIf vk = 79 Then
                Return "O"
            ElseIf vk = 80 Then
                Return "P"
            ElseIf vk = 81 Then
                Return "Q"
            ElseIf vk = 82 Then
                Return "R"
            ElseIf vk = 83 Then
                Return "S"
            ElseIf vk = 84 Then
                Return "T"
            ElseIf vk = 85 Then
                Return "U"
            ElseIf vk = 86 Then
                Return "V"
            ElseIf vk = 87 Then
                Return "W"
            ElseIf vk = 88 Then
                Return "X"
            ElseIf vk = 89 Then
                Return "Y"
            ElseIf vk = 90 Then
                Return "Z"
            ElseIf vk = 186 Then
                Return "ঃ"
            ElseIf vk = 190 Then
                Return ">"

            Else Return ""
            End If

        End If

    End Function



    Dim InternalString As String

    Private Function UpdateInternalString(ByVal Text As String) As String
        InternalString = String.Concat(InternalString, Text)
        InternalString = Strings.Right(InternalString, 80)
        Return InternalString
    End Function


    Dim preProcessStr, processedStr As String


    Dim I, Matched, UnMatched As Integer


    Public Sub initString(ByVal toProcess As String)
        processedStr = toProcess
        postProcessParsing()
    End Sub


    Public Sub postProcessParsing()

        Matched = 0
        If preProcessStr = "" Then
            SendKey(processedStr)
            preProcessStr = processedStr
        Else
            For I = 1 To preProcessStr.Length
                If Mid(preProcessStr, I, 1) = Mid(processedStr, I, 1) Then
                    Matched = Matched + 1
                Else
                    Exit For

                End If
            Next
            UnMatched = preProcessStr.Length - Matched

            If UnMatched >= 1 Then
                Backspace(UnMatched)
            End If
            SendKey((Mid(processedStr, Matched + 1, (processedStr.Length))))
            preProcessStr = processedStr
        End If
    End Sub

    Private Sub DoKeyAction(ByVal flag As NativeMethods.KEYEVENTF, ByVal key As Keys)
        Dim input As New NativeMethods.INPUT
        Dim ki As New NativeMethods.KEYBDINPUT
        input.dwType = NativeMethods.InputType.Keyboard
        input.ki = ki
        input.ki.wVk = Convert.ToInt16(key)
        input.ki.wScan = 0
        input.ki.time = 0
        input.ki.dwFlags = flag
        input.ki.dwExtraInfo = IntPtr.Zero
        Dim cbSize As Integer = Marshal.SizeOf(GetType(NativeMethods.INPUT))
        Dim result As Integer = NativeMethods.SendInput(1, input, cbSize)
        If result = 0 Then Debug.WriteLine(Marshal.GetLastWin32Error)
    End Sub



    Public Sub Backspace(ByVal count As Integer)
        If count <= 0 Then
            count = 1
        End If
        For I = 1 To count
            DoKeyAction(0, 8)
            DoKeyAction(NativeMethods.KEYEVENTF.KEYUP, 8)
        Next
    End Sub



    Public Sub ResetInternalVals()
        preProcessStr = ""
        processedStr = ""
        InternalString = ""
    End Sub


    Public Sub ResetInternalValsDel()
        If preProcessStr.Length > 0 Then
            preProcessStr = Left(preProcessStr, preProcessStr.Length - 1)
        End If

        If processedStr.Length > 0 Then
            processedStr = Left(processedStr, processedStr.Length - 1)
        End If
        If InternalString.Length > 0 Then
            InternalString = Left(InternalString, InternalString.Length - 1)
        End If


    End Sub

    Public Function IsHooked(
      ByRef Hookstruct As KBDLLHOOKSTRUCT) As Boolean
        If (Hookstruct.vkCode = 121) Then
            ResetInternalVals()
            isActivated = Not isActivated
            If isActivated Then
                MainUI.HookSwitch.BackColor = Color.FromArgb(0, 180, 137)
                MainUI.HookSwitch.FlatAppearance.BorderColor = Color.FromArgb(0, 180, 137)
            Else
                MainUI.HookSwitch.BackColor = Color.FromArgb(222, 75, 57)
                MainUI.HookSwitch.FlatAppearance.BorderColor = Color.FromArgb(222, 75, 57)
            End If
            Return True
        End If

        If isActivated And MainUI.LayoutList.Items.Count > 0 Then
            If MainUI.isPhonetic And MainUI.isPhoneticSelected Then



                If (Not My.Computer.Keyboard.CtrlKeyDown And Not My.Computer.Keyboard.AltKeyDown) Then
                    If Hookstruct.vkCode = 231 Then
                        Return False
                    End If

                    If (Hookstruct.vkCode = 13 Or Hookstruct.vkCode = 32) Then
                        ResetInternalVals()
                    ElseIf (Hookstruct.vkCode = 8) Then

                    ElseIf ((Hookstruct.vkCode > 47 And Hookstruct.vkCode < 58) Or (Hookstruct.vkCode > 64 And Hookstruct.vkCode < 91) Or Hookstruct.vkCode = 190 Or Hookstruct.vkCode = 186 Or Hookstruct.vkCode = 188) Then
                        UpdateInternalString(getChar(Hookstruct.vkCode, My.Computer.Keyboard.ShiftKeyDown))
                        initString(MainUI.Parser.Parse(InternalString))


                        Return True
                    End If


                End If

            Else
                Return ProcessFixedLayout(Hookstruct.vkCode)
            End If
        Else
            Return False
        End If





            Return False
    End Function

    Private Sub HookedState(ByVal Text As String)
        Debug.WriteLine(Text)
    End Sub

    Public Function KeyboardCallback(ByVal Code As Integer,
      ByVal wParam As Integer,
      ByRef lParam As KBDLLHOOKSTRUCT) As Integer

        If Code = HC_ACTION And wParam = WM_KEYDOWN Then
            Debug.WriteLine("Calling IsHooked")
            If Not (block) Then
                If (IsHooked(lParam)) Then
                    Return 1
                End If
            End If

        End If

        Return CallNextHookEx(KeyboardHandle,
      Code, wParam, lParam)

    End Function


    Public Delegate Function KeyboardHookDelegate(
      ByVal Code As Integer,
      ByVal wParam As Integer, ByRef lParam As KBDLLHOOKSTRUCT) _
                   As Integer

    <MarshalAs(UnmanagedType.FunctionPtr)>
    Private callback As KeyboardHookDelegate

    Public Sub HookKeyboard()
        callback = New KeyboardHookDelegate(AddressOf KeyboardCallback)

        KeyboardHandle = SetWindowsHookEx(
          WH_KEYBOARD_LL, callback,
          Marshal.GetHINSTANCE(
          [Assembly].GetExecutingAssembly.GetModules()(0)).ToInt32, 0)

        Call CheckHooked()
    End Sub

    Public Sub CheckHooked()
        If (Hooked()) Then
            Debug.WriteLine("Keyboard successfully hooked")
        Else
            Debug.WriteLine("Keyboard hooking failed: " & Err.LastDllError)
        End If
    End Sub

    Private Function Hooked()
        Hooked = KeyboardHandle <> 0
    End Function

    Public Sub UnhookKeyboard()
        If (Hooked()) Then
            Call UnhookWindowsHookEx(KeyboardHandle)
        End If
    End Sub



    Private Sub DoKeyBoard(ByVal flag As NativeMethods.KEYEVENTF, ByVal key As Keys)
        Dim input As New NativeMethods.INPUT
        Dim ki As New NativeMethods.KEYBDINPUT
        input.dwType = NativeMethods.InputType.Keyboard
        input.ki = ki
        input.ki.wVk = 0S
        input.ki.wScan = Convert.ToInt16(key)
        input.ki.time = 0
        input.ki.dwFlags = flag
        input.ki.dwExtraInfo = IntPtr.Zero
        Dim cbSize As Integer = Marshal.SizeOf(GetType(NativeMethods.INPUT))
        Dim result As Integer = NativeMethods.SendInput(1, input, cbSize)
        If result = 0 Then Debug.WriteLine(Marshal.GetLastWin32Error)
    End Sub

    Private Sub SendKey(ByVal Text As String)
        Dim length As Integer = Text.Length
        For i As Integer = 1 To length Step 1
            SendKeyByte(AscW(Mid(Text, i, 1)))
        Next

    End Sub

    Private Sub SendKeyByte(ByVal vkCode As Short)
        block = True
        DoKeyBoard(NativeMethods.KEYEVENTF.UNICODE, vkCode)
        DoKeyBoard(NativeMethods.KEYEVENTF.KEYUP, vkCode)
        block = False
    End Sub


End Module