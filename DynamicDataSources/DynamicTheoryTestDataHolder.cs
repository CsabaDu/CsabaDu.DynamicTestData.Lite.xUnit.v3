// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Lite.xUnit.v3.DynamicDataSources;

public abstract class DynamicTheoryTestDataHolder(ArgsCode argsCode, PropsCode propsCode)
: DynamicDataHolderSource<ITheoryTestData>(argsCode, propsCode)
{
    protected override void Add<TTestData>(TTestData testData)
    {
        var theoryTestData = DataHolder as TheoryTestData<TTestData>;
        bool isTypedDataHolder = theoryTestData is not null;

        Add(
            isTypedDataHolder,
            theoryTestData!,
            testData,
            theoryTestData!.Add);
    }

    protected override void InitDataHolder<TTestData>(TTestData testData)
    => DataHolder = new TheoryTestData<TTestData>(
        testData,
        this,
        null);
}
