Module program_instructions
    Sub instructionsforuse()
        InitialiseScreen()
        Console.WriteLine("Information for using this program:
Use the up and down arrow keys for navigating the menus
Use the enter key to select an option
Press 'I' to bring up information on the algorithm

When inputting integer values:
The up and down arrow keys increment by 1
The right and left arrow keys increment by 10
The 'M' key will set the number to the maximum value it can be
The 'H' key will set the number to half of the maximum value it can be

When saving files:
The file cannot have the same name as an existing file")
        Console.ReadKey()
    End Sub
End Module
