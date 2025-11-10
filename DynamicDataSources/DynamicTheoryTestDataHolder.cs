// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Lite.xUnit.v3.DynamicDataSources;

public abstract class DynamicTheoryTestDataHolder(ArgsCode argsCode, PropsCode propsCode)
: DynamicDataSource<ITheoryTestData>(argsCode, propsCode)
{
    protected override void Add<TTestData>(TTestData testData)
    {
        if (DataHolder is not TheoryTestData<TTestData> theoryTestData)
        {
            InitDataHolder(testData, null);
            return;
        }

        var namedTestCases = theoryTestData as IEnumerable<INamedTestCase>;

        if (namedTestCases!.Any(testData.Equals))
        {
            return;
        }

        theoryTestData.Add(testData);
    }

    protected void InitDataHolder<TTestData>(
        TTestData testData,
        string? testMethodName)
    where TTestData : notnull, ITestData
    => DataHolder = new TheoryTestData<TTestData>(
        testData,
        this,
        testMethodName);

    protected void InitDataHolder<TTestData>(
        IEnumerable<TTestData> testDatas,
        string? testMethodName)
    where TTestData : notnull, ITestData
    => DataHolder = new TheoryTestData<TTestData>(
        testDatas,
        this,
        testMethodName);
}
