// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Lite.xUnit.v3.DataHolders;
using CsabaDu.DynamicTestData.Lite.xUnit.v3.DataHolders.Interfaces;

namespace CsabaDu.DynamicTestData.Lite.xUnit.v3.DynamicDataSources;

public abstract class DynamicTheoryTestDataSource(ArgsCode argsCode, PropsCode propsCode)
: DynamicDataHolderSource<ITheoryTestData>(argsCode, propsCode)
{
    protected override void Add<TTestData>(TTestData testData)
    {
        bool isDataHolderTyped =
            IsDataHolderTyped(out TheoryTestData<TTestData>? theoryTestData);

        Add(isDataHolderTyped,
            testData,
            theoryTestData!.Add);
    }

    protected override void InitDataHolder<TTestData>(TTestData testData)
    => DataHolder = new TheoryTestData<TTestData>(
        testData,
        this,
        null);
}
