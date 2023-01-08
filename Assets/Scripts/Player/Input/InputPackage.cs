using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPackage
{
    public InputPackage()
    {
    }
    public InputPackage(FunctionPointer pointer, int priority)
    {
        this.pointer = pointer;
        this.priority = priority;
    }
    public InputPackage(InputPackage inputPackage)
    {
        this.pointer = inputPackage.pointer;
        this.priority = inputPackage.priority;
    }

    public delegate void FunctionPointer();
    public FunctionPointer pointer;
    public FunctionPointer FailPointer;
    public int priority;

    public static void InputPackageHandler(InputPackage package, List<InputPackage> packageList, bool isSending)
    {
        if (isSending)
        {
            packageList.Add(package);
        }
        else
        {
            packageList.Remove(package);
        }
    }
}
