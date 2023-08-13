using System;
using System.IO;

namespace WordsearchAssessedCoursework
{
    class Program
    {
        ///struct word is used for the data of each hidden word in the save file
        struct word
        {
            public string wordString;
            public int startX;
            public int startY;
            public string direction;
            public int found;
            public int endX;
            public int endY;
        }

        static void wordsearchUpdate(char[,] wordsearchChars, char[,] hiddenWordChars, word[] words, char[,] hiddenWordFound, char[] wordChars, int i, int z, int xdir, int ydir, out bool error)
        {
            error = false;
            try
            {
                wordsearchChars[words[z].startX + xdir * i, words[z].startY + ydir * i] = wordChars[i];
                if (char.IsLetter(hiddenWordChars[words[z].startX + xdir * i, words[z].startY + ydir * i]) == true && hiddenWordChars[words[z].startX + xdir * i, words[z].startY + ydir * i] != wordChars[i])
                {
                    error = true;
                }
                hiddenWordChars[words[z].startX + xdir * i, words[z].startY + ydir * i] = wordChars[i];

                if (words[z].found == 1)
                {
                    hiddenWordFound[words[z].startX + xdir * i, words[z].startY + ydir * i] = '1';
                }
                if (i == words[z].wordString.Length - 1)
                {
                    words[z].endX = words[z].startX + xdir * i;
                    words[z].endY = words[z].startY + ydir * i;
                }
            }
            catch
            {
                error = true;
            }
        }

        static void createWordSearch(int numberOfColumns, int numberOfRows, int numberOfWords, word[] words, out char[,] wordsearchChars, out char[,] hiddenWordChars, out char[,] hiddenWordFound, out bool errorGlobal)
        {
            ///2d array that stores all the characters in the wordsearch and their positions
            wordsearchChars = new char[numberOfColumns, numberOfRows];
            hiddenWordChars = new char[numberOfColumns, numberOfRows];
            hiddenWordFound = new char[numberOfColumns, numberOfRows];
            ///fills the wordsearch with random letters
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    Random rnd = new Random();
                    char randomChar = (char)rnd.Next('a', 'z');
                    wordsearchChars[j, i] = randomChar;
                }
            }
            bool errorLocal = false;
            errorGlobal = false;
            for (int z = 0; z < numberOfWords;z++)
            {
                char[] wordChars = words[z].wordString.ToCharArray();
                for (int i = 0; i < words[z].wordString.Length; i++)
                {
                    if (words[z].direction == "up")
                    {
                        wordsearchUpdate(wordsearchChars, hiddenWordChars, words, hiddenWordFound, wordChars, i, z, 0, -1, out errorLocal);
                        if(errorLocal == true)
                        {
                            errorGlobal = true;
                        }
                    }
                    else if (words[z].direction == "down")
                    {
                        wordsearchUpdate(wordsearchChars, hiddenWordChars, words, hiddenWordFound, wordChars, i, z, 0, 1, out errorLocal);
                        if (errorLocal == true)
                        {
                            errorGlobal = true;
                        }
                    }
                    else if (words[z].direction == "left")
                    {
                        wordsearchUpdate(wordsearchChars, hiddenWordChars, words, hiddenWordFound, wordChars, i, z, -1, 0, out errorLocal);
                        if (errorLocal == true) 
                        {
                            errorGlobal = true;
                        }
                    }
                    else if (words[z].direction == "right")
                    {
                        wordsearchUpdate(wordsearchChars, hiddenWordChars, words, hiddenWordFound, wordChars, i, z, 1, 0, out errorLocal);
                        if (errorLocal == true) 
                        {
                            errorGlobal = true;
                        }
                    }
                    else if (words[z].direction == "leftup")
                    {
                        wordsearchUpdate(wordsearchChars, hiddenWordChars, words, hiddenWordFound, wordChars, i, z, -1, -1, out errorLocal);
                        if (errorLocal == true) 
                        {
                            errorGlobal = true;
                        }
                    }
                    else if (words[z].direction == "rightup")
                    {
                        wordsearchUpdate(wordsearchChars, hiddenWordChars, words, hiddenWordFound, wordChars, i, z, 1, -1, out errorLocal);
                        if (errorLocal == true) 
                        {
                            errorGlobal = true;
                        }
                    }
                    else if (words[z].direction == "leftdown")
                    {
                        wordsearchUpdate(wordsearchChars, hiddenWordChars, words, hiddenWordFound, wordChars, i, z, -1, 1, out errorLocal);
                        if (errorLocal == true) 
                        {
                            errorGlobal = true;
                        }
                    }
                    else if (words[z].direction == "rightdown")
                    {
                        wordsearchUpdate(wordsearchChars, hiddenWordChars, words, hiddenWordFound, wordChars, i, z, 1, 1, out errorLocal);
                        if (errorLocal == true) 
                        {
                            errorGlobal = true;
                        }
                    }

                }
            }            
        }


        static void displayWordSearch(int numberOfColumns, int numberOfRows, char[,] wordsearchChars, char[,] hiddenWordChars, char[,] hiddenWordFound)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  ");
            ///writes the first line of the word search with the coords of each column
            for (int z = 0; z < numberOfColumns; z++)
            {
                Console.Write(z + " ");
            }
            Console.Write("\n");
            ///writes out the row coords for each line and displays the wordsearch characters
            for (int i = 0; i < numberOfRows; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(i + " ");
                for (int j = 0; j < numberOfColumns; j++)
                {
                    if ((wordsearchChars[j, i] == hiddenWordChars[j, i]) && (hiddenWordFound[j,i] == '1'))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(wordsearchChars[j, i]);
                        Console.Write(" ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.Write(wordsearchChars[j, i]);
                        Console.Write(" ");
                    }
                    if (j == (numberOfColumns - 1))
                    {
                        Console.Write("\n");
                    }                   
                }
            }
            Console.ResetColor();
        }
        static void playWordSearch(int numberOfColumns, int numberOfRows, int numberOfWords, word[] words, out bool fileError)
        {
            while (true)
            {
                fileError = false;
                char[,] wordsearchChars, hiddenWordChars, hiddenWordFound;
                createWordSearch(numberOfColumns, numberOfRows, numberOfWords, words, out wordsearchChars, out hiddenWordChars, out hiddenWordFound, out fileError);
                if(fileError == true)
                {
                    Console.WriteLine("Error in wordsearch file detected");
                    break;
                }
                displayWordSearch(numberOfColumns, numberOfRows, wordsearchChars, hiddenWordChars, hiddenWordFound);
                Console.WriteLine("Words to find");
                int wordsFound = 0;
                Console.WriteLine("Enter save at the start of a round to save your game!");
                for (int i = 0; i < numberOfWords; i++)
                {
                    if (words[i].found != 1)
                    {
                        Console.WriteLine(words[i].wordString);
                    }
                    else
                    {
                        wordsFound++;
                    }
                }
                if(wordsFound == numberOfWords)
                {
                    Console.WriteLine("Well done you found all the hidden words!");
                    break;
                }
                Console.WriteLine(" Please enter start column");
                int startColumn = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter start row");
                int startRow = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter end column");
                int endColumn = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter end row");
                int endRow = int.Parse(Console.ReadLine());
                for (int i = 0; i < numberOfWords; i++)
                {
                    if (startColumn == words[i].startX && startRow == words[i].startY && endColumn == words[i].endX && endRow == words[i].endY && words[i].found != 1)
                    {
                        words[i].found = 1;
                    }
                }
            }
        }
        ///module loadWordSearch retrieves all the individual data elements from the requested wordsearch file
        static void loadWordSearch(string fileName, out int numberOfColumns, out int numberOfRows, out int numberOfWords, out word[] words, out bool error)
        {
            error = false;
            words = new word[0];
            StreamReader reader = new StreamReader(fileName + ".wrd");
            
            ///skips one line in order to only read word data
            string firstLine = reader.ReadLine();
            ///retrieves number of rows, columns and words from first line and stores them each in a variable
            string[] firstLineValues = firstLine.Split(',');
            numberOfColumns = int.Parse(firstLineValues[0]);
            numberOfRows = int.Parse(firstLineValues[1]);
            numberOfWords = int.Parse(firstLineValues[2]);

            try
            {
                words = new word[(numberOfWords + 1) * 4];
            }
            catch
            {
                error = true;
            }

            ///stores the word itself, its starting position and direction each in a record inside struct words
            for (int i = 0; i < numberOfWords; i++)
            {
                try
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    words[i].wordString = values[0];
                    words[i].startX = int.Parse(values[1]);
                    words[i].startX = int.Parse(values[1]);
                    words[i].startY = int.Parse(values[2]);
                    words[i].direction = values[3];
                }
                catch
                {
                    error = true;                   
                }
            }

            reader.Close();
        }

        static void Main(string[] args)
        {
            ///menu
            Console.WriteLine("Wordsearch Application");
            while(true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Use Default Wordsearch");
                Console.WriteLine("2. Load wordsearch from file");
                Console.WriteLine("3. Resume last saved wordsearch");
                string userChoice = Console.ReadLine();
                ///default wordsearch option
                if (userChoice == "1")
                {

                }
                ///load wordsearch from file option
                else if (userChoice == "2")
                {
                    bool fileError = false;
                    Console.WriteLine("What file do you want to load?");
                    for(int i = 1; i < 8; i++)
                    {
                        Console.WriteLine(i + " - " + "File0" + i);
                    }
                    int fileChoice = int.Parse(Console.ReadLine());
                    int numberOfColumns, numberOfRows, numberOfWords;
                    word[] words;
                    loadWordSearch("File0" + fileChoice, out numberOfColumns, out numberOfRows, out numberOfWords, out words, out fileError);
                    if(fileError == false)
                    {
                        playWordSearch(numberOfColumns, numberOfRows, numberOfWords, words, out fileError);
                    }
                    else
                    {
                        Console.WriteLine("Error when loading file, please choose another file to load");
                    }
                }
                ///resume last save option
                else if (userChoice == "3")
                {

                }
                else
                {
                    Console.WriteLine("Please pick a valid option...");
                }
            }
                       

        }
    }
}
