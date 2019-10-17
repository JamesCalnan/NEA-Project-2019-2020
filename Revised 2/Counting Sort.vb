Module Counting_Sort
    Sub countingSort(array As List(Of Double), delay As Integer)
        Dim minimumValue = array.Min()
        Dim maximumValue = array.Max()
        Dim z = 0
        Dim count(array.Count) As Integer
        For i = minimumValue To maximumValue
            count(i) = 0
        Next
        For i = 0 To array.Count - 1
            count(array(i)) += 1
        Next
        For i = minimumValue To maximumValue
            While count(i) > 0
                If ExitCase() Then Exit Sub
                AnimateSort(array, delay)
                array(z) = i
                z += 1
                count(i) -= 1
            End While
        Next
        AnimateSort(array)
    End Sub
End Module