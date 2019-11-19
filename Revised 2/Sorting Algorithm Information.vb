Module Sorting_Algorithm_Information
    Function returnBogosortInfo()
        Return "The Process in bogo sort is to shuffle the array until it is sorted
Pseudocode from wikipedia
while not isInOrder(deck):
    shuffle(deck)"
    End Function
    Function returnBogobogosortInfo()
        Return "This is a verson of bogosort
It works by recursively calling itself with smaller and smaller copies of the beginning list to see if they are sorted"
    End Function
    Function returnBozosortInfo()
        Return "Bozosort works by randomly picking two indexes, swapping the values at each index and then checking if it is sorted"
    End Function
    Function returnBricksortInfo()
        Return "Brick Sort works comparing all odd/even indexed pairs of adjacent elements in the list
and if a pair is in the wrong order the first the elements are switched"
    End Function
    Function returnBubblesortInfo()
        Return "Bubble sort works by looping through the list and comparing adjacent elements and swapping them if they are in the wrong order"
    End Function
    Function returnBucketsortInfo()
        Return "Bucket sort works by putting the elements of the original list into a number of buckets
Each bucket is then sorted infividually
The buckets are then combined to make the full list"
    End Function
    Function returnCocktailshakersortInfo()
        Return "Cocktail shaker sort is a variation of bubble sort
It works by applying bubble sort in both directions"
    End Function
    Function returnCombsortInfo()
        Return "Comb sort is an improved version of bubble sort
Comb sort uses a gap value which dictates which elements are comapred
This gap value is shrunk over iterations so until it reaches a value of 1"
    End Function
    Function returnCountingsortInfo()
        Return "Counting sort works by bcounting the number of objects that have distint key values
Then arithmetic is done to calculate the position of each object"
    End Function
    Function returnCyclesortInfo()
        Return "Cycle Sort is an inplace sorting algorithm
It works by using two rules
    1. If the element is already at the correct position, do nothing.
    2. If it is not, we will write it to its intended position. That position is inhabited by a different element b, which we then 
       have to move to its correct position. This process of displacing elements to their correct positions continues until an element is 
       moved to the original position of a. This completes a cycle."
    End Function
    Function returnGnomesortInfo()
        Return "
Gnome Sort is based on the comcept of a garden gnome sorting his flower pots
The gnome looks at the flower pot next to him and the previous one if they are in the right order
he moves onto the next pot, if they aren't in the right order then he swaps them and steps one pot backwards
If there is no previous pot he steps forward one pot"
    End Function
    Function returnHeapsortInfo()
        Return "Heap sort works by ordering the list into a binary tree"
    End Function
    Function returnInsertionsortInfo()
        Return "At each position  in the list it checks the value there against the largest value in the sorted list, which is the elemts that have already been looked at
If it is lager then it leaves the element in place and moves to the next
If it is smaller it finds the correct position within the sorted list and shifts all larger elements up by one"
    End Function
    Function returnMergesortInfo()
        Return "Merge sort works by
Dividing the unsorted lists in a numbers of sublists each containing one element
These sublists are then repeatdly merged to produce new sublists until there is only one list remaining"
    End Function
    Function returnPancakesortInfo()
        Return "The goal of the pancake sort is to have as few flips as possible
Pseudocode from GeeksForGeeks
1) Start from current size equal to n and reduce current size by one while it’s greater than 1. Let the current size be curr_size. Do following for every curr_size
……a) Find index of the maximum element in arr[0..curr_szie-1]. Let the index be ‘mi’
……b) Call flip(arr, mi)
……c) Call flip(arr, curr_size-1)"
    End Function
    Function returnPigeonholesortInfo()
        Return "Pigeonhole sort
Pseudocode from GeeksForGeeks
    1. Find minimum and maximum values in array. Let the minimum and maximum values be ‘min’ and ‘max’ respectively. Also find range as ‘max-min-1’.
    2. Set up an array of initially empty ""pigeonholes"" the same size as of the range.
    3. Visit each element of the array and then put each element in its pigeonhole. An element arr[i] is put in hole at index arr[i] – min.
    4. Start the loop all over the pigeonhole array in order and put the elements from non- empty holes back into the original array."
    End Function
    Function returnQuicksortInfo()
        Return "Quicksort
Quicksort works by picking an element to be a pivot point
The algorithm then reorders the list so that all elements with less than the value of the pivot come before it and all of the values greater than the pivot come after it
These two steps are recursively applied to the list"
    End Function
    Function returnSelectionsortInfo()
        Return "Selection sort works by repeatdly funding the minimum element in the unsorted part of the list
and putting it at the beginning of the list"
    End Function
    Function returnShellsortInfo()
        Return "Shell Sort is a variation of insertion sort
it works by moving elements only one position instead of being placed at the beggining like in insertion sort"
    End Function
    Function returnSlowsortInfo()
        Return "Slow sort is a variation of a divide and conquer algorithm
It works by recursively sorting the first half of the list
Sorting the second half of the list recursively
Finding the maximum of the whole list and placing it at the end of the list
Then recursively sorting the whole list"
    End Function
    Function returnStoogesortInfo()
        Return "Stoogesort
Psuedocode from wikipedia
 function stoogesort(array L, i = 0, j = length(L)-1){
     if L[i] > L[j] then
         L[i] ↔ L[j]
     if (j - i + 1) > 2 then
         t = (j - i + 1) / 3
         stoogesort(L, i  , j-t)
         stoogesort(L, i+t, j  )
         stoogesort(L, i  , j-t)
     return L
 }"
    End Function
End Module
