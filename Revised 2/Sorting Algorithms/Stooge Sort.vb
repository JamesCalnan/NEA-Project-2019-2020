Module Stooge_Sort
    Sub stoogeSort(arr As List(Of Double), l As Integer, h As Integer, delay As Integer)
        If ExitCase() Then Exit Sub
        If l >= h Then Exit Sub
        If arr(l) > arr(h) Then
            Swap(arr, l, h)
            AnimateSort(arr, delay)
        End If
        If h - l + 1 > 2 Then
            Dim t As Integer = (h - l + 1) / 3
            stoogeSort(arr, l, h - t, delay)
            stoogeSort(arr, l + t, h, delay)
            stoogeSort(arr, l, h - t, delay)
            stoogeSort(arr, l + t, h, delay)
        End If
    End Sub
End Module
