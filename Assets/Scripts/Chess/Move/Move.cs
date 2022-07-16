using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    private ulong bitboard;

    private ulong wb = 0UL;
    private ulong wk = 0UL;
    private ulong wn = 0UL;
    private ulong wp = 0UL;
    private ulong wq = 0UL;
    private ulong wr = 0UL;

    private ulong bb = 0UL;
    private ulong bk = 0UL;
    private ulong bn = 0UL;
    private ulong bp = 0UL;
    private ulong bq = 0UL;
    private ulong br = 0UL;

    private ulong whites = 0UL;
    private ulong blacks = 0UL;

    public const ulong Rank_1 = 0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_11111111;
    public const ulong Rank_2 = 0b_00000000_00000000_00000000_00000000_00000000_00000000_11111111_00000000;
    public const ulong Rank_3 = 0b_00000000_00000000_00000000_00000000_00000000_11111111_00000000_00000000;
    public const ulong Rank_4 = 0b_00000000_00000000_00000000_00000000_11111111_00000000_00000000_00000000;
    public const ulong Rank_5 = 0b_00000000_00000000_00000000_11111111_00000000_00000000_00000000_00000000;
    public const ulong Rank_6 = 0b_00000000_00000000_11111111_00000000_00000000_00000000_00000000_00000000;
    public const ulong Rank_7 = 0b_00000000_11111111_00000000_00000000_00000000_00000000_00000000_00000000;
    public const ulong Rank_8 = 0b_11111111_00000000_00000000_00000000_00000000_00000000_00000000_00000000;

    public const ulong File_A = 0b_10000000_10000000_10000000_10000000_10000000_10000000_10000000_10000000;
    public const ulong File_B = 0b_01000000_01000000_01000000_01000000_01000000_01000000_01000000_01000000;
    public const ulong File_C = 0b_00100000_00100000_00100000_00100000_00100000_00100000_00100000_00100000;
    public const ulong File_D = 0b_00010000_00010000_00010000_00010000_00010000_00010000_00010000_00010000;
    public const ulong File_E = 0b_00001000_00001000_00001000_00001000_00001000_00001000_00001000_00001000;
    public const ulong File_F = 0b_00000100_00000100_00000100_00000100_00000100_00000100_00000100_00000100;
    public const ulong File_G = 0b_00000010_00000010_00000010_00000010_00000010_00000010_00000010_00000010;
    public const ulong File_H = 0b_00000001_00000001_00000001_00000001_00000001_00000001_00000001_00000001;

    // public const long Diag_A8 = 0b10000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long Diag_B8 = 0b01000000_10000000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long Diag_C8 = 0b00100000_01000000_10000000_00000000_00000000_00000000_00000000_00000000;
    // public const long Diag_D8 = 0b00010000_00100000_01000000_10000000_00000000_00000000_00000000_00000000;
    // public const long Diag_E8 = 0b00001000_00010000_00100000_01000000_10000000_00000000_00000000_00000000;
    // public const long Diag_F8 = 0b00000100_00001000_00010000_00100000_01000000_10000000_00000000_00000000;
    // public const long Diag_G8 = 0b00000010_00000100_00001000_00010000_00100000_01000000_10000000_00000000;
    // public const long Diag_H8 = 0b00000001_00000010_00000100_00001000_00010000_00100000_01000000_10000000;
    // public const long Diag_H7 = 0b00000000_00000001_00000010_00000100_00001000_00010000_00100000_01000000;
    // public const long Diag_H6 = 0b00000000_00000000_00000001_00000010_00000100_00001000_00010000_00100000;
    // public const long Diag_H5 = 0b00000000_00000000_00000000_00000001_00000010_00000100_00001000_00010000;
    // public const long Diag_H4 = 0b00000000_00000000_00000000_00000000_00000001_00000010_00000100_00001000;
    // public const long Diag_H3 = 0b00000000_00000000_00000000_00000000_00000000_00000001_00000010_00000100;
    // public const long Diag_H2 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000001_00000010;
    // public const long Diag_H1 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000001;

    // public const long Anti_A1 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_10000000;
    // public const long Anti_B1 = 0b00000000_00000000_00000000_00000000_00000000_00000000_10000000_01000000;
    // public const long Anti_C1 = 0b00000000_00000000_00000000_00000000_00000000_10000000_01000000_00100000;
    // public const long Anti_D1 = 0b00000000_00000000_00000000_00000000_10000000_01000000_00100000_00010000;
    // public const long Anti_E1 = 0b00000000_00000000_00000000_10000000_01000000_00100000_00010000_00001000;
    // public const long Anti_F1 = 0b00000000_00000000_10000000_01000000_00100000_00010000_00001000_00000100;
    // public const long Anti_G1 = 0b00000000_10000000_01000000_00100000_00010000_00001000_00000100_00000010;
    // public const long Anti_H1 = 0b10000000_01000000_00100000_00010000_00001000_00000100_00000010_00000001;
    // public const long Anti_H2 = 0b01000000_00100000_00010000_00001000_00000100_00000010_00000001_00000000;
    // public const long Anti_H3 = 0b00100000_00010000_00001000_00000100_00000010_00000001_00000000_00000000;
    // public const long Anti_H4 = 0b00010000_00001000_00000100_00000010_00000001_00000000_00000000_00000000;
    // public const long Anti_H5 = 0b00001000_00000100_00000010_00000001_00000000_00000000_00000000_00000000;
    // public const long Anti_H6 = 0b00000100_00000010_00000001_00000000_00000000_00000000_00000000_00000000;
    // public const long Anti_H7 = 0b00000010_00000001_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long Anti_H8 = 0b00000001_00000000_00000000_00000000_00000000_00000000_00000000_00000000;

    public const ulong Main_Diag = 0b_00000001_00000010_00000100_00001000_00010000_00100000_01000000_10000000;
    public const ulong Main_Anti = 0b_10000000_01000000_00100000_00010000_00001000_00000100_00000010_00000001;

    private readonly ulong[] Ranks = {Rank_8, Rank_7, Rank_6, Rank_5, Rank_4, Rank_3, Rank_2, Rank_1};
    private readonly ulong[] Files = {File_A, File_B, File_C, File_D, File_E, File_F, File_G, File_H};
    // private readonly long[] Diags = {Diag_A8, Diag_B8, Diag_C8, Diag_D8, Diag_E8, Diag_F8, Diag_G8, Diag_H8, Diag_H7, Diag_H6, Diag_H5, Diag_H4, Diag_H3, Diag_H2, Diag_H1};
    // private readonly long[] Antis = {Anti_A1, Anti_B1, Anti_C1, Anti_D1, Anti_E1, Anti_F1, Anti_G1, Anti_H1, Anti_H2, Anti_H3, Anti_H4, Anti_H5, Anti_H6, Anti_H7, Anti_H8};

    public const ulong A8 = 0b_10000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long B8 = 0b01000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long C8 = 0b00100000_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long D8 = 0b00010000_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
    public const ulong E8 = 0b_00001000_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long F8 = 0b00000100_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long G8 = 0b00000010_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long H8 = 0b00000001_00000000_00000000_00000000_00000000_00000000_00000000_00000000;

    // public const long A7 = 0b00000000_10000000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long B7 = 0b00000000_01000000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long C7 = 0b00000000_00100000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long D7 = 0b00000000_00010000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long E7 = 0b00000000_00001000_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long F7 = 0b00000000_00000100_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long G7 = 0b00000000_00000010_00000000_00000000_00000000_00000000_00000000_00000000;
    // public const long H7 = 0b00000000_00000001_00000000_00000000_00000000_00000000_00000000_00000000;

    // public const long A6 = 0b00000000_00000000_10000000_00000000_00000000_00000000_00000000_00000000;
    // public const long B6 = 0b00000000_00000000_01000000_00000000_00000000_00000000_00000000_00000000;
    // public const long C6 = 0b00000000_00000000_00100000_00000000_00000000_00000000_00000000_00000000;
    // public const long D6 = 0b00000000_00000000_00010000_00000000_00000000_00000000_00000000_00000000;
    // public const long E6 = 0b00000000_00000000_00001000_00000000_00000000_00000000_00000000_00000000;
    // public const long F6 = 0b00000000_00000000_00000100_00000000_00000000_00000000_00000000_00000000;
    // public const long G6 = 0b00000000_00000000_00000010_00000000_00000000_00000000_00000000_00000000;
    // public const long H6 = 0b00000000_00000000_00000001_00000000_00000000_00000000_00000000_00000000;

    // public const long A5 = 0b00000000_00000000_00000000_10000000_00000000_00000000_00000000_00000000;
    // public const long B5 = 0b00000000_00000000_00000000_01000000_00000000_00000000_00000000_00000000;
    // public const long C5 = 0b00000000_00000000_00000000_00100000_00000000_00000000_00000000_00000000;
    // public const long D5 = 0b00000000_00000000_00000000_00010000_00000000_00000000_00000000_00000000;
    // public const long E5 = 0b00000000_00000000_00000000_00001000_00000000_00000000_00000000_00000000;
    // public const long F5 = 0b00000000_00000000_00000000_00000100_00000000_00000000_00000000_00000000;
    // public const long G5 = 0b00000000_00000000_00000000_00000010_00000000_00000000_00000000_00000000;
    // public const long H5 = 0b00000000_00000000_00000000_00000001_00000000_00000000_00000000_00000000;

    // public const long A4 = 0b00000000_00000000_00000000_00000000_10000000_00000000_00000000_00000000;
    // public const long B4 = 0b00000000_00000000_00000000_00000000_01000000_00000000_00000000_00000000;
    // public const long C4 = 0b00000000_00000000_00000000_00000000_00100000_00000000_00000000_00000000;
    // public const long D4 = 0b00000000_00000000_00000000_00000000_00010000_00000000_00000000_00000000;
    // public const long E4 = 0b00000000_00000000_00000000_00000000_00001000_00000000_00000000_00000000;
    // public const long F4 = 0b00000000_00000000_00000000_00000000_00000100_00000000_00000000_00000000;
    // public const long G4 = 0b00000000_00000000_00000000_00000000_00000010_00000000_00000000_00000000;
    // public const long H4 = 0b00000000_00000000_00000000_00000000_00000001_00000000_00000000_00000000;

    // public const long A3 = 0b00000000_00000000_00000000_00000000_00000000_10000000_00000000_00000000;
    // public const long B3 = 0b00000000_00000000_00000000_00000000_00000000_01000000_00000000_00000000;
    // public const long C3 = 0b00000000_00000000_00000000_00000000_00000000_00100000_00000000_00000000;
    // public const long D3 = 0b00000000_00000000_00000000_00000000_00000000_00010000_00000000_00000000;
    // public const long E3 = 0b00000000_00000000_00000000_00000000_00000000_00001000_00000000_00000000;
    // public const long F3 = 0b00000000_00000000_00000000_00000000_00000000_00000100_00000000_00000000;
    // public const long G3 = 0b00000000_00000000_00000000_00000000_00000000_00000010_00000000_00000000;
    // public const long H3 = 0b00000000_00000000_00000000_00000000_00000000_00000001_00000000_00000000;

    // public const long A2 = 0b00000000_00000000_00000000_00000000_00000000_00000000_10000000_00000000;
    // public const long B2 = 0b00000000_00000000_00000000_00000000_00000000_00000000_01000000_00000000;
    // public const long C2 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00100000_00000000;
    // public const long D2 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00010000_00000000;
    // public const long E2 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00001000_00000000;
    // public const long F2 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000100_00000000;
    // public const long G2 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000010_00000000;
    // public const long H2 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000001_00000000;

    public const ulong A1 = 0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_10000000;
    // public const long B1 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_01000000;
    // public const long C1 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_00100000;
    // public const long D1 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_00010000;
    public const ulong E1 = 0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_00001000;
    // public const long F1 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000100;
    // public const long G1 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000010;
    // public const long H1 = 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000001;


    private readonly int[] DeBruijnTable64 = {
        0, 58, 1, 59, 47, 53, 2, 60, 
        39, 48, 27, 54, 33, 42, 3, 61,
        51, 37, 40, 49, 18, 28, 20, 55, 
        30, 34, 11, 43, 14, 22, 4, 62,
        57, 46, 52, 38, 26, 32, 41, 50, 
        36, 17, 19, 29, 10, 13, 21, 56,
        45, 25, 31, 35, 16, 9, 12, 44, 
        24, 15, 8, 23, 7, 6, 5, 63
    };


    public void ConvertBoardToBinary(char[,] board) 
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
                        break;
                    case 'b':
                        bb |= 1UL << (y * 8 + x);
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
                        break;
                    case 'B':
                        wb |= 1UL << (y * 8 + x);
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

    public ulong GetMoves(int x, int y, char type, List<string> record) 
    {
        ulong position = 1UL << (y * 8 + x);
        switch (type)
        {
            case 'r': case 'R':
                return GetRookMoves(position, type, x, y);
            case 'n': case 'N':
                return GetKnightMoves(position, type);
            case 'b': case 'B':
                return GetBishopMoves(position, type, x, y);
            case 'q': case 'Q':
                return GetQueenMoves(position, type, x, y);
            case 'k': case 'K':
                return GetKingMoves(position, type, record);
            case 'p': case 'P':
                return GetPawnMoves(position, type, record);
        }

        Debug.LogWarning("Wrong piece type has been given to GetMoves().");
        return 0UL;
    }

    private ulong GetRookMoves(ulong position, char type, int x, int y)
    {
        ulong rankMask = Ranks[y];
        ulong fileMask = Files[x];
        ulong allies = GetAllies(type);

        // Hyperbola Quintessence (o^(o-2r))
        // Horizontal Attacks
        ulong possibleMoves = (((bitboard & rankMask) - position - position) ^ Reverse(Reverse(bitboard & rankMask) - Reverse(position) - Reverse(position))) & ~allies;
        // Vertical Attacks
        possibleMoves |= (((bitboard & fileMask) - position - position) ^ Reverse(Reverse(bitboard & fileMask) - Reverse(position) - Reverse(position))) & ~allies;

        return possibleMoves;
    }

    private ulong GetKnightMoves(ulong position, char type)
    {
        ulong allies = GetAllies(type);

        // Up Up Left
        ulong possibleMoves = (position << 17) & ~allies & ~File_H;

        // Up Up Right
        possibleMoves |= (position << 15) & ~allies & ~File_A;

        // Up Left Left
        possibleMoves |= (position << 10) & ~allies & ~File_G & ~File_H;

        // Up Right Right
        possibleMoves |= (position << 6) & ~allies & ~File_A & ~File_B;

        // Down Left left
        possibleMoves |= (position >> 6) & ~allies & ~File_G & ~File_H;

        // Down Right Right
        possibleMoves |= (position >> 10) & ~allies & ~File_A & ~File_B;

        // Down Down Left
        possibleMoves |= (position >> 15) & ~allies & ~File_H;

        // Down Down Right 
        possibleMoves |= (position >> 17) & ~allies & ~File_A;

        return possibleMoves;
    }

    private ulong GetBishopMoves(ulong position, char type, int x, int y)
    {
        ulong diagonalMask = GetDiagonalMask(x, y);
        ulong antiDiagonalMask = GetAntiDiagonalMask(x, y);
        ulong allies = GetAllies(type);

        // Hyperbola Quintessence (o^(o-2r))
        // Diagonal Attacks
        ulong possibleMoves = (((bitboard & diagonalMask) - position - position) ^ Reverse(Reverse(bitboard & diagonalMask) - Reverse(position) - Reverse(position))) & ~allies;
        // Anti-Diagonal Attacks
        possibleMoves |= (((bitboard & antiDiagonalMask) - position - position) ^ Reverse(Reverse(bitboard & antiDiagonalMask) - Reverse(position) - Reverse(position))) & ~allies;

        return possibleMoves;
    }
    
    private ulong GetQueenMoves(ulong position, char type, int x, int y)
    {
        if (type == 'q')
        {
            return GetRookMoves(position, 'r', x, y) | GetBishopMoves(position, 'b', x, y);
        }
        return GetRookMoves(position, 'R', x, y) | GetBishopMoves(position, 'B', x, y);
    }

    private ulong GetKingMoves(ulong position, char type, List<string> record)
    {
        ulong possibleMoves = 0UL;

        ulong allies = GetAllies(type);
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
                        possibleMoves |= (position << 2) & ~bitboard;
                    }

                    if (canRight)
                    {
                        possibleMoves |= (position >> 2) & ~bitboard;
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
                        possibleMoves |= (position << 2) & ~bitboard;
                    }

                    if (canRight)
                    {
                        possibleMoves |= (position >> 2) & ~bitboard;
                    }
                }
            }
        }

        // Up Left
        possibleMoves |= (position << 9) & ~allies & ~File_H;

        // Up
        possibleMoves |= (position << 8) & ~allies;

        // Up Right
        possibleMoves |= (position << 7) & ~allies & ~File_A;

        // Left
        possibleMoves |= (position << 1) & ~allies & ~File_H;

        // Right
        possibleMoves |= (position >> 1) & ~allies & ~File_A;

        // Down Left
        possibleMoves |= (position >> 7) & ~allies & ~File_H;

        // Down
        possibleMoves |= (position >> 8) & ~allies;

        // Down Right
        possibleMoves |= (position >> 9) & ~allies & ~File_A;

        return possibleMoves;
    }

    private ulong GetPawnMoves(ulong position, char type, List<string> record)
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
            possibleMoves |= (position >> 8) & ~bitboard & ~Rank_1;

            // Double Move
            possibleMoves |= ((position & Rank_7) >> 16) & ~bitboard & ~(bitboard >> 8);

            // Left Capture
            possibleMoves |= (position >> 7) & blacks & ~File_H;

            // Right Capture
            possibleMoves |= (position >> 9) & blacks & ~File_A;

            // Left En Passant
            possibleMoves |= (position >> 7) & Rank_4 & movedX;

            // Right En Passant
            possibleMoves |= (position >> 9) & Rank_4 & movedX; 
        }
        else
        {
            // Single Move
            possibleMoves |= (position << 8) & ~bitboard & ~Rank_8;

            // Double Move
            possibleMoves |= ((position & Rank_2) << 16) & ~bitboard & ~(bitboard << 8);

            // Left Capture
            possibleMoves |= (position << 9) & blacks & ~File_H;

            // Right Capture
            possibleMoves |= (position << 7) & blacks & ~File_A;

            // Left En Passant
            possibleMoves |= (position << 9) & Rank_5 & movedX;

            // Right En Passant
            possibleMoves |= (position << 7) & Rank_5 & movedX;      
        }

        return possibleMoves;
    }

    private ulong GetDiagonalMask(int x, int y)
    {
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

    private ulong GetAntiDiagonalMask(int x, int y)
    {
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

    private ulong GetAllies(char type)
    {
        if (type < 'a')
        {
            return whites;
        }
        return blacks;
    }

    private ulong Reverse(ulong binary)
    {
        // Reverse bits by swapping positions. Total 30 operations.
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

    private ulong DirectCheck(char type)
    {
        int kingX = 0;
        int kingY = 0;

        if (type < 'a') // White
        {
            (kingX, kingY) = GetCoords(wk);

            // Pawn Checks
            if (((wk << 7) & bp) != 0UL)
            {
                return (wk << 7);
            }

            if (((wk << 9) & bp) != 0UL)
            {
                return (wk << 9);
            }

            // Knight Check
            ulong knights = GetKnightMoves(wk, 'K') & bn;
            if (knights != 0UL)
            {
                return knights;
            }

            // Sliders Check
            ulong sliders = GetQueenMoves(wk, 'Q', kingX, kingY) & (bb | bq | br);
            if (sliders != 0UL)
            {
                return sliders;
            }
        }
        else
        {
            (kingX, kingY) = GetCoords(bk);
            // Pawn Checks
            if (((bk >> 7) & wp) != 0UL)
            {
                return (bk >> 7);
            }

            if (((bk >> 9) & wp) != 0UL)
            {
                return (bk >> 9);
            }

            // Knight Check
            ulong knights = GetKnightMoves(bk, 'k') & wn;
            if (knights != 0UL)
            {
                return knights;
            }

            // Bishop/Rook/Queen Check
            ulong sliders = GetQueenMoves(bk, 'q', kingX, kingY) & (wb | wr | wq);
            if (sliders != 0UL)
            {
                return sliders;
            } 
        }
        
        Debug.LogWarning("Invalid type given to DirectCheck() method.");
        return 0UL;
    }

    /*
    De Bruijn Bit Position Algorithm
    Authors: Eric Cole, Mark Dickinson
    An algorithm that finds the log base 2 of an 64 bit integer in O(log(n)) operations
    Uses the DeBrujnTable64 lookup table.
    */
    private (int, int) GetCoords(ulong position)
    {
        position |= position >> 1;
        position |= position >> 2;
        position |= position >> 4;
        position |= position >> 8;
        position |= position >> 16;
        position |= position >> 32;

        int index = 63 - DeBruijnTable64[(position * 0b_00000011_11110110_11101010_11110010_11001101_00100111_00010100_01100001) >> 58];

        return (index / 8, index % 8);
    }
}

/*
Move is used to get the moves of a certain piece in chess.
More specifically, the legal moves that are checked by 
    1) Getting the ally King's position.
    2) Simulates the move ona a copied board.
    3) Checking all 8 directions and 8 Knight positions 

Also used in negaMax algorithm.
    For each piece on the board, simulate all the moves.
*/ 