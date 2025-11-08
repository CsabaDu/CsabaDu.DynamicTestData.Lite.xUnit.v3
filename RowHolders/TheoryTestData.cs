// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Lite.xUnit.v3.RowHolders;

public class TheoryTestData<TTestData>
: TheoryDataBase<ITheoryTestDataRow, TTestData>,
 ITheoryTestData
where TTestData : notnull, ITestData
{
    private TheoryTestData(IDataStrategy dataStrategy)
    {
        argsCode = Guard.ArgumentNotNull(
            dataStrategy,
            nameof(dataStrategy))
            .ArgsCode;
    }

    private TheoryTestData(
        IDataStrategy dataStrategy,
        string? testMethodName)
    : this(dataStrategy)
    {
        this.testMethodName = testMethodName;
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
    : this(dataStrategy)
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

    private readonly ArgsCode argsCode;
    private readonly string? testMethodName;

    protected override ITheoryTestDataRow Convert(TTestData row)
    => new TheoryTestDataRow<TTestData>(
        row,
        argsCode,
        testMethodName);
}
