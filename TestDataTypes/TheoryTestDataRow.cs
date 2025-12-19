// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Lite.xUnit.v3.TestDataTypes.Interfaces;

namespace CsabaDu.DynamicTestData.Lite.xUnit.v3.TestDataTypes;

public abstract class TheoryTestDataRow(
    ITestData testData,
    ArgsCode argsCode)
: TheoryDataRowBase,
ITheoryTestDataRow
{
    public TheoryTestDataRow(
        ITestData testData,
        ArgsCode argsCode,
        string? testMethodName)
    : this(testData, argsCode)
    {
        TestDisplayName =
            testData.GetDisplayName(testMethodName);
    }

    #region Fields
    protected ITestData _testData = testData;
    #endregion

    #region Properties
    public ArgsCode ArgsCode =>
        argsCode.Defined(nameof(argsCode));
    #endregion

    #region Methods
    #region Static methods
    public static ArgsCode GetArgsCode(IDataStrategy dataStrategy)
    => Guard.ArgumentNotNull(dataStrategy, nameof(dataStrategy))
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

public sealed class TheoryTestDataRow<TTestData>
: TheoryTestDataRow,
ITheoryTestDataRow
where TTestData : notnull, ITestData
{
    public TheoryTestDataRow(
    TTestData testData,
    ArgsCode argsCode,
    string? testMethodName)
    : base(testData, argsCode, testMethodName)
    {
    }
        
    public TheoryTestDataRow(
        TheoryTestDataRow<TTestData> other,
        ArgsCode argsCode,
        string? testMethodName)
    : base(GetTestData(other), argsCode)
    {
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

    public TheoryTestDataRow(
        TheoryTestDataRow<TTestData> other,
        IDataStrategy dataStrategy,
        string? testMethodName)
    : this(other, GetArgsCode(dataStrategy), testMethodName)
    {
    }
}
