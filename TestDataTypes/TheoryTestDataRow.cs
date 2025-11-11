// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Lite.xUnit.v3.TestDataTypes;

public sealed class TheoryTestDataRow<TTestData>(
    TTestData testData,
    ArgsCode argsCode)
: TheoryDataRowBase,
ITheoryTestDataRow
where TTestData : notnull, ITestData
{
    #region Constructors
    public TheoryTestDataRow(
        TheoryTestDataRow<TTestData> other,
        ArgsCode argsCode,
        string? testMethodName)
    : this(
          (TTestData)other.GetTestData(),
          argsCode)
    {
        SetTheoryDataRow(
            other,
            testMethodName);
    }

    public TheoryTestDataRow(
        TheoryTestDataRow<TTestData> other,
        IDataStrategy dataStrategy,
        string? testMethodName)
    : this(
          other,
          Guard.ArgumentNotNull(
              dataStrategy,
              nameof(dataStrategy))
          .ArgsCode,
          testMethodName)
    {
    }

    public TheoryTestDataRow(
        TTestData testData,
        ArgsCode argsCode,
        string? testMethodName)
    : this(testData, argsCode)
    {
        TestDisplayName = GetTestDisplayName(
            testMethodName,
            testData);
    }

    public TheoryTestDataRow(
        TTestData testData,
        IDataStrategy dataStrategy,
        string? testMethodName)
    : this(
          testData,
          Guard.ArgumentNotNull(
              dataStrategy,
              nameof(dataStrategy))
          .ArgsCode,
          testMethodName)
    {
    }
    #endregion

    #region Fields
    private TTestData _testData = testData;
    #endregion

    #region Properties
    public ArgsCode ArgsCode { get; private set; }
        = argsCode.Defined(nameof(argsCode));
    #endregion

    #region Methods
    public bool Equals(INamedTestCase? other)
    => _testData.Equals(other);

    public override bool Equals(object? obj)
    => _testData.Equals(obj);

    public override int GetHashCode()
    => _testData.GetHashCode();

    public ITestData GetTestData()
    => _testData;

    public string GetTestCaseName()
    => _testData.GetTestCaseName();

    #region Non-Public Methods
    protected override object?[] GetData()
    => [_testData];

    private void SetTheoryDataRow(
        TheoryTestDataRow<TTestData> other,
        string? testMethodName)
    {
        ArgsCode = other.ArgsCode;
        _testData = other._testData;

        Explicit = other.Explicit;
        Skip = other.Skip;
        Label = other.Label;
        SkipType = other.SkipType;
        SkipUnless = other.SkipUnless;
        SkipWhen = other.SkipWhen;
        TestDisplayName = GetTestDisplayName(
            testMethodName,
            other)
            ?? other.TestDisplayName;
        Timeout = other.Timeout;
        Traits = other.Traits ?? [];
    }

    private string? GetTestDisplayName(
        string? testMethodName,
        INamedTestCase namedTestCase)
    => ArgsCode == ArgsCode.Properties ?
        GetDisplayName(
            testMethodName,
            namedTestCase.GetTestCaseName())
        : testMethodName;
    #endregion
    #endregion
}
