using Microsoft.ML;

namespace NewsAnalysis;

public static class Context
{
    public static MLContext MlContext => new(450);
}