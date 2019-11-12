Module Maze_Terms
    Sub mazeterms()
        InitialiseScreen()
        Console.WriteLine("Terms that are used in this program:
A perfect maze is a maze that has no loops, all of the maze generation algorithms in this program (except for conways game of life) generate perfect mazes
A perfect maze has only one solution
Braiding a maze is the process of removing dead-ends, which adds loops/cycles
Sparseness in a maze is a maze which doesn't use the entire grid
A unicursal maze is a maze without any junctions, this can be refered to as a labyrinth
The Elitism of a maze indicates the length of the solution of the maze

There are two types of maze creation
    Wall adders
        this is an algorithm that generates the maze by adding walls to the grid
    Passage carvers
        this is an algorithm that carves passages to create a maze")
        Console.ReadKey()
    End Sub
End Module
