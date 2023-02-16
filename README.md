# Kings-Corner
A .NET7.0 application to play King's Corner on the command line.

**Requirements**
-.NET 7.0
It was written in Windows 10

**Tutorial**
    The game is played with an ordinary deck of playing cards. 
    Each turn, you draw a single card and must place it somewhere on a 4x4 grid.
    
    Each card is represented with a 2-character code ('KS' for King of Spades, '0H' for 10 of Hearts, '3C' for 3 of Clubs, etc.)
    The board is represented by an alphanumeric grid. During the game, it should look something like this:
    ---a--b--c--d----
    1  3D 9H JC AH  -
    2  QS 5D __ 3H  -
    3  QH __ __ __  -
    4  KD JS __ __  -
    -----------------
    
    But you can't just put any card anywhere.
    Kings can only be placed in corners.
    Queens can only be placed in the leftmost and rightmost squares (But not corners).
    Jacks can only be placed in the top or bottom squares (again, not counting corners).
    ---a--b--c--d----
    1  K- J- J- K-  -
    2  Q- __ __ Q-  -
    3  Q- __ __ Q-  -
    4  K- J- J- K-  -
    -----------------
    Aces and Numbered cards can be placed anywhere.

    If two cards add up to 10, you can match them together and remove them from the board.
    (ex: a 3 and a 7, an ace and a nine, or two fives can be matched together. A ten can be matched with itself.)
    (You can ignore suits.)

    If you run out of cards in the deck, you win!
    If you can't find a legal move, you lose. Type 'quit' to exit the game.
    (Don't feel bad. Not all games are winnable.)
    (Type 'help' to see the controls.)

**Controls**
      > place <square> (Place a card on the board. Ex: 'place a1')
      > match <square_1> <square_2> (Match two cards. Ex: 'match a3 b2')
      > help (Show this help page)
      > show (Show the board)
      > quit (Exit the game)
    If there are no legal moves, you lose. Type 'quit' to exit the game.
