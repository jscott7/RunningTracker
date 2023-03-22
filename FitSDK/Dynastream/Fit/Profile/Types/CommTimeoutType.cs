#region Copyright
////////////////////////////////////////////////////////////////////////////////
// The following FIT Protocol software provided may be used with FIT protocol
// devices only and remains the copyrighted property of Garmin Canada Inc.
// The software is being provided on an "as-is" basis and as an accommodation,
// and therefore all warranties, representations, or guarantees of any kind
// (whether express, implied or statutory) including, without limitation,
// warranties of merchantability, non-infringement, or fitness for a particular
// purpose, are specifically disclaimed.
//
// Copyright 2019 Garmin Canada Inc.
////////////////////////////////////////////////////////////////////////////////
// ****WARNING****  This file is auto-generated!  Do NOT edit this file.
// Profile Version = 20.96Release
// Tag = production/akw/20.96.00-0-g6471b23
////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dynastream.Fit
{
    /// <summary>
    /// Implements the profile CommTimeoutType type as a class
    /// </summary>
    public static class CommTimeoutType 
    {
        public const ushort WildcardPairingTimeout = 0; // Timeout pairing to any device
        public const ushort PairingTimeout = 1; // Timeout pairing to previously paired device
        public const ushort ConnectionLost = 2; // Temporary loss of communications
        public const ushort ConnectionTimeout = 3; // Connection closed due to extended bad communications
        public const ushort Invalid = (ushort)0xFFFF;


    }
}

