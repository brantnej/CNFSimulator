# CNFSimulator
This program for CS 321 takes the definition of a CNF grammar as an input and then prompts the user for an input. 
It then uses the CYK algorithm to determine if the input string is in the grammar and outputs the table.

# Input
The definition of the grammar is in JSON and is in the format
```
{
  "Variables": [
    "A",
    "B",
    ...
  ],
  "Terminals": [
    "a",
    "b",
    ...
  ],
  "Rules": [
    {
      "Start": "S",
      "Productions": [
        [ "A", "B" ],
        [ "a" ],
        ...
      ]
    },
    ...
  "Start": "S"
}
```
where the start symbol is in the Variables array and each rule starts with a Variable, and each production is either 2 variables or 1 terminal.

# Dependencies
This program uses Newtonsoft for deserializing JSON to a grammar, and ConsoleTable for outputting the CYK table.
