Module Comb_Sort
    Function getNextGap(gap As Integer)
        gap = gap * 10 / 14
        If gap < 1 Then Return 1
        Return gap
    End Function
    Sub CombSort(arr As List(Of Double), delay As Integer)
        Dim n = arr.Count - 1
        Dim gap = n
        Dim swapped = True
        While Not gap = 1 Or swapped
            If ExitCase() Then Exit Sub
            gap = getNextGap(gap)
            swapped = False
            For i = 0 To n - gap Step 1
                If arr(i) > arr(i + gap) Then
                    Swap(arr, i, i + gap)
                    swapped = True
                    AnimateSort(arr, delay)
                End If
            Next
        End While
    End Sub
End Module
