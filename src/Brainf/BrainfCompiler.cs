using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Brainf
{
    /// <summary>
    /// Implementation of the `Brainfuck` compiler. 
    /// </summary>
    public sealed class BrainfCompiler : IBrainfCompiler
    {
        private static readonly Type MemoryType = typeof(IBrainfMemory);
        private static readonly Type StreamType = typeof(IBrainfStream);
        private static readonly MethodInfo MemoryPointerGetter;
        private static readonly MethodInfo MemoryPointerSetter;
        private static readonly MethodInfo MemoryCellValueGetter;
        private static readonly MethodInfo MemoryCellValueSetter;
        private static readonly MethodInfo StreamWriteMethod;
        private static readonly MethodInfo StreamReadMethod;

        /// <summary>
        /// Default implementation the `Brainfuck` compiler.
        /// </summary>
        public static IBrainfCompiler Default { get; } = new BrainfCompiler();

        static BrainfCompiler()
        {
            // ReSharper disable once PossibleNullReferenceException
            var memoryPointerAccessors = MemoryType
                .GetProperty(nameof(IBrainfMemory.Pointer))
                .GetAccessors();

            // ReSharper disable once PossibleNullReferenceException
            var memoryCellValueAccessors = MemoryType
                .GetProperty(nameof(IBrainfMemory.CellValue))
                .GetAccessors();

            MemoryPointerGetter = memoryPointerAccessors[0];
            MemoryPointerSetter = memoryPointerAccessors[1];
            MemoryCellValueGetter = memoryCellValueAccessors[0];
            MemoryCellValueSetter = memoryCellValueAccessors[1];

            StreamWriteMethod = StreamType.GetMethod(nameof(IBrainfStream.Write), BindingFlags.Instance | BindingFlags.Public);
            StreamReadMethod = StreamType.GetMethod(nameof(IBrainfStream.Read), BindingFlags.Instance | BindingFlags.Public);
        }

        /// <inheritdoc />
        public Action<IBrainfStream> Compile<TMemory>(IBrainfProgram program)
            where TMemory : IBrainfMemory, new()
        {
            if (program == null) 
                throw new ArgumentNullException(nameof(program));

            var dynamicMethod = new DynamicMethod("brainf_program", null, new[] { StreamType });
            var il = dynamicMethod.GetILGenerator();

            var startLoopLabels = new Stack<Label>();
            var endLoopLabels = new Stack<Label>();

            il.DeclareLocal(typeof(IBrainfMemory));
            il.Emit(OpCodes.Newobj, typeof(TMemory).GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Stloc_0);

            var operations = program.GetOperations();

            for (int i = 0; i < operations.Length; i++)
            {
                var operation = operations[i];
                int count = operation.Count;
                
                switch (operation.Kind)
                {
                    case BrainfKind.MoveR:
                        il.Emit(OpCodes.Ldloc_0);
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Callvirt, MemoryPointerGetter);
                        EmitInt(il, count);
                        il.Emit(OpCodes.Add);
                        il.Emit(OpCodes.Callvirt, MemoryPointerSetter);
                        break;
                    
                    case BrainfKind.MoveL:
                        il.Emit(OpCodes.Ldloc_0);
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Callvirt, MemoryPointerGetter);
                        EmitInt(il, count);
                        il.Emit(OpCodes.Sub);
                        il.Emit(OpCodes.Callvirt, MemoryPointerSetter);
                        break;
                    
                    case BrainfKind.Inc:
                        il.Emit(OpCodes.Ldloc_0);
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Callvirt, MemoryCellValueGetter);
                        EmitInt(il, count);
                        il.Emit(OpCodes.Add);
                        il.Emit(OpCodes.Callvirt, MemoryCellValueSetter);
                        break;
                    
                    case BrainfKind.Dec:
                        il.Emit(OpCodes.Ldloc_0);
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Callvirt, MemoryCellValueGetter);
                        EmitInt(il, count);
                        il.Emit(OpCodes.Sub);
                        il.Emit(OpCodes.Callvirt, MemoryCellValueSetter);
                        break;
                    
                    case BrainfKind.Out:
                        for (int _ = 0; _ < count; _++)
                        {
                            il.Emit(OpCodes.Ldarg_0);
                            il.Emit(OpCodes.Ldloc_0);
                            il.Emit(OpCodes.Callvirt, MemoryCellValueGetter);
                            il.Emit(OpCodes.Callvirt, StreamWriteMethod);
                        }
                        break;
                    
                    case BrainfKind.In:
                        for (int _ = 0; _ < count; _++)
                        {
                            il.Emit(OpCodes.Ldloc_0);
                            il.Emit(OpCodes.Ldarg_0);
                            il.Emit(OpCodes.Callvirt, StreamReadMethod);
                            il.Emit(OpCodes.Callvirt, MemoryCellValueSetter);
                        }
                        break;
                    
                    case BrainfKind.LoopStart:
                        for (int _ = 0; _ < count; _++)
                        {
                            var startLoopLabel = il.DefineLabel();
                            startLoopLabels.Push(startLoopLabel);
                        
                            il.MarkLabel(startLoopLabel);
                        
                            il.Emit(OpCodes.Ldloc_0);
                            il.Emit(OpCodes.Callvirt, MemoryCellValueGetter);
                            il.Emit(OpCodes.Ldc_I4_0);
                            il.Emit(OpCodes.Ceq);

                            var endLoopLabel = il.DefineLabel();
                            il.Emit(OpCodes.Brtrue, endLoopLabel);
                            endLoopLabels.Push(endLoopLabel);
                        }
                        break;
                    
                    case BrainfKind.LoopEnd:
                        for (int _ = 0; _ < count; _++)
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

            return (Action<IBrainfStream>) dynamicMethod.CreateDelegate(typeof(Action<IBrainfStream>));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
}