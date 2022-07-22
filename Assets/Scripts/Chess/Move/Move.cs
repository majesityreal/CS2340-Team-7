using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public static ulong bitboard; // 64 bit number which contains all occupied squares

    public static ulong wb = 0UL; // White Bishops
    public static ulong wk = 0UL; // White King
    public static ulong wn = 0UL; // White Knights
    public static ulong wp = 0UL; // White Pawns
    public static ulong wq = 0UL; // White Queen
    public static ulong wr = 0UL; // White Rooks

    public static ulong bb = 0UL; // Black Bishops
    public static ulong bk = 0UL; // Black King
    public static ulong bn = 0UL; // Black Knights
    public static ulong bp = 0UL; // Black Pawns
    public static ulong bq = 0UL; // Black Queen
    public static ulong br = 0UL; // Black Rooks

    public static ulong whites = 0UL; // Occupied Squares by White Pieces
    public static ulong blacks = 0UL; // Occupied Squares by Black Pieces

    // Piece Counters used for Inefficient Materials (Draw)    
    public static int wbCount = 0;
    public static int wnCount = 0;
    public static int bbCount = 0;
    public static int bnCount = 0;

    // Rank Masks
    public const ulong Rank_1 = 0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_11111111;
    public const ulong Rank_2 = 0b_00000000_00000000_00000000_00000000_00000000_00000000_11111111_00000000;
    public const ulong Rank_3 = 0b_00000000_00000000_00000000_00000000_00000000_11111111_00000000_00000000;
    public const ulong Rank_4 = 0b_00000000_00000000_00000000_00000000_11111111_00000000_00000000_00000000;
    public const ulong Rank_5 = 0b_00000000_00000000_00000000_11111111_00000000_00000000_00000000_00000000;
    public const ulong Rank_6 = 0b_00000000_00000000_11111111_00000000_00000000_00000000_00000000_00000000;
    public const ulong Rank_7 = 0b_00000000_11111111_00000000_00000000_00000000_00000000_00000000_00000000;
    public const ulong Rank_8 = 0b_11111111_00000000_00000000_00000000_00000000_00000000_00000000_00000000;

    // File Masks
    public const ulong File_A = 0b_10000000_10000000_10000000_10000000_10000000_10000000_10000000_10000000;
    public const ulong File_B = 0b_01000000_01000000_01000000_01000000_01000000_01000000_01000000_01000000;
    public const ulong File_C = 0b_00100000_00100000_00100000_00100000_00100000_00100000_00100000_00100000;
    public const ulong File_D = 0b_00010000_00010000_00010000_00010000_00010000_00010000_00010000_00010000;
    public const ulong File_E = 0b_00001000_00001000_00001000_00001000_00001000_00001000_00001000_00001000;
    public const ulong File_F = 0b_00000100_00000100_00000100_00000100_00000100_00000100_00000100_00000100;
    public const ulong File_G = 0b_00000010_00000010_00000010_00000010_00000010_00000010_00000010_00000010;
    public const ulong File_H = 0b_00000001_00000001_00000001_00000001_00000001_00000001_00000001_00000001;

    // Diagonal Masks
    public const ulong Main_Diag = 0b_00000001_00000010_00000100_00001000_00010000_00100000_01000000_10000000;
    public const ulong Main_Anti = 0b_10000000_01000000_00100000_00010000_00001000_00000100_00000010_00000001;

    private static readonly ulong[] Ranks = {Rank_8, Rank_7, Rank_6, Rank_5, Rank_4, Rank_3, Rank_2, Rank_1};
    private static readonly ulong[] Files = {File_A, File_B, File_C, File_D, File_E, File_F, File_G, File_H};

    // Positions Checked for Castling
    public const ulong A8 = 0b_10000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
    public const ulong D8 = 0b_00010000_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
    public const ulong E8 = 0b_00001000_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
    public const ulong F8 = 0b_00000100_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
    public const ulong H8 = 0b_00000001_00000000_00000000_00000000_00000000_00000000_00000000_00000000;

    public const ulong A1 = 0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_10000000;
    public const ulong D1 = 0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_00010000;
    public const ulong E1 = 0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_00001000;
    public const ulong F1 = 0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000100;
    public const ulong H1 = 0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000001;

    // De Bruijn Table for converting position to x, y coordinates
    private static readonly int[] DeBruijnTable64 = {
        0, 58, 1, 59, 47, 53, 2, 60, 
        39, 48, 27, 54, 33, 42, 3, 61,
        51, 37, 40, 49, 18, 28, 20, 55, 
        30, 34, 11, 43, 14, 22, 4, 62,
        57, 46, 52, 38, 26, 32, 41, 50, 
        36, 17, 19, 29, 10, 13, 21, 56,
        45, 25, 31, 35, 16, 9, 12, 44, 
        24, 15, 8, 23, 7, 6, 5, 63
    };

    public static void ConvertBoardToBinary(char[,] board) // Sets bitboard and wX & bX bitboards to be set for legal move calculations
    {
        br = 0UL;
        bn = 0UL;
        bb = 0UL;
        bk = 0UL;
        bp = 0UL;
        wr = 0UL;
        wn = 0UL;
        wb = 0UL;
        wk = 0UL;
        wp = 0UL;

        bbCount = 0;
        bnCount = 0;

        wbCount = 0;
        wnCount = 0;

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++) {
                switch (board[7 - x, 7 - y]) 
                {
                    case 'r':
                        br |= 1UL << (y * 8 + x);
                        break;
                    case 'n':
                        bn |= 1UL << (y * 8 + x);
                        bnCount++;
                        break;
                    case 'b':
                        bb |= 1UL << (y * 8 + x);
                        bbCount++;
                        break;
                    case 'q':
                        bq |= 1UL << (y * 8 + x);
                        break;
                    case 'k':
                        bk |= 1UL << (y * 8 + x);
                        break;
                    case 'p':
                        bp |= 1UL << (y * 8 + x);
                        break;
                    case 'R':
                        wr |= 1UL << (y * 8 + x);
                        break;
                    case 'N':
                        wn |= 1UL << (y * 8 + x);
                        wnCount++;
                        break;
                    case 'B':
                        wb |= 1UL << (y * 8 + x);
                        wbCount++;
                        break;
                    case 'Q':
                        wq |= 1UL << (y * 8 + x);
                        break;
                    case 'K':
                        wk |= 1UL << (y * 8 + x);
                        break;
                    case 'P':
                        wp |= 1UL << (y * 8 + x);
                        break;
                }
            }
        }
        
        bitboard = br | bn | bb | bq | bk | bp | wr | wn | wb | wq | wk | wp;
        whites = wb | wk | wn | wp | wq | wr;
        blacks = br | bn | bb | bq | bk | bp;
    }

    public static ulong GetMoves(int x, int y, char type, List<string> record) // Returns ulong of legal move positions
    {
        ulong position = 1UL << (- 8 * y - x + 63);
        Debug.Log("Position of " + type + " is " + x + " " + y + " " + position);
        switch (type)
        {
            case 'r': case 'R':
                return PreventCheck(position, type, x, y, GetRookMoves(position, type, x, y));
            case 'n': case 'N':
                return PreventCheck(position, type, x, y, GetKnightMoves(position, type));
            case 'b': case 'B':
                return PreventCheck(position, type, x, y, GetBishopMoves(position, type, x, y));
            case 'q': case 'Q':
                return PreventCheck(position, type, x, y, GetQueenMoves(position, type, x, y));
            case 'k': case 'K':
                return KingMoveCheck(GetKingMoves(position, type, record), type);
            case 'p': case 'P':
                return PreventCheck(position, type, x, y, GetPawnMoves(position, type, record));
        }

        Debug.LogWarning("Wrong piece type has been given to GetMoves().");
        return 0UL;
    }

    public static List<int> GetMovesList(int x, int y, char type, List<string> record) // Returns list of legal move positions
    {
        List<int> moveList = new List<int>();
        ulong moves = GetMoves(x, y, type, record);
        Debug.Log("This is GetMovesList x, y, type: " + x + y + type);
        Debug.Log("There's possible moves: " + (moves != 0UL));

        int threshold = 64;
        while (moves != 0UL && threshold > 0)
        {
            (int moveX, int moveY) = GetCoords(moves);
            Debug.Log("X: " + moveX + " Y: " + moveY);
            moves &= ~(1UL << (-8 * moveY - moveX + 63));
            moveList.Add(moveY * 8 + moveX);
            threshold--;
        }

        return moveList;
    }

    private static ulong GetBishopMoves(ulong position, char type, int x, int y) // Returns bitboard of possible new positions
    {
        ulong diagonalMask = GetDiagonalMask(x, y);
        ulong antiDiagonalMask = GetAntiDiagonalMask(x, y);
        ulong allies = type < 'a' ? whites : blacks;

        // Hyperbola Quintessence (o^(o-2r))
        // Diagonal Attacks
        ulong possibleMoves = HyperbolaQuintessence(position, diagonalMask, allies);
        // Anti-Diagonal Attacks
        possibleMoves |= HyperbolaQuintessence(position, antiDiagonalMask, allies);

        return possibleMoves;
    }

    private static ulong HyperbolaQuintessence(ulong position, ulong mask, ulong allies)
    {
        return (((bitboard & mask) - position - position) 
        ^ Reverse(Reverse(bitboard & mask) - Reverse(position) - Reverse(position))) & mask & ~allies;
    }

    private static ulong GetRookMoves(ulong position, char type, int x, int y) // Returns bitboard of possible new positions
    {
        ulong rankMask = Ranks[y];
        ulong fileMask = Files[x];
        ulong allies = type < 'a' ? whites : blacks;

        // Hyperbola Quintessence (o^(o-2r))
        // Horizontal Attacks
        ulong possibleMoves = HyperbolaQuintessence(position, rankMask, allies);
        // Vertical Attacks
        possibleMoves |= HyperbolaQuintessence(position, fileMask, allies);

        return possibleMoves;
    }

    private static ulong GetKnightMoves(ulong position, char type) // Returns bitboard of possible new positions
    {
        ulong allies = type < 'a' ? whites : blacks;

        // Up Up Left
        ulong possibleMoves = (position << 17) & ~File_H & ~allies;

        // Up Up Right
        possibleMoves |= (position << 15) & ~File_A & ~allies;

        // Up Left Left
        possibleMoves |= (position << 10) & ~File_G & ~File_H & ~allies;

        // Up Right Right
        possibleMoves |= (position << 6) & ~File_A & ~File_B & ~allies;

        // Down Left left
        possibleMoves |= (position >> 6) & ~File_G & ~File_H & ~allies;

        // Down Right Right
        possibleMoves |= (position >> 10) & ~File_A & ~File_B & ~allies;

        // Down Down Left
        possibleMoves |= (position >> 15) & ~File_H & ~allies;

        // Down Down Right 
        possibleMoves |= (position >> 17) & ~File_A & ~allies;

        return possibleMoves;
    }
    
    private static ulong GetQueenMoves(ulong position, char type, int x, int y) // Returns bitboard of possible new positions
    {
        if (type == 'q')
        {
            return GetRookMoves(position, 'r', x, y) | GetBishopMoves(position, 'b', x, y);
        }
        return GetRookMoves(position, 'R', x, y) | GetBishopMoves(position, 'B', x, y);
    }

    private static List<ulong> GetKingMoves(ulong position, char type, List<string> record) // Returns bitboard of possible new positions
    {
        List<ulong> possibleMoves = new List<ulong>();
        ulong allies = type < 'a' ? whites : blacks;

        if (type == 'k')
        {  
            // Castling
            if (bk == E8)
            {
                if ((br & A8) != 0UL)
                {   
                    bool canLeft = true;
                    bool canRight = true;

                    foreach (string rec in record)
                    {
                        if (!(canLeft || canRight))
                        {
                            break;
                        }

                        if (rec == null)
                        {
                            Debug.LogWarning("Record has a null string.");
                            canLeft = false;
                            canRight = false;
                            break;
                        }

                        if (rec[0] == 'k')
                        {
                            canLeft = false;
                            canRight = false;
                            break;
                        }

                        if (rec[0] == 'r' && rec[1] == '0' && rec[2] == '0')
                        {
                            canLeft = false;
                            continue;
                        }

                        if (rec[0] == 'r' && rec[1] == '7' && rec[2] == '0')
                        {
                            canRight = false;
                            continue;
                        }
                    }

                    if (canLeft)
                    {
                        possibleMoves.Add((position << 2) & ~bitboard);
                    }

                    if (canRight)
                    {
                        possibleMoves.Add((position >> 2) & ~bitboard);
                    }
                }
            }
        }
        else
        {
            // Castling
            if (wk == E1)
            {
                if ((wr & A1) != 0UL)
                {   
                    bool canLeft = true;
                    bool canRight = true;

                    foreach (string rec in record)
                    {
                        if (!(canLeft || canRight))
                        {
                            break;
                        }

                        if (rec == null)
                        {
                            Debug.LogWarning("Record has a null string.");
                            canLeft = false;
                            canRight = false;
                            break;
                        }

                        if (rec[0] == 'k')
                        {
                            canLeft = false;
                            canRight = false;
                            break;
                        }

                        if (rec[0] == 'r' && rec[1] == '0' && rec[2] == '7')
                        {
                            canLeft = false;
                            continue;
                        }

                        if (rec[0] == 'r' && rec[1] == '7' && rec[2] == '7')
                        {
                            canRight = false;
                            continue;
                        }
                    }

                    if (canLeft)
                    {
                        possibleMoves.Add((position << 2) & ~bitboard);
                    }

                    if (canRight)
                    {
                        possibleMoves.Add((position >> 2) & ~bitboard);
                    }
                }
            }
        }

        // Up Left
        possibleMoves.Add((position << 9) & ~File_H & ~allies);

        // Up
        possibleMoves.Add((position << 8) & ~allies);

        // Up Right
        possibleMoves.Add((position << 7) & ~File_A & ~allies);

        // Left
        possibleMoves.Add((position << 1) & ~File_H & ~allies);

        // Right
        possibleMoves.Add((position >> 1) & ~File_A & ~allies);

        // Down Left
        possibleMoves.Add((position >> 7) & ~File_H & ~allies);

        // Down
        possibleMoves.Add((position >> 8) & ~allies);

        // Down Right
        possibleMoves.Add((position >> 9) & ~File_A & ~allies);

        return possibleMoves;
    }

    private static ulong GetPawnMoves(ulong position, char type, List<string> record) // Returns bitboard of possible new positions
    {
        ulong possibleMoves = 0UL;

        // En Passant
        ulong movedX = 0UL;
        if (record.Count > 3)
        {
            if (record[record.Count - 1].Length == 4)
            {
                if (record[record.Count - 1][1] - record[record.Count - 1][3] == 2)
                {
                    switch (record[record.Count - 1][0])
                    {
                        case '0':
                            movedX = File_A;
                            break;
                        case '1':
                            movedX = File_B;
                            break;
                        case '2':
                            movedX = File_C;
                            break;
                        case '3':
                            movedX = File_D;
                            break;
                        case '4':
                            movedX = File_E;
                            break;
                        case '5':
                            movedX = File_F;
                            break;
                        case '6':
                            movedX = File_G;
                            break;
                        case '7':
                            movedX = File_H;
                            break;
                    }
                }
            }
        }

        if (type == 'p')
        {
            // Single Move
            possibleMoves |= (position >> 8) & ~Rank_1 & ~bitboard;

            // Double Move
            possibleMoves |= ((position & Rank_7) >> 16) & ~bitboard & ~(bitboard >> 8);

            // Left Capture
            possibleMoves |= (position >> 7) & ~File_H & whites;

            // Right Capture
            possibleMoves |= (position >> 9) & ~File_A & whites;

            // Left En Passant
            possibleMoves |= (position >> 7) & Rank_4 & movedX;

            // Right En Passant
            possibleMoves |= (position >> 9) & Rank_4 & movedX; 
        }
        else
        {
            // Single Move
            possibleMoves |= (position << 8) & ~Rank_8 & ~bitboard;

            // Double Move
            possibleMoves |= ((position & Rank_2) << 16) & ~bitboard & ~(bitboard << 8);

            // Left Capture
            possibleMoves |= (position << 9) & ~File_H & blacks;

            // Right Capture
            possibleMoves |= (position << 7) & ~File_A & blacks;

            // Left En Passant
            possibleMoves |= (position << 9) & Rank_5 & movedX;

            // Right En Passant
            possibleMoves |= (position << 7) & Rank_5 & movedX;      
        }

        return possibleMoves;
    }

    private static ulong PreventCheck(ulong position, char type, int x, int y, ulong possibleMoves) // Returns bitboard of legal new positions
    {
        // ulong directCheck = 0b_11111111_11111111_11111111_11111111_11111111_11111111_11111111_11111111;
        // bitboard &= ~position;
        // int kingX = 0;
        // int kingY = 0;

        // if (type < 'a') // White
        // {
        //     ulong allies = whites & ~position;

        //     // Pawn Checks
        //     if (((wk << 7) & bp) != 0UL)
        //     {
        //         directCheck = (wk << 7);
        //     }

        //     if (((wk << 9) & bp) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = (wk << 9);
        //     }

        //     // Knight Check
        //     ulong knights = GetKnightMoves(wk, 'K');
        //     if ((knights & bn) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = knights;
        //     }


        //     // Vertical Checks
        //     (kingX, kingY) = GetCoords(wk);
        //     ulong fileMask = Files[x];
        //     ulong ups = ((bitboard & fileMask) ^ ((bitboard & fileMask) - wk - wk)) & fileMask & ~allies;
        //     if ((ups & (br | bq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = ups;
        //     }

        //     ulong downs = ((bitboard & fileMask) ^ Reverse(Reverse(bitboard & fileMask) - Reverse(wk) - Reverse(wk))) & fileMask & ~allies;
        //     if ((downs & (br | bq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = ups;
        //     }
            

        //     // Horizontal Checks
        //     ulong rankMask = Ranks[y];
        //     ulong lefts = ((bitboard & rankMask) ^ ((bitboard & rankMask) - wk - wk)) & rankMask & ~allies;
        //     if ((lefts & (br | bq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = lefts;
        //     }

        //     ulong rights = ((bitboard & rankMask) ^ Reverse(Reverse(bitboard & rankMask) - Reverse(wk) - Reverse(wk))) & rankMask & ~allies;
        //     if ((rights & (br | bq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = rights;
        //     }
            
            
        //     // Diagonal Checks
        //     ulong diagonalMask = GetDiagonalMask(x, y);
        //     ulong upDiag = ((bitboard & diagonalMask) ^ ((bitboard & diagonalMask) - wk - wk)) & diagonalMask & ~allies;
        //     if ((upDiag & (bb | bq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = upDiag;
        //     }
            
        //     ulong downDiag = ((bitboard & diagonalMask) ^ Reverse(Reverse(bitboard & diagonalMask) - Reverse(wk) - Reverse(wk))) & diagonalMask & ~allies;
        //     if ((downDiag & (bb | bq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = downDiag;
        //     }
            

        //     // Anti-Diagonal Checks
        //     ulong antiDiagonalMask = GetAntiDiagonalMask(x, y);
        //     ulong upAnti = ((bitboard & antiDiagonalMask) ^ ((bitboard & antiDiagonalMask) - wk - wk)) & antiDiagonalMask & ~allies;
        //     if ((upAnti & (bb | bq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = upAnti;
        //     }

        //     ulong downAnti = ((bitboard & antiDiagonalMask) ^ Reverse(Reverse(bitboard & antiDiagonalMask) - Reverse(wk) - Reverse(wk))) & antiDiagonalMask & ~allies;
        //     if ((downAnti & (bb | bq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }
        //         directCheck = downAnti;
        //     }
        // }
        // else
        // {
        //     ulong allies = blacks & ~position;

        //     // Pawn Checks
        //     if (((bk >> 7) & wp) != 0UL)
        //     {
        //         directCheck = (bk >> 7);
        //     }

        //     if (((bk >> 9) & wp) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = (bk >> 9);
        //     }

        //     // Knight Check
        //     ulong knights = GetKnightMoves(bk, 'k');
        //     if ((knights & wn) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = knights;
        //     }


        //     // Vertical Checks
        //     (kingX, kingY) = GetCoords(bk);
        //     ulong fileMask = Files[x];
        //     ulong ups = ((bitboard & fileMask) ^ ((bitboard & fileMask) - bk - bk)) & fileMask & ~allies;
        //     if ((ups & (wr | wq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = ups;
        //     }

        //     ulong downs = ((bitboard & fileMask) ^ Reverse(Reverse(bitboard & fileMask) - Reverse(bk) - Reverse(bk))) & fileMask & ~allies;
        //     if ((downs & (wr | wq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = ups;
        //     }
            

        //     // Horizontal Checks
        //     ulong rankMask = Ranks[y];
        //     ulong lefts = ((bitboard & rankMask) ^ ((bitboard & rankMask) - bk - bk)) & rankMask & ~allies;
        //     if ((lefts & (wr | wq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = lefts;
        //     }

        //     ulong rights = ((bitboard & rankMask) ^ Reverse(Reverse(bitboard & rankMask) - Reverse(bk) - Reverse(bk))) & rankMask & ~allies;
        //     if ((rights & (wr | wq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = rights;
        //     }
            
            
        //     // Diagonal Checks
        //     ulong diagonalMask = GetDiagonalMask(x, y);
        //     ulong upDiag = ((bitboard & diagonalMask) ^ ((bitboard & diagonalMask) - bk - bk)) & diagonalMask & ~allies;
        //     if ((upDiag & (wb | wq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = upDiag;
        //     }
            
        //     ulong downDiag = ((bitboard & diagonalMask) ^ Reverse(Reverse(bitboard & diagonalMask) - Reverse(bk) - Reverse(bk))) & diagonalMask & ~allies;
        //     if ((downDiag & (wb | wq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = downDiag;
        //     }
            

        //     // Anti-Diagonal Checks
        //     ulong antiDiagonalMask = GetAntiDiagonalMask(x, y);
        //     ulong upAnti = ((bitboard & antiDiagonalMask) ^ ((bitboard & antiDiagonalMask) - bk - bk)) & antiDiagonalMask & ~allies;
        //     if ((upAnti & (wb | wq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }

        //         directCheck = upAnti;
        //     }

        //     ulong downAnti = ((bitboard & antiDiagonalMask) ^ Reverse(Reverse(bitboard & antiDiagonalMask) - Reverse(bk) - Reverse(bk))) & antiDiagonalMask & ~allies;
        //     if ((downAnti & (wb | wq)) != 0UL)
        //     {
        //         if (directCheck != ulong.MaxValue)
        //         {
        //             bitboard |= position;
        //             return 0UL;
        //         }
        //         directCheck = downAnti;
        //     }
        // }
        
        // bitboard |= position;
        // return directCheck & possibleMoves;
        return possibleMoves;
    }

    public static ulong KingMoveCheck(List<ulong> possibleMoves, char type) // Returns bitboard of legal new positions
    {
        int kingX = 0;
        int kingY = 0;
        ulong origin = bitboard;

        // if (type == 'K')
        // {
        //     bitboard &= ~wk;

        //     for (int i = possibleMoves.Count - 1; i >= 0; i--)
        //     {
        //         ulong allies = whites & ~wk | possibleMoves[i];
        //         // Pawn Checks
        //         if (((possibleMoves[i] << 7) & bp) != 0UL)
        //         {
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         if (((possibleMoves[i] << 9) & bp) != 0UL)
        //         {
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         // Knight Check
        //         ulong knights = GetKnightMoves(possibleMoves[i], 'N');
        //         if ((knights & bn) != 0UL)
        //         {
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         // Vertical Checks
        //         (kingX, kingY) = GetCoords(possibleMoves[i]);
        //         bitboard |= possibleMoves[i];

        //         ulong fileMask = Files[kingX];
        //         ulong ups = ((bitboard & fileMask) ^ ((bitboard & fileMask) - possibleMoves[i] - possibleMoves[i])) & fileMask & ~allies;
        //         if ((ups & (br | bq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         ulong downs = ((bitboard & fileMask) ^ Reverse(Reverse(bitboard & fileMask) - Reverse(possibleMoves[i]) - Reverse(possibleMoves[i]))) & fileMask & ~allies;
        //         if ((downs & (br | bq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         // Horizontal Checks
        //         ulong rankMask = Ranks[kingY];
        //         ulong lefts = ((bitboard & rankMask) ^ ((bitboard & rankMask) - possibleMoves[i] - possibleMoves[i])) & rankMask & ~allies;
        //         if ((lefts & (br | bq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         ulong rights = ((bitboard & rankMask) ^ Reverse(Reverse(bitboard & rankMask) - Reverse(possibleMoves[i]) - Reverse(possibleMoves[i]))) & rankMask & ~allies;
        //         if ((rights & (br | bq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         // Diagonal Checks
        //         ulong diagonalMask = GetDiagonalMask(kingX, kingY);
        //         ulong upDiag = ((bitboard & diagonalMask) ^ ((bitboard & diagonalMask) - possibleMoves[i] - possibleMoves[i])) & diagonalMask & ~allies;
        //         if ((upDiag & (bb | bq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }
                
        //         ulong downDiag = ((bitboard & diagonalMask) ^ Reverse(Reverse(bitboard & diagonalMask) - Reverse(possibleMoves[i]) - Reverse(possibleMoves[i]))) & diagonalMask & ~allies;
        //         if ((downDiag & (bb | bq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         // Anti-Diagonal Checks
        //         ulong antiDiagonalMask = GetAntiDiagonalMask(kingX, kingY);
        //         ulong upAnti = ((bitboard & antiDiagonalMask) ^ ((bitboard & antiDiagonalMask) - possibleMoves[i] - possibleMoves[i])) & antiDiagonalMask & ~allies;
        //         if ((upAnti & (bb | bq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         ulong downAnti = ((bitboard & antiDiagonalMask) ^ Reverse(Reverse(bitboard & antiDiagonalMask) - Reverse(possibleMoves[i]) - Reverse(possibleMoves[i]))) & antiDiagonalMask & ~allies;
        //         if ((downAnti & (bb | bq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         bitboard &= ~possibleMoves[i];
        //     }

        //     bitboard &= wk;
        // }
        // else
        // {
        //     bitboard &= ~bk;

        //     for (int i = possibleMoves.Count - 1; i >= 0; i--)
        //     {
        //         ulong allies = blacks & ~bk | possibleMoves[i];

        //         // Pawn Checks
        //         if (((possibleMoves[i] >> 7) & wp) != 0UL)
        //         {
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         if (((possibleMoves[i] >> 9) & wp) != 0UL)
        //         {
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         // Knight Check
        //         ulong knights = GetKnightMoves(possibleMoves[i], 'n');
        //         if ((knights & wn) != 0UL)
        //         {
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         // Vertical Checks
        //         (kingX, kingY) = GetCoords(possibleMoves[i]);
        //         bitboard |= possibleMoves[i];

        //         ulong fileMask = Files[kingX];
        //         ulong ups = ((bitboard & fileMask) ^ ((bitboard & fileMask) - possibleMoves[i] - possibleMoves[i])) & fileMask & ~allies;

        //         if ((ups & (wr | wq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         ulong downs = ((bitboard & fileMask) ^ Reverse(Reverse(bitboard & fileMask) - Reverse(possibleMoves[i]) - Reverse(possibleMoves[i]))) & fileMask & ~allies;
        //         if ((downs & (wr | wq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         // Horizontal Checks
        //         ulong rankMask = Ranks[kingY];
        //         ulong lefts = ((bitboard & rankMask) ^ ((bitboard & rankMask) - possibleMoves[i] - possibleMoves[i])) & rankMask & ~allies;
        //         if ((lefts & (wr | wq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         ulong rights = ((bitboard & rankMask) ^ Reverse(Reverse(bitboard & rankMask) - Reverse(possibleMoves[i]) - Reverse(possibleMoves[i]))) & rankMask & ~allies;
        //         if ((rights & (wr | wq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         // Diagonal Checks
        //         ulong diagonalMask = GetDiagonalMask(kingX, kingY);
        //         ulong upDiag = ((bitboard & diagonalMask) ^ ((bitboard & diagonalMask) - possibleMoves[i] - possibleMoves[i])) & diagonalMask & ~allies;
        //         if ((upDiag & (wb | wq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }
                
        //         ulong downDiag = ((bitboard & diagonalMask) ^ Reverse(Reverse(bitboard & diagonalMask) - Reverse(possibleMoves[i]) - Reverse(possibleMoves[i]))) & diagonalMask & ~allies;
        //         if ((downDiag & (wb | wq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         // Anti-Diagonal Checks
        //         ulong antiDiagonalMask = GetAntiDiagonalMask(kingX, kingY);
        //         ulong upAnti = ((bitboard & antiDiagonalMask) ^ ((bitboard & antiDiagonalMask) - possibleMoves[i] - possibleMoves[i])) & antiDiagonalMask & ~allies;
        //         if ((upAnti & (wb | wq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         ulong downAnti = ((bitboard & antiDiagonalMask) ^ Reverse(Reverse(bitboard & antiDiagonalMask) - Reverse(possibleMoves[i]) - Reverse(possibleMoves[i]))) & antiDiagonalMask & ~allies;
        //         if ((downAnti & (wb | wq)) != 0UL)
        //         {
        //             bitboard &= ~possibleMoves[i];
        //             possibleMoves.RemoveAt(i);
        //             continue;
        //         }

        //         bitboard &= ~possibleMoves[i];
        //     }

        //     bitboard &= bk;
        // }

        ulong kingMoves = 0UL;
        foreach (ulong move in possibleMoves)
        {
            kingMoves |= move;
        }
        bitboard = origin;
        return kingMoves;
    } 

    private static ulong GetDiagonalMask(int x, int y) // Returns diagonal mask based on piece's position
    {
        if (x < 0 || x > 7 || y < 0 || y > 7)
        {
            return 0UL;
        }

        ulong diagonalMask = Main_Diag;
        if (x + y < 7)
        {
            diagonalMask <<= ((7 - x - y) * 8);
        }
        else
        {
            diagonalMask >>= ((x + y - 7) * 8);
        }

        return diagonalMask;
    }

    private static ulong GetAntiDiagonalMask(int x, int y) // Returns anti-diagonal mask based on piece's position
    {
        if (x < 0 || x > 7 || y < 0 || y > 7)
        {
            return 0UL;
        }

        ulong antiDiagonalMask = Main_Anti;
        if (x - y > 0)
        {
            antiDiagonalMask <<= ((x - y) * 8);
        }
        else
        {
            antiDiagonalMask >>= ((y - x) * 8);
        }

        return antiDiagonalMask;
    }

    private static ulong Reverse(ulong binary) // Reverse bits by swapping positions. Total 30 operations.
    {
        binary = (binary & 0b_11111111_11111111_11111111_11111111_00000000_00000000_00000000_00000000) >> 32 
                |(binary & 0b_00000000_00000000_00000000_00000000_11111111_11111111_11111111_11111111) << 32;

        binary = (binary & 0b_11111111_11111111_00000000_00000000_11111111_11111111_00000000_00000000) >> 16 
                |(binary & 0b_00000000_00000000_11111111_11111111_00000000_00000000_11111111_11111111) << 16;

        binary = (binary & 0b_11111111_00000000_11111111_00000000_11111111_00000000_11111111_00000000) >> 8 
                |(binary & 0b_00000000_11111111_00000000_11111111_00000000_11111111_00000000_11111111) << 8;

        binary = (binary & 0b_11110000_11110000_11110000_11110000_11110000_11110000_11110000_11110000) >> 4 
                |(binary & 0b_00001111_00001111_00001111_00001111_00001111_00001111_00001111_00001111) << 4;

        binary = (binary & 0b_11001100_11001100_11001100_11001100_11001100_11001100_11001100_11001100) >> 2 
                |(binary & 0b_00110011_00110011_00110011_00110011_00110011_00110011_00110011_00110011) << 2;

        binary = (binary & 0b_10101010_10101010_10101010_10101010_10101010_10101010_10101010_10101010) >> 1 
                |(binary & 0b_01010101_01010101_01010101_01010101_01010101_01010101_01010101_01010101) << 1;

        return binary;
    }

    /*
    De Bruijn Bit Position Algorithm
    Authors: Eric Cole, Mark Dickinson
    An algorithm that finds the log base 2 of the MSD of a given 64 bit ulong in O(log(n)) operations
    Uses the DeBrujnTable64 lookup table.
    */
    private static (int, int) GetCoords(ulong position)
    {
        position |= position >> 1;
        position |= position >> 2;
        position |= position >> 4;
        position |= position >> 8;
        position |= position >> 16;
        position |= position >> 32;

        int index = 63 - DeBruijnTable64[(position * 0b_00000011_11110110_11101010_11110010_11001101_00100111_00010100_01100001) >> 58];

        return (index % 8 , index / 8);
    }
}