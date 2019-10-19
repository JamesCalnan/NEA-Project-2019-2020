Module PigeonHole_Sort
    Sub pigeonholeSort(a As List(Of Double), delay As Integer)
        Dim n = a.Count - 1
        Dim min = a.Min()
        Dim max = a.Max()
        Dim size = max - min + 1
        Dim holes(size) As Double
        For Each x In a
            holes(x - min) += 1
        Next
        Dim i = 0
        For count = 0 To n
            While holes(count) > 0
                holes(count) -= 1
                AnimateSort(a)
                a(i) = count + min
                i += 1
            End While
        Next
        AnimateSort(a)
    End Sub
End Module