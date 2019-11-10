Module Satallos_Shuffle_Algorithm
    Sub SattolosAlgorithm(ByRef source As List(Of Double))
        Dim r As New Random
        Dim i = source.Count
        While i > 1
            i -= 1
            Dim j = r.Next(i)
            Swap(source, j, i)
            AnimateSort(source)
        End While
    End Sub
End Module
