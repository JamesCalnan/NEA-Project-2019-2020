Module SlowSort
    Sub slowsort(a As List(Of Double), i As Integer, j As Integer, Optional delay As Integer = 0)
        If i >= j Then Exit Sub
        'AnimateSort(a, delay)
        Dim m = (i + j) \ 2
        slowsort(a, i, m, delay)
        slowsort(a, m + 1, j, delay)
        If a(j) < a(m) Then
            Swap(a, j, m)
            AnimateSort(a, delay)
        End If
        slowsort(a, i, j - 1, delay)
    End Sub
End Module
