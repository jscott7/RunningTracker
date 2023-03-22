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
    /// Implements the profile WktStepDuration type as an enum
    /// </summary>
    public enum WktStepDuration : byte
    {
        Time = 0,
        Distance = 1,
        HrLessThan = 2,
        HrGreaterThan = 3,
        Calories = 4,
        Open = 5,
        RepeatUntilStepsCmplt = 6,
        RepeatUntilTime = 7,
        RepeatUntilDistance = 8,
        RepeatUntilCalories = 9,
        RepeatUntilHrLessThan = 10,
        RepeatUntilHrGreaterThan = 11,
        RepeatUntilPowerLessThan = 12,
        RepeatUntilPowerGreaterThan = 13,
        PowerLessThan = 14,
        PowerGreaterThan = 15,
        TrainingPeaksTss = 16,
        RepeatUntilPowerLastLapLessThan = 17,
        RepeatUntilMaxPowerLastLapLessThan = 18,
        Power3sLessThan = 19,
        Power10sLessThan = 20,
        Power30sLessThan = 21,
        Power3sGreaterThan = 22,
        Power10sGreaterThan = 23,
        Power30sGreaterThan = 24,
        PowerLapLessThan = 25,
        PowerLapGreaterThan = 26,
        RepeatUntilTrainingPeaksTss = 27,
        RepetitionTime = 28,
        Reps = 29,
        Invalid = 0xFF


    }
}

