Imports System.IO
Imports System.Drawing
Module Menus
    Sub Menu(arr() As String, topitem As String, Optional Exitavailable As Boolean = True)
        Dim temparr() As String = {"Solve using the A* algorithm",
                                   "Solve using Dijkstra's algorithm",
                                   "Solve using Best-first search",
                                   "Solve using Breadth-first search",
                                   "Solve using Depth-first search (using iteration)",
                                   "Solve using Depth-first search (using recursion)",
                                   "Solve using a recursive algorithm",
                                   "Solve using the Lee Algorithm (Wave Propagation)",
                                   "Solve using the dead end filling method",
                                   "Solve using the left-hand rule",
                                   "Solve using the right-hand rule",
                                   "",
                                   "Play the maze", "Braid (remove dead ends)", "Partial braid (remove some dead ends)", "Make the maze sparse (remove some passages)", "Make the maze unicursal",
                                   "",
                                   "Get the average corridor length",
                                   "Get the amount of corners in the maze",
                                   "Get the amount of junctions in the maze",
                                   "Get the amount of Dead-ends in the maze",
                                   "Get the elitism of the maze (path length)",
                                   "Get the solution percentage",
                                   "",
                                   "Save the maze as points",
                                   "Save the maze as a png image",
                                   "Save the maze as an ascii text file",
                                   "",
                                   "Clear the maze and return to the menu"}
        Dim allColours() As String = GetAllConsoleColours()
        Dim pathColour = ConsoleColor.White
        Dim backGroundColour = ConsoleColor.Black
        Dim solvingColour = ConsoleColor.DarkGray
        Dim input = ""
        Dim previousAlgorithm = ""
        Dim previousMaze, loadedMaze As New List(Of Node)
        Dim width, height, delayMs, solvingDelay, yPosAfterMaze, y, lastMazeGenItem As Integer
        For i = 0 To arr.Count - 1
            If arr(i) = "" Then
                lastMazeGenItem = i
                Exit For
            End If
        Next
        y = 1
        Dim limits(3) As Integer
        Dim screenWidth As Integer = Console.WindowWidth / 2
        Dim showMazeGeneration, showPath As Boolean
        Console.Clear()
        Dim currentCol As Integer = Console.CursorTop
        Console.ResetColor()
        MsgColour($"{topitem}: ", ConsoleColor.White)
        For i = 0 To arr.Count - 1
            If arr(i) = arr(y) Then
                MsgColour($"> {arr(1)}  ", ConsoleColor.Green)
            Else
                If arr(i) = "Change the path colour           current colour: " Then
                    Console.WriteLine($" {arr(i)}{pathColour.ToString()}")
                ElseIf arr(i) = "Change the background colour     current colour: " Then
                    Console.WriteLine($" {arr(i)}{backGroundColour.ToString()}")
                ElseIf arr(i) = "Change the solving colour        current colour: " Then
                    Console.WriteLine($" {arr(i)}{solvingColour.ToString()}")
                Else
                    Console.WriteLine($" {arr(i)}")
                End If
            End If
        Next
        While 1
            Console.CursorVisible = False
            Dim info() As String = {"Information for using this program:", "Use the up and down arrow keys for navigating the menus", "Use the enter key to select an option", "Press 'I' to bring up information on the algorithm", "", "When inputting integer values:", "The up and down arrow keys increment by 1", "The right and left arrow keys increment by 10", "The 'M' key will set the number to the maximum value it can be", "The 'H' key will set the number to half of the maximum value it can be"}
            For i = 0 To info.Count - 1
                Console.SetCursorPosition(screenWidth - info(i).Length / 2, i)
                If i <> 0 Then Console.ForegroundColor = (ConsoleColor.Magenta)
                Console.Write(info(i))
            Next
            SetBoth(ConsoleColor.Black)
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "DownArrow"
                    y += 1
                    If y = arr.Count Then y = 1
                    If arr(y) = "" Or arr(y) = "Generate a maze using one of the following algorithms" Then y += 1
                Case "UpArrow"
                    y -= 1
                    If y = 0 Then y = arr.Count - 1
                    If arr(y) = "" Or arr(y) = "Generate a maze using one of the following algorithms" Then y -= 1
                Case "Enter"
                    Console.ForegroundColor = ConsoleColor.White
                    Dim availablePath As New List(Of Node)
                    If y <= lastMazeGenItem Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0, If(arr(y) = "   Make your own maze" Or arr(y) = "   Conway's game of life (Maze generation)", True, False), If(arr(y) = "   Conway's game of life (Maze generation)", False, True))
                        If arr(y) = "   Recursive Backtracker Algorithm (using iteration)" Then
                            availablePath = RecursiveBacktracker.RecursiveBacktracker(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Conway's game of life (Maze generation)" Then
                            simulateLife(limits, True)
                        ElseIf arr(y) = "   Recursive Backtracker Algorithm (using recursion)" Then
                            Dim r As New Random
                            If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
                            Dim currentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
                            Dim prev As Cell = currentCell '(Limits(0) + 3, Limits(1) + 2)
                            Dim v As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
                            v(currentCell) = True
                            Dim path As New List(Of Node)
                            Dim stopwatch As Stopwatch = Stopwatch.StartNew()
                            SetBoth(pathColour)
                            path.Add(New Node(currentCell.X, currentCell.Y))
                            If showMazeGeneration Then currentCell.Print("██")
                            path = RecursiveBacktrackerRecursively(currentCell, limits, path, v, prev, r, showMazeGeneration, delayMs, pathColour)
                            PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
                            SetBoth(pathColour)
                            If Not showMazeGeneration Then PrintMazeHorizontally(path, limits(2), limits(3))
                            AddStartAndEnd(path, limits, pathColour)
                            availablePath = path
                        ElseIf arr(y) = "   Recursive Backtracker Algorithm (using iteration, not using a stack)" Then
                            availablePath = RecursiveBacktrackerNotUsingStack(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Hunt and Kill Algorithm (first cell)" Then
                            availablePath = HuntAndKillRefactored(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Hunt and Kill Algorithm (random cell)" Then
                            availablePath = HuntAndKillRefactoredRandom(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Prim's Algorithm (simplified)" Then
                            availablePath = Prims_Simplified(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Prim's Algorithm (true)" Then
                            availablePath = Prims_True(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Aldous-Broder Algorithm" Then
                            availablePath = AldousBroder.AldousBroder(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Growing Tree Algorithm" Then
                            Dim arrOptions() As String = {"Newest (Recursive Backtracker)", "Random (Prim's simplified)", "Newest/Random, 75/25 split", "Newest/Random, 50/50 split", "Newest/Random, 25/75 split", "Oldest", "Middle", "Newest/Oldest, 50/50 split", "Oldest/Random, 50/50 split"}
                            Dim cellSelectionMethod() As Integer = PreGenMenu(arrOptions, "What Cell selection method would you like to use: ")
                            availablePath = GrowingTree(limits, delayMs, cellSelectionMethod, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Sidewinder Algorithm" Then
                            availablePath = Sidewinder(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Binary Tree Algorithm (top down)" Or arr(y) = "   Binary Tree Algorithm (random)" Then
                            Dim arrOptions() As String = {"Northwest", "Northeast", "Southwest", "Southeast"}
                            Dim bias() As Integer = PreGenMenu(arrOptions, "Cell bias: ")
                            If arr(y) = "   Binary Tree Algorithm (top down)" Then
                                availablePath = BinaryTree(limits, delayMs, showMazeGeneration, bias, pathColour, backGroundColour)
                            Else
                                availablePath = BinaryTreeRandom(limits, delayMs, showMazeGeneration, bias, pathColour, backGroundColour)
                            End If
                        ElseIf arr(y) = "   Wilson's Algorithm (9 options)" Then
                            Console.ResetColor()
                            Console.Clear()
                            Dim wilsonsOption As String = SolvingMenu({"random", "top to bottom", "bottom to top", "left to right", "right to left", "collapsing rectangle", "expanding rectangle", "collapsing diamond", "expanding diamond"}, "Cell selection method: ", 0, 0)
                            availablePath = WilsonsRefectored(limits, delayMs, showMazeGeneration, wilsonsOption, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Eller's Algorithm" Then
                            availablePath = Ellers(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Kruskal's Algorithm (simplified)" Then
                            availablePath = KruskalsSimplified(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Kruskal's Algorithm (true)" Then
                            availablePath = KruskalsTrue(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Houston's Algorithm" Then
                            availablePath = Houstons(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Spiral Backtracker Algorithm" Then
                            availablePath = SpiralBacktracker(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Custom Algorithm" Then
                            availablePath = Custom(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Make your own maze" Then
                            availablePath = UserCreateMaze.UserCreateMaze(limits, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Borůvka's Algorithm (top down)" Then
                            availablePath = BoruvkasAlgorithm.BoruvkasAlgorithm(limits, delayMs, showMazeGeneration, pathColour, backGroundColour, "")
                        ElseIf arr(y) = "   Borůvka's Algorithm (random)" Then
                            availablePath = BoruvkasAlgorithm.BoruvkasAlgorithm(limits, delayMs, showMazeGeneration, pathColour, backGroundColour, "shuffle")
                        ElseIf arr(y) = "   Reverse-Delete Algorithm (best-first search)" Then
                            availablePath = reverseDeleteAlgorithm(limits, delayMs, showMazeGeneration, pathColour, backGroundColour, "bestfs")
                        ElseIf arr(y) = "   Reverse-Delete Algorithm (breadth-first search)" Then
                            availablePath = reverseDeleteAlgorithm(limits, delayMs, showMazeGeneration, pathColour, backGroundColour, "bfs")
                        ElseIf arr(y) = "   Reverse-Delete Algorithm (depth-first search)" Then
                            availablePath = reverseDeleteAlgorithm(limits, delayMs, showMazeGeneration, pathColour, backGroundColour, "dfs")
                        ElseIf arr(y) = "   Randomised Breadth-First Search" Then
                            availablePath = RandomisedBFS(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Dungeon Creation Algorithm" Then
                            availablePath = createPassages(limits, showMazeGeneration, pathColour, backGroundColour, delayMs)
                        End If
                        If Not IsNothing(availablePath) And arr(y) <> "   Conway's game of life (Maze generation)" Then Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm, temparr, pathColour, backGroundColour, solvingColour)
                    Else
                        If arr(y) = "Load the previously generated maze" Then
                            PrintPreviousMaze(previousMaze, previousAlgorithm, showPath, yPosAfterMaze, solvingDelay, temparr, pathColour, backGroundColour, solvingColour)
                        ElseIf arr(y) = "Sorting Algorithm visualisations" Then
                            Console.ResetColor()
                            Console.Clear()
                            Dim shuffleAlgorithm As String = SolvingMenu({"Simple shuffle", "Fisher–Yates shuffle", "Fisher–Yates shuffle (inside out)", "Sattolo's algorithm", "", "Return to the menu"}, "What algorithm would you like to use to suffle the array", 0, 0, True)
                            Console.ResetColor()
                            Console.Clear()
                            If Not shuffleAlgorithm = Nothing Then
                                Dim sortingAlgorithm As String = SolvingMenu({"Bogo Sort", "Bogobogo Sort", "Bozo Sort", "Bubble Sort (using recursion)", "Bubble Sort (using iteration)", "Insertion Sort (using iteration)", "Insertion Sort (using recursion)", "Merge Sort", "Quick Sort", "Selection Sort", "Shell Sort", "Gnome Sort", "Slow Sort", "Cocktail shaker sort", "Pancake Sort", "Comb Sort", "Cycle Sort", "Stooge Sort", "Heap Sort", "Odd-Even Sort / Brick Sort", "Counting Sort", "Bucket Sort", "Pigeonhole Sort", "", "Return to the menu"}, "What Sorting Algorithm do you want to use", 0, 0, True)
                                If Not sortingAlgorithm = Nothing Then
                                    Dim a As New List(Of Double)
                                    Console.ResetColor()
                                    Console.Clear()
                                    Console.ForegroundColor = ConsoleColor.White
                                    Dim delay = GetIntInputArrowKeys("Delay when sorting:  ", 100, 0, False)
                                    For i = 1 To GetIntInputArrowKeys("How many numbers do you want to be in the list: ", Console.WindowHeight - 3, 4, True)
                                        a.Add(i)
                                    Next
                                    Console.ResetColor()
                                    Console.Clear()
                                    Select Case shuffleAlgorithm
                                        Case "Simple shuffle"
                                            Shuffle(a)
                                        Case "Fisher–Yates shuffle"
                                            FisherYatesShuffle(a)
                                        Case "Fisher–Yates shuffle (inside out)"
                                            FisherYatesShuffleInsideOut(a)
                                        Case "Sattolo's algorithm"
                                            SattolosAlgorithm(a)
                                    End Select
                                    Select Case sortingAlgorithm
                                        Case "Bogo Sort"
                                            BogosortAlgorithm.BogoSort(a, delay)
                                        Case "Bogobogo Sort"
                                            BogobogoSort(a, delay)
                                        Case "Pigeonhole Sort"
                                            pigeonholeSort(a, delay)
                                        Case "Bucket Sort"
                                            BucketSort(a, delay)
                                        Case "Counting Sort"
                                            countingSort(a, delay)
                                        Case "Odd-Even Sort / Brick Sort"
                                            oddEverSort(a, delay)
                                        Case "Heap Sort"
                                            HeapSort.heapSort(a, delay)
                                        Case "Stooge Sort"
                                            Stooge_Sort.stoogeSort(a, 0, a.Count - 1, delay)
                                        Case "Cycle Sort"
                                            Cycle_Sort.cycleSort(a, delay)
                                        Case "Comb Sort"
                                            Comb_Sort.CombSort(a, delay)
                                        Case "Pancake Sort"
                                            Pancake_Sort.pancakeSort(a, delay)
                                        Case "Bozo Sort"
                                            BozoSort.BozoSort(a, delay)
                                        Case "Bubble Sort (using recursion)"
                                            BubbleSortAlgorithm.bubbleSortRecursive(a, delay)
                                        Case "Bubble Sort (using iteration)"
                                            BubbleSortAlgorithm.BubbleSortOptimisedAlternate(a, delay)
                                        Case "Insertion Sort (using iteration)"
                                            InsertionSortAlgorithm.InsertionsortI(a, True, delay)
                                        Case "Insertion Sort (using recursion)"
                                            InsertionSortAlgorithm.InsertionsortR(a, a.Count() - 1)
                                            AnimateSort(a)
                                        Case "Merge Sort"
                                            AnimateSort(a)
                                            a = MergeSort.Mergesort(a, a.Count, delay)
                                            AnimateSort(a)
                                        Case "Quick Sort"
                                            QuickSortAlgorithm.Quicksort(a, 0, a.Count - 1, delay)
                                        Case "Selection Sort"
                                            SelectionSortAlgorithm.SelectionSort(a, delay)
                                        Case "Shell Sort"
                                            ShellSort.ShellSort(a, delayMs)
                                        Case "Gnome Sort"
                                            Gnome_Sort.gnomeSort(a, delay)
                                        Case "Slow Sort"
                                            SlowSort.slowsort(a, 0, a.Count - 1, delay)
                                        Case "Cocktail shaker sort"
                                            cocktailShakerSort.cocktailShakerSort(a, delay)
                                    End Select
                                    PrintMessageMiddle("Done", 0, ConsoleColor.Green)
                                    Console.ReadKey()
                                End If
                            End If
                        ElseIf arr(y) = "Save the previously generated maze as a list of points" Then
                            If previousMaze.Count > 1 Then
                                SaveMazeTextFile(previousMaze, previousAlgorithm)
                            Else
                                Console.Clear()
                                MsgColour("No previous maze available", ConsoleColor.Red)
                                Console.ReadKey()
                            End If
                        ElseIf arr(y) = "Save the previous maze as a png image" Then
                            If previousMaze.Count > 1 Then
                                Console.Clear()
                                Console.ForegroundColor = ConsoleColor.White
                                Dim filename As String = GetValidFileName()
                                SaveMazePng(previousMaze, $"Algorithm used to generate this maze: {previousAlgorithm}", filename, pathColour, backGroundColour)
                            Else
                                Console.Clear()
                                MsgColour("No previous maze available", ConsoleColor.Red)
                                Console.ReadKey()
                            End If
                        ElseIf arr(y) = "Change the path colour           current colour: " Then
                            pathColour = ColourChange(allColours)
                        ElseIf arr(y) = "Change the background colour     current colour: " Then
                            backGroundColour = ColourChange(allColours)
                        ElseIf arr(y) = "Change the solving colour        current colour: " Then
                            solvingColour = ColourChange(allColours)
                        ElseIf arr(y) = "Load a maze from a text file (list of points)" Then
                            LoadMazeTextFile(loadedMaze, yPosAfterMaze, previousMaze, temparr, showPath, solvingDelay, pathColour, backGroundColour, solvingColour)
                        ElseIf arr(y) = "Load a maze from an image file" Then
                            Dim tempMaze As List(Of Node) = LoadMazePng(temparr, previousAlgorithm, pathColour, backGroundColour, solvingColour)
                            If IsNothing(tempMaze) Then
                                Console.Clear()
                                Console.ForegroundColor = ConsoleColor.Red
                                Console.BackgroundColor = ConsoleColor.Black
                                Console.WriteLine("The maze that you tried to load is invalid")
                                Console.ReadKey()
                            Else
                                previousMaze = tempMaze
                            End If
                        ElseIf arr(y) = "Save the previous maze to ascii text file" Then
                            If previousMaze.Count > 0 Then
                                SaveMazeAscii(previousMaze)
                            Else
                                Console.Clear()
                                Console.ForegroundColor = ConsoleColor.Red
                                Console.WriteLine($"No previous maze available")
                                Console.ReadKey()
                            End If
                        ElseIf arr(y) = "Load a maze from an ascii text file" Then
                            Console.Clear()
                            Dim tempMaze As List(Of Node) = LoadMazeAscii(temparr, pathColour, backGroundColour, solvingColour)
                            If IsNothing(tempMaze) Then
                                Console.Clear()
                                Console.ForegroundColor = ConsoleColor.Red
                                Console.BackgroundColor = ConsoleColor.Black
                                Console.WriteLine("The maze that you tried to load is too big for the console window, please decrease font size and try again")
                                Console.ReadKey()
                            Else
                                previousMaze = tempMaze
                            End If
                        ElseIf y = arr.Count - 1 Then
                            If Exitavailable Then
                                End
                            Else
                                Console.Clear()
                                MsgColour("Unavailable", ConsoleColor.Red)
                                Console.ReadKey()
                            End If
                        ElseIf arr(y) = "Conway's game of life" Then
                            GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0, If(arr(y) = "Conway's game of life", True, False), If(arr(y) = "Conway's game of life", False, True))
                            simulateLife(limits, False)
                        Else
                            OptionNotReady()
                        End If
                    End If
                    Console.BackgroundColor = (ConsoleColor.Black)
                    Console.Clear()
                    MsgColour($"{topitem}: ", ConsoleColor.Yellow)
                Case "I"
                    If y < lastMazeGenItem Then
                        InitialiseScreen()
                        If arr(y) = "   Recursive Backtracker Algorithm (using iteration)" Then
                            RecrusiveBacktrackerInfo()
                        ElseIf arr(y) = "   Reverse-Delete Algorithm (best-first search)" Or arr(y) = "   Reverse-Delete Algorithm (breadth-first search)" Or arr(y) = "   Reverse-Delete Algorithm (depth-first search)" Then
                            ReverseDeleteAlgorithmInfo()
                        ElseIf arr(y) = "   Recursive Backtracker Algorithm (using recursion)" Then
                            RecrusiveBacktrackerRecursionInfo()
                        ElseIf arr(y) = "   Recursive Backtracker Algorithm (using iteration, not using a stack)" Then
                            RecrusiveBacktrackerNotStackInfo()
                        ElseIf arr(y) = "   Hunt and Kill Algorithm (first cell)" Then
                            HuntAndKillInfoFirstCell()
                        ElseIf arr(y) = "   Hunt and Kill Algorithm (random cell)" Then
                            HuntAndKillInfoRandomCell()
                        ElseIf arr(y) = "   Prim's Algorithm (simplified)" Then
                            Prims_SimplifiedINFO()
                        ElseIf arr(y) = "   Prim's Algorithm (true)" Then
                            Prims_TrueINFO()
                        ElseIf arr(y) = "   Aldous-Broder Algorithm" Then
                            AldousBroderInfo()
                        ElseIf arr(y) = "   Growing Tree Algorithm" Then
                            GrowingTreeInfo()
                        ElseIf arr(y) = "   Sidewinder Algorithm" Then
                            SidewinderInfo()
                        ElseIf arr(y) = "   Binary Tree Algorithm (top down)" Or arr(y) = "   Binary Tree Algorithm (random)" Then
                            BinaryTreeInfo()
                        ElseIf arr(y) = "   Wilson's Algorithm (9 options)" Then
                            WilsonsInfo()
                        ElseIf arr(y) = "   Eller's Algorithm" Then
                            EllersInfo()
                        ElseIf arr(y) = "   Kruskal's Algorithm (simplified)" Then
                            KruskalsInfoSimplified()
                        ElseIf arr(y) = "   Kruskal's Algorithm (true)" Then
                            KruskalsInfoTrue()
                        ElseIf arr(y) = "   Houston's Algorithm" Then
                            HoustonsInfo()
                        ElseIf arr(y) = "   Spiral Backtracker Algorithm" Then
                            SpiralBacktrackerInfo()
                        ElseIf arr(y) = "   Custom Algorithm" Then
                            CustomAlgorithmInfo()
                        ElseIf arr(y) = "   Randomised Breadth-First Search" Then
                            RandomisedBreadthFirstSearch()
                        ElseIf arr(y) = "   Borůvka's Algorithm (top down)" Or arr(y) = "   Borůvka's Algorithm (random)" Then
                            BoruvkasAlgorithmInfo()
                        Else
                            OptionNotReady()
                        End If
                        Console.ReadKey()
                        Console.BackgroundColor = (ConsoleColor.Black)
                        Console.Clear()
                        MsgColour("What Maze Generation Algorithm do you want to use: ", ConsoleColor.Yellow)
                    End If
            End Select
            Console.ResetColor()
            Console.ForegroundColor = (ConsoleColor.White)
            Dim Count = 1
            For Each MenuOption In arr
                Console.SetCursorPosition(0, Count + currentCol)
                If MenuOption = "Change the path colour           current colour: " Then
                    Console.Write($" {MenuOption}{pathColour.ToString()}    ")
                ElseIf MenuOption = "Change the background colour     current colour: " Then
                    Console.Write($" {MenuOption}{backGroundColour.ToString()}    ")
                ElseIf MenuOption = "Change the solving colour        current colour: " Then
                    Console.WriteLine($" {MenuOption}{solvingColour.ToString()}      ")
                Else
                    Console.Write($" {MenuOption}    ")
                End If
                Count += 1
            Next
            Console.SetCursorPosition(0, y + 1)
            If arr(y) = "Change the path colour           current colour: " Then
                MsgColour($"> {arr(y)}{pathColour.ToString()}  ", ConsoleColor.Green)
            ElseIf arr(y) = "Change the background colour     current colour: " Then
                MsgColour($"> {arr(y)}{backGroundColour.ToString()}  ", ConsoleColor.Green)
            ElseIf arr(y) = "Change the solving colour        current colour: " Then
                MsgColour($"> {arr(y)}{solvingColour.ToString()}  ", ConsoleColor.Green)
            Else
                MsgColour($"> {arr(y)}  ", ConsoleColor.Green)
            End If
        End While
    End Sub
    Sub Solving(availablePath As List(Of Node), Limits() As Integer, ByRef previousMaze As List(Of Node), ByRef Input As String, yPosAfterMaze As Integer, showPath As Boolean, solvingDelay As Integer, ByRef algorithm As String, ByRef setPreivousAlgorithm As String, tempArr() As String, pathColour As ConsoleColor, backGroundColour As ConsoleColor, solvingColour As ConsoleColor)
        setPreivousAlgorithm = algorithm
        previousMaze.Clear()
        previousMaze.AddRange(availablePath)
        PreSolving(Limits, availablePath, previousMaze, Input, yPosAfterMaze, tempArr)
        SolvingInput(Input, showPath, yPosAfterMaze, solvingDelay, availablePath, algorithm, pathColour, backGroundColour, solvingColour)
    End Sub
    Sub PreSolving(limits() As Integer, availablePath As List(Of Node), ByRef previousMaze As List(Of Node), ByRef input As String, ByRef yPosAfterMaze As Integer, tempArr() As String)
        Console.BackgroundColor = (ConsoleColor.Black)
        yPosAfterMaze = limits(3) + 1
        DisplayAvailablePositions(availablePath.Count)
        Console.SetCursorPosition(0, yPosAfterMaze + 3)
        input = SolvingMenu(tempArr, "What would you like to do with the maze", limits(2) + 4, 3)
    End Sub
    Sub ClearHorizontal(y As Integer, ClearMessage As Boolean, setafter As Boolean)
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, y)
        If ClearMessage Then Console.Write("                                                                                                               ")
        Console.SetCursorPosition(0, y + If(setafter, 1, 0))
    End Sub
    Function HorizontalYesNo(ColumnPosition As Integer, message As String, ClearMessage As Boolean, ClearBefore As Boolean, SetAfter As Boolean) As Boolean
        If ClearBefore Then Console.Clear()
        Console.ForegroundColor = (ConsoleColor.White)
        Dim Choice = True
        Dim x, y As Integer
        y = ColumnPosition
        Console.SetCursorPosition(x, y)
        Console.Write(message)
        MsgColour("> Yes", ConsoleColor.Green)
        Console.SetCursorPosition(message.Length + 10, y)
        Console.Write(" No")
        While 1
            Console.ForegroundColor = ConsoleColor.Black
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "RightArrow"
                    If Choice Then Choice = False
                Case "LeftArrow"
                    If Not Choice Then Choice = True
                Case "Enter"
                    ClearHorizontal(y, ClearMessage, SetAfter)
                    If Choice Then
                        Return True
                    Else
                        Return False
                    End If
                Case "Escape"
                    Return Nothing
                Case "Y"
                    ClearHorizontal(y, ClearMessage, SetAfter)
                    Return True
                Case "N"
                    ClearHorizontal(y, ClearMessage, SetAfter)
                    Return False
            End Select
            Console.SetCursorPosition(0, y)
            Console.ForegroundColor = ConsoleColor.White
            Console.Write($"{message}  Yes        No")
            If Choice Then
                Console.SetCursorPosition(message.Length, y)
                MsgColour("> Yes", ConsoleColor.Green)
                Console.SetCursorPosition(message.Length + 8, y)
                Console.Write("                    ")
                Console.SetCursorPosition(message.Length + 11, y)
                Console.Write("No")
            ElseIf Not Choice Then
                Console.SetCursorPosition(message.Length + 8, y)
                Console.Write("                    ")
                Console.SetCursorPosition(message.Length + 9, y)
                MsgColour("> No", ConsoleColor.Green)
            End If
        End While
        Return Nothing
    End Function
    Function SolvingMenu(arr() As String, Message As String, X As Integer, Y_ As Integer, Optional ExitCase As Boolean = False)
        Dim temparr() As String = arr
        Dim CurrentCol = 0 'Console.CursorTop
        Dim y = 0
        Console.SetCursorPosition(X, y + Y_)
        MsgColour(Message, ConsoleColor.Yellow)
        Console.SetCursorPosition(X, y + 1 + Y_)
        MsgColour($"> {arr(0)}", ConsoleColor.Green)
        For i = 1 To arr.Count - 1
            Console.SetCursorPosition(X, i + 1 + Y_)
            Console.Write($" {arr(i)}")
        Next
        While 1
            Console.BackgroundColor = (ConsoleColor.Black)
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "DownArrow"
                    y += 1
                    If y = arr.Count Then y = 0
                    If arr(y) = "" Then y += 1
                Case "UpArrow"
                    y -= 1
                    If y = -1 Then y = arr.Count - 1
                    If arr(y) = "" Then y -= 1
                Case "S"
                    For i = 0 To arr.Count
                        Console.SetCursorPosition(X, i + Y_)
                        Console.Write("                                                        ")
                    Next
                    Return "s"
                Case "Enter"
                    If ExitCase Then
                        If temparr(y) = "Return to the menu" Then Return Nothing
                    End If
                        For i = 0 To arr.Count
                        Console.SetCursorPosition(X, i + Y_)
                        Console.Write("                                                        ")
                    Next
                    Return temparr(y)
            End Select
            Console.ForegroundColor = (ConsoleColor.White)
            Dim count = 1
            For Each MenuOption In arr
                Console.SetCursorPosition(X, count + CurrentCol + Y_)
                Console.Write($" {MenuOption}  ")
                count += 1
            Next
            Console.SetCursorPosition(X, y + 1 + Y_)
            MsgColour($"> {arr(y)}", ConsoleColor.Green)
        End While
        Return Nothing
    End Function
    Function GetIntInputArrowKeys(message As String, NumMax As Integer, NumMin As Integer, ClearMessage As Boolean)
        Console.Write(message)
        Console.ForegroundColor = (ConsoleColor.Magenta)
        Dim cursorleft, cursortop As Integer
        cursorleft = Console.CursorLeft
        cursortop = Console.CursorTop
        Console.SetCursorPosition(cursorleft, cursortop)
        Dim current As Integer = NumMin
        Console.Write(current)
        While 1
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "RightArrow"
                    current += 10
                    If current > NumMax Then current = NumMax
                Case "LeftArrow"
                    current -= 10
                    If current < NumMin Then current = NumMin
                Case "UpArrow"
                    current += 1
                    If current > NumMax Then current = NumMax
                Case "DownArrow"
                    current -= 1
                    If current < NumMin Then current = NumMin
                Case "M"
                    current = NumMax
                Case "H"
                    current = NumMax / 2
                Case "Enter"
                    Exit While
            End Select
            Console.SetCursorPosition(cursorleft, cursortop)
            Console.Write("   ")
            Console.SetCursorPosition(cursorleft, cursortop)
            Console.Write(current)
        End While
        If ClearMessage Then
            Console.SetCursorPosition(0, cursortop)
            Console.Write("".PadLeft(message.Length + 5, " "c))
        End If
        Console.SetCursorPosition(0, cursortop + 1)
        Console.ForegroundColor = (ConsoleColor.White)
        Return current
    End Function
    Sub SolvingInput(input As String, showpath As Boolean, YposAfterMaze As Integer, solvingdelay As Integer, Maze As List(Of Node), Algorithm As String, pathColour As ConsoleColor, backGroundColour As ConsoleColor, solvingColour As ConsoleColor)
        If input = "Solve using the A* algorithm" Then
            Console.SetCursorPosition(0, YposAfterMaze + 2)
            Dim heuristic = GetIntInputArrowKeys("Heuristic: ", 60, 1, True)
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            If HorizontalYesNo(YposAfterMaze + 2, "Do you want to use the optimised version of A*: ", True, False, False) Then
                AStar(Maze, showpath, True, solvingdelay, heuristic, solvingColour)
            Else
                'Dim neededNodes As List(Of Node) = GetNeededNodes(Maze)
                'Dim adjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, Maze)
                AStarWiki(Maze, showpath, True, solvingdelay, heuristic, solvingColour)
            End If
        ElseIf input = "Solve using Dijkstra's algorithm" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            'Dim neededNodes As List(Of Node) = GetNeededNodes(Maze)
            'Dim AdjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, Maze)
            Dijkstras(Maze, showpath, solvingdelay, solvingColour)
        ElseIf input = "Solve using Breadth-first search" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Bfs(Maze, showpath, True, solvingdelay, solvingColour)
        ElseIf input = "Solve using Best-first search" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Bestfs(Maze, showpath, True, solvingdelay, solvingColour)
        ElseIf input = "Solve using a recursive algorithm" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dim visited As New Dictionary(Of Node, Boolean)
            Dim correctPath As New Dictionary(Of Node, Boolean)
            For Each node In Maze
                visited(node) = False
            Next
            SetBoth(ConsoleColor.Black)
            Console.ForegroundColor = ConsoleColor.White
            Console.SetCursorPosition(0, Console.WindowHeight - 1)
            Console.Write("Solving                            ")
            SetBoth(solvingColour)
            Dim stopwatch As Stopwatch = Stopwatch.StartNew()
            Dim b As Boolean = RecursiveSolve(Maze, visited, correctPath, Maze(Maze.Count - 2).X, Maze(Maze.Count - 2).Y, New Node(Maze(Maze.Count - 1).X, Maze(Maze.Count - 1).Y), showpath, solvingdelay)
            SetBoth(ConsoleColor.Green)
            For Each thing In correctPath
                If thing.Value Then thing.Key.Print("XX")
            Next
            Maze(Maze.Count - 1).Print("XX")
            SetBoth(ConsoleColor.Black)
            Console.ForegroundColor = ConsoleColor.White
            Console.SetCursorPosition(35, Console.WindowHeight - 1)
            Console.Write($"Time taken: {stopwatch.Elapsed.TotalSeconds}")
            Console.ReadKey()
        ElseIf input = "Solve using Depth-first search (using iteration)" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            'Dim neededNodes As List(Of Node) = GetNeededNodes(Maze)
            'Dim adjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, Maze)
            DFS_Iterative(Maze, showpath, True, solvingdelay, solvingColour)
        ElseIf input = "Solve using Depth-first search (using recursion)" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dim startV As New Node(Maze(Maze.Count - 2).X, Maze(Maze.Count - 2).Y)
            Dim goal As New Node(Maze(Maze.Count - 1).X, Maze(Maze.Count - 1).Y)
            Dim discovered As New Dictionary(Of Node, Boolean)
            For Each node In Maze
                discovered(node) = False
            Next
            Dim cameFrom As New Dictionary(Of Node, Node)
            Dim timer As Stopwatch = Stopwatch.StartNew
            SetBoth(solvingColour)
            DFS_Recursive(Maze, startV, discovered, cameFrom, goal, showpath, solvingdelay, False)
            ReconstructPath(cameFrom, goal, startV, $"{timer.Elapsed.TotalSeconds}")
            Console.ReadKey()
        ElseIf input = "Play the maze" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps you have taken in the maze: ", True, False, False)
            PlaymazeSubroutine(Maze, showpath, pathColour, backGroundColour)
        ElseIf input = "Clear the maze and return to the menu" Then
            Console.Clear()
        ElseIf input = "Save the maze as points" Then
            SaveMazeTextFile(Maze, $"Algorithm used to generate this maze: {Algorithm}")
        ElseIf input = "Save the maze as a png image" Then
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.White
            Dim filename As String = GetValidFileName()
            SaveMazePng(Maze, Algorithm, filename, pathColour, backGroundColour)
        ElseIf input = "s" Then
            'Sd(Maze)
        ElseIf input = "Solve using the dead end filling method" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            DeadEndFiller(Maze, showpath, solvingdelay, pathColour, solvingColour)
        ElseIf input = "Solve using the left-hand rule" Then
            Console.SetCursorPosition(0, YposAfterMaze + 2)
            solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            WallFollowerAlgorithm(Maze, solvingdelay, "LHR", solvingColour)
        ElseIf input = "Solve using the right-hand rule" Then
            Console.SetCursorPosition(0, YposAfterMaze + 2)
            solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            WallFollowerAlgorithm(Maze, solvingdelay, "", solvingColour)
        ElseIf input = "Solve using the Lee Algorithm (Wave Propagation)" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Lee(Maze, showpath, solvingdelay, solvingColour)
        ElseIf input = "Make the maze unicursal" Then
            Dim mazeCopy As List(Of Node) = (From node In Maze Select New Node(node.X, node.Y)).ToList()
            Dim uniMaze As List(Of Node) = Unicursal(mazeCopy, pathColour, backGroundColour)
            If IsNothing(uniMaze) Then
                MsgColour("The maze that you tried to make unicursal is too big, please try a smaller maze", ConsoleColor.Red)
                Console.ReadKey()
            Else
                Maze = uniMaze
                Dim greatestX As Integer = (From node In uniMaze Select node.X).Concat(New Integer() {greatestX}).Max()
                Dim greatestY As Integer = (From node In uniMaze Select node.Y).Concat(New Integer() {greatestY}).Max()
                Dim temparr() As String = {"Solve using the A* algorithm",
                                "Solve using Dijkstra's algorithm",
                                "Solve using Breadth-first search",
                                "Solve using Best-first search",
                                "Solve using Depth-first search (using iteration)",
                                "Solve using Depth-first search (using recursion)",
                                "Solve using a recursive algorithm",
                                "Solve using the Lee Algorithm (Wave Propagation)",
                                 "Solve using the dead end filling method",
                                "Solve using the left-hand rule",
                                "Solve using the right-hand rule",
                                "",
                                "Play the maze",
                                "",
                                "Get the average corridor length",
            "Get the amount of corners in the maze",
            "Get the amount of junctions in the maze",
            "Get the amount of Dead-ends in the maze",
            "Get the elitism of the maze (path length)",
            "Get the solution percentage",
                               "",
                               "Save the maze as points",
                               "Save the maze as a png image",
                               "Save the maze as an ascii text file",
                               "",
                               "Clear the maze and return to the menu"}
                input = SolvingMenu(temparr, "What would you like to do with the maze", greatestX + 5, 3)

                SolvingInput(input, True, greatestY + 2, solvingdelay, uniMaze, "", pathColour, backGroundColour, solvingColour)
            End If
        ElseIf input = "Braid (remove dead ends)" Or input = "Partial braid (remove some dead ends)" Then
            If input = "Braid (remove dead ends)" Then
                EliminateDeadEnds(Maze, pathColour, backGroundColour)
            ElseIf input = "Partial braid (remove some dead ends)" Then
                PartialBraid(Maze, pathColour, backGroundColour)
            End If
            Dim greatestX As Integer = (From node In Maze Select node.X).Concat(New Integer() {greatestX}).Max()
            Dim temparr() As String = {"Solve using the A* algorithm",
                "Solve using Dijkstra's algorithm",
                "Solve using Breadth-first search",
                "Solve using Best-first search",
                "Solve using Depth-first search (using iteration)",
                "Solve using Depth-first search (using recursion)",
                "Solve using a recursive algorithm",
                "Solve using the Lee Algorithm (Wave Propagation)",
                 "Solve using the dead end filling method",
                "Solve using the left-hand rule",
                "Solve using the right-hand rule",
                "",
                "Play the maze", "Make the maze unicursal",
                "",
                "Get the average corridor length",
            "Get the amount of corners in the maze",
            "Get the amount of junctions in the maze",
            "Get the amount of Dead-ends in the maze",
            "Get the elitism of the maze (path length)",
            "Get the solution percentage",
               "",
               "Save the maze as points",
               "Save the maze as a png image",
               "Save the maze as an ascii text file",
               "",
               "Clear the maze and return to the menu"}
            input = SolvingMenu(temparr, "What would you like to do with the maze", greatestX + 6, 3)
            SolvingInput(input, showpath, YposAfterMaze, solvingdelay, Maze, "", pathColour, backGroundColour, solvingColour)
        ElseIf input = "Make the maze sparse (remove some passages)" Then
            Sparsify(Maze, pathColour, backGroundColour)
            Dim greatestX As Integer
            greatestX = (From node In Maze Select node.X).Concat(New Integer() {greatestX}).Max()
            Dim temparr() As String = {"Solve using the A* algorithm",
            "Solve using Dijkstra's algorithm",
            "Solve using Breadth-first search",
            "Solve using Best-first search",
            "Solve using Depth-first search (using iteration)",
            "Solve using Depth-first search (using recursion)",
            "Solve using a recursive algorithm",
            "Solve using the Lee Algorithm (Wave Propagation)",
            "Solve using the dead end filling method",
            "Solve using the left-hand rule",
            "Solve using the right-hand rule",
            "",
            "Play the maze",
            "Make the maze sparse (remove some passages)",
            "",
            "Get the average corridor length",
            "Get the amount of corners in the maze",
            "Get the amount of junctions in the maze",
            "Get the amount of Dead-ends in the maze",
            "Get the elitism of the maze (path length)",
            "Get the solution percentage",
            "",
            "Save the maze as points",
            "Save the maze as a png image",
            "Save the maze as an ascii text file",
            "",
            "Clear the maze and return to the menu"}
            input = SolvingMenu(temparr, "What would you like to do with the maze", greatestX + 6, 3)
            SolvingInput(input, showpath, YposAfterMaze, solvingdelay, Maze, "", pathColour, backGroundColour, solvingColour)
        ElseIf input = "Get the amount of Dead-ends in the maze" Then
            Console.SetCursorPosition(0, Console.WindowHeight - 1)
            SetBoth(ConsoleColor.Black)
            Console.ForegroundColor = ConsoleColor.White
            Dim deCount As Integer = GetDeadEndCount(Maze)
            Console.Write($"Number of dead-ends: {deCount}     Percentage of the maze: {Math.Ceiling((deCount / Maze.Count) * 100)}%")
            Console.ReadKey()
        ElseIf input = "Get the amount of junctions in the maze" Then
            Console.SetCursorPosition(0, Console.WindowHeight - 1)
            SetBoth(ConsoleColor.Black)
            Console.ForegroundColor = ConsoleColor.White
            Dim jCount As Integer = GetJunctionCount(Maze)
            Console.Write($"Number of junctions: {jCount}       Percentage of the maze: {Math.Ceiling((jCount / Maze.Count) * 100)}%")
            Console.ReadKey()
        ElseIf input = "Get the amount of corners in the maze" Then
            Console.SetCursorPosition(0, Console.WindowHeight - 1)
            SetBoth(ConsoleColor.Black)
            Console.ForegroundColor = ConsoleColor.White
            Dim cCount As Integer = GetCornerCount(Maze)
            Console.Write($"Number of corners: {cCount}     Percentage of the maze: {Math.Ceiling((cCount / Maze.Count) * 100)}%")
            Console.ReadKey()
        ElseIf input = "Get the average corridor length" Then
            Console.SetCursorPosition(0, Console.WindowHeight - 1)
            SetBoth(ConsoleColor.Black)
            Console.ForegroundColor = ConsoleColor.White
            Console.Write($"Average corridor length: {Math.Ceiling(StraightWays(Maze))}")
            Console.ReadKey()
        ElseIf input = "Get the elitism of the maze (path length)" Then
            Dim pathLength As Integer = -1
            Dijkstras(Maze, False, 0, 0, pathLength)
            Console.SetCursorPosition(0, Console.WindowHeight - 1)
            SetBoth(ConsoleColor.Black)
            Console.ForegroundColor = ConsoleColor.White
            Console.Write($"Path length: {pathLength}")
            Console.ReadKey()
        ElseIf input = "Get the solution percentage" Then
            Dim pathLength As Integer = -1
            Dijkstras(Maze, False, 0, 0, pathLength)
            Console.SetCursorPosition(0, Console.WindowHeight - 1)
            SetBoth(ConsoleColor.Black)
            Console.ForegroundColor = ConsoleColor.White
            Console.Write($"Solution percentage: {Math.Ceiling(pathLength / Maze.Count * 100)}%")
            Console.ReadKey()
        ElseIf input = "Save the maze as an ascii text file" Then
            SaveMazeAscii(Maze)
        ElseIf input = "" Then
            Console.Clear()
            Console.WriteLine("A critical error has occured that has caused the program to no longer work")
            End
        End If
    End Sub
    Sub GetMazeInfo(ByRef Width As Integer, ByRef Height As Integer, ByRef DelayMS As Integer, ByRef Limits() As Integer, ByRef ShowGeneration As Boolean, Clear As Boolean, y As Integer, Optional NeedExtraInfo As Boolean = True, Optional fullScreen As Boolean = False)
        Console.SetCursorPosition(0, y)
        If Not NeedExtraInfo Then
            ShowGeneration = HorizontalYesNo(Console.CursorTop, "Do you want to see the maze being generated: ", False, If(Clear, True, False), False)
            Console.SetCursorPosition(0, Console.CursorTop + 1)
            If ShowGeneration Then
                DelayMS = GetIntInputArrowKeys("Delay when making the Maze (MS): ", 100, 0, False)
            Else
                DelayMS = 0
            End If
        End If
        If NeedExtraInfo Then Console.Clear()
        Width = GetIntInputArrowKeys($"Width of the Maze: ", (Console.WindowWidth - If(fullScreen, 58, 12)) / 2, 20, False) * 2
        Height = GetIntInputArrowKeys($"Height of the Maze: ", Console.WindowHeight - 6, 20, False)
        If Width Mod 2 = 0 Then
            Width += 1
        End If
        If Height Mod 2 = 0 Then
            Height += 1
        End If
        Limits = {5, 3, Width, Height}
        Console.Clear()
    End Sub
End Module