using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Brainf.Memory;
using Brainf.Program;
using Brainf.Streams;

namespace Brainf.Compiler;

/// <summary>
/// Implementation of the `Brainfuck` compiler.
/// </summary>
public sealed class BrainfCompiler : IBrainfCompiler
{
    private const string DynamicMethodName = "brainf_program";

    /// <inheritdoc />
    public Action<IBrainfMemory, IBrainfStream> Compile(IBrainfProgram program)
    {
        if (program == null)
            throw new ArgumentNullException(nameof(program));

        var memoryType = typeof(IBrainfMemory);

        var memoryPointerProperty = memoryType
            .GetProperty(nameof(IBrainfMemory.Pointer))!;

        var memoryPointerGetter = memoryPointerProperty.GetGetMethod()!;
        var memoryPointerSetter = memoryPointerProperty.GetSetMethod()!;

        var memoryCellValueProperty = memoryType
            .GetProperty(nameof(IBrainfMemory.CellValue))!;

        var memoryCellValueGetter = memoryCellValueProperty.GetGetMethod()!;
        var memoryCellValueSetter = memoryCellValueProperty.GetSetMethod()!;

        var streamType = typeof(IBrainfStream);

        var streamWriteMethod = streamType
            .GetMethod(nameof(IBrainfStream.Write), BindingFlags.Instance | BindingFlags.Public);

        var streamReadMethod = streamType
            .GetMethod(nameof(IBrainfStream.Read), BindingFlags.Instance | BindingFlags.Public);

        var dynamicMethod = new DynamicMethod(DynamicMethodName, null, new[] { memoryType, streamType });
        var il = dynamicMethod.GetILGenerator();

        var startLoopLabels = new Stack<Label>();
        var endLoopLabels = new Stack<Label>();

        foreach (var operation in program.GetOperations())
        {
            var count = operation.Count;

            switch (operation.Kind)
            {
                case BrainfKind.MoveR:
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Callvirt, memoryPointerGetter);
                    EmitInt(il, count);
                    il.Emit(OpCodes.Add);
                    il.Emit(OpCodes.Callvirt, memoryPointerSetter);
                    break;

                case BrainfKind.MoveL:
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Callvirt, memoryPointerGetter);
                    EmitInt(il, count);
                    il.Emit(OpCodes.Sub);
                    il.Emit(OpCodes.Callvirt, memoryPointerSetter);
                    break;

                case BrainfKind.Inc:
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Callvirt, memoryCellValueGetter);
                    EmitInt(il, count);
                    il.Emit(OpCodes.Add);
                    il.Emit(OpCodes.Callvirt, memoryCellValueSetter);
                    break;

                case BrainfKind.Dec:
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Callvirt, memoryCellValueGetter);
                    EmitInt(il, count);
                    il.Emit(OpCodes.Sub);
                    il.Emit(OpCodes.Callvirt, memoryCellValueSetter);
                    break;

                case BrainfKind.Out:
                    for (var _ = 0; _ < count; _++)
                    {
                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Callvirt, memoryCellValueGetter);
                        il.Emit(OpCodes.Callvirt, streamWriteMethod);
                    }
                    break;

                case BrainfKind.In:
                    for (var _ = 0; _ < count; _++)
                    {
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Callvirt, streamReadMethod);
                        il.Emit(OpCodes.Callvirt, memoryCellValueSetter);
                    }
                    break;

                case BrainfKind.LoopStart:
                    for (var _ = 0; _ < count; _++)
                    {
                        var startLoopLabel = il.DefineLabel();
                        startLoopLabels.Push(startLoopLabel);

                        il.MarkLabel(startLoopLabel);

                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Callvirt, memoryCellValueGetter);
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

        return (Action<IBrainfMemory, IBrainfStream>)dynamicMethod
            .CreateDelegate(typeof(Action<IBrainfMemory, IBrainfStream>));
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
                else
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