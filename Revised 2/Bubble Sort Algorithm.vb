Module BubbleSortAlgorithm
    Function BubbleSort(a As List(Of Double))
        Dim n As Integer = a.Count
        Dim swapped As Boolean
        Do
            swapped = False
            For i = 1 To n - 1
                If a(i - 1) > a(i) Then
                    Swap(a, i - 1, i)
                    swapped = True
                End If
            Next
            AnimateSort(a, 1)
        Loop Until Not swapped
        Return a
    End Function
    Function BubbleSortOptimisedAlternate(a As List(Of Double))
        Dim n As Integer = a.Count
        Do
            Dim newn = 0
            For i = 1 To n - 1
                If a(i - 1) > a(i) Then
                    Swap(a, i - 1, i)
                    newn = i
                    AnimateSort(a, i)
                End If
            Next

            n = newn
        Loop Until n <= 1
        Return a
    End Function
    Function BubbleSortOptimised(a As List(Of Double))
        Dim n As Integer = a.Count
        Dim swapped As Boolean
        Do
            swapped = False
            For i = 1 To n - 1
                If a(i - 1) > a(i) Then
                    Swap(a, i - 1, i)
                    swapped = True
                End If
            Next
            n -= 1
        Loop Until Not swapped
        Return a
    End Function
End Module
