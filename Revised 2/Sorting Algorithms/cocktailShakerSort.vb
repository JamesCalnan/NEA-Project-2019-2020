Module cocktailShakerSort
    Sub cocktailShakerSort(a As List(Of Double), Optional delay As Integer = 0)
        Dim swapped As Boolean
        Do
            If ExitCase() Then Exit Sub
            swapped = False
            For i = 0 To a.Count - 2
                If a(i) > a(i + 1) Then
                    Swap(a, i, i + 1)
                    swapped = True
                    AnimateSort(a, delay)
                End If
            Next
            If Not swapped Then Exit Do
            swapped = False
            For i = a.Count - 2 To 0 Step -1
                If a(i) > a(i + 1) Then
                    Swap(a, i, i + 1)
                    swapped = True
                    AnimateSort(a, delay)
                End If
            Next
        Loop While swapped
    End Sub
End Module
