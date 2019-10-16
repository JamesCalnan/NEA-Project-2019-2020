Module Comb_Sort
    Function getNextGap(gap As Integer)
        gap = (gap * 10) / 14
        If gap < 1 Then
            Return 1
        End If
        Return gap
    End Function
    Sub CombSort(arr As List(Of Double), delay As Integer)
        Dim n = arr.Count - 1
        Dim gap = n
        Dim swapped = True
        While Not gap = 1 Or swapped
            gap = getNextGap(gap)
            swapped = False
            For i = 0 To n - gap Step 1
                If arr(i) > arr(i + gap) Then
                    Swap(arr, i, i + gap)
                    swapped = True
                End If
            Next
            AnimateSort(arr, delay)
        End While
    End Sub
End Module
