﻿

Imports System.Runtime.InteropServices

Public Class NativeMethods

    <DllImport("user32.dll", SetLastError:=True)>
    Friend Shared Function SendInput(ByVal cInputs As Int32, ByRef pInputs As INPUT, ByVal cbSize As Int32) As Int32
    End Function

    <StructLayout(LayoutKind.Explicit, Pack:=1, Size:=28)>
    Friend Structure INPUT
        <FieldOffset(0)> Public dwType As InputType
        <FieldOffset(4)> Public ki As KEYBDINPUT
        <FieldOffset(4)> Public hi As HARDWAREINPUT
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Friend Structure KEYBDINPUT
        Public wVk As Int16
        Public wScan As Int16
        Public dwFlags As KEYEVENTF
        Public time As Int32
        Public dwExtraInfo As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Friend Structure HARDWAREINPUT
        Public uMsg As Int32
        Public wParamL As Int16
        Public wParamH As Int16
    End Structure

    Friend Enum InputType As Integer
        Keyboard = 1
        Hardware = 2
    End Enum

    <Flags()>
    Friend Enum KEYEVENTF As Integer
        KEYDOWN = 0
        EXTENDEDKEY = 1
        KEYUP = 2
        [UNICODE] = 4
        SCANCODE = 8
    End Enum


    <Flags()>
    Friend Enum MOUSEEVENTF As Integer
        MOVE = &H1
        LEFTDOWN = &H2
        LEFTUP = &H4
        RIGHTDOWN = &H8
        RIGHTUP = &H10
        MIDDLEDOWN = &H20
        MIDDLEUP = &H40
        XDOWN = &H80
        XUP = &H100
        VIRTUALDESK = &H400
        WHEEL = &H800
        ABSOLUTE = &H8000
    End Enum


End Class
