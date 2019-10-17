Module QuickSortAlgorithm
    Function Quicksort(a As List(Of Double), lo As Integer, hi As Integer, Optional delay As Integer = 0)
        'Initial call would be quicksort(list,0,list.count-1)
        If lo < hi Then
            Dim p As Integer = Partition(a, lo, hi, delay)
            Quicksort(a, lo, p - 1)
            Quicksort(a, p + 1, hi)
        End If
        AnimateSort(a)
        Return a
    End Function
    Function Partition(a As List(Of Double), lo As Integer, hi As Integer, delay As Integer)
        Dim pivot As Double = a(hi)
        Dim i As Integer = lo
        For j = lo To hi
            If a(j) < pivot Then
                AnimateSort(a, delay)
                Swap(a, i, j)
                i += 1
            End If
        Next
        Dim tempitem As Double = a(i)
        a(i) = a(hi)
        a(hi) = tempitem
        Return i
    End Function

End Module