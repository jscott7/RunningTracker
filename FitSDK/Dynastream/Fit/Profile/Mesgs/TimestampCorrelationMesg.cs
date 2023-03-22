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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Linq;

namespace Dynastream.Fit
{
    /// <summary>
    /// Implements the TimestampCorrelation profile message.
    /// </summary>
    public class TimestampCorrelationMesg : Mesg
    {
        #region Fields
        #endregion

        /// <summary>
        /// Field Numbers for <see cref="TimestampCorrelationMesg"/>
        /// </summary>
        public sealed class FieldDefNum
        {
            public const byte Timestamp = 253;
            public const byte FractionalTimestamp = 0;
            public const byte SystemTimestamp = 1;
            public const byte FractionalSystemTimestamp = 2;
            public const byte LocalTimestamp = 3;
            public const byte TimestampMs = 4;
            public const byte SystemTimestampMs = 5;
            public const byte Invalid = Fit.FieldNumInvalid;
        }

        #region Constructors
        public TimestampCorrelationMesg() : base(Profile.GetMesg(MesgNum.TimestampCorrelation))
        {
        }

        public TimestampCorrelationMesg(Mesg mesg) : base(mesg)
        {
        }
        #endregion // Constructors

        #region Methods
        ///<summary>
        /// Retrieves the Timestamp field
        /// Units: s
        /// Comment: Whole second part of UTC timestamp at the time the system timestamp was recorded.</summary>
        /// <returns>Returns DateTime representing the Timestamp field</returns>
        public DateTime GetTimestamp()
        {
            Object val = GetFieldValue(253, 0, Fit.SubfieldIndexMainField);
            if(val == null)
            {
                return null;
            }

            return TimestampToDateTime(Convert.ToUInt32(val));
            
        }

        /// <summary>
        /// Set Timestamp field
        /// Units: s
        /// Comment: Whole second part of UTC timestamp at the time the system timestamp was recorded.</summary>
        /// <param name="timestamp_">Nullable field value to be set</param>
        public void SetTimestamp(DateTime timestamp_)
        {
            SetFieldValue(253, 0, timestamp_.GetTimeStamp(), Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the FractionalTimestamp field
        /// Units: s
        /// Comment: Fractional part of the UTC timestamp at the time the system timestamp was recorded.</summary>
        /// <returns>Returns nullable float representing the FractionalTimestamp field</returns>
        public float? GetFractionalTimestamp()
        {
            Object val = GetFieldValue(0, 0, Fit.SubfieldIndexMainField);
            if(val == null)
            {
                return null;
            }

            return (Convert.ToSingle(val));
            
        }

        /// <summary>
        /// Set FractionalTimestamp field
        /// Units: s
        /// Comment: Fractional part of the UTC timestamp at the time the system timestamp was recorded.</summary>
        /// <param name="fractionalTimestamp_">Nullable field value to be set</param>
        public void SetFractionalTimestamp(float? fractionalTimestamp_)
        {
            SetFieldValue(0, 0, fractionalTimestamp_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the SystemTimestamp field
        /// Units: s
        /// Comment: Whole second part of the system timestamp</summary>
        /// <returns>Returns DateTime representing the SystemTimestamp field</returns>
        public DateTime GetSystemTimestamp()
        {
            Object val = GetFieldValue(1, 0, Fit.SubfieldIndexMainField);
            if(val == null)
            {
                return null;
            }

            return TimestampToDateTime(Convert.ToUInt32(val));
            
        }

        /// <summary>
        /// Set SystemTimestamp field
        /// Units: s
        /// Comment: Whole second part of the system timestamp</summary>
        /// <param name="systemTimestamp_">Nullable field value to be set</param>
        public void SetSystemTimestamp(DateTime systemTimestamp_)
        {
            SetFieldValue(1, 0, systemTimestamp_.GetTimeStamp(), Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the FractionalSystemTimestamp field
        /// Units: s
        /// Comment: Fractional part of the system timestamp</summary>
        /// <returns>Returns nullable float representing the FractionalSystemTimestamp field</returns>
        public float? GetFractionalSystemTimestamp()
        {
            Object val = GetFieldValue(2, 0, Fit.SubfieldIndexMainField);
            if(val == null)
            {
                return null;
            }

            return (Convert.ToSingle(val));
            
        }

        /// <summary>
        /// Set FractionalSystemTimestamp field
        /// Units: s
        /// Comment: Fractional part of the system timestamp</summary>
        /// <param name="fractionalSystemTimestamp_">Nullable field value to be set</param>
        public void SetFractionalSystemTimestamp(float? fractionalSystemTimestamp_)
        {
            SetFieldValue(2, 0, fractionalSystemTimestamp_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the LocalTimestamp field
        /// Units: s
        /// Comment: timestamp epoch expressed in local time used to convert timestamps to local time</summary>
        /// <returns>Returns nullable uint representing the LocalTimestamp field</returns>
        public uint? GetLocalTimestamp()
        {
            Object val = GetFieldValue(3, 0, Fit.SubfieldIndexMainField);
            if(val == null)
            {
                return null;
            }

            return (Convert.ToUInt32(val));
            
        }

        /// <summary>
        /// Set LocalTimestamp field
        /// Units: s
        /// Comment: timestamp epoch expressed in local time used to convert timestamps to local time</summary>
        /// <param name="localTimestamp_">Nullable field value to be set</param>
        public void SetLocalTimestamp(uint? localTimestamp_)
        {
            SetFieldValue(3, 0, localTimestamp_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the TimestampMs field
        /// Units: ms
        /// Comment: Millisecond part of the UTC timestamp at the time the system timestamp was recorded.</summary>
        /// <returns>Returns nullable ushort representing the TimestampMs field</returns>
        public ushort? GetTimestampMs()
        {
            Object val = GetFieldValue(4, 0, Fit.SubfieldIndexMainField);
            if(val == null)
            {
                return null;
            }

            return (Convert.ToUInt16(val));
            
        }

        /// <summary>
        /// Set TimestampMs field
        /// Units: ms
        /// Comment: Millisecond part of the UTC timestamp at the time the system timestamp was recorded.</summary>
        /// <param name="timestampMs_">Nullable field value to be set</param>
        public void SetTimestampMs(ushort? timestampMs_)
        {
            SetFieldValue(4, 0, timestampMs_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the SystemTimestampMs field
        /// Units: ms
        /// Comment: Millisecond part of the system timestamp</summary>
        /// <returns>Returns nullable ushort representing the SystemTimestampMs field</returns>
        public ushort? GetSystemTimestampMs()
        {
            Object val = GetFieldValue(5, 0, Fit.SubfieldIndexMainField);
            if(val == null)
            {
                return null;
            }

            return (Convert.ToUInt16(val));
            
        }

        /// <summary>
        /// Set SystemTimestampMs field
        /// Units: ms
        /// Comment: Millisecond part of the system timestamp</summary>
        /// <param name="systemTimestampMs_">Nullable field value to be set</param>
        public void SetSystemTimestampMs(ushort? systemTimestampMs_)
        {
            SetFieldValue(5, 0, systemTimestampMs_, Fit.SubfieldIndexMainField);
        }
        
        #endregion // Methods
    } // Class
} // namespace
