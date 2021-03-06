﻿Module Gnome_Sort
    Sub gnomeSort(a As List(Of Double), Optional delay As Integer = 0)
        Dim pos = 0
        While pos < a.Count
            If ExitCase() Then Exit Sub
            If pos = 0 OrElse a(pos) >= a(pos - 1) Then
                pos += 1
            Else
                AnimateSort(a, delay)
                Swap(a, pos, pos - 1)
                pos -= 1
            End If
        End While
        AnimateSort(a)
    End Sub
End Module
