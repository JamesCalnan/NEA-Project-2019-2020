Module Comb_Sort
    Function getNextGap(gap As Integer)
        gap = gap * 10 / 14
        If gap < 1 Then Return 1
        Return gap
    End Function
    Sub CombSort(a As List(Of Double), delay As Integer)
        Dim n = a.Count - 1
        Dim gap = n
        Dim swapped = True
        While Not gap = 1 Or swapped
            If ExitCase() Then Exit Sub
            gap = getNextGap(gap)
            swapped = False
            For i = 0 To n - gap
                If a(i) > a(i + gap) Then
                    Swap(a, i, i + gap)
                    swapped = True
                    AnimateSort(a, delay)
                End If
            Next
        End While
    End Sub
End Module
