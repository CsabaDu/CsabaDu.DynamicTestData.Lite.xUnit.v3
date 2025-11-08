// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Lite.xUnit.v3.DataRows.Interfaces;

public interface ITheoryTestDataRow
: ITheoryDataRow,
INamedTestCase
{
    ArgsCode ArgsCode { get; }

    ITestData GetTestData();
}
