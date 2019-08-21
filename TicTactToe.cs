using System;


namespace ConsoleApplication1 {

    public class TicTacToe
    {
        public static int privcounter = 0;

        private enum val { X, O };

        private int choice;

        private int[,] array;

        private int left_center = -1, right_center = -1, up_center = -1, down_center = -1, center = -1, upleft_corner = -1, upright_corner = -1, downleft_corner = -1, downright_corner = -1;

        private int s1, s2, s3, s4, s5;






        public TicTacToe()
        {




            Console.WriteLine("Welcome to tictactoe interactive, would you like to play against a firend or against a a COM ?\n");
            choice = 0;
            s1 = s2 = s3 = s4 = s5 = 0;
            array = new int[3, 3];



        }




        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }




        public void printoptions()
        {

            Console.WriteLine("1.Play against a friend\n");

            Console.WriteLine("2.Play against AI\n");

            Console.WriteLine("Press aby other number key to exit game\n");

            String choiceinput = Console.ReadLine();

            //  while (Console.ReadLine() is String) {

            //  Console.WriteLine("Error : Input is not of type integer. Enter either 1 or 2 :");

            //}



            if (choicecheck(choice, choiceinput) == 1)
            {
                printgame(array);
                twoplayer(array);
            }
            else
            {
                printgame(array);
                AIplayer(array);

            }




        }


        public int choicecheck(int option, String Input)
        {

            /*
                while (option != 1 && option != 2)
                {

                    
                    try
                    {
                   
                    throw new Exception("Error : The value you have entered is incorrect, Enter a valid choice : ");

                    //Console.WriteLine();

                    }
                    catch (Exception ex)
                    {

                        Console.Write(ex.Message);
                        option = int.Parse(Console.ReadLine());
                    }

                }

            */

            while (!int.TryParse(Input, out option) && option != 1 && option != 2)
            {
                try
                {

                    throw new Exception("Error : The value you have entered is incorrect, Enter a valid choice : ");

                    //Console.WriteLine();

                }
                catch (Exception ex)
                {

                    Console.Write(ex.Message);
                    Input = Console.ReadLine();
                }


            }



            return option;
        }



        public void printgame(int[,] matrix)
        {



            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n    R/C    0      1      2   \n\n");


            for (int i = 0; i < 3; i++)
            {
                Console.Write("   " + i + "   ");
                for (int j = 0; j < 3; j++)
                {

                    matrix[i, j] = -1;
                    Console.Write("|  *  |");

                }

                Console.WriteLine("\n");

            }




        }


        void reprint(int[,] matrix)
        {

            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n   R/C    0      1      2   \n\n");


            for (int i = 0; i < 3; i++)
            {
                Console.Write("   " + i + "   ");
                for (int j = 0; j < 3; j++)
                {

                    if (matrix[i, j] == -1)
                    {
                        Console.Write("|  *  |");

                    }
                    else if (matrix[i, j] == 1)
                    {
                        Console.Write("|  O  |");

                    }
                    else
                    {

                        Console.Write("|  X  |");

                    }

                }

                Console.WriteLine("\n");

            }



        }





        public void twoplayer(int[,] matrix)
        {
            int counter = 0;
            int negcounter = 9;
            int back2back = 0;
            int Xcount = 0;
            int Ocount = 0;

            while (counter > -1 && negcounter > 0)
            {
                Console.WriteLine("Remaining move(s): {0}", negcounter);
                if (counter % 2 == 0)
                {
                    Console.WriteLine("(Player 1) X number = " + Xcount + "| (Player 2) O number = " + Ocount);
                    Console.WriteLine("Curret Player : " + val.X + " Choose the Row and Column seperated by a WHITESPACE :");

                    String[] tokens = Console.ReadLine().Split();

                    int row = Int32.Parse(tokens[0]);
                    int col = Int32.Parse(tokens[1]);

                    int placer = (int)val.X;


                    while (validater(row, col, matrix) == false)
                    {

                        Console.WriteLine("Error : the inputs are not correct, enter the correct values :");

                        tokens = Console.ReadLine().Split();

                        row = Int32.Parse(tokens[0]);
                        col = Int32.Parse(tokens[1]);

                        placer = (int)val.X;

                    }


                    placing(row, col, matrix, placer);
                    counter++;
                    Xcount++;
                    negcounter--;
                    reprint(matrix);
                    back2back = winnercheck(matrix, placer);

                    if (back2back == 3)
                    {

                        Console.WriteLine("Player 1 has won the game !\nPress any key to continue");
                        break;
                    }
                    else
                    {

                        ;
                    }

                }
                else
                {
                    Console.WriteLine("(Player 1) X number = " + Xcount + "| (Player 2) O number = " + Ocount);
                    Console.WriteLine("Curret Player : " + val.O + " Choose the Row and Column seperated by a WHITESPACE :");

                    String[] tokens = Console.ReadLine().Split();

                    int row = Int32.Parse(tokens[0]);
                    int col = Int32.Parse(tokens[1]);

                    int placer = (int)val.O;


                    while (validater(row, col, matrix) == false)
                    {

                        Console.WriteLine("Error : the inputs are not correct, enter the correct values :");

                        tokens = Console.ReadLine().Split();

                        row = Int32.Parse(tokens[0]);
                        col = Int32.Parse(tokens[1]);

                        placer = (int)val.O;

                    }


                    placing(row, col, matrix, placer);
                    counter++;
                    Ocount++;
                    negcounter--;
                    reprint(matrix);
                    back2back = winnercheck(matrix, placer);

                    if (back2back == 3)
                    {

                        Console.WriteLine("Player 2 has won the game !\nPress any key to continue");
                        break;

                    }

                }

            }


            if (negcounter == 9 && back2back != 3)
            {

                Console.WriteLine("No one has won\nPress any key to continue");


            }
            else if (back2back != 3){


                String d = "draw !";
                Console.WriteLine("It's a {0}", d);

            }



        }

        public void AIplayer(int[,] matrix)
        {

            int counter = 0;
            int negcounter = 9;
            int back2back = 0;
            int Xcount = 0;
            int Ocount = 0;

            while (counter > -1 && negcounter > 0)
            {
                Console.WriteLine("Remaining move(s): {0}", negcounter);
                if (counter % 2 == 0)
                {
                    Console.WriteLine("(Player 1) X number = " + Xcount + "| (Player 2) O number = " + Ocount);
                    Console.WriteLine("Curret Player : " + val.X + " Choose the Row and Column seperated by a WHITESPACE :");

                    OffensiveAI(negcounter, matrix, (int)val.X, (int)val.O);


                    int placer = (int)val.X;





                    counter++;
                    Xcount++;
                    negcounter--;
                    reprint(matrix);
                    back2back = winnercheck(matrix, placer);

                    if (back2back == 3)
                    {

                        Console.WriteLine("Player 1 has won the game !\nPress any key to continue");
                        break;
                    }
                    else
                    {

                        ;
                    }

                }
                else
                {
                    Console.WriteLine("(Player 1) X number = " + Xcount + "| (Player 2) O number = " + Ocount);
                    Console.WriteLine("Curret Player : " + val.O + " Choose the Row and Column seperated by a WHITESPACE :");

                    String[] tokens = Console.ReadLine().Split();

                    int row = Int32.Parse(tokens[0]);
                    int col = Int32.Parse(tokens[1]);

                    int placer = (int)val.O;


                    while (validater(row, col, matrix) == false)
                    {

                        Console.WriteLine("Error : the inputs are not correct, enter the correct values :");

                        tokens = Console.ReadLine().Split();

                        row = Int32.Parse(tokens[0]);
                        col = Int32.Parse(tokens[1]);

                        placer = (int)val.O;

                    }


                    placing(row, col, matrix, placer);
                    counter++;
                    Ocount++;
                    negcounter--;
                    reprint(matrix);
                    back2back = winnercheck(matrix, placer);

                    if (back2back == 3)
                    {

                        Console.WriteLine("Player 2 has won the game !\nPress any key to continue");
                        break;

                    }

                }

            }


            if (negcounter == 9 && back2back != 3)
            {

                Console.WriteLine("No one has won\nPress any key to continue");


            }
            else if(back2back != 3)
            {

                String d = "draw !";
                Console.WriteLine("It's a {0}", d);
                

            }


        }

        public void placing(int row, int col, int[,] matrix, int placer)
        {//places the respective player's piece onto the game


            matrix[row, col] = placer;

            if (row == 0 && col == 0)
            {
                upleft_corner = placer;
            }
            else if (row == 0 && col == 1)
            {
                up_center = placer;
            }
            else if (row == 0 && col == 2)
            {
                upright_corner = placer;
            }
            else if (row == 1 && col == 0)
            {
                left_center = placer;
            }
            else if (row == 1 && col == 1)
            {
                center = placer;
            }
            else if (row == 1 && col == 2)
            {
                right_center = placer;
            }
            else if (row == 2 && col == 0)
            {
                downleft_corner = placer;
            }
            else if (row == 2 && col == 1)
            {
                down_center = placer;
            }
            else if (row == 2 && col == 2)
            {
                downright_corner = placer;
            }
            else
            {

                ;
            }


        }


        public bool validater(int row, int col, int[,] matrix)
        {//checks to see if inputs are valid


            if (row > 2 || col > 2)
            {

                return false;

            }


            if (matrix[row, col] == -1)
            {
                return true;

            }
            else
            {

                return false;

            }


        }


        public int winnercheck(int[,] matrix, int placervalue)
        {

            int numcount = 0;








            // check diagonal \ 
            for (int i = 0; i < 3; i++)
            {

                if (matrix[i, i] == placervalue)
                {

                    numcount++;
                }


                if (numcount == 3)
                {


                    return numcount;
                }


            }

            numcount = 0;


            //check diagonal /
            for (int z = 0; z < 3; z++)
            {



                if (matrix[2 - z, z] == placervalue)
                {

                    numcount++;
                }


                if (numcount == 3)
                {


                    return numcount;
                }





            }

            numcount = 0;

            //check horizontal 
            for (int l = 0; l < 3; l++)
            {

                for (int k = 0; k < 3; k++)
                {

                    if (matrix[l, k] == placervalue)
                    {

                        numcount++;


                    }


                    if (numcount == 3)
                    {


                        return numcount;
                    }

                }

                numcount = 0;
            }

            numcount = 0;

            //check vertical
            for (int l = 0; l < 3; l++)
            {

                for (int k = 0; k < 3; k++)
                {

                    if (matrix[k, l] == placervalue)
                    {

                        numcount++;


                    }







                    if (numcount == 3)
                    {


                        return numcount;
                    }

                }
                numcount = 0;
            }

            numcount = 0;

            /*

            if (matrix[0, 0] == placervalue)
            {
                upleft_corner = placervalue;
            }
            else if (matrix[0, 1] == placervalue)
            {
                up_center = placervalue;
            }
            else if (matrix[0, 2] == placervalue)
            {
                upright_corner = placervalue;
            }
            else if (matrix[1, 0] == placervalue)
            {
                left_center = placervalue;
            }
            else if (matrix[1, 1] == placervalue)
            {
                center = placervalue;
            }
            else if (matrix[1, 2] == placervalue)
            {
                right_center = placervalue;
            }
            else if (matrix[2, 0] == placervalue)
            {
                downleft_corner = placervalue;
            }
            else if (matrix[2, 1] == placervalue)
            {
                down_center = placervalue;
            }
            else if (matrix[2, 2] == placervalue)
            {
                downright_corner = placervalue;
            }
            else
            {

                ;
            }
           

            
            */




            return numcount;




        }


        public void OffensiveAI(int remainingturns, int[,] matrix, int X = 0, int O = 1)
        {


            if (remainingturns == 9)
            {
                //   Random rand = new Random();

                int placement = RandomNumber(0, 5);
                
                switch (placement)
                {


                    case 0:
                        placing(0, 0, matrix, X);
                        upleft_corner = X;
                        break;
                    case 1:
                        placing(2, 0, matrix, X);
                        downleft_corner = X;
                        break;
                    case 2:
                        placing(1, 1, matrix, X);
                        center = X;
                        break;
                    case 3:
                        placing(0, 2, matrix, X);
                        upright_corner = X;
                        break;
                    case 4:
                        placing(2, 2, matrix, X);
                        downright_corner = X;
                        break;

                    default:
                        Console.Write("Default case :");
                        break;

                }

            }
            else
            {

                AIwinningmove(matrix, X);
                AIoffensivedefense(matrix, O, X);


                if (remainingturns == 7 && privcounter == 0)
                {

                    if (center == X)
                    {

                        if (matrix[0, 1] == O)
                        {

                            if (validater(2, 0, matrix))
                            {

                                placing(2, 0, matrix, X);
                                downleft_corner = X;
                                privcounter++;

                            }
                            else
                            {

                                placing(2, 2, matrix, X);
                                downright_corner = X;
                                privcounter++;
                            }


                        }
                        else if (matrix[1, 0] == O)
                        {

                            if (validater(2, 2, matrix))
                            {

                                placing(2, 2, matrix, X);
                                downright_corner = X;
                                privcounter++;
                            }
                            else
                            {

                                placing(0, 2, matrix, X);
                                upleft_corner = X;
                                privcounter++;
                            }

                        }
                        else if (matrix[1, 2] == O)
                        {

                            if (validater(0, 0, matrix))
                            {

                                placing(0, 0, matrix, X);
                                upright_corner = X;
                                privcounter++;
                            }
                            else
                            {

                                placing(2, 0, matrix, X);
                                downleft_corner = X;
                                privcounter++;
                            }

                        }
                        else if (matrix[2, 1] == O)
                        {
                            if (validater(0, 0, matrix))
                            {

                                placing(0, 0, matrix, X);
                                upleft_corner = X;
                                privcounter++;
                            }
                            else
                            {

                                placing(0, 2, matrix, X);
                                upright_corner = X;
                                privcounter++;
                            }
                        }









                        if (matrix[0, 0] == O)
                        {

                            placing(2, 2, matrix, X);
                            downright_corner = X;
                            privcounter++;

                        }
                        else if (matrix[0, 2] == O)
                        {



                            placing(2, 0, matrix, X);
                            downleft_corner = X;
                            privcounter++;

                        }
                        else if (matrix[2, 0] == O)
                        {


                            placing(0, 2, matrix, X);
                            upright_corner = X;
                            privcounter++;


                        }
                        else if (matrix[2, 2] == O)
                        {

                            placing(0, 0, matrix, X);
                            upleft_corner = X;
                            privcounter++;

                        }















                    }

                    else if (upleft_corner == X)
                    {


                        if (center == O)
                        {

                            placing(2, 2, matrix, X);
                            privcounter++;
                            s5++;
                        }



                        else if (left_center == O || downleft_corner == O)
                        {

                            placing(0, 2, matrix, X);
                            privcounter++;
                            s1++;

                            // if (left_center == O && upleft_corner == O) {

                            //   placing(2, 2, matrix, X);

                            //}



                        }
                        else if (up_center == O || upright_corner == O)
                        {

                            placing(2, 0, matrix, X);
                            privcounter++;
                            s1++;


                        }





                        else if (right_center == O || downright_corner == O)
                        {

                            placing(0, 2, matrix, X);
                            privcounter++;
                            s2++;

                            // if (left_center == O && upleft_corner == O) {

                            //   placing(2, 2, matrix, X);

                            //}



                        }
                        else if (down_center == O || downright_corner == O)
                        {

                            placing(2, 0, matrix, X);
                            privcounter++;
                            s2++;


                        }



                    }
                    else if (upright_corner == X)
                    {

                        if (center == O)
                        {

                            placing(2, 0, matrix, X);
                            privcounter++;
                            s5++;
                        }



                        else if (right_center == O || downright_corner == O)
                        {

                            placing(0, 0, matrix, X);
                            privcounter++;
                            s1++;
                        }
                        else if (up_center == O || upleft_corner == O)
                        {

                            placing(2, 2, matrix, X);
                            privcounter++;
                            s1++;


                        }




                        else if (left_center == O || downleft_corner == O)
                        {

                            placing(0, 0, matrix, X);
                            privcounter++;
                            s2++;
                        }
                        else if (down_center == O || downleft_corner == O)
                        {

                            placing(2, 2, matrix, X);
                            privcounter++;
                            s2++;


                        }




                    }
                    else if (downleft_corner == X)
                    {


                        if (center == O)
                        {

                            placing(0, 2, matrix, X);
                            privcounter++;
                            s5++;
                        }




                        else if (left_center == O || upleft_corner == O)
                        {

                            placing(2, 2, matrix, X);
                            privcounter++;
                            s1++;
                        }
                        else if (down_center == O || downright_corner == O)
                        {

                            placing(0, 0, matrix, X);
                            privcounter++;
                            s1++;

                        }




                        else if (right_center == O || upright_corner == O)
                        {

                            placing(2, 2, matrix, X);
                            privcounter++;
                            s2++;
                        }
                        else if (up_center == O || upright_corner == O)
                        {

                            placing(2, 2, matrix, X);
                            privcounter++;
                            s2++;


                        }






                    }
                    else if (downright_corner == X)
                    {


                        if (center == O)
                        {

                            placing(0, 0, matrix, X);
                            privcounter++;
                            s5++;
                        }


                        else if (right_center == O || upright_corner == O)
                        {

                            placing(2, 0, matrix, X);
                            privcounter++;
                            s1++;
                        }
                        else if (down_center == O || downleft_corner == O)
                        {

                            placing(0, 2, matrix, X);
                            privcounter++;
                            s1++;


                        }





                        else if (left_center == O || upleft_corner == O)
                        {

                            placing(2, 0, matrix, X);
                            privcounter++;
                            s2++;
                        }
                        else if (up_center == O || upleft_corner == O)
                        {

                            placing(2, 0, matrix, X);
                            privcounter++;
                            s2++;

                        }



                    }









                }
                else if (remainingturns == 5 && privcounter == 0)
                {






                    if (s1 > 0)
                    {

                        if (validater(0, 0, matrix) && matrix[1, 0] != O && matrix[0, 1] != O)
                        {

                            placing(0, 0, matrix, X);
                            privcounter++;
                        }
                        else if (validater(0, 2, matrix) && matrix[1, 2] != O && matrix[0, 1] != O)
                        {

                            placing(0, 2, matrix, X);
                            privcounter++;
                        }
                        else if (validater(2, 0, matrix) && matrix[1,0] != O && matrix[2,1] != O)
                        {

                            placing(2, 0, matrix, X);
                            privcounter++;
                        }
                        else
                        {

                            placing(2, 2, matrix, X);
                            privcounter++;

                        }

                    }
                    else if (s2 > 0)
                    {
                        if (validater(0, 0, matrix) && matrix[1, 0] != O && matrix[0, 1] != O)
                        {

                            placing(0, 0, matrix, X);
                            privcounter++;
                        }
                        else if (validater(0, 2, matrix) && matrix[1, 2] != O && matrix[0, 1] != O)
                        {

                            placing(0, 2, matrix, X);
                            privcounter++;
                        }
                        else if (validater(2, 0, matrix) && matrix[1, 0] != O && matrix[2, 1] != O)
                        {

                            placing(2, 0, matrix, X);
                            privcounter++;
                        }
                        else
                        {

                            placing(2, 2, matrix, X);
                            privcounter++;

                        }

                    }
                    else
                    {

                        //  Random var = new Random();
                        int value = RandomNumber(0, 3);
                        int value2 = RandomNumber(0, 3);

                        while (!validater(value, value2, matrix))
                        {
                            value = RandomNumber(0, 3);
                            value2 = RandomNumber(0, 3);

                        }

                        placing(value, value2, matrix, X);


                    }



                }
                else
                {

                    if (privcounter == 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {

                            for (int j = 0; j < 3; j++)
                            {

                                if (matrix[i, j] == -1)
                                {


                                    matrix[i, j] = X;
                                    i = j = 3;
                                    break;

                                }


                            }

                        }
                    }


                }


                privcounter = 0;

            }







        }

        void AIwinningmove(int[,] matrix, int AI)
        {


            if (matrix[0, 0] == -1 && ((center == AI && downright_corner == AI) || (left_center == AI && downleft_corner == AI) || (up_center == AI && upright_corner == AI)))
            {

                placing(0, 0, matrix, AI);
                upleft_corner = AI;
                privcounter++;
            }
            else if (matrix[0, 1] == -1 && ((center == AI && down_center == AI) || (upright_corner == AI && upleft_corner == AI)))
            {

                placing(0, 1, matrix, AI);
                up_center = AI;
                privcounter++;
            }
            else if (matrix[0, 2] == -1 && ((center == AI && downleft_corner == AI) || (right_center == AI && downright_corner == AI) || (up_center == AI && upleft_corner == AI)))
            {

                placing(0, 2, matrix, AI);
                upright_corner = AI;
                privcounter++;
            }
            else if (matrix[1, 0] == -1 && ((center == AI && right_center == AI) || (upleft_corner == AI && downleft_corner == AI)))
            {

                placing(1, 0, matrix, AI);
                left_center = AI;
                privcounter++;
            }
            else if (matrix[1, 1] == -1 && ((left_center == AI && right_center == AI) || (up_center == AI && down_center == AI) || (upright_corner == AI && downleft_corner == AI) || (upleft_corner == AI && downright_corner == AI)))
            {

                placing(1, 1, matrix, AI);
                center = AI;
                privcounter++;
            }
            else if (matrix[1, 2] == -1 && ((center == AI && left_center == AI) || (upright_corner == AI && downright_corner == AI)))
            {

                placing(1, 2, matrix, AI);
                right_center = AI;
                privcounter++;
            }
            else if (matrix[2, 0] == -1 && ((center == AI && upright_corner == AI) || (left_center == AI && upleft_corner == AI) || (down_center == AI && downright_corner == AI)))
            {

                placing(2, 0, matrix, AI);
                downleft_corner = AI;
                privcounter++;
            }
            else if (matrix[2, 1] == -1 && ((center == AI && up_center == AI) || (downright_corner == AI && downleft_corner == AI)))
            {

                placing(2, 1, matrix, AI);
                down_center = AI;
                privcounter++;
            }
            else if (matrix[2, 2] == -1 && ((center == AI && upleft_corner == AI) || (right_center == AI && upright_corner == AI) || (down_center == AI && downleft_corner == AI)))
            {

                placing(2, 2, matrix, AI);
                downright_corner = AI;
                privcounter++;
            }



        }

        void AIoffensivedefense(int[,] matrix, int OPP, int AI)
        {




            if (matrix[0, 0] == -1 && ((center == OPP && downright_corner == OPP) || (left_center == OPP && downleft_corner == OPP) || (up_center == OPP && upright_corner == OPP)))
            {

                placing(0, 0, matrix, AI);
                upleft_corner = AI;
                privcounter++;
            }
            else if (matrix[0, 1] == -1 && ((center == OPP && down_center == OPP) || (upright_corner == OPP && upleft_corner == OPP)))
            {

                placing(0, 1, matrix, AI);
                up_center = AI;
                privcounter++;
            }
            else if (matrix[0, 2] == -1 && ((center == OPP && downleft_corner == OPP) || (right_center == OPP && downright_corner == OPP) || (up_center == OPP && upleft_corner == OPP)))
            {

                placing(0, 2, matrix, AI);
                upright_corner = AI;
                privcounter++;
            }
            else if (matrix[1, 0] == -1 && ((center == OPP && right_center == OPP) || (upleft_corner == OPP && downleft_corner == OPP)))
            {

                placing(1, 0, matrix, AI);
                left_center = AI;
                privcounter++;

            }
            else if (matrix[1, 1] == -1 && ((left_center == OPP && right_center == OPP) || (up_center == OPP && down_center == OPP) || (upright_corner == OPP && downleft_corner == OPP) || (upleft_corner == OPP && downright_corner == OPP)))
            {

                placing(1, 1, matrix, AI);
                center = AI;
                privcounter++;

            }
            else if (matrix[1, 2] == -1 && ((center == OPP && left_center == OPP) || (upright_corner == OPP && downright_corner == OPP)))
            {

                placing(1, 2, matrix, AI);
                right_center = AI;
                privcounter++;

            }
            else if (matrix[2, 0] == -1 && ((center == OPP && upright_corner == OPP) || (left_center == OPP && upleft_corner == OPP) || (down_center == OPP && downright_corner == OPP)))
            {

                placing(2, 0, matrix, AI);
                downleft_corner = AI;
                privcounter++;
            }
            else if (matrix[2, 1] == -1 && ((center == OPP && up_center == OPP) || (downright_corner == OPP && downleft_corner == OPP)))
            {

                placing(2, 1, matrix, AI);
                down_center = AI;
                privcounter++;

            }
            else if (matrix[2, 2] == -1 && ((center == OPP && upleft_corner == OPP) || (right_center == OPP && upright_corner == OPP) || (down_center == OPP && downleft_corner == OPP)))
            {

                placing(2, 2, matrix, AI);
                downright_corner = AI;
                privcounter++;

            }



        }




    }

    
}