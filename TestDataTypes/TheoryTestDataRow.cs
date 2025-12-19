// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Lite.xUnit.v3.TestDataTypes.Interfaces;

namespace CsabaDu.DynamicTestData.Lite.xUnit.v3.TestDataTypes;

public class TheoryTestDataRow(
    ITestData testData,
    ArgsCode argsCode)
: TheoryDataRowBase,
ITheoryTestDataRow
{
    #region Fields
    protected ITestData _testData = testData;
    #endregion

    #region Properties
    public ArgsCode ArgsCode { get; protected set; }
        = argsCode.Defined(nameof(argsCode));
    #endregion

    #region Methods
    #region Static methods
    public static ArgsCode GetArgsCode(IDataStrategy dataStrategy)
    => Guard.ArgumentNotNull(
        dataStrategy,
        nameof(dataStrategy))
        .ArgsCode;

    public static TTestData GetTestData<TTestData>(
        TheoryTestDataRow<TTestData> theoryTestDataRow)
    where TTestData : notnull, ITestData
    => (TTestData)theoryTestDataRow.GetTestData();
    #endregion

    public bool ContainedBy(IEnumerable<INamedTestCase>? namedTestCases)
    => namedTestCases?.Any(Equals) == true;

    public bool Equals(INamedTestCase? other)
    => _testData.Equals(other);

    public override bool Equals(object? obj)
    => _testData.Equals(obj);

    public string? GetDisplayName(string? testMethodName)
    => _testData.GetDisplayName(testMethodName);

    public override int GetHashCode()
    => _testData.GetHashCode();

    public ITestData GetTestData()
    => _testData;

    public string TestCaseName
    => _testData.TestCaseName;

    #region Non-Public Methods
    protected override sealed object?[] GetData()
    => [_testData];
    #endregion
    #endregion
}

public sealed class TheoryTestDataRow<TTestData> : TheoryTestDataRow,
ITheoryTestDataRow
where TTestData : notnull, ITestData
{
    #region Constructors
    // Main constructor
    public TheoryTestDataRow(
        TTestData testData,
        ArgsCode argsCode,
        string? testMethodName)
    : base(testData, argsCode)
    {
        TestDisplayName =
            testData.GetDisplayName(testMethodName);
    }

    // Copy constructor with argsCode and testMethodName
    public TheoryTestDataRow(
        TheoryTestDataRow<TTestData> other,
        ArgsCode argsCode,
        string? testMethodName)
    : base(GetTestData(other), argsCode)
    {
        ArgsCode = other.ArgsCode;
        _testData = other._testData;

        Explicit = other.Explicit;
        Skip = other.Skip;
        Label = other.Label;
        SkipType = other.SkipType;
        SkipUnless = other.SkipUnless;
        SkipWhen = other.SkipWhen;
        TestDisplayName = other.GetDisplayName(testMethodName)
            ?? other.TestDisplayName;
        Timeout = other.Timeout;
        Traits = other.Traits ?? [];
    }

    // Copy constructor with dataStrategy and testMethodName
    public TheoryTestDataRow(
        TheoryTestDataRow<TTestData> other,
        IDataStrategy dataStrategy,
        string? testMethodName)
    : this(other, GetArgsCode(dataStrategy), testMethodName)
    {
    }
    #endregion
}
