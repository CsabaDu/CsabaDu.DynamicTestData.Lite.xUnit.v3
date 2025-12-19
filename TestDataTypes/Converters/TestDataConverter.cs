// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Lite.xUnit.v3.TestDataTypes;

namespace CsabaDu.DynamicTestData.Lite.XUnit.v3.TestDataTypes.Converters;

public static class TestDataConverter
{
    public static TheoryTestDataRow<TTestData> ToTheoryTestDataRow<TTestData>(
        this TTestData testData,
        ArgsCode argsCode,
        string? testMethodName = null)
    where TTestData : notnull, ITestData
    => new(testData, argsCode, testMethodName);
}
