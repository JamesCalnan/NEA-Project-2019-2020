Module Stooge_Sort
    Sub stoogeSort(ByRef a As List(Of Double), l As Integer, h As Integer, Optional delay As Integer = 0)

        If l >= h Then Return

        If a(l) > a(h) Then
            Dim t = a(l)
            a(l) = a(h)
            a(h) = t
            AnimateSort(a)
        End If
        If h - l + 1 > 2 Then
            Dim t = Int((h - l + 1) / 3)
            stoogeSort(a, l, h - t)
            stoogeSort(a, l + t, h)
            stoogeSort(a, l, h - t)
        End If

    End Sub
End Module
