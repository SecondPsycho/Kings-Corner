/***********************************
 * King's Corner, by Cordell King
 * February 2023
***********************************/

class Program {

//* Testing stuff
public struct Globals
{
    public Globals()
    {
        //initialize here
        valid_inputs = new string[] {
            "A1","A2","A3","A4",
            "B1","B2","B3","B4",
            "C1","C2","C3","C4",
            "D1","D2","D3","D4",
            "1A","2A","3A","4A",
            "1B","2B","3B","4B",
            "1C","2C","3C","4C",
            "1D","2D","3D","4D"};
        to_letter = new char[] {'A','B','C','D'};
    }
    //methods here
    public string[] valid_inputs;
    public char[] to_letter;
}

public class Coords 
{
    public Coords(int px, int py) {
        x = px;
        y = py;
    }
    public int x;
    public int y;
}

public class Input 
{
    public Input() {
        code = 0;
        target_0 = new Coords(0,0);
        target_1 = new Coords(0,0);
    }
    public int code;
    public Coords target_0;
    public Coords target_1;
}

//Shuffle helper function; Swaps two card locations in a given deck
static void swap(string[] b_deck, int index0, int index1){
    var tmp = b_deck[index0];
    b_deck[index0] = b_deck[index1];
    b_deck[index1] = tmp;
}

//A function to randomly shuffle the deck
static void shuffle(string[] a_deck) {
    Random rnd = new Random();
    int len = a_deck.Length;
    for (int i = 1; i < len; i++) {
        swap(a_deck, i, rnd.Next()%(i+1));
    };
}

//Print the Board to the Screen
static void print_board(string[,] a_board) {
    Console.Write("\n---a--b--c--d----\n");
    for (int i = 0; i < 4; i++) {
        Console.Write(i+1);
        Console.Write("  ");
        for (int j = 0; j < 4; j++) {
            Console.Write(a_board[i,j]);
            Console.Write(' ');
        };
        Console.Write(" -\n");
    };
    Console.Write("-----------------\n");
}

//Print the card the Player just drew
static void print_next_card(string[] a_deck, int a_next_card) {
    Console.Write($"\nYour next card is: {a_deck[a_next_card]}.\n");
}

/* Create a tutorial/help page WIP
//var Tutorial = "Welcome to King's Corner!";
void print_tutorial() {

}
//*/

//Turn a string into coordinates
//WARNING: No protection. Only put a string in here if it has already passed the "valid input" test.
static Coords interpret(string coordinates) {
    char first_char = coordinates[0];
    char second_char = coordinates[1];

    switch (first_char) {
        case 'A':
            return new Coords(0,second_char - '1');
        case 'B':
            return new Coords(1,second_char - '1');
        case 'C':
            return new Coords(2,second_char - '1');
        case 'D':
            return new Coords(3,second_char - '1');
        default:
            switch (second_char) {
                case 'A':
                    return new Coords(0,first_char - '1');
                case 'B':
                    return new Coords(1,first_char - '1');
                case 'C':
                    return new Coords(2,first_char - '1');
                case 'D':
                    return new Coords(3,first_char - '1');
                default:
                    Console.WriteLine("Catastrophic Error.");
                    return new Coords(-1,-1);
            };
    };
}

//Set the final_input
static Input calibrate_input(Input final_input, int code) {
    final_input.code = code;
    return final_input;
}
static Input calibrate_input(Input final_input, int code, string coordinates_0) {
    final_input.code = code;
    final_input.target_0 = interpret(coordinates_0);
    return final_input;
}
static Input calibrate_input(Input final_input, int code, string coordinates_0, string coordinates_1) {
    final_input.code = code;
    final_input.target_0 = interpret(coordinates_0);
    final_input.target_1 = interpret(coordinates_1);
    return final_input;
}

//*Cue Tutorial
static void print_tutorial() {
    string the_text = "Welcome to King's Corner!\n" +
    "The game is played with an ordinary deck of playing cards.\n" + 
    "Each turn, you draw a single card and must place it somewhere on a 4x4 grid.\n" +
    "\n" +
    "Each card is represented with a 2-character code ('KS' for King of Spades, '0H' for 10 of Hearts, '3C' for 3 of Clubs, etc.)\n" +
    "The board is represented by an alphanumeric grid. During the game, it should look something like this:\n" +
    "---a--b--c--d----\n" +
    "1  3D 9H JC AH  -\n" +
    "2  QS 5D __ 3H  -\n" +
    "3  QH __ __ __  -\n" +
    "4  KD JS __ __  -\n" +
    "-----------------\n" +
    "\n" +
    "But you can't just put any card anywhere.\n" +
    "Kings can only be placed in corners.\n" +
    "Queens can only be placed in the leftmost and rightmost squares (But not corners).\n" +
    "Jacks can only be placed in the top or bottom squares (again, not counting corners).\n" +
    "---a--b--c--d----\n" +
    "1  K- J- J- K-  -\n" +
    "2  Q- __ __ Q-  -\n" +
    "3  Q- __ __ Q-  -\n" +
    "4  K- J- J- K-  -\n" +
    "-----------------\n" +
    "Aces and Numbered cards can be placed anywhere.\n" +
    "\n" +
    "If two cards add up to 10, you can match them together and remove them from the board.\n" +
    "(ex: a 3 and a 7, an ace and a nine, or two fives can be matched together. A ten can be matched with itself.)\n" +
    "(You can ignore suits.)\n" +
    "\n" +
    "If you run out of cards in the deck, you win!\n" +
    "If you can't find a legal move, you lose. Type 'quit' to exit the game.\n" +
    "(Don't feel bad. Not all games are winnable.)\n" +
    "(Type 'help' to see the controls.)\n" +
    "Good Luck!\n";

    Console.Write(the_text);
}
//*/

//Cue Help
static void print_help(string user_input) {
    if (user_input != "") {
        Console.Write($"\"{user_input}\" is not a valid input. ");
    };
    Console.WriteLine("Try one of the following:");
    Console.Write("  > place <square> (Place a card on the board. Ex: 'place a1')\n" +
    "  > match <square_1> <square_2> (Match two cards. Ex: 'match a3 b2')\n" +
    "  > help (Show this help page)\n" +
    "  > show (Show the board)\n" +
    "  > quit (Exit the game)\n");
    Console.WriteLine("If there are no legal moves, you lose. Type 'quit' to exit the game.");
}

//A helper function for that one dumb edge case
static bool isTen(string[,] board, string user_input) {
    var coordinates = interpret(user_input);
    if (board[coordinates.y,coordinates.x][0] == '0') {
        return true;
    };
    return false;
}

//Get input and make sure it's valid
static Input get_input(string[] deck, string[,] board, int next_card, Globals globals) {
    /* Return Code Key
     * 0: Invalid Input
     * 1: Quit
     * 2: Place
     * 3: Match
    //*/
    var final_input = new Input();

    //Collect User Input 
    //(It adds a '*' to the front, then removes it. This is to get rid of those annoying "string might be null" warnings.)
    Console.Write(">>> ");
    string original_input = (("*" + Console.ReadLine()).Substring(1));
    string user_input = original_input.ToUpper();
    string command;

    //Handle User Input
    //Acknowledge alternate 'place' syntax (Forgot to write "place")
    if (user_input.Length == 2) {
        if (globals.valid_inputs.Contains(user_input)) {
            if (isTen(board, user_input)) {
                final_input = calibrate_input(final_input, 3, user_input, user_input);
            } else {
                final_input = calibrate_input(final_input, 2, user_input);
            };
        } else {
            print_help(original_input);
        };

    //Process any four-letter commands
    } else if (user_input.Length == 4) {
        command = user_input.Substring(0,4);

        //Process 'help' command
        if (command == "HELP") {
            print_help("");

        //Process 'quit' command
        } else if (command == "QUIT" || command == "EXIT") {
            Console.WriteLine("Exiting Game.");
            final_input = calibrate_input(final_input, 1);
        }

        //Process 'show' command
        else if (command == "SHOW") {
            print_board(board);
            print_next_card(deck, next_card);
        };

    //Acknowledge alternate 'match' syntax (forgot to write "match")
    } else if (user_input.Length == 5) {
        string input_0 = user_input.Substring(0,2);
        string input_1 = user_input.Substring(3,2);
        if (globals.valid_inputs.Contains(input_0) && globals.valid_inputs.Contains(input_1)) {
            final_input = calibrate_input(final_input, 3, input_0, input_1);
        } else {
            print_help(original_input);
        };

    
    //8 Character Commands
    } else if (user_input.Length == 8) {

        //Acknowledge 'place' command
        if (user_input.Substring(0,5) == "PLACE") {
        
            string input_0 = user_input.Substring(6,2);
            if (globals.valid_inputs.Contains(input_0)) {
                final_input = calibrate_input(final_input, 2, input_0);
            } else {
                Console.WriteLine("Coordinates not valid.");
            };
        
        //Process 'tutorial' command
        } else if (user_input == "TUTORIAL") {
            print_tutorial();
        };
    //Acknowledge singular 'match' command
    } else if (user_input.Length == 8 && user_input.Substring(0,5) == "MATCH") {
        
        string input_0 = user_input.Substring(6,2);
        if (globals.valid_inputs.Contains(input_0)) {
            final_input = calibrate_input(final_input, 3, input_0, input_0);
        } else {
            Console.WriteLine("Coordinates not valid.");
        };
    //Acknowledge 'match' command
    } else if (user_input.Length == 11 && user_input.Substring(0,5) == "MATCH") {
        string input_0 = user_input.Substring(6,2);
        string input_1 = user_input.Substring(9,2);
        if (globals.valid_inputs.Contains(input_0) && globals.valid_inputs.Contains(input_1)) {
            final_input = calibrate_input(final_input, 3, input_0, input_1);
        } else {
            Console.WriteLine("Coordinates not valid.");
        };
    
    //Any other Input
    } else {
        print_help(original_input);
    };

    return final_input;
}

//Keep asking for Input until you get a valid input
static Input get_valid_input(string[] deck, string[,] board, int next_card, Globals globals) {
    var final_input = get_input(deck, board, next_card, globals);
    while (final_input.code == 0) {
        final_input = get_input(deck, board, next_card, globals);
    };
    return final_input;
}

//A helper function which restricts the Kings, Queens and Jacks to their legal squares
static int card_can_go_there(Coords target, string card) {
    /* Return Code Key
     * 0: Valid Input
     * 1: Invalid Jack Placement
     * 2: Invalid Queen Placement
     * 3: Invalid King Placement
    //*/
    switch (card[0]) {
        case 'J':
            if ((target.x == 1 || target.x == 2) && (target.y == 0 || target.y == 3)) {
                return 0;
            };
            return 1;
        case 'Q':
            if ((target.x == 0 || target.x == 3) && (target.y == 1 || target.y == 2)) {
                return 0;
            };
            return 2;
        case 'K':
            if ((target.x == 0 || target.x == 3) && (target.y == 0 || target.y == 3)) {
                return 0;
            };
            return 3;
        default:
            return 0;
    };
}

//A helper function to see if two cards can match with each other
static bool matchable(char card_0, char card_1) {
    switch (card_0) {
        case 'A':
            if (card_1 == '9') { return true; }
            else { return false; };
        case 'J':
        case 'Q':
        case 'K':
            return false;
        default:
            switch (card_1) {
                case 'A':
                    if (card_0 == '9') { return true; }
                    else { return false; };
                case 'J':
                case 'Q':
                case 'K':
                    return false;
                default:
                    if ((card_0 - '0') + (card_1 - '0') == 10) { 
                        return true; 
                    }
                    else { return false; };
            };
    };
}

//Run one turn of the game
static int run_turn(string[] deck, string[,] board, int next_card, Globals globals) {
    //Print the game state
    print_board(board);
    print_next_card(deck, next_card);

    //Initialize Return Value
    var user_input = new Input();

    //Keep a boolean to track if the turn is complete
    bool end_turn = false;
    while (!end_turn) {
        
        //Get a valid user input, and interpret it
        user_input = get_valid_input(deck, board, next_card, globals);

        //Perform Game Logic
        if (user_input.code == 1){
            end_turn = true;

        //Logic if we're placing a card
        } else if (user_input.code == 2) {
            if (board[user_input.target_0.y,user_input.target_0.x] == "__") {
                int legal_move = card_can_go_there(user_input.target_0, deck[next_card]);
                switch (legal_move) {
                    //Jack
                    case 1:
                        Console.WriteLine("Jacks can only be placed in the top and bottom squares (not including corners).");
                        break;
                    //Queen
                    case 2:
                        Console.WriteLine("Queens can only be placed in the rightmost and leftmost squares (not including corners).");
                        break;
                    //King
                    case 3:
                        Console.WriteLine("Kings can only be placed in the corners.");
                        break;
                    //Nothing Wrong
                    default:
                        Console.WriteLine($"Placed {deck[next_card]} at {globals.to_letter[user_input.target_0.x]}{user_input.target_0.y}.");
                        board[user_input.target_0.y,user_input.target_0.x] = deck[next_card];
                        end_turn = true;
                        break;
                };
            } else {
                Console.WriteLine($"That space is already occupied by {board[user_input.target_0.y,user_input.target_0.x]}.");
            };

        //Logic if we're matching cards
        } else if (user_input.code == 3) {
            if (board[user_input.target_0.y,user_input.target_0.x] == board[user_input.target_1.y,user_input.target_1.x]) {
                if (board[user_input.target_0.y,user_input.target_0.x][0] == '0') {
                    Console.WriteLine($"Matched {board[user_input.target_0.y,user_input.target_0.x]} with itself.");
                    board[user_input.target_0.y,user_input.target_0.x] = "__";
                    end_turn = true;
                } else {
                    Console.WriteLine($"Cannot match {board[user_input.target_0.y,user_input.target_0.x]} with itself.");
                };
            } else if (matchable(board[user_input.target_0.y,user_input.target_0.x][0], board[user_input.target_1.y,user_input.target_1.x][0])){
                Console.WriteLine($"Matched {board[user_input.target_0.y,user_input.target_0.x]} with {board[user_input.target_1.y,user_input.target_1.x]}.");
                board[user_input.target_0.y,user_input.target_0.x] = "__";
                board[user_input.target_1.y,user_input.target_1.x] = "__";
                end_turn = true;
            } else {
                Console.WriteLine($"Cannot Match {board[user_input.target_0.y,user_input.target_0.x]} with {board[user_input.target_1.y,user_input.target_1.x]}.");
            };
        };
    };
    
    return user_input.code;
}

//Run a game
static void new_game(string[] deck, string[,] board, Globals globals){
    //Setup the variables
    shuffle(deck);
    int next_card = 0;
    int game_state = 0;

    //Print a Welcome
    Console.WriteLine("\nWelcome to King's Corner!\nProgrammed by Cordell King.\nFor a tutorial, type 'tutorial'. For controls, type 'help'. To quit, type 'quit'.");

    //Main Game Loop
    game_state = run_turn(deck, board, next_card, globals);
    while (game_state != 1) {
        
        //Move to the next card in the deck (if applicable)
        if (game_state == 2) { 
            next_card++;

            //Check for Victory
            if (next_card >= deck.Length) {
                print_board(board);
                Console.WriteLine("YOU WIN!");
                break;
            };
        };
        game_state = run_turn(deck, board, next_card, globals);
    };

    //On Quit
    Console.WriteLine("Thanks for Playing!");
}

//*/
public static void Main() {

    /* An alternate version of the deck which uses unicode characters
    string[] deck = {
    "A♢","2♢","3♢","4♢","5♢","6♢","7♢","8♢","9♢","⑽♢","J♢","Q♢","K♢",
    "A♡","2♡","3♡","4♡","5♡","6♡","7♡","8♡","9♡","⑽♡","J♡","Q♡","K♡",
    "A♧","2♧","3♧","4♧","5♧","6♧","7♧","8♧","9♧","⑽♧","J♧","Q♧","K♧",
    "A♤","2♤","3♤","4♤","5♤","6♤","7♤","8♤","9♤","⑽♤","J♤","Q♤","K♤"};
    //*/

    //* Define the Deck
    string[] deck = new string[] {
        "AD","2D","3D","4D","5D","6D","7D","8D","9D","0D","JD","QD","KD",
        "AH","2H","3H","4H","5H","6H","7H","8H","9H","0H","JH","QH","KH",
        "AC","2C","3C","4C","5C","6C","7C","8C","9C","0C","JC","QC","KC",
        "AS","2S","3S","4S","5S","6S","7S","8S","9S","0S","JS","QS","KS"};
    //*/

    /* A Full Board for Testing
    string[,] board = new string[4,4] {
        {"AD","2D","3D","4D"},
        {"5D","0D","5C","6C"},
        {"7H","8H","9S","QS"},
        {"KD","JH","JC","KC"}};
    //*/

    //* Define the Board
    string[,] board = new string[4,4] {
        {"__","__","__","__"},
        {"__","__","__","__"},
        {"__","__","__","__"},
        {"__","__","__","__"}};
    //*/

    //Create a New Game
    new_game(deck, board, new Globals());
}
}