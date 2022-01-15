using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerTools
{
    public static int Ground = LayerMask.NameToLayer("Ground");
    public static int Cube = LayerMask.NameToLayer("Cube");
}

public static class LayeMask
{
    public static int Ground = 1 << LayerTools.Ground;
    public static int Cube = 1 << LayerTools.Cube;

}
