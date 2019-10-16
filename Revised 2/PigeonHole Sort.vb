Module PigeonHole_Sort
    Sub pigeonholeSort(arr As List(Of Double))
        Dim n = arr.Count - 1
        Dim min = arr(0)
        Dim max = arr(0)
        Dim range, i, j, index As Integer
        For a = 0 To n
            If arr(a) > max Then max = arr(a)
            If arr(a) < min Then min = arr(a)
        Next
        range = max - min + 1

        Dim holes(range) As Double

        For i = 0 To arr.Count - 1
            holes(arr(i) - min) = arr(i)
        Next


        index = 0


        For j = 0 To range - 1
            While holes(j) >= 0
                arr(index) = j + min
                index += 1
            End While
        Next



    End Sub
End Module
