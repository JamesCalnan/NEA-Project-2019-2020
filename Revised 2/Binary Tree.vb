Imports NEA_2019
Imports System.Drawing
Public Class PriorityQueue(Of T)
    Private items As Binary_Tree(Of T)
    Public Sub New()
        items = New Binary_Tree(Of T)
    End Sub
    Public Sub Enqueue(ByVal value As T, ByVal priority As Integer)
        items.insert(New QueueItem(Of T)(value, 0))
    End Sub
    Public Function ExtractMin() As T
        Dim minPriority As QueueItem(Of T) = items.minValue()
        Dim output As T = items.findFirst(minPriority).value
        items.delete(minPriority)
        Return output
    End Function
    Public Sub DecreasePriority(ByVal value As T, ByVal newPriority As Integer)
        Dim tempItem As New QueueItem(Of T)(value, 0)
        Dim item As QueueItem(Of T) = items.findExact(tempItem).Clone
        items.delete(item, True)
        items.insert(New QueueItem(Of T)(value, newPriority))
    End Sub
    Public Function IsEmpty()
        Return items.isEmpty
    End Function
End Class
Public Class QueueItem(Of T)
    Public value As T
    Public priority As Integer
    Public Sub New(ByVal _value As T, ByVal _priority As Integer)
        value = _value
        priority = _priority
    End Sub
    Public Overrides Function Equals(obj As Object) As Boolean
        Dim item = TryCast(obj, QueueItem(Of T))
        Return item IsNot Nothing AndAlso
               EqualityComparer(Of T).Default.Equals(value, item.value) AndAlso
               priority = item.priority
    End Function
    Public Function CompareTo(ByVal other As QueueItem(Of T)) As Integer
        If ReferenceEquals(Me, other) Then Return 0
        If ReferenceEquals(Nothing, other) Then Return 1
        Return priority.CompareTo(other.priority)
    End Function
    Public Overrides Function GetHashCode() As Integer
        Dim hashCode As Long = 913891427
        hashCode = (hashCode * -1521134295 + EqualityComparer(Of T).Default.GetHashCode(value)).GetHashCode()
        hashCode = (hashCode * -1521134295 + priority.GetHashCode()).GetHashCode()
        Return hashCode
    End Function
    Public Function Clone() As QueueItem(Of T)
        Return New QueueItem(Of T)(value, priority)
    End Function
End Class

Public Class Binary_Tree(Of T)
    Public root As TreeItem(Of T)
    Public Sub delete(ByVal value As QueueItem(Of T), Optional ByVal strict As Boolean = False)
        root = deleteRecursive(root, value, strict)
    End Sub
    Public Function deleteRecursive(ByVal root As TreeItem(Of T), ByVal value As QueueItem(Of T), ByVal strict As Boolean)
        If IsNothing(root) Then Return root
        If value.CompareTo(root.value) < 0 Then
            root.left = deleteRecursive(root.left, value, strict)
        ElseIf value.CompareTo(root.value) > 0 Or strict And Not value.Equals(root.value) And value.CompareTo(root.value) = 0 Then
            root.right = deleteRecursive(root.right, value, strict)
        Else
            If IsNothing(root.left) Then Return root.right
            If IsNothing(root.right) Then Return root.left
            root.value = minValue(root.right)
            root.right = deleteRecursive(root.right, root.value, strict)
        End If
        Return root
    End Function
    Public Sub insert(ByVal value As QueueItem(Of T))
        root = insertRecursive(root, value)
    End Sub
    Private Function insertRecursive(ByVal root As TreeItem(Of T), ByVal value As QueueItem(Of T))
        If IsNothing(root) Then
            root = New TreeItem(Of T)(value)
            Return root
        End If
        If value.CompareTo(root.value) < 0 Then
            root.left = insertRecursive(root.left, value)
        ElseIf value.CompareTo(root.value) > 0 Then
            root.right = insertRecursive(root.right, value)
        End If
        Return root
    End Function
    'Public Sub add(ByVal value As QueueItem)
    '    root = addRecursive(root, value)
    'End Sub
    'Private Function addRecursive(ByVal current As TreeItem, ByVal value As QueueItem)
    '    If IsNothing(current) Then
    '        Return New TreeItem(value)
    '    End If
    '    If value <= current.value Then
    '        current.left = addRecursive(current.left, value)
    '    ElseIf value >= current.value Then
    '        current.right = addRecursive(current.right, value)
    '    Else
    '        Return current
    '    End If
    '    Return current
    'End Function
    'Public Sub traverseInOrder(ByVal node As TreeItem)
    '    If Not IsNothing(node) Then
    '        traverseInOrder(node.left)
    '        Console.WriteLine(node.value)
    '        traverseInOrder(node.right)
    '    End If
    'End Sub
    'Private Function containsValueRecursive(ByVal current As TreeItem, ByVal value As Integer) As Boolean
    '    If IsNothing(current) Then Return False
    '    If value = current.value Then Return True
    '    Return If(value < current.value, containsValueRecursive(current.left, value), containsValueRecursive(current.right, value))
    'End Function
    'Public Function containsValue(ByVal value As Integer)
    '    Return containsValueRecursive(root, value)
    'End Function

    'Private Function deleteRecursive(ByVal current As TreeItem, ByVal value As Integer)
    '    If IsNothing(current) Then
    '        Return Nothing
    '    End If
    '    If value = current.value Then
    '        If IsNothing(current.left) And IsNothing(current.right) Then
    '            Return Nothing
    '        End If
    '        If IsNothing(current.right) Then
    '            Return current.left
    '        End If
    '        If IsNothing(current.left) Then
    '            Return current.right
    '        End If
    '        Dim smallestValue = findSmallestValue(current.right)
    '        current.value = smallestValue
    '        current.right = deleteRecursive(current.right, smallestValue)
    '        Return current
    '    End If
    '    If value < current.value Then
    '        current.left = deleteRecursive(current.left, value)
    '        Return current
    '    End If
    '    current.right = deleteRecursive(current.right, value)
    '    Return current
    'End Function
    'Public Sub delete(ByVal value As Integer)
    '    root = deleteRecursive(root, value)
    'End Sub
    Public Function findFirst(ByVal value As QueueItem(Of T)) As QueueItem(Of T)
        Return findFirstRecursive(root, value).value
    End Function
    Private Function findFirstRecursive(ByVal root As TreeItem(Of T), ByVal value As QueueItem(Of T)) As TreeItem(Of T)
        If value.CompareTo(root.value) < 0 Then Return findFirstRecursive(root.left, value)
        If value.CompareTo(root.value) > 0 Then Return findFirstRecursive(root.right, value)
        Return root
    End Function
    Public Function findExact(ByVal value As QueueItem(Of T)) As QueueItem(Of T)
        Return findExactRecursive(root, value).value
    End Function
    Private Function findExactRecursive(ByVal root As TreeItem(Of T), ByVal value As QueueItem(Of T)) As TreeItem(Of T)
        If IsNothing(root) Then Return Nothing
        If value.Equals(root.value) Then Return root
        Dim output As TreeItem(Of T)
        output = findExactRecursive(root.left, value)
        If Not IsNothing(output) Then Return output
        output = findExactRecursive(root.right, value)
        If Not IsNothing(output) Then Return output
        Return output
    End Function
    Private Function minValue(ByVal root As TreeItem(Of T)) As QueueItem(Of T)
        Dim min As QueueItem(Of T) = root.value
        While Not IsNothing(root.left)
            min = root.left.value
            root = root.left
        End While
        Return min
    End Function
    Public Function minValue() As QueueItem(Of T)
        Return Me.minValue(root)
    End Function
    'Private Function findSmallestValue(ByVal root As TreeItem)
    '    Return If(IsNothing(root.left), root.value, findSmallestValue(root.left))
    'End Function
    'Public Function ExtractMin()
    '    Return findSmallestValue(root)
    'End Function
    Public Function getSize() As Integer
        Return getSizeRecursive(root)
    End Function
    Private Function getSizeRecursive(ByVal current As TreeItem(Of T))
        Return If(IsNothing(current), 0, getSizeRecursive(current.left) + 1 + getSizeRecursive(current.right))
    End Function
    Public Function isEmpty() As Boolean
        Return IsNothing(root)
    End Function
End Class
Public Class TreeItem(Of T)
    Public value As QueueItem(Of T)
    Public left, right As TreeItem(Of T)
    Public Sub New(ByVal val As QueueItem(Of T))
        value = val
        left = Nothing
        right = Nothing
    End Sub
End Class