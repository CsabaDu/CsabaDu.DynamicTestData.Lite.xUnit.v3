// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Lite.xUnit.v3.TestDataTypes;

namespace CsabaDu.DynamicTestData.Lite.XUnit.TestDataTypes.Converters;

public static class CollectionConverter
{
    public static IEnumerable<TheoryTestDataRow<TTestData>> ToTestCaseTestDataCollection<TTestData>(
        this IEnumerable<TTestData> testDataCollection,
        ArgsCode argsCode,
        string? testMethodName = null)
    where TTestData : notnull, ITestData
    {
        return testDataCollection.Convert(
            TestDataConverter.ToTheoryTestDataRow,
            nameof(TestDataConverter.ToTheoryTestDataRow),
            argsCode,
            testMethodName);
    }
}