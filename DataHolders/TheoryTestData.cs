// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Lite.xUnit.v3.DataHolders;

public class TheoryTestData<TTestData>
: TheoryDataBase<ITheoryTestDataRow, TTestData>,
 ITheoryTestData
where TTestData : notnull, ITestData
{
    #region Constructors
    private TheoryTestData(
        IDataStrategy dataStrategy,
        string? testMethodName)
    {
        argsCode = Guard.ArgumentNotNull(
            dataStrategy,
            nameof(dataStrategy))
            .ArgsCode;

        if (testMethodName is not null)
        {
            this.testMethodName = testMethodName;
        }
    }

    public TheoryTestData(
        TTestData testData,
        IDataStrategy dataStrategy,
        string? testMethodName)
    : this(dataStrategy, testMethodName)
    {
        Add(testData);
    }

    public TheoryTestData(
        TheoryTestData<TTestData> other,
        IDataStrategy dataStrategy,
        string? testMethodName)
    : this(dataStrategy, testMethodName)
    {
        AddRange(other.Select(
            row => new TheoryTestDataRow<TTestData>(
                (TheoryTestDataRow<TTestData>)row,
                dataStrategy,
                testMethodName)));
    }

    public TheoryTestData(
        IEnumerable<TTestData> testDatas,
        IDataStrategy dataStrategy,
        string? testMethodName)
    : this(dataStrategy, testMethodName)

    {
        AddRange(testDatas.Select(
            row => new TheoryTestDataRow<TTestData>(
                row,
                dataStrategy,
                testMethodName)));
    }
    #endregion

    #region Fields
    private readonly ArgsCode argsCode;
    private readonly string? testMethodName;
    #endregion

    #region Methods
    protected override ITheoryTestDataRow Convert(TTestData row)
    => new TheoryTestDataRow<TTestData>(
        row,
        argsCode,
        testMethodName);
    #endregion
}
