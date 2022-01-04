using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Brainf;

/// <summary>
/// Implementation of the `Brainfuck` compiler.
/// </summary>
public sealed class BrainfCompiler : IBrainfCompiler
{
    /// <summary>
    /// Default implementation the `Brainfuck` compiler.
    /// </summary>
    public static IBrainfCompiler Default { get; } = new BrainfCompiler();

    /// <inheritdoc />
    public Action<TMemory, TStream> Compile<TMemory, TStream>(IBrainfProgram program)
        where TMemory : IBrainfMemory
        where TStream : IBrainfStream
    {
        if (program == null)
            throw new ArgumentNullException(nameof(program));

        var memoryType = typeof(TMemory);
        var streamType = typeof(TStream);

        // ReSharper disable once PossibleNullReferenceException
        var memoryPointerAccessors = memoryType
            .GetProperty(nameof(IBrainfMemory.Pointer))
            .GetAccessors();

        // ReSharper disable once PossibleNullReferenceException
        var memoryCellValueAccessors = memoryType
            .GetProperty(nameof(IBrainfMemory.CellValue))
            .GetAccessors();

        var memoryPointerGetter = memoryPointerAccessors[0];
        var memoryPointerSetter = memoryPointerAccessors[1];
        var memoryCellValueGetter = memoryCellValueAccessors[0];
        var memoryCellValueSetter = memoryCellValueAccessors[1];

        var streamWriteMethod = streamType.GetMethod(nameof(IBrainfStream.Write), BindingFlags.Instance | BindingFlags.Public);
        var streamReadMethod = streamType.GetMethod(nameof(IBrainfStream.Read), BindingFlags.Instance | BindingFlags.Public);

        var dynamicMethod = new DynamicMethod("brainf_program", null, new[] { memoryType, streamType });
        var il = dynamicMethod.GetILGenerator();

        var startLoopLabels = new Stack<Label>();
        var endLoopLabels = new Stack<Label>();

        var operations = program.GetOperations();

        for (var i = 0; i < operations.Length; i++)
        {
            var operation = operations[i];
            var count = operation.Count;

            switch (operation.Kind)
            {
                case BrainfKind.MoveR:
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Call, memoryPointerGetter);
                    EmitInt(il, count);
                    il.Emit(OpCodes.Add);
                    il.Emit(OpCodes.Call, memoryPointerSetter);
                    break;

                case BrainfKind.MoveL:
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Call, memoryPointerGetter);
                    EmitInt(il, count);
                    il.Emit(OpCodes.Sub);
                    il.Emit(OpCodes.Call, memoryPointerSetter);
                    break;

                case BrainfKind.Inc:
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Call, memoryCellValueGetter);
                    EmitInt(il, count);
                    il.Emit(OpCodes.Add);
                    il.Emit(OpCodes.Call, memoryCellValueSetter);
                    break;

                case BrainfKind.Dec:
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Call, memoryCellValueGetter);
                    EmitInt(il, count);
                    il.Emit(OpCodes.Sub);
                    il.Emit(OpCodes.Call, memoryCellValueSetter);
                    break;

                case BrainfKind.Out:
                    for (var _ = 0; _ < count; _++)
                    {
                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Call, memoryCellValueGetter);
                        il.Emit(OpCodes.Callvirt, streamWriteMethod);
                    }
                    break;

                case BrainfKind.In:
                    for (var _ = 0; _ < count; _++)
                    {
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Callvirt, streamReadMethod);
                        il.Emit(OpCodes.Call, memoryCellValueSetter);
                    }
                    break;

                case BrainfKind.LoopStart:
                    for (var _ = 0; _ < count; _++)
                    {
                        var startLoopLabel = il.DefineLabel();
                        startLoopLabels.Push(startLoopLabel);

                        il.MarkLabel(startLoopLabel);

                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Call, memoryCellValueGetter);
                        il.Emit(OpCodes.Ldc_I4_0);
                        il.Emit(OpCodes.Ceq);

                        var endLoopLabel = il.DefineLabel();
                        il.Emit(OpCodes.Brtrue, endLoopLabel);
                        endLoopLabels.Push(endLoopLabel);
                    }
                    break;

                case BrainfKind.LoopEnd:
                    for (var _ = 0; _ < count; _++)
                    {
                        il.Emit(OpCodes.Br, startLoopLabels.Pop());
                        il.MarkLabel(endLoopLabels.Pop());
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        il.Emit(OpCodes.Ret);

        return (Action<TMemory, TStream>) dynamicMethod.CreateDelegate(typeof(Action<TMemory, TStream>));
    }

    private static void EmitInt(ILGenerator il, int value)
    {
        switch (value)
        {
            case 0:
                il.Emit(OpCodes.Ldc_I4_0);
                break;

            case 1:
                il.Emit(OpCodes.Ldc_I4_1);
                break;

            case 2:
                il.Emit(OpCodes.Ldc_I4_2);
                break;

            case 3:
                il.Emit(OpCodes.Ldc_I4_3);
                break;

            case 4:
                il.Emit(OpCodes.Ldc_I4_4);
                break;

            case 5:
                il.Emit(OpCodes.Ldc_I4_5);
                break;

            case 6:
                il.Emit(OpCodes.Ldc_I4_6);
                break;

            case 7:
                il.Emit(OpCodes.Ldc_I4_7);
                break;

            case 8:
                il.Emit(OpCodes.Ldc_I4_8);
                break;

            default:
                if (value > 8)
                {
                    il.Emit(OpCodes.Ldc_I4, value);
                }
                else if (value < 0)
                {
                    il.Emit(OpCodes.Ldc_I4_M1);

                    if (value < -1)
                    {
                        il.Emit(OpCodes.Mul);
                        EmitInt(il, Math.Abs(value));
                    }
                }
                break;
        }
    }
}