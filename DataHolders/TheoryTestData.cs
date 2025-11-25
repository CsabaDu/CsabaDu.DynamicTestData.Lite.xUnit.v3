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
    #endregion

    #region Fields
    private readonly ArgsCode argsCode;
    private readonly string? testMethodName;
    #endregion

    #region Methods

    public override void Add(ITheoryTestDataRow row)
    {
        if (row?.ContainedBy(this) != true)
        {
            base.Add(row!);
        }
    }

    public new void Add(TTestData testData)
    {
        if (!testData.ContainedBy(this))
        {
            base.Add(testData);
        }
    }

    public new void AddRange(IEnumerable<TTestData> rows)
    {
        foreach (var row in Guard.ArgumentNotNull(rows))
        {
            Add(row);
        }
    }

    public new void AddRange(params TTestData[] rows)
    {
        foreach (var row in Guard.ArgumentNotNull(rows))
        {
            Add(row);
        }
    }

    protected override ITheoryTestDataRow Convert(TTestData row)
    => new TheoryTestDataRow<TTestData>(
        row,
        argsCode,
        testMethodName);
    #endregion
}
